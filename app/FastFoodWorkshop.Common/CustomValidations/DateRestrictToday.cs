namespace FastFoodWorkshop.Common.CustomValidations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateRestrictToday : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime today = Convert.ToDateTime(value);
            return today <= DateTime.UtcNow;
        }
    }
}
