namespace FastFoodWorkshop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Complaint
    {
        [Key]
        public int Id { get; set; }

        public int FastFoodUserId { get; set; }
        public virtual FastFoodUser FastFoodUser { get; set; }

        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public string Description { get; set; }

        public DateTime DateOfComplaint => DateTime.UtcNow;
    }
}