namespace FastFoodWorkshop.ServiceModels.User
{
    using Common.CustomValidations;
    using Common.WebConstants;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserRegisterViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = StringConstants.DateTimeFormat, ApplyFormatInEditMode = true)]
        [DateRestrictToday(ErrorMessage = ErrorMessages.DateCannotBeAfterToday)]
        public DateTime BirthDate { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = ErrorMessages.InputMaxLength)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(StringConstants.Password, ErrorMessage = ErrorMessages.PasswordsDoNotMatch)]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        public string PhoneNumber { get; set; }

        [BindProperty]
        [MaxFileSize(NumericConstants.PictureSizeLimit, ErrorMessage = ErrorMessages.FileIsTooBig)]
        public IFormFile Picture { get; set; }
    }
}
