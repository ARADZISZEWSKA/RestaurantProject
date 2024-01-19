using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantPageProject.Models;
using RestaurantPageProject.Repository;
using RestaurantPageProject.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RestaurantPageProject.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<MenuItems> _menuItems;
        private readonly IRepository<Category> _category;

        public HomeController(ILogger<HomeController> logger, IRepository<MenuItems> menuItems, IRepository<Category> category)
        {
            _logger = logger;
            _menuItems = menuItems;
            _category = category;
        }

        public IActionResult Index()
        {
            var categoriesWithMenuItems = new List<CategoryWithMenuItemsVM>();

            var categories = _category.GetAll().ToList();

            foreach (var category in categories)
            {
                var menuItems = _menuItems.GetAll(includeProperties: "Category")
                                           .Where(item => item.CategoryId == category.Id)
                                           .ToList();

                var categoryWithMenuItems = new CategoryWithMenuItemsVM
                {
                    MenuItemsList = menuItems,  // Zmiana nazwy w³aœciwoœci
                    CategoryList = categories.Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString(),
                        Selected = (c.Id == category.Id)
                    })
                };

                categoriesWithMenuItems.Add(categoryWithMenuItems);
            }

            return View(categoriesWithMenuItems);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
