using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace VendingMachineApp.Data.Validation
{
    public class SupplierNameAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true;
            var name = value.ToString();
            if (string.IsNullOrWhiteSpace(name)) return false;
            // Only letters, spaces, dots, dashes, apostrophes, min 2 chars
            return Regex.IsMatch(name, @"^[A-Za-zČčĆćŽžŠšĐđ\-'. ]{2,}$");
        }
    }

    public class ContactPersonAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true;
            var name = value.ToString();
            if (string.IsNullOrWhiteSpace(name)) return false;
            // Only letters, spaces, dots, dashes, apostrophes, min 2 chars
            return Regex.IsMatch(name, @"^[A-Za-zČčĆćŽžŠšĐđ\-'. ]{2,}$");
        }
    }

    public class AddressAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true;
            var address = value.ToString();
            if (string.IsNullOrWhiteSpace(address)) return false;
            // At least 5 chars
            return address.Length >= 5;
        }
    }
}
