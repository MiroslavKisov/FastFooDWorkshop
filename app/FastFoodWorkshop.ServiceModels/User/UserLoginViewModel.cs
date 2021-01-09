namespace FastFoodWorkshop.ServiceModels.User
{
    using Common.WebConstants;
    using System.ComponentModel.DataAnnotations;

    public class UserLoginViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
