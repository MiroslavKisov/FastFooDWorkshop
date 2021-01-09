namespace FastFoodWorkshop.ServiceModels.Applicant
{
    using Common.CustomValidations;
    using Common.WebConstants;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EducationViewModel
    {
        [Required]
        [DataType(DataType.DateTime)]
        [DateRestrictToday(ErrorMessage = ErrorMessages.DateCannotBeAfterToday)]
        public DateTime StartDate { get; set; }
    
        [DataType(DataType.DateTime)]
        [DateGreaterThan(StringConstants.StartDate)]
        [DateRestrictToday(ErrorMessage = ErrorMessages.DateCannotBeAfterToday)]
        public DateTime? EndDate { get; set; }

        [Required]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        [DataType(DataType.Text)]
        public string OrganizationName { get; set; }
    }
}
