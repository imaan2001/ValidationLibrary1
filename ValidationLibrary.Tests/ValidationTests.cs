using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationLibrary1.Core;
using ValidationLibrary1.Rules;

namespace ValidationLibrary1.Tests
{

    public class ValidationTests
    {
        [Fact]
        public void RequiredRule_ShouldFail_WhenValueIsNull()
        {
            var rule = new RequiredRule("Field required");
            var result = new ValidationResult();

            rule.Validate("Username", null, result);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void MinLengthRule_ShouldFail_WhenTextTooShort()
        {
            var rule = new MinLengthRule(5);
            var result = new ValidationResult();

            rule.Validate("Username", "abc", result);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void MaxLengthRule_ShouldFail_WhenTextTooLong()
        {
            var rule = new MaxLengthRule(5);
            var result = new ValidationResult();

            rule.Validate("Username", "toolongtext", result);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void EmailRule_ShouldPass_ForValidEmail()
        {
            var rule = new EmailRule();
            var result = new ValidationResult();

            rule.Validate("Email", "user@gmail.com", result);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void RangeRule_ShouldFail_WhenOutsideRange()
        {
            var rule = new RangeRule(18, 60);
            var result = new ValidationResult();

            rule.Validate("Age", 70, result);

            Assert.False(result.IsValid);
        }
    }
}
