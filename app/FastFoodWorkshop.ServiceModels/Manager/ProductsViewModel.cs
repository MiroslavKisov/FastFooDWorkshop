namespace FastFoodWorkshop.ServiceModels.Manager
{
    using System.Collections.Generic;

    public class ProductsViewModel
    {
        public ICollection<ProductViewModel> Products { get; set; }
    }
}
