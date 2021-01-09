namespace FastFoodWorkshop.ServiceModels.Applicant
{
    using Common.WebConstants;
    using Common.CustomValidations;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class JobInputModel
    {
        [Required]
        [DataType(DataType.DateTime)]
        [DateRestrictToday(ErrorMessage = ErrorMessages.DateCannotBeAfterToday)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DateGreaterThan(StringConstants.StartDate)]
        [DateRestrictToday(ErrorMessage = ErrorMessages.DateCannotBeAfterToday)]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(NumericConstants.MinimumNumberLimit, NumericConstants.MaximumNumberLimit, ErrorMessage = ErrorMessages.ValueCannotBeNegative)]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        public string CompanyName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(NumericConstants.MaximumTextAreaLength, MinimumLength = NumericConstants.MinimumTextAreaLength, ErrorMessage = ErrorMessages.TextAreaMaxLength)]
        public string JobDescription { get; set; }
    }
}

