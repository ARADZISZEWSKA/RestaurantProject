using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantPageProject.Models;

namespace RestaurantPageProject.ViewModels
{
    public class MenuItemsVM
    {
        public MenuItems MenuItem { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
//"ViewModel" oznacza obiekt, który jest używany w warstwie widoku i służy do przekazywania danych z kontrolera do widoku.
//model for View
//strongly type view - there is a model for the view