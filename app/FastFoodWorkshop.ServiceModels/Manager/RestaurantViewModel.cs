namespace FastFoodWorkshop.ServiceModels.Manager
{
    using Common.WebConstants;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RestaurantViewModel
    {
        public int Id { get; set; }

        public string RestaurantId { get; set; }

        [Required]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        public string Address { get; set; }

        public ICollection<OrderViewModel> Orders { get; set; }

        public ICollection<ComplaintViewModel> Complaints { get; set; }

        public ICollection<DeliveryCarViewModel> DeliveryCars { get; set; }
    }
}
