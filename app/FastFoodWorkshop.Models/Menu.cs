namespace FastFoodWorkshop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Menu
    {
        public Menu()
        {
            this.Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Discount { get; set; }

        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
