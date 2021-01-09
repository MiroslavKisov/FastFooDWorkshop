namespace FastFoodWorkshop.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        public Order()
        {
            this.Menus = new HashSet<Menu>();
        }

        [Key]
        public int Id { get; set; }

        public DateTime DateOfOrder { get; set; }

        public OrderType Type { get; set; }

        public int? FastFoodUserId { get; set; }
        public virtual FastFoodUser FastFoodUser { get; set; }

        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }     
    }
}