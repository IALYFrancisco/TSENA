using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TSENA.Models;

namespace TSENA.Controllers {
    public class UserController : Controller {
        private readonly UserContext _context;

        public UserController(UserContext context){
            _context = context;
        }

       [HttpPost]
public async Task<IActionResult> Register(User model) {
    if(ModelState.IsValid){
        var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == model.Email);
        if(existingUser != null){
            ViewData["Error"] = "Cet email existe déjà";
            return View(model);
        }

        // Correction ici
        model.Password = HashPassword(model.Password);

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

    }
}