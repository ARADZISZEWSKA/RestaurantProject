using RestaurantPageProject.Data;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RestaurantPageProject;
using static RestaurantPageProject.CustomValidation.CustomValidationForReserve;


namespace RestaurantPageProject.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane.")]
        [DisplayName("Imię")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Liczba osób jest wymagana.")]
        [DisplayName("Liczba osób")]
        [Range(1, 4, ErrorMessage =
            "Maksymalna liczba osób na rezerwacje to 4. " +
            "Jeżeli chcesz zrobić rezerwacje na większą ilość osób, skontaktuj się z nami telefonicznie.")]
        public int NumberOfPeople { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany.")]
        [DisplayName("Numer telefonu")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Numer telefonu musi składać się z 9 cyfr.")]
        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "Data jest wymagana.")]
        [DisplayName("Data")]
        [FutureDate(ErrorMessage = "Nie można wybrać daty wcześniejszej niż obecna.")]
        [UniqueReservation(ErrorMessage = "Istnieje już rezerwacja na tę datę i godzinę.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Godzina rezerwacji jest wymagana.")]
        [DisplayName("Godzina")]
        public string SelectedHour { get; set; }

        public string? Note { get; set; }
    }
}
