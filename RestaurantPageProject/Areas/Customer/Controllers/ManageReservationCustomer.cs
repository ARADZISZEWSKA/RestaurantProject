using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantPageProject.Data;
using RestaurantPageProject.Models;
using RestaurantPageProject.Repository;

namespace RestaurantPageProject.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class ManageReservationCustomer : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Reservation> _reservationsRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public ManageReservationCustomer(ApplicationDbContext db,IHttpContextAccessor httpContextAccessor,IRepository<Reservation> reservationRepository,UserManager<IdentityUser> userManager)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _reservationsRepository = reservationRepository;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            // Pobierz bieżącego zalogowanego użytkownika
            var currentUser = _userManager.GetUserAsync(User).Result;

            if (!string.IsNullOrEmpty(currentUser.PhoneNumber))
            {
                // Pobierz rezerwacje tego użytkownika na podstawie numeru telefonu
                var userReservations = _db.Reservations
                    .Where(r => r.PhoneNumber == int.Parse(currentUser.PhoneNumber))
                    .ToList();

                return View(userReservations);
            }
            else
            {
                // Obsłuż sytuację, gdy użytkownik nie ma przypisanego numeru telefonu
                // Tu możesz wyświetlić odpowiednie komunikaty lub przekierować użytkownika
                // do innej akcji widoku
                return View(new List<Reservation>()); // Zwróć pustą listę w przypadku braku numeru telefonu
            }
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

    }
}
