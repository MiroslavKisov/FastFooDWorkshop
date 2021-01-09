namespace FastFoodWorkshop.Areas.Manager.Controllers
{
    using Service.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class ManageController : BaseController
    {
        public ManageController(
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

        }

        public IActionResult Manage()
        {
            return this.View();
        }
    }
}
