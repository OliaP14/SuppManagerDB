using System.ComponentModel.DataAnnotations;

namespace SuppManagerDB.WebApp.App.Validation
{
    public class NotEmptyStringAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            if (value is string text && !string.IsNullOrWhiteSpace(text))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Field cannot be empty");
        }
    }
}
