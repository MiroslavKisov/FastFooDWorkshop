namespace FastFoodWorkshop.Common.WebConstants
{
    public class ErrorMessages
    {
        public const string InternalServerError = "Internal server error";

        public const string EndDateMustBeGraterThan = "End date must be grater than Start date";

        public const string ValueMustImplementIComparable = "Value has not implemented IComparable interface";

        public const string ComparisonPropertyNotFound = "Comparison property with this name not found";

        public const string TypeOfFieldsNotSame = "The types of the fields to compare are not the same.";

        public const string InvalidEntry = "Invalid entry";

        public const string DateCannotBeAfterToday = "The date must be today or before that";

        public const string PasswordsDoNotMatch = "The password and confirmation password do not match.";

        public const string NotOldEnough = "You must be at least 18 years old to apply for job";

        public const string InputMaxLength = "Input cannot exceed 100 symbols";

        public const string TextAreaMaxLength = "Text length must be between 50 and 3000 symbols!";

        public const string FileIsTooBig = "File size must be less than 1MB";

        public const string UnableToLoadUser = "Unable to load user";

        public const string InvalidLoginAttempt = "Invalid login attempt";

        public const string RoleDoesNotExist = "Role does not exist";

        public const string UsernameAlreadyExists = "Username already exists";

        public const string EmailAlreadyExist = "Email already exists";

        public const string ProductAlreadyExist = "Product with that name already exist";

        public const string RestaurantAlreadyExist = "Restaurant with that name already exist";

        public const string ValueCannotBeNegative = "Value cannot be negative";

        public const string JoinUsFormIsNotFilled = "You must fill this form first and then you can proceed";

        public const string AlreadyAppliedWithEmail = "You have applied with that email";

        public const string UserWithEmailDoesNotExist = "User with email address {0} does not exist";

        public const string DiscountOutOffLimit = "Discount must be between 0 and 0.99";

        public const string MenuDoesNotExist = "Menu with Id {0} does not exist";

        public const string ProductDoesNotExist = "Product with Id {0} does not exist";

        public const string ProductAlreadyDetached = "Product with Id {0} was already detached";

        public const string CarDoesNotExist = "Car with Id {0} does not exist";

        public const string CarAlreadyDetached = "Car with Id {0} was already detached";

        public const string RestaurantDoesNotExist = "Restaurant with Id {0} does not exist";

        public const string CategoryDoesNotExist = "Category with Id {0} does not exist";
    }
}
