namespace FastFoodWorkshop.Areas.Manager.Controllers
{
    using Service.Contracts;
    using ServiceModels.Manager;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Common.WebConstants;
    using System;

    public class ProductsController : BaseController
    {
        private readonly IProductService productService;

        public ProductsController(
            ICategoryService categoryService,
            IMenuService menuService,
            IRestaurantService restaurantService,
            IProductService productService)
            :base(
                 categoryService,
                 productService, 
                 restaurantService, 
                 menuService)
        {
            this.productService = productService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.productService.AddProductAsync(model);
                TempData[TempDataKeys.TempDataSuccessProductKey] = string.Format(SuccessMessages.ProductSuccessMessage, model.Name);
            }

            return RedirectToAction(StringConstants.Manage, StringConstants.Manage, new { area = StringConstants.Manager });
        }

        public async Task<IActionResult> Products(ProductsViewModel model)
        {
            model.Products = await this.productService.MapProductsAsync(model);
            return this.View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ProductViewModel model = null;

            try
            {
               model = await this.productService.MapProductAsync(id);
            }
            catch (Exception)
            {
                TempData[TempDataKeys.TempDataNonExistentProductKey] = string.Format(ErrorMessages.ProductDoesNotExist, id);
                return RedirectToAction(StringConstants.Manage, StringConstants.Manage, new { area = StringConstants.Manager });
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await this.productService.EditProductAsync(id, model);
                    TempData[TempDataKeys.TempDataSuccessEditProductKey] = string.Format(SuccessMessages.ProductEditSuccessMessage, model.Name);
                }
            }
            catch (Exception)
            {
                TempData[TempDataKeys.TempDataNonExistentProductKey] = string.Format(ErrorMessages.ProductDoesNotExist, id);
                return RedirectToAction(StringConstants.Manage, StringConstants.Manage, new { area = StringConstants.Manager });
            }

            return RedirectToAction(StringConstants.Products, StringConstants.Product, new { area = StringConstants.Manager });
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = await this.productService.MapProductAsync(id);
                await this.productService.DeleteProductAsync(id);
                TempData[TempDataKeys.TempDataSuccessDeleteProductKey] = string.Format(SuccessMessages.ProductDeleteSuccessMessage, model.Name);
            }
            catch (Exception)
            {
                TempData[TempDataKeys.TempDataNonExistentProductKey] = string.Format(ErrorMessages.ProductDoesNotExist, id);
                return RedirectToAction(StringConstants.Products, StringConstants.Products, new { area = StringConstants.Manager });
            }

            return RedirectToAction(StringConstants.Products, StringConstants.Products, new { area = StringConstants.Manager });
        }
    }
}
