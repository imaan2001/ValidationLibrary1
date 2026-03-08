# ValidationLibrary1

A flexible and reusable validation library built in **C# / .NET 8**.

Developed as a technical exercise demonstrating clean architecture, the Open/Closed Principle, and a fluent API for configuring field validation rules.

---

## Features

- Field-level validation with grouped error messages
- Customisable error messages with runtime placeholders (`{field}`, `{min}`, `{max}`)
- Fluent API for readable, chainable rule configuration
- Easily extensible — new rules require no changes to existing code
- Supports both string and collection types in `RequiredRule`

---

## Project Structure

```
ValidationLibrary1/
  Core/
    IValidationRule.cs          ← contract all rules must implement
    ValidationRuleBase.cs       ← shared base (error message handling)
    ValidationResult.cs         ← stores errors grouped by field name
    FieldValidator.cs           ← manages rules for a single field
    Validator.cs                ← main engine, orchestrates all fields
  Rules/
    RequiredRule.cs
    MinLengthRule.cs
    MaxLengthRule.cs
    EmailRule.cs
    RangeRule.cs
  Extensions/
    FieldValidatorExtensions.cs ← fluent API helpers
  Tests/                        ← placeholder, ready for xUnit
  Program.cs                    ← console demo entry point
```

---

## Validation Rules

| Rule | Description |
|---|---|
| `RequiredRule` | Fails if the field is null or blank (trims whitespace by default) |
| `MinLengthRule` | Fails if value length is below the minimum |
| `MaxLengthRule` | Fails if value length exceeds the maximum |
| `EmailRule` | Validates email format using a regular expression |
| `RangeRule` | Validates a numeric value falls within [min, max] |

---

## Usage

```csharp
var validator = new Validator();

validator.Field("Username")
    .Required("Username is required")
    .MinLength(5)
    .MaxLength(15);

validator.Field("Email")
    .Required("Email is required")
    .AddRule(new EmailRule("Invalid email format"));

validator.Field("Age")
    .AddRule(new RangeRule(18, 60, "Age must be between 18 and 60"));

var data = new Dictionary<string, object>
{
    { "Username", "Al" },
    { "Email",    "notanemail" },
    { "Age",      15.0 }
};

var result = validator.Validate(data);

if (!result.IsValid)
{
    foreach (var field in result.Errors)
        foreach (var error in field.Value)
            Console.WriteLine($"{field.Key}: {error}");
}
```

**Output:**

```
Validation Failed
Username: Minimum length is 5
Email: Invalid email format
Age: Age must be between 18 and 60
```

---

## Running the Console Demo

```bash
git clone  https://github.com/imaan2001/ValidationLibrary1.git
cd ValidationLibrary1
dotnet run --project ValidationLibrary1
```

You will be prompted to enter a Username, Email, and Age. The program validates the input and prints a pass or fail result.

---

## Extending the Library

Adding a new rule requires only one new class. No existing code needs to change.

```csharp
public class PhoneRule : ValidationRuleBase
{
    private static readonly Regex PhoneRegex = new Regex(@"^\+?[0-9\s\-]{7,15}$");

    public PhoneRule(string message = "{field} must be a valid phone number.")
        : base(message) { }

    public override void Validate(string fieldName, object value, ValidationResult result)
    {
        if (value == null) return;
        var text = value.ToString();
        if (!PhoneRegex.IsMatch(text))
            result.AddError(fieldName, ErrorMessage.Replace("{field}", fieldName));
    }
}
```

Then register it:

```csharp
validator.Field("Phone").AddRule(new PhoneRule());
```

---

## Design Principles

- **Open/Closed Principle** — the library is open for extension, closed for modification
- **Single Responsibility** — each rule class does exactly one thing
- **Separation of Concerns** — `Validator` orchestrates without knowing rule details
- **Fluent API** — extension methods on `FieldValidator` allow clean, readable setup
- **Consistent chaining** — `AddRule` returns `this`, so direct calls and extension methods chain equally

---

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download)

---

## Author

**Muhammed Imaan**  
Technical Exercise Submission — February 2026
