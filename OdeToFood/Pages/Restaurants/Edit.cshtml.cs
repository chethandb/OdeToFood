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

        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();

            if (restaurantId.HasValue)
            {
                Restaurant = restaurantData.GetById(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }

            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // we have to make sure to populate the select list for Post as well 
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            var addedRestaurant = false;

            if (Restaurant.Id > 0)
            {                
                Restaurant = restaurantData.Update(Restaurant);
            }
            else
            {
                addedRestaurant = true;
                restaurantData.Add(Restaurant);
            }

            // TempData is like dictionary of Key Value pairs
            // its just temporary
            // any request to this displays and its forgotten once done
            TempData["Message"] = addedRestaurant ? "New Restaurant info added!" : "Sucessfully updated Restaurant!";

            restaurantData.Commit();
            // implement post redirect get pattern - PRG 
            // this makes sure user is left on a safe page where they can refresh and wouldn't change any data
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
        }
    }
}
