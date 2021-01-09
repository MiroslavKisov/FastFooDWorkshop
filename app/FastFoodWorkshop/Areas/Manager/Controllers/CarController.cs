namespace FastFoodWorkshop.Areas.Manager.Controllers
{
    using ServiceModels.Manager;
    using Microsoft.AspNetCore.Mvc;
    using Service.Contracts;
    using System.Threading.Tasks;
    using Common.WebConstants;
    using System;

    public class CarController : BaseController
    {
        private readonly ICarService carService;

        public CarController(
            ICarService carService,
            IProductService productService,
            IRestaurantService restaurantService,
            IMenuService menuService,
            ICategoryService categoryService)
            :base(
                 categoryService, 
                 productService, 
                 restaurantService, 
                 menuService)
        {
            this.carService = carService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(DeliveryCarViewModel carModel)
        {
            if (ModelState.IsValid)
            {
                await this.carService.AddCarAsync(carModel);
                TempData[TempDataKeys.TempDataSuccessCarKey] = string.Format(SuccessMessages.CarSuccessMessage, carModel.Model);
            }

            return RedirectToAction(StringConstants.Manage, StringConstants.Manage, new { area = StringConstants.Manager });
        }

        public async Task<IActionResult> Cars(DeliveryCarsViewModel model)
        {
            model.DeliveryCars = await this.carService.MapCarsAsync(model);
            return this.View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            DeliveryCarViewModel model = null;

            try
            {
                model = await this.carService.MapCarAsync(id);
            }
            catch (Exception)
            {
                TempData[TempDataKeys.TempDataNonExistentCarKey] = string.Format(ErrorMessages.CarDoesNotExist, id);
                return RedirectToAction(StringConstants.Manage, StringConstants.Manage, new { area = StringConstants.Manager });
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DeliveryCarViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await this.carService.EditCarAsync(id, model);
                    TempData[TempDataKeys.TempDataSuccessEditCarKey] = string.Format(SuccessMessages.CarEditSuccessMessage, model.Model);
                }
            }
            catch (Exception)
            {
                TempData[TempDataKeys.TempDataNonExistentCarKey] = string.Format(ErrorMessages.CarDoesNotExist, id);
                return RedirectToAction(StringConstants.Manage, StringConstants.Manage, new { area = StringConstants.Manager });
            }

            return RedirectToAction(StringConstants.Cars, StringConstants.Car, new { area = StringConstants.Manager });
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = await this.carService.MapCarAsync(id);
                await this.carService.DeleteCarAsync(id);
                TempData[TempDataKeys.TempDataSuccessDeleteCarKey] = string.Format(SuccessMessages.CarDeleteSuccessMessage, model.Model);
            }
            catch (Exception)
            {
                TempData[TempDataKeys.TempDataNonExistentCarKey] = string.Format(ErrorMessages.CarDoesNotExist, id);
                return RedirectToAction(StringConstants.Products, StringConstants.Products, new { area = StringConstants.Manager });
            }

            return RedirectToAction(StringConstants.Cars, StringConstants.Car, new { area = StringConstants.Manager });
        }
    }
}
