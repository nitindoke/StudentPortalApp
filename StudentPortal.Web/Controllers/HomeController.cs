using Microsoft.AspNetCore.Mvc;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;
using System.Diagnostics;

namespace StudentPortal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (TempData["UserName"] == null)
            {
                TempData["UserName"] = "Guest";
            }
            //var users = new Users
            //{
            //    UserName = userName
            //};

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
