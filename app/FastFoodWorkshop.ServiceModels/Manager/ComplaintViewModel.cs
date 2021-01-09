namespace FastFoodWorkshop.ServiceModels.Manager
{
    using System;

    public class ComplaintViewModel
    {
        public int Id { get; set; }

        public string FastFoodUserId { get; set; }
        public string FastFoodUsername { get; set; }

        public string RestaurantId { get; set; }
        public string RestaurantName { get; set; }

        public string Description { get; set; }

        public DateTime DateOfComplaint => DateTime.UtcNow;
    }
}
