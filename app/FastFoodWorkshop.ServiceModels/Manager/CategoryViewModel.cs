namespace FastFoodWorkshop.ServiceModels.Manager
{
    using Common.WebConstants;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        public string Name { get; set; }

        public ICollection<ProductViewModel> Products { get; set; }
    }
}
