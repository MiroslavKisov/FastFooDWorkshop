namespace FastFoodWorkshop.ServiceModels.User
{
    using Common.WebConstants;
    using System.ComponentModel.DataAnnotations;

    public class UserComplaintViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(NumericConstants.MaximumTextAreaLength, MinimumLength = NumericConstants.MinimumTextAreaLength, ErrorMessage = ErrorMessages.TextAreaMaxLength)]
        public string Description { get; set; }

        public string RestaurantId { get; set; }
    }
}
