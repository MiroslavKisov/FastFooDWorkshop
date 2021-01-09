namespace FastFoodWorkshop.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class FastFoodUser : IdentityUser<int>
    {
        public FastFoodUser()
        {
            this.Recepies = new HashSet<Recipe>();
            this.Orders = new HashSet<Order>();
            this.Complaints = new HashSet<Complaint>();
        }

        public byte[] Picture { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public DateTime BirthDate { get; set; }

        public int Age => DateTime.UtcNow.Year - BirthDate.Year;

        public virtual ICollection<Recipe> Recepies { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Complaint> Complaints { get; set; }
    }
}
