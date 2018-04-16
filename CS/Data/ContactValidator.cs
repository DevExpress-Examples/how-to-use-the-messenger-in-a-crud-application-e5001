using System.ComponentModel.DataAnnotations;

namespace DataAnnotationAttributes.Model {
    public class ContactValidator {
        public static ValidationResult ValidateString(object value) {
            if(value == null || value.ToString().Length > 25)
                return new ValidationResult("Value is too long");
            return ValidationResult.Success;
        }
    }
}
