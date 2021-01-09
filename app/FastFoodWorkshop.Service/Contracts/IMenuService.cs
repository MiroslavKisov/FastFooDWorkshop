namespace FastFoodWorkshop.Service.Contracts
{
    using Models;
    using ServiceModels.Manager;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMenuService
    {
        Task<int> CreateMenuAsync(MenuViewModel model);

        Task<Menu> GetMenuAsync(int id);

        Task<ICollection<Menu>> GetMenusAsync();

        Task<ICollection<MenuViewModel>> MapMenusAsync(MenusViewModel model);

        Task<MenuViewModel> MapMenuAsync(int id);

        Task EditMenuAsync(int id, MenuViewModel model);

        Task DeleteMenuAsync(int id);
    }
}
