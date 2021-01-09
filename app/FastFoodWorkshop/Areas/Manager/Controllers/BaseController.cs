namespace FastFoodWorkshop.Areas.Manager.Controllers
{
    using Common.WebConstants;
    using Service.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Linq;

    [RequireHttps]
    [Area(StringConstants.Manager)]
    [Authorize(Roles = StringConstants.ManagerRole)]
    public class BaseController : Controller
    {
        private readonly IRestaurantService restaurantService;
        private readonly IProductService productService;
        private readonly IMenuService menuService;
        private readonly ICategoryService categoryService;

        public BaseController(
            ICategoryService categoryService,
            IProductService productService,
            IRestaurantService restaurantService,
            IMenuService menuService)
        {
            this.categoryService = categoryService;
            this.menuService = menuService;
            this.productService = productService;
            this.restaurantService = restaurantService;
        }
        public override async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var restaurants = await this.restaurantService.GetRestaurantsAsync();
            var categories = await this.categoryService.GetCategoriesAsync();
            var products = await this.productService.GetProductsAsync();
            var menus = await this.menuService.GetMenusAsync();

            this.ViewBag.Menus = new SelectList(menus, StringConstants.Id, StringConstants.Name);
            this.ViewBag.Products = new SelectList(products, StringConstants.Id, StringConstants.Name);
            this.ViewBag.RestaurantsList = new SelectList(restaurants, StringConstants.Id, StringConstants.Name);
            this.ViewBag.Categories = new SelectList(categories, StringConstants.Id, StringConstants.Name);
            //this.ViewBag.Categories = categories.ToList();
            //this.ViewBag.MenusList = menus.ToList();
            this.ViewBag.restaurants = restaurants;
        }
    }
}

