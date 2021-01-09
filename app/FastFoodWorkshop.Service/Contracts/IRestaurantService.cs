namespace FastFoodWorkshop.Service.Contracts
{
    using ServiceModels.Manager;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRestaurantService
    {
        Task AddRestaurantAsync(RestaurantViewModel model);

        Task<ICollection<Restaurant>> GetRestaurantsAsync();

        Task<Restaurant> GetRestaurantAsync(int id);

        Task<RestaurantViewModel> MapRestaurantAsync(int id);

        Task<ICollection<RestaurantViewModel>> MapRestaurantsAsync(RestaurantsViewModel model);

        Task EditRestaurantAsync(int id, RestaurantViewModel model);

        Task DeleteRestaurantAsync(int id);
    }
}
