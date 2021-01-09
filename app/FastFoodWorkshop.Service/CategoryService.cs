namespace FastFoodWorkshop.Service
{
    using AutoMapper;
    using Contracts;
    using Data;
    using Common.WebConstants;
    using Microsoft.Extensions.Logging;
    using Models;
    using ServiceModels.Manager;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;
    using System.Linq;

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoryRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public CategoryService(
            ILogger<CategoryService> logger,
            IRepository<Category> categoryRepository,
            IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
        }

        public async Task AddCategoryAsync(CategoryViewModel model)
        {
            try
            {
                var category = this.mapper.Map<Category>(model);

                await this.categoryRepository.AddAsync(category);

                await this.categoryRepository.SaveChangesAsync();

                this.logger.LogInformation(string.Format(LogMessages.CategoryAdded, category.Id));
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            try
            {
                var category = this.categoryRepository.All().FirstOrDefault(e => e.Id == id);

                if (category == null)
                {
                    throw new ApplicationException(string.Format(ErrorMessages.CategoryDoesNotExist, id));
                }

                this.categoryRepository.Delete(category);

                await this.categoryRepository.SaveChangesAsync();

                this.logger.LogInformation(string.Format(LogMessages.CategoryDeleted, category.Id));
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task EditCategoryAsync(int id, CategoryViewModel model)
        {
            try
            {
                var category = this.categoryRepository.All().FirstOrDefault(e => e.Id == id);

                if (category == null)
                {
                    throw new ApplicationException(string.Format(ErrorMessages.CategoryDoesNotExist, id));
                }

                category.Name = model.Name;

                this.categoryRepository.Update(category);

                await this.categoryRepository.SaveChangesAsync();

                this.logger.LogInformation(string.Format(LogMessages.CategoryAdded, category.Id));
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task<ICollection<Category>> GetCategoriesAsync()
        {
            var category = categoryRepository.All().ToList();

            return await Task.FromResult<ICollection<Category>>(category);
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            var category = this.categoryRepository.All().FirstOrDefault(e => e.Id == id);

            return await Task.FromResult<Category>(category);
        }

        public async Task<ICollection<CategoryViewModel>> MapCategoriesAsync(CategoriesViewModel model)
        {
            var category = this.categoryRepository.All().ToList();

            model.Categories = this.mapper.Map<ICollection<CategoryViewModel>>(category);

            return await Task.FromResult<ICollection<CategoryViewModel>>(model.Categories);
        }

        public async Task<CategoryViewModel> MapCategoryAsync(int id)
        {
            var category = this.categoryRepository.All().FirstOrDefault(e => e.Id == id);

            if (category == null)
            {
                throw new ApplicationException(string.Format(ErrorMessages.CategoryDoesNotExist, id));
            }

            var model = this.mapper.Map<CategoryViewModel>(category);

            return await Task.FromResult<CategoryViewModel>(model);
        }
    }
}
