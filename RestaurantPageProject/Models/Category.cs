using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RestaurantPageProject.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane.")]
        [DisplayName("Nazwa kategorii")]
        [MaxLength(30)]
        [RegularExpression("^[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ\\s]*$", ErrorMessage = "Nazwa może zawierać tylko litery oraz spacje.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [DisplayName("Kolejność wyświetania kategorii")]
        [Range(1, 15, ErrorMessage = "Liczba musi być w przedziale 1-10")]
        public int? DisplayOrder { get; set; }
    }
}
