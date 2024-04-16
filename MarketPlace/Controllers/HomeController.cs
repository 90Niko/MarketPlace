using MarketPlace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MarketPlace.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
       {
            if (statusCode == 404)
            {
                return View("NotFound");
            }
            if (statusCode == 500)
            {
                return View("Error");
            }
            if (statusCode == 403)
            {
                return View("AccessDenied");
            }
            if (statusCode == 401)
            {
                return View("Unauthorized");
            }
            if (statusCode == 400)
            {
                return View("BadRequest");
            }
            return View();
        }
    }
}
