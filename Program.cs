using System;
using System.Collections.Generic;
using ValidationLibrary1.Core;
using ValidationLibrary1.Extensions;
using ValidationLibrary1.Rules;
using ValidationLibrary1.Rules;
namespace ValidationLibrary1
{

    class Program
    {
        static void Main()
        {
            // Create validator
            var validator = new Validator();

            // Configure Username rules
            validator.Field("Username")
                    .Required("Username is required")
                    .MinLength(5)
                    .MaxLength(15);

            // Configure Email rules
            validator.Field("Email")
                    .Required("Email is required")
                    .AddRule(new EmailRule("Invalid email format"));

            // Configure Age rules
            validator.Field("Age")
                    .AddRule(new RangeRule(18, 60, "Age must be between 18 and 60"));

            // Collect user input
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Age: ");
            string ageText = Console.ReadLine();

            // Convert Age safely
            object ageValue = null;
            if (double.TryParse(ageText, out double age))
            {
                ageValue = age;
            }

            // Prepare input dictionary
            var data = new Dictionary<string, object>
        {
            { "Username", username },
            { "Email", email },
            { "Age", ageValue }
        };

            // Run validation
            var result = validator.Validate(data);

            Console.WriteLine();
            Console.WriteLine("Validation Result:");
            Console.WriteLine("-------------------");

            if (result.IsValid)
            {
                Console.WriteLine("Validation Passed ");
            }
            else
            {
                Console.WriteLine("Validation Failed ");

                foreach (var field in result.Errors)
                {
                    foreach (var error in field.Value)
                    {
                        Console.WriteLine($"{field.Key}: {error}");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}