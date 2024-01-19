using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantPageProject.Models;

namespace RestaurantPageProject.ViewModels
{
    public class CategoryWithMenuItemsVM
    {
        public IEnumerable<MenuItems> MenuItemsList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
