using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

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

        public string Message { get; set; }

        public ListModel(IConfiguration config)
        {
            this.config = config;
        }

        public void OnGet()
        {
            Message = config["Message"];
        }
    }
}
