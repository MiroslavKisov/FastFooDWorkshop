namespace FastFoodWorkshop.ServiceModels.User
{
    using Common.WebConstants;
    using System.ComponentModel.DataAnnotations;

    public class UserExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        public string Email { get; set; }
    }
}
