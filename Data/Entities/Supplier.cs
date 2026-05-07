namespace VendingMachineApp.Data.Entities
{
    /// <summary>
    /// Complex class representing a product supplier
    /// 1-N relationship with Product
    /// </summary>
    using System.ComponentModel.DataAnnotations;
    public class Supplier
    {
        
        [Key]
        public int SupplierId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string ContactPerson { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        // Navigation Properties
        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
        public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();
    }
}