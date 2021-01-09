namespace FastFoodWorkshop.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public double ProteinsQuantity { get; set; }

        public double CarbohydratesQuantity { get; set; }

        public double FatQuantity { get; set; }

        public double TotalCalories { get; set; }

        public decimal Price { get; set; }

        public byte[] Picture { get; set; }

        public string PictureAsString { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int? MenuId { get; set; }
        public virtual Menu Menu { get; set; }
    }
}

