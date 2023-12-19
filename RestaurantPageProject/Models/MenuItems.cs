using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RestaurantPageProject.Models
{
    public class MenuItems
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [DisplayName("Nazwa dania")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [DisplayName("Opis dania")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [DisplayName("Cena")]
        public double Price { get; set; }

        
    }
}
