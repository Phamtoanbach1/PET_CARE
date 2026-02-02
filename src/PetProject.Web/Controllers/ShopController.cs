using Microsoft.AspNetCore.Mvc;
using PetProject.Application.Interfaces;
using System.Threading.Tasks;

namespace PetProject.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Index()
        {
            var products = await _shopService.GetProductsAsync();
            return View(products);
        }

        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "id" })]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _shopService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        public IActionResult Cart()
        {
            return View();
        }
    }
}
