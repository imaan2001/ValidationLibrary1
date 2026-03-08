using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ValidationLibrary1.Core;

namespace ValidationLibrary1.Rules
{
    public class EmailRule : ValidationRuleBase
    {
        private static readonly Regex EmailRegex =
            new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);

        public EmailRule(string message = "{field} is not a valid email.")
            : base(message)
        {
        }

        public override void Validate(string fieldName, object value, ValidationResult result)
        {
            if (value == null)
                return;

            var text = value.ToString();

            if (string.IsNullOrEmpty(text))
                return;

            if (!EmailRegex.IsMatch(text))
            {
                var msg = ErrorMessage.Replace("{field}", fieldName);
                result.AddError(fieldName, msg);
            }
        }
    }
}
