using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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
        public double? Price { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [DisplayName("Kategoria")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

    }
}
