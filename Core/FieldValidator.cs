using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationLibrary1.Core
{
    public class FieldValidator
    {
        private readonly List<IValidationRule> _rules = new();

        public FieldValidator AddRule(IValidationRule rule)
        {
            _rules.Add(rule);
            return this;
        }

        public void Validate(string fieldName, object value, ValidationResult result)
        {
            foreach (var rule in _rules)
            {
                rule.Validate(fieldName, value, result);
            }
        }
    }

}
