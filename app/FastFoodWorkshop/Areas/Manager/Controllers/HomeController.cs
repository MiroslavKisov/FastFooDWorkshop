namespace FastFoodWorkshop.Areas.Manager.Controllers
{
    using Service.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        public HomeController(
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

        }

        public IActionResult Index()
        {
            return this.View();
        }
    }
}
