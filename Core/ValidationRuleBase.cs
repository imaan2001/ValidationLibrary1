using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationLibrary1.Core
{
    public abstract class ValidationRuleBase : IValidationRule
    {
        protected readonly string ErrorMessage;

        protected ValidationRuleBase(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public abstract void Validate(string fieldName, object value, ValidationResult result);
    }

}
