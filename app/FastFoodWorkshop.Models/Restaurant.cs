namespace FastFoodWorkshop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Restaurant
    {
        public Restaurant()
        {
            this.Orders = new HashSet<Order>();
            this.DeliveryCars = new HashSet<DeliveryCar>();
            this.Complaints = new HashSet<Complaint>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public virtual ICollection<Complaint> Complaints { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<DeliveryCar> DeliveryCars { get; set; }
    }
}
