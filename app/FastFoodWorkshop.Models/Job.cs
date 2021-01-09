namespace FastFoodWorkshop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Job
    {
        [Key]
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Salary { get; set; }

        public string CompanyName { get; set; }

        public string JobDescription { get; set; }

        public int ApplicantCVId { get; set; }
        public virtual ApplicantCV ApplicantCV { get; set; }
    }
}
