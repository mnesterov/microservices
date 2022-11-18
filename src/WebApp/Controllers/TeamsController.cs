using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class TeamsController : Controller
{
    private readonly IConfiguration _configuration;

    public TeamsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View(new TeamsViewModel{ NbaServicesApiUrl = _configuration["NbaServicesApiUrl"] });
    }
}
