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
        public int NumberOfPeople { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [DisplayName("Numer telefonu")]
        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [DisplayName("Data")]
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public bool IsConfirmed { get; set; }

    }
}
