using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationLibrary1.Core
{
    public class Validator
    {
        private readonly Dictionary<string, FieldValidator> _fields
            = new();

        public FieldValidator Field(string fieldName)
        {
            if (!_fields.ContainsKey(fieldName))
            {
                _fields[fieldName] = new FieldValidator();
            }

            return _fields[fieldName];
        }

        public ValidationResult Validate(Dictionary<string, object> data)
        {
            var result = new ValidationResult();

            foreach (var field in _fields)
            {
                data.TryGetValue(field.Key, out var value);

                field.Value.Validate(field.Key, value, result);
            }

            return result;
        }
    }
}
