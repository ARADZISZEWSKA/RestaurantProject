using Microsoft.AspNetCore.Mvc;
using RestaurantPageProject.Data;
using RestaurantPageProject.Models;

namespace RestaurantPageProject.Controllers
{
    public class MenuItemsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public MenuItemsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<MenuItems> objMenuItemsList = _db.Menu.ToList();
            return View(objMenuItemsList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(MenuItems obj) //obj jest to co wpiszemy w utworz kategorie
        {
            if (ModelState.IsValid)
            {
                _db.Menu.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Utworzono nową pozycję";
                return RedirectToAction("Index", "MenuItems");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            MenuItems? categoryFromDb = _db.Menu.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(MenuItems obj)
        {

            if (ModelState.IsValid)
            {
                _db.Menu.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Edytowano pozycję";
                return RedirectToAction("Index", "MenuItems");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            MenuItems? categoryFromDb = _db.Menu.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            MenuItems? obj = _db.Menu.Find(id);

            if (id == null)
            {
                return NotFound();
            }
            _db.Menu.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Pozycja została usunięta";
            return RedirectToAction("Index", "MenuItems");


        }
    }
}

