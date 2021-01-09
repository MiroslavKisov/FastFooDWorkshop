namespace FastFoodWorkshop.Service
{
    using AutoMapper;
    using Contracts;
    using Data;
    using ServiceModels.Manager;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System;
    using Microsoft.Extensions.Logging;
    using Common.WebConstants;

    public class RestaurantService : IRestaurantService
    {
        private readonly IRepository<Restaurant> restaurantRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public RestaurantService(
            IRepository<Restaurant> restaurantRepository, 
            IMapper mapper,
            ILogger<RestaurantService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.restaurantRepository = restaurantRepository;
        }

        public async Task<ICollection<Restaurant>> GetRestaurantsAsync()
        {
            var restaurants = this.restaurantRepository.All().ToList();

            return await Task.FromResult<ICollection<Restaurant>>(restaurants);
        }

        public async Task AddRestaurantAsync(RestaurantViewModel model)
        {
            try
            {
                var restaurant = this.mapper.Map<Restaurant>(model);

                await this.restaurantRepository.AddAsync(restaurant);

                await this.restaurantRepository.SaveChangesAsync();

                this.logger.LogInformation(string.Format(LogMessages.RestaurantAdded, restaurant.Id));
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task DeleteRestaurantAsync(int id)
        {
            try
            {
                var restaurant = this.restaurantRepository.All().FirstOrDefault(e => e.Id == id);

                if (restaurant == null)
                {
                    throw new ApplicationException(string.Format(ErrorMessages.RestaurantDoesNotExist, id));
                }

                this.restaurantRepository.Delete(restaurant);

                await this.restaurantRepository.SaveChangesAsync();

                this.logger.LogInformation(string.Format(LogMessages.RestaurantDeleted, restaurant.Id));
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task EditRestaurantAsync(int id, RestaurantViewModel model)
        {
            try
            {
                var restaurant = this.restaurantRepository.All().FirstOrDefault(e => e.Id == id);

                if (restaurant == null)
                {
                    throw new ApplicationException(string.Format(ErrorMessages.RestaurantDoesNotExist, id));
                }

                restaurant.Name = model.Name;
                restaurant.Address = model.Address;

                this.restaurantRepository.Update(restaurant);

                await this.restaurantRepository.SaveChangesAsync();

                this.logger.LogInformation(string.Format(LogMessages.RestaurantUpdated, restaurant.Id));
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task<Restaurant> GetRestaurantAsync(int id)
        {
            var restaurant = this.restaurantRepository.All().FirstOrDefault(e => e.Id == id);

            return await Task.FromResult<Restaurant>(restaurant);
        }

        public async Task<RestaurantViewModel> MapRestaurantAsync(int id)
        {
            var restaurant = this.restaurantRepository.All().FirstOrDefault(e => e.Id == id);

            if (restaurant == null)
            {
                throw new ApplicationException(string.Format(ErrorMessages.RestaurantDoesNotExist, id));
            }

            var model = this.mapper.Map<RestaurantViewModel>(restaurant);

            return await Task.FromResult<RestaurantViewModel>(model);
        }

        public async Task<ICollection<RestaurantViewModel>> MapRestaurantsAsync(RestaurantsViewModel model)
        {
            var restaurant = this.restaurantRepository.All().ToList();

            model.Restaurants = this.mapper.Map<ICollection<RestaurantViewModel>>(restaurant);

            return await Task.FromResult<ICollection<RestaurantViewModel>>(model.Restaurants);
        }
    }
}
