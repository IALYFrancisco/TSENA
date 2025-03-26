using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Common;
using RestSharp;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TSENA.Models;
using DotNetEnv;

Env.Load();

namespace TSENA.Controllers {
    public class AuthenticationController : Controller {
        private readonly UserContext _context;
        private readonly ResetPasswordTokensContext _passwordContext;

        private readonly IConfiguration _configuration;

        public AuthenticationController(UserContext context, ResetPasswordTokensContext passwordContext, IConfiguration configuration){
            _context = context;
            _passwordContext = passwordContext;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Register(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("Name,Email,Password")] User model) {
            if(ModelState.IsValid){
                var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == model.Email);
                if(existingUser != null){
                    ViewData["Error"] = "Un utilisateur avec cet email existe déjà.";
                    return View(model);
                }

                // Correction ici
#pragma warning disable CS8604 // Possible null reference argument.
                model.Password = HashPassword(model.Password);
#pragma warning restore CS8604 // Possible null reference argument.

                _context.User.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            return View(model);
        }

        private string HashPassword(string Password){
            using (var sha256 = SHA256.Create()){
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        [HttpGet]
        public IActionResult Login(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login (User model){
            if(ModelState.IsValid){
                var user = await _context.User.FirstOrDefaultAsync(u => u.Email == model.Email);
#pragma warning disable CS8604 // Possible null reference argument.
                if (user != null && VerifyPassword(model.Password, user.Password)){
                    await SignInUser(user.Email);
                    return RedirectToAction("Index", "ShopManagement");
                }
#pragma warning restore CS8604 // Possible null reference argument.
                ViewData["Error"] = "Email ou mot de passe incorrect";
            }
            return View(model);
        }

        private bool VerifyPassword(string password, string storedHash){
            return HashPassword(password) == storedHash;
        }

        private async Task SignInUser(string Email){
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Email, Email)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(){
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "ShopManagement");
        }

        [HttpGet]
        public IActionResult ForgotPassword(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email){
            
            // Vérification de l'utilisateur avec l'email
            var user = _context.User.FirstOrDefault(u => u.Email == email);

            if(user == null){
                ViewData["error"] = "Aucun compte associé à cet email";
                return View();
            }

            var token = Guid.NewGuid().ToString();
            var expirationDate = DateTime.UtcNow.AddHours(1);
            var resetToken = new ResetPasswordTokens {
                Email = email,
                Token = token,
                ExpirationDate = expirationDate,
                NewPassword = "",
                ConfirmPassword = ""
            };

            _passwordContext.ResetPasswordTokens.Add(resetToken);
            _passwordContext.SaveChanges();

            var resetLink = Url.Action("NewPassword", "Authentication", new { token = token }, Request.Scheme);
            
            await SendPasswordResetEmail(email, resetLink);

            return RedirectToAction("EmailSent");

        }

        public async Task SendPasswordResetEmail(string email, string? resetLink){

            if(string.IsNullOrEmpty(resetLink)){
                throw new ArgumentNullException(nameof(resetLink), "Le lien de réinitialisation ne peut pas être null.");
            }

            var apiKey = Environment.GetEnvironmentVariable("BREVO_API_KEY");

            var client = new RestClient("https://api.brevo.com/v3/smtp/email");
            var request = new RestRequest("" , Method.Post);

            request.AddHeader("accept", "application/json");
#pragma warning disable CS8604 // Possible null reference argument.
            request.AddHeader("api-key", apiKey);
#pragma warning restore CS8604 // Possible null reference argument.
            request.AddHeader("Content-Type", "application/json");

            var body = new {
                sender = new {
                    email = "franciscoialy43@gmail.com",
                    name = "TSENA"
                },
                to = new [] { new { email = email } },
                subject = "Réinitialisation de votre mot de passe",
                htmlContent = $"<strong>Bonjour,</strong><br><br>Pour réinitialiser votre mot de passe, veuillez cliquer sur le lien suivant :<br><a href=\"{resetLink}\">Réinitialiser le mot de passe</a><br><br>Cordialement,<br>L'équipe",
                textContent = $"Bonjour,\n\nPour réinitialiser votre mot de passe, veuillez cliquer sur le lien suivant :\n{resetLink}\n\nCordialement,\nL'équipe"
            };

            request.AddJsonBody(body);

            var response = await client.ExecuteAsync(request);

            if(response.IsSuccessful){
                Console.WriteLine("Email envoyé avec succès");
            }else{
                Console.WriteLine($"Erreur lors de l'envoi de l'email: {response.Content}");
            }

        }

        public IActionResult EmailSent(){
            return View();
        }

        [HttpGet]
        public IActionResult NewPassword(string token){
            if(string.IsNullOrEmpty(token)){
                return BadRequest("Token invalide.");
            }

            var resetToken = _passwordContext.ResetPasswordTokens.FirstOrDefault(t => t.Token == token && t.ExpirationDate > DateTime.UtcNow);

            if(resetToken == null){
                return BadRequest("Token expiré ou invalide.");
            }

            return View("NewPassword", new ResetPasswordTokens {Token = token} );
        }

        [HttpPost]
        public async Task<IActionResult> NewPassword(ResetPasswordTokens model){
            if(!ModelState.IsValid){
                return View("NewPassword", model);
            }

            var resetToken = _passwordContext.ResetPasswordTokens.FirstOrDefault(t => t.Token == model.Token && t.ExpirationDate > DateTime.UtcNow);

            if (resetToken == null){
                ModelState.AddModelError("", "Utilisateur non trouvé.");
                return View("NewPassword", model);
            }

            var user = _context.User.FirstOrDefault(u => u.Email == resetToken.Email);

            if(user == null){
                ModelState.AddModelError("", "Utilisateur non trouvé");
                return View("NewPassword", model);
            }

            //Vérifier que NewPassword et ConfirmPassword sont égaux
            if(model.NewPassword != model.ConfirmPassword){
                ViewData["Error"] = "Les deux mot de passe ne se ressemblent pas, essayez à nouveau";
                return View();
            }

#pragma warning disable CS8604 // Possible null reference argument.
            user.Password = HashPassword(model.NewPassword);
#pragma warning restore CS8604 // Possible null reference argument.

            _context.User.Update(user);
            await _context.SaveChangesAsync();

            _passwordContext.ResetPasswordTokens.Remove(resetToken);
            await _passwordContext.SaveChangesAsync();

            TempData["SuccessMessage"] = "Votre mot de passe a été réinitilisé avec succèss. Vous pouvez maintenant vous connecter.";

            return RedirectToAction("Login");

        }

    }
}