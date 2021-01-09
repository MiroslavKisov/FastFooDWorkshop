namespace FastFoodWorkshop.Areas.Manager.Controllers
{
    using Common.WebConstants;
    using Service.Contracts;
    using ServiceModels.Manager;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class MenuController : BaseController
    {
        private readonly IMenuService menuService;
        private readonly IProductService productService;

        public MenuController(
            ICategoryService categoryService,
            IRestaurantService restaurantService,
            IProductService productService,
            IMenuService menuService)
            :base(
                 categoryService,
                 productService, 
                 restaurantService, 
                 menuService)
        {
            this.productService = productService;
            this.menuService = menuService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuViewModel model)
        {
            if (ModelState.IsValid)
            {
                var menuId = await this.menuService.CreateMenuAsync(model);
                TempData[TempDataKeys.TempDataSuccessMenuKey] = string.Format(SuccessMessages.MenuSuccessMessage, model.Name);
            }

            return RedirectToAction(StringConstants.Manage, StringConstants.Manage, new { area = StringConstants.Manager });
        }

        public async Task<IActionResult> Menus(MenusViewModel model)
        {
            model.Menus = await this.menuService.MapMenusAsync(model);
            return this.View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            MenuViewModel model = null;

            try
            {
                model = await this.menuService.MapMenuAsync(id);
            }
            catch (Exception)
            {
                TempData[TempDataKeys.TempDataNonExistentMenuKey] = string.Format(ErrorMessages.MenuDoesNotExist, id);
                return RedirectToAction(StringConstants.Manage, StringConstants.Manage, new { area = StringConstants.Manager });
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MenuViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await this.menuService.EditMenuAsync(id, model);
                    TempData[TempDataKeys.TempDataSuccessEditMenuKey] = string.Format(SuccessMessages.MenuEditSuccessMessage, model.Name);
                }
            }
            catch (Exception)
            {
                TempData[TempDataKeys.TempDataNonExistentMenuKey] = string.Format(ErrorMessages.MenuDoesNotExist, id);
                return RedirectToAction(StringConstants.Manage, StringConstants.Manage, new { area = StringConstants.Manager });
            }

            return RedirectToAction(StringConstants.Menus, StringConstants.Menu, new { area = StringConstants.Manager });
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = await this.menuService.MapMenuAsync(id);
                await this.menuService.DeleteMenuAsync(id);
                TempData[TempDataKeys.TempDataSuccessDeleteMenuKey] = string.Format(SuccessMessages.MenuDeleteSuccessMessage, model.Name);
            }
            catch (Exception)
            {
                TempData[TempDataKeys.TempDataNonExistentMenuKey] = string.Format(ErrorMessages.MenuDoesNotExist, id);
                return RedirectToAction(StringConstants.Manage, StringConstants.Manage, new { area = StringConstants.Manager });
            }

            return RedirectToAction(StringConstants.Menus, StringConstants.Menu, new { area = StringConstants.Manager });
        }

        public async Task<IActionResult> Detach(int id)
        {
            try
            {
                await this.productService.DetachFromMenuAsync(id);
                var product = await this.productService.GetProductAsync(id);
                if (product.MenuId == null)
                {
                    TempData[TempDataKeys.TempDataProductAlreadyDetached] = string.Format(ErrorMessages.ProductAlreadyDetached, id);
                }
                else
                {
                    TempData[TempDataKeys.TempDataSuccessProductDetached] = string.Format(SuccessMessages.ProductDetachSuccessMessage, product.Name);
                }
            }
            catch (Exception)
            {
                TempData[TempDataKeys.TempDataNonExistentProductKey] = string.Format(ErrorMessages.ProductDoesNotExist, id);
                return RedirectToAction(StringConstants.Manage, StringConstants.Manage, new { area = StringConstants.Manager });
            }

            return RedirectToAction(StringConstants.Menus, StringConstants.Menu, new { area = StringConstants.Manager });
        }
    }
}
