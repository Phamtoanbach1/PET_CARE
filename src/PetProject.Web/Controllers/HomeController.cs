using Microsoft.AspNetCore.Mvc;
using PetProject.Application.Interfaces;
using PetProject.Web.Models;
using System.Diagnostics;

namespace PetProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShopService _shopService;

        public HomeController(ILogger<HomeController> logger, IShopService shopService)
        {
            _logger = logger;
            _shopService = shopService;
        }

        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "*" })]
        public async Task<IActionResult> Index()
        {
            var services = await _shopService.GetServicesAsync();
            return View(services);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Services()
        {
            var services = await _shopService.GetServicesAsync();
            return View(services);
        }

        public IActionResult About()
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
