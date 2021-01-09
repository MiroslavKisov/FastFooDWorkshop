namespace FastFoodWorkshop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DeliveryCar
    {
        [Key]
        public int Id { get; set; }

        public string Model { get; set; }

        public double Mileage { get; set; }

        public DateTime ProductionDate { get; set; }

        public double TankCapacity { get; set; }

        public double ConsumptionPerMile { get; set; }

        public int? RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
