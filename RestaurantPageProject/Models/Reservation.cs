using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RestaurantPageProject.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [DisplayName("Imię")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [DisplayName("Liczba osób")]
        [Range(1, 4, ErrorMessage =
            "Maksymalna liczba osób na rezerwacje to 4. " +
            "Jeżeli chcesz zrobić rezerwacje na większą ilość osób, skontaktuj się z nami telefonicznie.")]
        public int NumberOfPeople { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [DisplayName("Numer telefonu")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Numer telefonu musi składać się z 9 cyfr.")]
        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [DisplayName("Data")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [DisplayName("Godzina")]
        public string SelectedHour { get; set; }

        public string? Note { get; set; }
    }
}
