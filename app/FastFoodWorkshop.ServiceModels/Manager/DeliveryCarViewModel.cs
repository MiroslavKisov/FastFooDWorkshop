namespace FastFoodWorkshop.ServiceModels.Manager
{
    using Common.WebConstants;
    using Common.CustomValidations;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DeliveryCarViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        [DataType(DataType.Text)]
        public string Model { get; set; }

        [Required]
        [Range(NumericConstants.MinimumNumberLimit, NumericConstants.MaximumNumberLimit, ErrorMessage = ErrorMessages.ValueCannotBeNegative)]
        public double Mileage { get; set; }

        [Required]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = StringConstants.DateTimeFormat, ApplyFormatInEditMode = true)]
        [DateRestrictToday(ErrorMessage = ErrorMessages.DateCannotBeAfterToday)]
        public DateTime ProductionDate { get; set; }

        [Required]
        [Range(NumericConstants.MinimumNumberLimit, NumericConstants.MaximumNumberLimit, ErrorMessage = ErrorMessages.ValueCannotBeNegative)]
        public double TankCapacity { get; set; }

        [Required]
        [Range(NumericConstants.MinimumNumberLimit, NumericConstants.MaximumNumberLimit, ErrorMessage = ErrorMessages.ValueCannotBeNegative)]
        public double ConsumptionPerMile { get; set; }

        public string RestaurantId { get; set; }

        public string RestaurantName { get; set; }
    }
}
