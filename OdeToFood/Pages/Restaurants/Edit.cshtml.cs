using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        // here we just want the binding to work for Post and hence we are not setting any other flag
        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData, 
                         IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int restaurantId)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            Restaurant = restaurantData.GetById(restaurantId);

            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Restaurant = restaurantData.Update(Restaurant);
                restaurantData.Commit();
            }

            // we have to make sure to populate the select list for Post as well 
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();            
            return Page();
        }
    }
}
