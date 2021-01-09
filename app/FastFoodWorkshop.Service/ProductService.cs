namespace FastFoodWorkshop.Service
{
    using AutoMapper;
    using Contracts;
    using Common.WebConstants;
    using Data;
    using Extensions;
    using Microsoft.Extensions.Logging;
    using Models;
    using System;
    using ServiceModels.Manager;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class ProductService : IProductService
    {
        private readonly ILogger logger;
        private readonly IMenuService menuService;
        private readonly IRepository<Category> categoryRepository;
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<Menu> menuRepository;
        private readonly IMapper mapper;

        public ProductService(
            IRepository<Category> categoryRepository,
            IMapper mapper,
            ILogger<ProductService> logger,
            IRepository<Product> productRepository,
            IMenuService menuService,
            IRepository<Menu> menuRepository)
        {
            this.menuRepository = menuRepository;
            this.menuService = menuService;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.productRepository = productRepository;
        }

        public async Task AddProductAsync(ProductViewModel model)
        {
            try
            {
                var product = this.mapper.Map<Product>(model);

                product.Picture = await model.Picture.UploadAsync();
                product.PictureAsString = Convert.ToBase64String(product.Picture);

                if (model.CategoryId != null)
                {
                    product.CategoryId = int.Parse(model.CategoryId);
                }

                if (model.MenuId != null)
                {
                    product.MenuId = int.Parse(model.MenuId);
                }

                product.TotalCalories =
                    (product.ProteinsQuantity * NumericConstants.ProteinCaloriesPerGram)
                    + (product.CarbohydratesQuantity * NumericConstants.GarbohydrateCaloriesPerGram)
                    + (product.FatQuantity * NumericConstants.FatCaloriesPerGram);

                await this.productRepository.AddAsync(product);

                await this.productRepository.SaveChangesAsync();

                this.logger.LogInformation(string.Format(LogMessages.ProductWasAdded, product.Id));
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task EditProductAsync(int id, ProductViewModel model)
        {
            try
            {
                var product = this.productRepository.All().FirstOrDefault(e => e.Id == id);

                if (product == null)
                {
                    throw new ApplicationException(string.Format(ErrorMessages.ProductDoesNotExist, id));
                }

                product.Name = model.Name;
                product.ProteinsQuantity = model.ProteinsQuantity;
                product.CarbohydratesQuantity = model.CarbohydratesQuantity;
                product.FatQuantity = model.FatQuantity;
                product.Price = model.Price;

                if (model.CategoryId != null)
                {
                    product.CategoryId = int.Parse(model.CategoryId);
                }

                if (model.MenuId != null)
                {
                    product.MenuId = int.Parse(model.MenuId);
                }

                product.Picture = await model.Picture.UploadAsync();
                product.PictureAsString = Convert.ToBase64String(product.Picture);

                this.productRepository.Update(product);

                await this.productRepository.SaveChangesAsync();

                this.logger.LogInformation(string.Format(LogMessages.ProductUpdated, product.Id));
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            try
            {
                var product = this.productRepository.All().FirstOrDefault(e => e.Id == id);

                if (product == null)
                {
                    throw new ApplicationException(string.Format(ErrorMessages.ProductDoesNotExist, id));
                }

                this.productRepository.Delete(product);

                await this.productRepository.SaveChangesAsync();

                this.logger.LogInformation(string.Format(LogMessages.ProductDeleted, product.Id));
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task DetachFromMenuAsync(int id)
        {
            try
            {
                var product = this.productRepository.All().FirstOrDefault(e => e.Id == id);

                if (product == null)
                {
                    throw new ApplicationException(string.Format(ErrorMessages.ProductDoesNotExist, id));
                }

                product.MenuId = null;

                await this.productRepository.SaveChangesAsync();

                if (product.MenuId != null)
                {
                    this.logger.LogInformation(string.Format(LogMessages.ProductDetachedFromMenu, product.Name, product.Menu.Name));
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task DetachFromCategoryAsync(int id)
        {
            try
            {
                var product = this.productRepository.All().FirstOrDefault(e => e.Id == id);

                if (product == null)
                {
                    throw new ApplicationException(string.Format(ErrorMessages.ProductDoesNotExist, id));
                }

                product.CategoryId = null;

                await this.productRepository.SaveChangesAsync();

                if (product.CategoryId != null)
                {
                    this.logger.LogInformation(string.Format(LogMessages.ProductDetachedFromMenu, product.Name, product.Category.Name));
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task<ProductViewModel> MapProductAsync(int id)
        {
            var product = this.productRepository.All().FirstOrDefault(e => e.Id == id);

            if (product == null)
            {
                throw new ApplicationException(string.Format(ErrorMessages.ProductDoesNotExist, id));
            }

            var model = this.mapper.Map<ProductViewModel>(product);

            return await Task.FromResult<ProductViewModel>(model);
        }

        public async Task<ICollection<ProductViewModel>> MapProductsAsync(ProductsViewModel model)
        {
            var product = this.productRepository.All().ToList();

            model.Products = this.mapper.Map<ICollection<ProductViewModel>>(product);

            return await Task.FromResult<ICollection<ProductViewModel>>(model.Products);
        }

        public async Task<ICollection<Product>> GetProductsAsync()
        {
            var products = productRepository.All().ToList();

            return await Task.FromResult<ICollection<Product>>(products);
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var product = this.productRepository.All().FirstOrDefault(e => e.Id == id);

            return await Task.FromResult<Product>(product);
        }
    }
}
