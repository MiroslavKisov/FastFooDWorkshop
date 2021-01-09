namespace FastFoodWorkshop.Areas.Manager.Controllers
{
    using Service.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using ServiceModels.Manager;
    using Common.WebConstants;
    using System;

    public class RestaurantController : BaseController
    {
        private readonly IRestaurantService restaurantService;

        public RestaurantController(
            ICategoryService categoryService,
            IRestaurantService restaurantService,
            IProductService productService,
            IMenuService menuService)
            : base(
                  categoryService,
                  productService, 
                  restaurantService,
                  menuService)
        {
            this.restaurantService = restaurantService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RestaurantViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.restaurantService.AddRestaurantAsync(model);
                TempData[TempDataKeys.TempDataSuccessRestaurantKey] = string.Format(SuccessMessages.RestaurantSuccessMessage, model.Name);
            }

            return RedirectToAction(StringConstants.Manage, StringConstants.Manage, new { area = StringConstants.Manager });
        }

        public async Task<IActionResult> Restaurants(RestaurantsViewModel model)
        {
            model.Restaurants = await this.restaurantService.MapRestaurantsAsync(model);
            return this.View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            RestaurantViewModel model = null;

            try
            {
                model = await this.restaurantService.MapRestaurantAsync(id);
            }
            catch (Exception)
            {
                TempData[TempDataKeys.TempDataNonExistentRestaurantKey] = string.Format(ErrorMessages.RestaurantDoesNotExist, id);
                return RedirectToAction(StringConstants.Manage, StringConstants.Manage, new { area = StringConstants.Manager });
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RestaurantViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await this.restaurantService.EditRestaurantAsync(id, model);
                    TempData[TempDataKeys.TempDataSuccessEditRestaurantKey] = string.Format(SuccessMessages.RestaurantEditSuccessMessage, model.Name);
                }
            }
            catch (Exception)
            {
                TempData[TempDataKeys.TempDataNonExistentRestaurantKey] = string.Format(ErrorMessages.RestaurantDoesNotExist, id);
                return RedirectToAction(StringConstants.Manage, StringConstants.Manage, new { area = StringConstants.Manager });
            }

            return RedirectToAction(StringConstants.Restaurants, StringConstants.Restaurant, new { area = StringConstants.Manager });
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = await this.restaurantService.MapRestaurantAsync(id);
                await this.restaurantService.DeleteRestaurantAsync(id);
                TempData[TempDataKeys.TempDataSuccessDeleteRestaurantKey] = string.Format(SuccessMessages.RestaurantDeleteSuccessMessage, model.Name);
            }
            catch (Exception)
            {
                TempData[TempDataKeys.TempDataNonExistentRestaurantKey] = string.Format(ErrorMessages.RestaurantDoesNotExist, id);
                return RedirectToAction(StringConstants.Restaurants, StringConstants.Restaurant, new { area = StringConstants.Manager });
            }

            return RedirectToAction(StringConstants.Restaurants, StringConstants.Restaurant, new { area = StringConstants.Manager });
        }
    }
}
