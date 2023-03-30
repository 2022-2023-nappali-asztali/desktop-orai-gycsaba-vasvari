using KretaDesktop.Localization;
using KretaDesktop.Validation.ValidationRules;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Controls;

namespace KretaDesktop.Validation
{
    public class FullNameValidation : ValidationRule
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
                if (!rules.UppercaseLetterAfterSpace)
                    return new ValidationResult(false, localization.GetStringResource("validationLowercaseLetterAfterSpace"));
            }
            return new ValidationResult(true,"");
        }
    }
}
