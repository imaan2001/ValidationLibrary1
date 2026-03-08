using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationLibrary1.Core
{
    public interface IValidationRule
    {
        void Validate(string fieldName, object value, ValidationResult result);
    }
}
