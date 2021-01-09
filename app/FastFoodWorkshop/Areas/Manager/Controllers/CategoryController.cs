namespace FastFoodWorkshop.Areas.Manager.Controllers
{
    using Common.WebConstants;
    using Service.Contracts;
    using ServiceModels.Manager;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System;

    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public CategoryController(
            ICategoryService categoryService,
            IRestaurantService restaurantService,
            IProductService productService,
            IMenuService menuService
            )
            : base(
                  categoryService,
                  productService,
                  restaurantService,
                  menuService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.categoryService.AddCategoryAsync(model);
                TempData[TempDataKeys.TempDataSuccessCategoryKey] = string.Format(SuccessMessages.CategorySuccessMessage, model.Name);
            }

            return RedirectToAction(StringConstants.Manage, StringConstants.Manage, new { area = StringConstants.Manager });
        }

        public async Task<IActionResult> Categories(CategoriesViewModel model)
        {
            model.Categories = await this.categoryService.MapCategoriesAsync(model);
            return this.View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            CategoryViewModel model = null;

            try
            {
                model = await this.categoryService.MapCategoryAsync(id);
            }
            catch (Exception)
            {
                TempData[TempDataKeys.TempDataNonExistentCategoryKey] = string.Format(ErrorMessages.CategoryDoesNotExist, id);
                return RedirectToAction(StringConstants.Manage, StringConstants.Manage, new { area = StringConstants.Manager });
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await this.categoryService.EditCategoryAsync(id, model);
                    TempData[TempDataKeys.TempDataSuccessEditCategoryKey] = string.Format(SuccessMessages.CategoryEditSuccessMessage, model.Name);
                }
            }
            catch (Exception)
            {
                TempData[TempDataKeys.TempDataNonExistentCategoryKey] = string.Format(ErrorMessages.CategoryDoesNotExist, id);
                return RedirectToAction(StringConstants.Manage, StringConstants.Manage, new { area = StringConstants.Manager });
            }

            return RedirectToAction(StringConstants.Categories, StringConstants.Category, new { area = StringConstants.Manager });
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = await this.categoryService.MapCategoryAsync(id);
                await this.categoryService.DeleteCategoryAsync(id);
                TempData[TempDataKeys.TempDataSuccessDeleteCategoryKey] = string.Format(SuccessMessages.CategoryDeleteSuccessMessage, model.Name);
            }
            catch (Exception)
            {
                TempData[TempDataKeys.TempDataNonExistentCategoryKey] = string.Format(ErrorMessages.CategoryDoesNotExist, id);
                return RedirectToAction(StringConstants.Manage, StringConstants.Manage, new { area = StringConstants.Manager });
            }

            return RedirectToAction(StringConstants.Categories, StringConstants.Category, new { area = StringConstants.Manager });
        }

        public async Task<IActionResult> Detach(int id)
        {
            try
            {
                await this.productService.DetachFromCategoryAsync(id);
                var product = await this.productService.GetProductAsync(id);
                if (product.CategoryId == null)
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

            return RedirectToAction(StringConstants.Categories, StringConstants.Category, new { area = StringConstants.Manager });
        }
    }
}
