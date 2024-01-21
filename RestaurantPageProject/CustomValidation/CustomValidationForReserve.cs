using RestaurantPageProject.Data;
using RestaurantPageProject.Models;
using System.ComponentModel.DataAnnotations;

namespace RestaurantPageProject.CustomValidation
{
    public class CustomValidationForReserve
    {
        //customm validation
        public class FutureDateAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                DateTime date = (DateTime)value;

                // Sprawdź, czy data jest późniejsza niż obecna
                return date >= DateTime.Now;
            }
        }
        public class UniqueReservationAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var reservation = (Reservation)validationContext.ObjectInstance;
                var db = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

                // Sprawdź, czy istnieje więcej niż 2 rezerwacje na daną datę i godzinę
                if (db.Reservations.Count(r => r.Date == reservation.Date && r.SelectedHour == reservation.SelectedHour) >= 2)
                {
                    return new ValidationResult("Brak miejsc. Wybierz inną datę lub godzinę.");
                }

                return ValidationResult.Success;
            }
        }
        //koniec custom validation
    }
}
