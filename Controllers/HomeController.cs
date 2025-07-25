using System.Diagnostics;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using TSENA.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Authorization;

namespace TSENA.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserContext _context;

    public HomeController(ILogger<HomeController> logger, UserContext context)
    {
        _logger = logger;
        _context = context;
    }

    [Authorize]
    public async Task<IActionResult> Parametre()
    {
        // Récupère l'email de l'utilisateur connecté à partir des Claims
        var email = User.FindFirstValue(ClaimTypes.Email);

         // Cherche l'utilisateur dans la base de données avec l'email récupéré
        var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);

        // Crée un modèle pour transmettre les informations (nom et email)
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var userConnected = new User {
            Name = user.Name,
            Email = user.Email,
            Password = user.Password
        };
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        // Passe le modèle à la vue
        return View(userConnected);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Authorize]
    public IActionResult Chart(){
        return View();
    }

    [Authorize]
    public IActionResult DetailsProduct(){
        return View();
    }

    [Authorize]
    [HttpGet]
    public IActionResult ChangePassword(){
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordModel model){
        if(ModelState.IsValid){
            var email = User.FindFirstValue(ClaimTypes.Email); 
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);
            if(model.NewPassword == model.ConfirmPassword){
                model.NewPassword = HashPassword(model.NewPassword);
                user.Password = model.NewPassword; 
            }
            _context.User.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Parametre");
        }else
        {
            return View(model);
        }
    }

    private string HashPassword(string Password){
        using (var sha256 = SHA256.Create()){
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));
            return Convert.ToBase64String(hashBytes);
        }
    }

}
