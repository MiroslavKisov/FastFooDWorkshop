namespace FastFoodWorkshop.ServiceModels.Manager
{
    using Common.WebConstants;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class MenuViewModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(NumericConstants.MaximumTextFieldLength, ErrorMessage = ErrorMessages.InputMaxLength)]
        public string Name { get; set; }

        [Range(NumericConstants.MinimumDiscountLimit, NumericConstants.MaximumDiscountLimit)]
        [Required]
        public decimal Discount { get; set; }

        public int? OrderId { get; set; }

        public ICollection<ProductViewModel> Products { get; set; }

        public double TotalProteins
        {
            get
            {
                if (this.Products != null)
                {
                    return this.Products.Sum(e => e.ProteinsQuantity);
                }

                return 0.0d;
            }
        }

        public double TotalCarbohydrates
        {
            get
            {
                if (this.Products != null)
                {
                    return this.Products.Sum(e => e.CarbohydratesQuantity);
                }

                return 0.0d;
            }
        }

        public double TotalFat
        {
            get
            {
                if (this.Products != null)
                {
                    return this.Products.Sum(e => e.FatQuantity);
                }

                return 0.0d;
            }
        }

        public double TotalCalories
        {
            get
            {
                if (this.Products != null)
                {
                    return this.Products.Sum(e => e.TotalCalories);
                }

                return 0.0d;
            }
        }

        public decimal TotalPrice
        {
            get
            {
                if (this.Products != null)
                {
                    return this.Products.Sum(e => e.Price) * (NumericConstants.DiscountConstant - this.Discount);
                }

                return 0.0m;
            }
        }
    }
}
