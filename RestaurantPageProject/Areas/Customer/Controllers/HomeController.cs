using Microsoft.AspNetCore.Mvc;
using RestaurantPageProject.Models;
using RestaurantPageProject.Repository;
using System.Diagnostics;

namespace RestaurantPageProject.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<MenuItems> _menuItems;
        public HomeController(ILogger<HomeController> logger, IRepository<MenuItems> menuItems)
        {
            _logger = logger;
            _menuItems = menuItems;
        }

        public IActionResult Index()
        {
            IEnumerable<MenuItems> menuItems = _menuItems.GetAll(includeProperties: "Category");
            return View(menuItems);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
