namespace FastFoodWorkshop.ServiceModels.Applicant
{
    using Common.WebConstants;
    using Common.CustomValidations;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;

    public class ApplicantCvViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        public string ApplicantFirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        public string ApplicantLastName { get; set; }

        [MaxFileSize(NumericConstants.PictureSizeLimit, ErrorMessage = ErrorMessages.FileIsTooBig)]
        public IFormFile Picture { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = StringConstants.DateTimeFormat, ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [DateRestrictBirthday(ErrorMessage = ErrorMessages.NotOldEnough)]
        public DateTime BirthDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(NumericConstants.MaximumTextAreaLength, MinimumLength = NumericConstants.MinimumTextAreaLength, ErrorMessage = ErrorMessages.TextAreaMaxLength)]
        public string MotivationalLetter { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        public string Email { get; set; }
    }
}
