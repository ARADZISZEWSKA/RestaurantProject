using Microsoft.AspNetCore.Mvc;
using RestaurantPageProject.Data;
using RestaurantPageProject.Models;
using System.Linq;

namespace RestaurantPageProject.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ReservationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reserve()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Reserve(Reservation model)
        {
            if (ModelState.IsValid)
            {
                _db.Reservations.Add(model);
                _db.SaveChanges();

                return RedirectToAction("ReservationConfirmed", "Reservation");
            }

            return View();
        }
        public ActionResult ReservationConfirmed()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReservationConfirmedPOST()
        {
            var reservations = _db.Reservations.ToList();

            return View(reservations);
        }
    }
}
