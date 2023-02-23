using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaDesktop.Validation.ValidationRules
{
    public class NameValidationRules
    {
        private readonly string _nameToValidate;

        public NameValidationRules(string nameToValidate)
        {
            _nameToValidate = nameToValidate;
        }

        public bool IsNameShort => _nameToValidate.Length<3 ? true : false;

        public bool IsFirstLetterUppercase 
            => _nameToValidate.Any() && char.IsUpper(_nameToValidate.ElementAt(0)) ? true : false;

        public bool IsOtherLetterLowercase
            => _nameToValidate.Substring(1).All(character => char.IsLower(character)) ? true : false;

        public bool IsOnlyLetters => _nameToValidate.Any(charachter => char.IsLetter(charachter)) ? true : false;

    }
}
