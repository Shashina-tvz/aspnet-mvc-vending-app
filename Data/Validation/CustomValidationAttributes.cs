using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace VendingMachineApp.Data.Validation
{
    public class EmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true;
            var email = value.ToString();
            if (string.IsNullOrWhiteSpace(email)) return true;
            // Simple email regex
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }

    public class PhoneNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true;
            var phone = value.ToString();
            if (string.IsNullOrWhiteSpace(phone)) return true;
            // Accepts +, numbers, spaces, dashes, min 7 digits
            return Regex.IsMatch(phone, @"^[+]?([0-9\-\s]){7,}$");
        }
    }

    public class DateNotInFutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true;
            if (value is DateTime dt)
                return dt <= DateTime.Now;
            return true;
        }
    }

    public class GreaterThanZeroAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true;
            if (value is int i)
                return i > 0;
            if (value is decimal d)
                return d > 0;
            return true;
        }
    }

    public class ExpirationAfterManufactureAttribute : ValidationAttribute
    {
        private readonly string _manufactureProperty;
        public ExpirationAfterManufactureAttribute(string manufactureProperty)
        {
            _manufactureProperty = manufactureProperty;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var expiration = value as DateTime?;
            var manufactureProp = validationContext.ObjectType.GetProperty(_manufactureProperty);
            if (manufactureProp == null)
                return new ValidationResult($"Unknown property: {_manufactureProperty}");
            var manufacture = manufactureProp.GetValue(validationContext.ObjectInstance) as DateTime?;
            if (expiration.HasValue && manufacture.HasValue && expiration <= manufacture)
                return new ValidationResult("Expiration date must be after manufacture date.");
            return ValidationResult.Success;
        }
    }
}
