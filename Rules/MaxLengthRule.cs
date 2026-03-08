using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationLibrary1.Core;

namespace ValidationLibrary1.Rules
{
    public class MaxLengthRule : ValidationRuleBase
    {
        private readonly int _maxLength;

        public MaxLengthRule(int maxLength, string message = "{field} must not exceed {max} character(s).")
            : base(message)
        {
            _maxLength = maxLength;
        }

        public override void Validate(string fieldName, object value, ValidationResult result)
        {
            if (value == null)
                return;

            var text = value.ToString();

            if (text.Length > _maxLength)
            {
                var msg = ErrorMessage
                    .Replace("{field}", fieldName)
                    .Replace("{max}", _maxLength.ToString());

                result.AddError(fieldName, msg);
            }
        }
    }
}
