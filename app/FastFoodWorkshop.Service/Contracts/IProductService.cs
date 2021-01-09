namespace FastFoodWorkshop.Service.Contracts
{
    using Models;
    using ServiceModels.Manager;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task AddProductAsync(ProductViewModel model);

        Task<ICollection<Product>> GetProductsAsync();

        Task<Product> GetProductAsync(int id);

        Task<ProductViewModel> MapProductAsync(int id);

        Task<ICollection<ProductViewModel>> MapProductsAsync(ProductsViewModel model);

        Task EditProductAsync(int id, ProductViewModel model);

        Task DeleteProductAsync(int id);

        Task DetachFromCategoryAsync(int id);

        Task DetachFromMenuAsync(int id);
    }
}
