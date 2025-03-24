using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TSENA.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

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

    public IActionResult Index()
    {
        return View();
    }

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

    public IActionResult Chart(){
        return View();
    }

    public IActionResult DetailsProduct(){
        return View();
    }

}
