namespace FastFoodWorkshop.Common.CustomValidations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateRestrictBirthday : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime birthDay = Convert.ToDateTime(value);
            DateTime today = DateTime.UtcNow;

            return today.Year - birthDay.Year >= 18;
        }
    }
}
