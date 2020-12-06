using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                  new Restaurant{ Id = 1, Name = "Hot Pizzzas!!", Location= "Maryland", Cuisine = CuisineType.Italian },
                  new Restaurant{ Id = 2, Name = "Cinnamon Club!", Location= "London", Cuisine = CuisineType.Mexican },
                  new Restaurant{ Id = 3, Name = "Chat Pat!!", Location= "New York", Cuisine = CuisineType.Indian },
            };

        }

        public IEnumerable<Restaurant> GetAll()
        {
            return from r in restaurants
                   orderby r.Name
                   select r;
        }
    }
}
