namespace FastFoodWorkshop.Service
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using Common.WebConstants;
    using Microsoft.Extensions.Logging;
    using Models;
    using Service.Contracts;
    using ServiceModels.Manager;
    using System.Collections.Generic;
    using System.Linq;

    public class CarService : ICarService
    {
        private readonly IRepository<DeliveryCar> carRepository;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public CarService(
            IRepository<DeliveryCar> carRepository,
            ILogger<CarService> logger,
            IMapper mapper)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.carRepository = carRepository;
        }

        public async Task AddCarAsync(DeliveryCarViewModel model)
        {
            try
            {
                var car = this.mapper.Map<DeliveryCar>(model);

                if (model.RestaurantId != null)
                {
                    car.RestaurantId = int.Parse(model.RestaurantId);
                }

                await this.carRepository.AddAsync(car);

                await this.carRepository.SaveChangesAsync();

                this.logger.LogInformation(string.Format(LogMessages.DeliveryCarAdded, car.Id));
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task DeleteCarAsync(int id)
        {
            try
            {
                var car = this.carRepository.All().FirstOrDefault(e => e.Id == id);

                if (car == null)
                {
                    throw new ApplicationException(string.Format(ErrorMessages.CarDoesNotExist, id));
                }

                this.carRepository.Delete(car);

                await this.carRepository.SaveChangesAsync();

                this.logger.LogInformation(string.Format(LogMessages.DeliveryCarDeleted, car.Id));
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task DetachFromRestaurantAsync(int id)
        {
            try
            {
                var car = this.carRepository.All().FirstOrDefault(e => e.Id == id);

                if (car == null)
                {
                    throw new ApplicationException(string.Format(ErrorMessages.CarDoesNotExist, id));
                }

                car.RestaurantId = null;

                await this.carRepository.SaveChangesAsync();

                if (car.RestaurantId != null)
                {
                    this.logger.LogInformation(string.Format(LogMessages.CarDetachedFromRestaurant, car.Model, car.Restaurant.Name));
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task EditCarAsync(int id, DeliveryCarViewModel model)
        {
            try
            {
                var car = this.carRepository.All().FirstOrDefault(e => e.Id == id);

                if (car == null)
                {
                    throw new ApplicationException(string.Format(ErrorMessages.CarDoesNotExist, id));
                }

                car.Model = model.Model;
                car.Mileage = model.Mileage;
                car.ProductionDate = model.ProductionDate;
                car.TankCapacity = model.TankCapacity;

                if (model.RestaurantId != null)
                {
                    car.RestaurantId = int.Parse(model.RestaurantId);
                }

                this.carRepository.Update(car);

                await this.carRepository.SaveChangesAsync();

                this.logger.LogInformation(string.Format(LogMessages.DeliveryCarUpdated, car.Id));
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task<ICollection<DeliveryCar>> GetCarAsync()
        {
            var car = carRepository.All().ToList();

            return await Task.FromResult<ICollection<DeliveryCar>>(car);
        }

        public async Task<DeliveryCar> GetCarAsync(int id)
        {
            var car = this.carRepository.All().FirstOrDefault(e => e.Id == id);

            return await Task.FromResult<DeliveryCar>(car);
        }

        public async Task<DeliveryCarViewModel> MapCarAsync(int id)
        {
            var car = this.carRepository.All().FirstOrDefault(e => e.Id == id);

            if (car == null)
            {
                throw new ApplicationException(string.Format(ErrorMessages.CarDoesNotExist, id));
            }

            var model = this.mapper.Map<DeliveryCarViewModel>(car);

            return await Task.FromResult<DeliveryCarViewModel>(model);
        }

        public async Task<ICollection<DeliveryCarViewModel>> MapCarsAsync(DeliveryCarsViewModel model)
        {
            var car = this.carRepository.All().ToList();

            model.DeliveryCars = this.mapper.Map<ICollection<DeliveryCarViewModel>>(car);

            return await Task.FromResult<ICollection<DeliveryCarViewModel>>(model.DeliveryCars);
        }
    }
}
