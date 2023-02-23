using KretaDesktop.Localization;
using KretaDesktop.Validation.ValidationRules;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Controls;

namespace KretaDesktop.Validation
{
    public class NameValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string nameToValidate)
            {
                NameValidationRules rules = new NameValidationRules(nameToValidate);
                ProjectLocalization localization=new ProjectLocalization();

                if (rules.IsNameShort)                                    
                    return new ValidationResult(false, localization.GetStringResource("validationNameIsShort"));
                if (!rules.IsFirstLetterUppercase)
                    return new ValidationResult(false, localization.GetStringResource("validationNameFirstLetterNotUppercase"));
                if (!rules.IsOtherLetterLowercase)
                    return new ValidationResult(false, localization.GetStringResource("validationOtherLetterNotLowercase"));
                if (!rules.IsOnlyLetters)
                    return new ValidationResult(false, localization.GetStringResource("validationNameOnlyLetters"));
            }

            return new ValidationResult(true,"");
        }
    }
}
