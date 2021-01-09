namespace FastFoodWorkshop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Education
    {
        [Key]
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string OrganizationName { get; set; }

        public int ApplicantCVId { get; set; }
        public virtual ApplicantCV ApplicantCV { get; set; }
    }
}
