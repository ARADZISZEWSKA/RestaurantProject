using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using RestaurantPageProject.Data;
using RestaurantPageProject.Models;
using RestaurantPageProject.Repository;
using RestaurantPageProject.ViewModels;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace RestaurantPageProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuItemsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<MenuItems> _menuRepository;


        public MenuItemsController(ApplicationDbContext db, IRepository<Category> categoryRepository, IRepository<MenuItems> menuRepository)
        {
            _db = db;
            _categoryRepository = categoryRepository;
            _menuRepository = menuRepository;

        }
        public IActionResult Index()
        {
            List<MenuItems> objMenuItemsList = _menuRepository.GetAll(includeProperties:"Category").ToList();
            return View(objMenuItemsList);
        }

        public IActionResult Upsert(int? id) //update+insert
        {
            MenuItemsVM menuItemVM = new() //mozemy tylko 1 obiekt przekazać w View, dlatego łączymy
            {
                CategoryList = _categoryRepository.GetAll().Select(u => new SelectListItem //TWORZY LISTE KATEGORII. Każdy obiekt SelectListItem w tej liście reprezentuje jedną kategorię 
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                MenuItem = new MenuItems() //obiekt, który zostanie wypełniony danymi wprowadzonymi przez użytkownika
            };
            if (id == null || id == 0) 
            {
                //create
                return View(menuItemVM);
            }
            else //próbujemy pobrać istniejący element z bazy danych za pomocą repozytorium
            {
                //update
                menuItemVM.MenuItem = _menuRepository.Get(u => u.Id == id);
                return View(menuItemVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(MenuItemsVM obj) //obj jest to co wpiszemy w utworz kategorie, dane z formularza
        {
            if (ModelState.IsValid)
            {
                //_db.Menu.Add(obj.MenuItem);
                //_db.SaveChanges();
                //TempData["success"] = "Utworzono nową pozycję";
                //return RedirectToAction("Index", "MenuItems");

                //if (obj.MenuItem.Id == 0)
                //{
                //    _db.Menu.Add(obj.MenuItem);
                //    TempData["success"] = "Utworzono nową pozycję";
                //}
                //else
                //{
                //    _db.Menu.Update(obj.MenuItem);
                //    TempData["success"] = "Edytowano pozycję";
                //}

                //_db.SaveChanges();
                //return RedirectToAction("Index", "MenuItems");
                if (obj.MenuItem.Id == 0)
                {
                    _db.Menu.Add(obj.MenuItem);
                    TempData["success"] = "Utworzono nową pozycję";
                }
                else
                {
                    var existingItem = _db.Menu.Find(obj.MenuItem.Id);
                    if (existingItem != null)
                    {
                        // Jeśli istnieje, zaktualizuj dane
                        _db.Entry(existingItem).CurrentValues.SetValues(obj.MenuItem);
                        TempData["success"] = "Edytowano pozycję";
                    }
                    else
                    {
                        // Jeśli nie istnieje, wyświetl błąd
                        TempData["error"] = "Nie można znaleźć pozycji do edycji";
                        return RedirectToAction("Index", "MenuItems");
                    }
                }

                _db.SaveChanges();
                return RedirectToAction("Index", "MenuItems");

            }
            else //jesli model jest invalid - ponownie wyświetla formularz, a obj.CategoryList ponownie wypełnia listą kategorii. Bez tego, rozwijalna lista kategorii w formularzu mogłaby być pusta
            {
                obj.CategoryList = _categoryRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(obj);
            };
        }


        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    MenuItems? categoryFromDb = _db.Menu.Find(id);

        //    if (categoryFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(categoryFromDb);
        //}
        //[HttpPost]
        //public IActionResult Edit(MenuItems obj)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _db.Menu.Update(obj);
        //        _db.SaveChanges();
        //        TempData["success"] = "Edytowano pozycję";
        //        return RedirectToAction("Index", "MenuItems");
        //    }
        //    return View();
        //}
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    MenuItems? categoryFromDb = _db.Menu.Find(id);

        //    if (categoryFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(categoryFromDb);
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? id)
        //{
        //    MenuItems? obj = _db.Menu.Find(id);

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    _db.Menu.Remove(obj);
        //    _db.SaveChanges();
        //    TempData["success"] = "Pozycja została usunięta";
        //    return RedirectToAction("Index", "MenuItems");


        //}

        #region APICALLS 

        [HttpGet] // aplikacje lub serwisy mogą używać tego API, aby uzyskać dostęp do tych danych i wykorzystać je 
        public IActionResult GetAll()
        {
            List<MenuItems> objMenuItemsList = _menuRepository.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objMenuItemsList });
        }

          
        public IActionResult Delete(int? id)
        {
            var deletedItem = _menuRepository.Get(u => u.Id == id);
            if(deletedItem == null)
            {
                return Json(new { success = false, message = "Nie udało się usunąć." });
            }

            _menuRepository.Remove(deletedItem); 
            _db.SaveChanges();

            TempData["success"] = "Pozycja została usunięta";
            return RedirectToAction("Index", "MenuItems");
        }

        #endregion
    }
}

