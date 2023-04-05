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

        public bool IsOtherLetterLowercaseOrSpace
            => _nameToValidate.Substring(1).All(character => char.IsLower(character) || char.IsWhiteSpace(character)) ? true : false;

        public bool IsOnlyLetters => _nameToValidate.Any(charachter => char.IsLetter(charachter)) ? true : false;

        public bool UppercaseLetterAfterSpace => !HaveLowercaseLetterAfterSpaceOrUpperAfterFirsUpper(_nameToValidate);

        private  bool HaveLowercaseLetterAfterSpaceOrUpperAfterFirsUpper(string name)
        {
            List<char> letters = new List<char>(name);
            for(int i=0;i<letters.Count;i++)
            {
                if (char.IsWhiteSpace(letters.ElementAt(i)))
                {
                    if ((i < letters.Count - 1) && (char.IsLower(letters.ElementAt(i + 1))))
                        return true;
                }
                if (char.IsUpper(letters.ElementAt(i)))
                {
                    if ((i>0) && (!char.IsWhiteSpace(letters.ElementAt(i-1))))
                        return true;
                }
            }
            return false;
        }


    }

}
