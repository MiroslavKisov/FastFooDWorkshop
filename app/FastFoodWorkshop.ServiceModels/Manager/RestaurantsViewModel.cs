namespace FastFoodWorkshop.ServiceModels.Manager
{
    using System.Collections.Generic;

    public class RestaurantsViewModel
    {
        public ICollection<RestaurantViewModel> Restaurants { get; set; }
    }
}
