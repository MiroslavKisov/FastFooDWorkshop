namespace FastFoodWorkshop.ServiceModels.Manager
{
    using Common.CustomValidations;
    using Common.WebConstants;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [Range(NumericConstants.MinimumNumberLimit, NumericConstants.MaximumNumberLimit, ErrorMessage = ErrorMessages.ValueCannotBeNegative)]
        public double ProteinsQuantity { get; set; }

        [Required]
        [Range(NumericConstants.MinimumNumberLimit, NumericConstants.MaximumNumberLimit, ErrorMessage = ErrorMessages.ValueCannotBeNegative)]
        public double CarbohydratesQuantity { get; set; }

        [Required]
        [Range(NumericConstants.MinimumNumberLimit, NumericConstants.MaximumNumberLimit, ErrorMessage = ErrorMessages.ValueCannotBeNegative)]
        public double FatQuantity { get; set; }

        public double TotalCalories { get; set; }

        [Required]
        [Range(NumericConstants.MinimumNumberLimit, NumericConstants.MaximumNumberLimit, ErrorMessage = ErrorMessages.ValueCannotBeNegative)]
        public decimal Price { get; set; }

        [Required]
        [MaxFileSize(NumericConstants.PictureSizeLimit, ErrorMessage = ErrorMessages.FileIsTooBig)]
        public IFormFile Picture { get; set; }

        public string PictureAsString { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string MenuId { get; set; }

        public string MenuName { get; set; }
    }
}
