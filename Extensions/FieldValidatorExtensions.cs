using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationLibrary1.Core;
using ValidationLibrary1.Rules;

namespace ValidationLibrary1.Extensions
{
    public static class FieldValidatorExtensions
    {
        public static FieldValidator Required(this FieldValidator validator, string message = "Field is required")
        {
            validator.AddRule(new RequiredRule(message));
            return validator;
        }

        public static FieldValidator MinLength(this FieldValidator validator, int length, string message = null)
        {
            message ??= $"Minimum length is {length}";
            validator.AddRule(new MinLengthRule(length, message));
            return validator;
        }

        public static FieldValidator MaxLength(this FieldValidator validator, int length, string message = null)
        {
            message ??= $"Maximum length is {length}";
            validator.AddRule(new MaxLengthRule(length, message));
            return validator;
        }
        public static FieldValidator Email(this FieldValidator validator)
        {
            validator.AddRule(new EmailRule());
            return validator;
        }

        public static FieldValidator Range(this FieldValidator validator, double min, double max)
        {
            validator.AddRule(new RangeRule(min, max));
            return validator;
        }
    }
}
