using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantPageProject.Data;
using RestaurantPageProject.Models;
using RestaurantPageProject.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPageProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReservationAdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IRepository<Reservation> _reservation;


        public ReservationAdminController(ApplicationDbContext db, IRepository<Reservation> reservation)
        {
            _db = db;
            _reservation = reservation;
        }

        // Akcja odpowiadająca za wyświetlenie wszystkich rezerwacji
        public async Task<IActionResult> Index()
        {
            var reservations = await _db.Reservations.ToListAsync();    // Pobranie wszystkich rezerwacji z bazy danych
            return View(reservations);                                  // Przekazanie listy rezerwacji do widoku
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _db.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(reservation);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _db.Reservations.FirstOrDefaultAsync(m => m.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _db.Reservations.FindAsync(id);
            _db.Reservations.Remove(reservation);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _db.Reservations.Any(e => e.Id == id);
        }


        #region APICALL
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Reservation> reservationsList = _reservation.GetAll().ToList();
            return Json(new { data = reservationsList });
        }

        #endregion


    }
}
