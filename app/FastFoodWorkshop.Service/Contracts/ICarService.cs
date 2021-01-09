namespace FastFoodWorkshop.Service.Contracts
{
    using Models;
    using ServiceModels.Manager;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICarService
    {
        Task AddCarAsync(DeliveryCarViewModel model);

        Task<ICollection<DeliveryCar>> GetCarAsync();

        Task<DeliveryCar> GetCarAsync(int id);

        Task<DeliveryCarViewModel> MapCarAsync(int id);

        Task<ICollection<DeliveryCarViewModel>> MapCarsAsync(DeliveryCarsViewModel model);

        Task EditCarAsync(int id, DeliveryCarViewModel model);

        Task DeleteCarAsync(int id);

        Task DetachFromRestaurantAsync(int id);
    }
}
