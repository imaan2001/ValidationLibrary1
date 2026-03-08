using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationLibrary1.Core;

namespace ValidationLibrary1.Rules
{
    public class RequiredRule : ValidationRuleBase
    {
        private readonly bool _trimWhitespace;

        public RequiredRule(string message = "The {field} field is required.", bool trimWhitespace = true)
            : base(message)
        {
            _trimWhitespace = trimWhitespace;
        }

        public override void Validate(string fieldName, object value, ValidationResult result)
        {
            if (value == null)
            {
                AddError(fieldName, result);
                return;
            }

            if (value is string str)
            {
                if (_trimWhitespace)
                    str = str.Trim();

                if (string.IsNullOrEmpty(str))
                    AddError(fieldName, result);

                return;
            }

            if (value is System.Collections.IEnumerable list && !(value is string))
            {
                var enumerator = list.GetEnumerator();
                if (!enumerator.MoveNext())
                    AddError(fieldName, result);
            }
        }

        private void AddError(string fieldName, ValidationResult result)
        {
            var msg = ErrorMessage.Replace("{field}", fieldName);
            result.AddError(fieldName, msg);
        }
    }
}
