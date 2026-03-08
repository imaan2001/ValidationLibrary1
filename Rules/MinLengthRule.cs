using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationLibrary1.Core;

namespace ValidationLibrary1.Rules
{

    public class MinLengthRule : ValidationRuleBase
    {
        private readonly int _minLength;

        public MinLengthRule(int minLength, string message = "{field} must be at least {min} character(s) long.")
            : base(message)
        {
            _minLength = minLength;
        }

        public override void Validate(string fieldName, object value, ValidationResult result)
        {
            if (value == null)
                return;

            var text = value.ToString();

            if (string.IsNullOrEmpty(text))
                return;

            if (text.Length < _minLength)
            {
                var msg = ErrorMessage
                    .Replace("{field}", fieldName)
                    .Replace("{min}", _minLength.ToString());

                result.AddError(fieldName, msg);
            }
        }
    }
}
