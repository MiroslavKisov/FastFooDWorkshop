namespace FastFoodWorkshop.Models
{
    using Common.WebConstants;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ApplicantCV
    {
        public ApplicantCV()
        {
            this.PreviousJobs = new HashSet<Job>();
            this.Schools = new HashSet<Education>();
        }

        [Key]
        public int Id { get; set; }

        public string ApplicantFirstName { get; set; }

        public string ApplicantLastName { get; set; }

        public byte[] Picture { get; set; }

        public DateTime Birthdate { get; set; }

        public int Age => DateTime.UtcNow.Year - Birthdate.Year;

        public string MotivationalLetter { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool IsHired { get; set; }

        public virtual ICollection<Job> PreviousJobs { get; set; }

        public virtual ICollection<Education> Schools { get; set; }
    }
}
