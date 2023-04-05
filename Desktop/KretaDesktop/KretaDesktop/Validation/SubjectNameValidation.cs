using KretaDesktop.Localization;
using KretaDesktop.Validation.ValidationRules;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Controls;

namespace KretaDesktop.Validation
{
    public class SubjectNameValidation : ValidationRule
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
                if (!rules.IsOtherLetterLowercaseOrSpace)
                    return new ValidationResult(false, localization.GetStringResource("validationOtherLetterNotLowercase"));
            }
            return new ValidationResult(true,"");
        }
    }
}
