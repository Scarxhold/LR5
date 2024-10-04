using LR5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics;
using System.Reflection;

namespace LR5.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Form() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(FormModels model)
        {
            if (ModelState.IsValid) 
            {
                CookieOptions options = new CookieOptions
                {
                    Expires = model.ExpirationDate
                };
                Response.Cookies.Append("UserValue", model.Value, options);

                return RedirectToAction("CheckCookies");
            }
            return View("Form", model);
        }

        public IActionResult CheckCookies()
        {
            if (Request.Cookies.TryGetValue("UserValue", out var value))
            {
                ViewBag.CookieValue = value;
            }
            else
            {
                ViewBag.CookieValue = "Cookie не найдено.";
            }

            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
