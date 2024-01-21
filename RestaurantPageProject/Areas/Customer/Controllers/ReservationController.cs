using Microsoft.AspNetCore.Mvc;
using RestaurantPageProject.Data;
using RestaurantPageProject.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using RestaurantPageProject.Repository;

namespace RestaurantPageProject.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httContextAccessor;
        private readonly IRepository<Reservation> _reservationsRepository;
        

        public ReservationController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, IRepository<Reservation> reservationRepository)
        {
            _db = db;
            _httContextAccessor = httpContextAccessor;
            _reservationsRepository = reservationRepository;
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
            //custom validation
            if(model.Name == model.LastName)
            {
                ModelState.AddModelError("name", "Imię i Nazwisko nie mogą być takie same.");
            }
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


        [HttpPost] //do wyswietlenia rezerwacji przy "dziękuje"
        public ActionResult ReservationConfirmedPOST()
        {
            var reservations = _db.Reservations.ToList();

            return View(reservations);
        }
    }
}
