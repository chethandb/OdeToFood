using Microsoft.AspNetCore.Mvc;
using OdeToFood.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.ViewComponents
{
    public class RestaurantViewComponent : ViewComponent
    {
        private readonly IRestaurantData restaurantData;

        public RestaurantViewComponent(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        public IViewComponentResult Invoke()
        {
            var count = restaurantData.GetCountOfRestaurants();

            // if no name is provided for the view it defaults            
            return View(count);
        }
    }
}
