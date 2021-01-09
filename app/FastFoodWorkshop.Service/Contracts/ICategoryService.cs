namespace FastFoodWorkshop.Service.Contracts
{
    using Models;
    using ServiceModels.Manager;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<ICollection<Category>> GetCategoriesAsync();

        Task AddCategoryAsync(CategoryViewModel model);

        Task<Category> GetCategoryAsync(int id);

        Task<CategoryViewModel> MapCategoryAsync(int id);

        Task<ICollection<CategoryViewModel>> MapCategoriesAsync(CategoriesViewModel model);

        Task EditCategoryAsync(int id, CategoryViewModel model);

        Task DeleteCategoryAsync(int id);
    }
}
