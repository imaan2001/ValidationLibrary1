using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationLibrary1.Core;

namespace ValidationLibrary1.Rules
{
    public class RangeRule : ValidationRuleBase
    {
        private readonly double _min;
        private readonly double _max;

        public RangeRule(double min, double max, string message = "{field} must be between {min} and {max}.")
            : base(message)
        {
            _min = min;
            _max = max;
        }

        public override void Validate(string fieldName, object value, ValidationResult result)
        {
            if (value == null)
                return;

            if (!double.TryParse(value.ToString(), out double number))
            {
                result.AddError(fieldName, $"{fieldName} must be a numeric value.");
                return;
            }

            if (number < _min || number > _max)
            {
                var msg = ErrorMessage
                    .Replace("{field}", fieldName)
                    .Replace("{min}", _min.ToString())
                    .Replace("{max}", _max.ToString());

                result.AddError(fieldName, msg);
            }
        }
    }
}
