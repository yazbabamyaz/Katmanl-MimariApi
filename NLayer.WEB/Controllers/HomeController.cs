using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using System.Diagnostics;

namespace NLayer.WEB.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel errorViewModel)
        {
            //heryere hitap eden sınıf o yüzden shared  de
            return View(errorViewModel);
        }
    }
}