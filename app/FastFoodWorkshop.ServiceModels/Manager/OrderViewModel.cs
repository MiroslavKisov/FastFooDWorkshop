namespace FastFoodWorkshop.ServiceModels.Manager
{
    using Models.Enums;
    using System;
    using System.Collections.Generic;

    public class OrderViewModel
    {
        public int Id { get; set; }

        public DateTime DateOfOrder { get; set; }

        public OrderType Type { get; set; }

        public string FastFoodUserId { get; set; }

        public string FastFoodUsername { get; set; }

        public string RestaurantId { get; set; }

        public virtual ICollection<MenuViewModel> Menus { get; set; }
    }
}
