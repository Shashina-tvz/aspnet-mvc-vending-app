using System.ComponentModel.DataAnnotations;
using VendingMachineApp.Data.Validation;

namespace VendingMachineApp.Data.Entities
{
    /// <summary>
    /// Complex class representing a product supplier
    /// 1-N relationship with Product
    /// </summary>
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Supplier name is required.")]
        [MaxLength(100, ErrorMessage = "Supplier name cannot exceed 100 characters.")]
        //[SupplierName(ErrorMessage = "Supplier name contains invalid characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
        //[PhoneNumber(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is required.")]
        [MaxLength(100, ErrorMessage = "Email address cannot exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        //[Address(ErrorMessage = "Address must contain at least 5 characters.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact person is required.")]
        [MaxLength(100, ErrorMessage = "Contact person name cannot exceed 100 characters.")]
        //[ContactPerson(ErrorMessage = "Contact person contains invalid characters.")]
        public string ContactPerson { get; set; } = string.Empty;

        [Required(ErrorMessage = "Registration date is required.")]
        public DateTime RegistrationDate { get; set; }

        // Navigation Properties
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();
    }
}