using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationLibrary1.Core
{
    public class ValidationResult
    {
        public bool IsValid => Errors.Count == 0;

        public Dictionary<string, List<string>> Errors { get; }
            = new Dictionary<string, List<string>>();

        public void AddError(string field, string message)
        {
            if (!Errors.ContainsKey(field))
            {
                Errors[field] = new List<string>();
            }

            Errors[field].Add(message);
        }
    }
}
