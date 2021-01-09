namespace FastFoodWorkshop.Controllers
{ 
    using ServiceModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Common.WebConstants;

    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            TempData[TempDataKeys.TempDataJoinUsWorkFlowKey] = false;
            return this.View();
        }

        [AllowAnonymous]
        public IActionResult ContactsPage()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
