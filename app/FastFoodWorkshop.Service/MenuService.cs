namespace FastFoodWorkshop.Service
{
    using System.Threading.Tasks;
    using Contracts;
    using ServiceModels.Manager;
    using System;
    using AutoMapper;
    using Models;
    using Data;
    using Microsoft.Extensions.Logging;
    using Common.WebConstants;
    using System.Linq;
    using System.Collections.Generic;

    public class MenuService : IMenuService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Menu> menuRepository;
        private readonly IRepository<Product> productRepository;
        private readonly ILogger logger;

        public MenuService(
            ILogger<MenuService> logger,
            IRepository<Menu> menuRepository,
            IMapper mapper,
            IRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
            this.logger = logger;
            this.menuRepository = menuRepository;
            this.mapper = mapper;
        }

        public async Task<int> CreateMenuAsync(MenuViewModel model)
        {
            int menuId = 0;

            try
            {
                var menu = this.mapper.Map<Menu>(model);

                await this.menuRepository.AddAsync(menu);

                menuId = await this.menuRepository.SaveChangesAsync();

                this.logger.LogInformation(string.Format(LogMessages.MenuCreated, menu.Id));
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }

            return menuId;
        }

        public async Task<Menu> GetMenuAsync(int id)
        {
            var menu = this.menuRepository.All().FirstOrDefault(e => e.Id == id);

            return await Task.FromResult<Menu>(menu);
        }

        public async Task<ICollection<Menu>> GetMenusAsync()
        {
            var menus = this.menuRepository.All().ToList();

            return await Task.FromResult<ICollection<Menu>>(menus);
        }

        public async Task<ICollection<MenuViewModel>> MapMenusAsync(MenusViewModel model)
        {
            var menu = this.menuRepository.All().ToList();

            model.Menus = this.mapper.Map<ICollection<MenuViewModel>>(menu);

            return await Task.FromResult<ICollection<MenuViewModel>>(model.Menus);
        }

        public async Task<MenuViewModel> MapMenuAsync(int id)
        {
            var menu = this.menuRepository.All().FirstOrDefault(e => e.Id == id);

            if (menu == null)
            {
                throw new ApplicationException(string.Format(ErrorMessages.MenuDoesNotExist, id));
            }

            MenuViewModel model = null;

            model = this.mapper.Map<MenuViewModel>(menu);

            return await Task.FromResult<MenuViewModel>(model);
        }

        public async Task EditMenuAsync(int id, MenuViewModel model)
        {
            try
            {
                var menu = this.menuRepository.All().FirstOrDefault(e => e.Id == id);

                if (menu == null)
                {
                    throw new ApplicationException(ErrorMessages.MenuDoesNotExist);
                }

                menu.Name = model.Name;
                menu.Discount = model.Discount;

                this.menuRepository.Update(menu);

                await this.menuRepository.SaveChangesAsync();

                this.logger.LogInformation(string.Format(LogMessages.MenuUpdated, menu.Id));
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public async Task DeleteMenuAsync(int id)
        {
            try
            {
                var menu = this.menuRepository.All().FirstOrDefault(e => e.Id == id);

                if (menu == null)
                {
                    throw new ApplicationException(string.Format(ErrorMessages.MenuDoesNotExist, id));
                }

                this.menuRepository.Delete(menu);

                await this.menuRepository.SaveChangesAsync();

                this.logger.LogInformation(string.Format(LogMessages.MenuDeleted, menu.Id));
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }
    }
}
