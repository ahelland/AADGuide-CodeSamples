using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using B2CNETCoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace B2CNETCoreWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "Partner")]
        public IActionResult Partner()
        {
            ViewData["Message"] = "Howdy partner!";
            return View();
        }

        [Authorize(Policy = "Customer")]
        public IActionResult Customer()
        {
            ViewData["Message"] = "Welcome dear customer. How may we help you today.";
            return View();
        }

        [Authorize(Policy = "Employee")]
        public IActionResult Employee()
        {
            ViewData["Message"] = "Get back to work!";
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
