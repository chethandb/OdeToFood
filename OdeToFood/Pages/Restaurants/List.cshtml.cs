using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{

    // ------------------------ PageModel -----------------------------------------------------------------------------------
    // basically PageModel is used to inject services that will give me access to the data that I need
    // and then use those services, like the configuration service, to fetch data and add that to properties 
    // and then bind to inside of the razor page
    //  ---------------------------------------------------------------------------------------------------------------------

    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IRestaurantData restaurantData;
        private readonly ILogger<ListModel> logger;

        public string ConfigMessage { get; set; }
        [TempData]
        public string Message { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }

        // ------------------------ BindProperty -----------------------------------------------------------------------------------
        // BindProperty tells the ASP.NET core framework when you're instantiating this class and you're getting ready to execute 
        // a method on this class to process an HTTP request, this particular property should recieve information from the request.
        // By default the BindProperty works only for Post, so by setting the flag  SupportsGet, we are asking it to work for Get request as well.
        //  ---------------------------------------------------------------------------------------------------------------------

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public ListModel(IConfiguration config, 
                         IRestaurantData restaurantData,
                         ILogger<ListModel> logger)
        {
            this.config = config;
            this.restaurantData = restaurantData;
            this.logger = logger;
        }

        public void OnGet()
        {
            logger.LogError("Executing ListModel");
            ConfigMessage = config["Message"];
            Restaurants = restaurantData.GetRestaurantsByName(SearchTerm);
        }
    }
}
