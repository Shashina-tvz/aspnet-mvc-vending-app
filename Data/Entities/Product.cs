namespace VendingMachineApp.Data.Entities
{
    /// <summary>
    /// Complex class representing a product that can be stocked in vending machines
    /// 1-N relationship with ProductSlot
    /// </summary>
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using VendingMachineApp.Data.Validation;
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [MaxLength(100, ErrorMessage = "Name can't exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Product category is required.")]
        public ProductCategory? Category { get; set; }

        [MaxLength(500, ErrorMessage = "Description can't exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Reorder threshold is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Reorder threshold must be greater than 0.")]
        public int? ReorderThreshold { get; set; }

        [Required(ErrorMessage = "Manufacture date is required.")]
        public DateTime ManufactureDate { get; set; }

        [Required(ErrorMessage = "Expiration date is required.")]
        [ExpirationAfterManufacture("ManufactureDate")]
        public DateTime ExpirationDate { get; set; }
        [ExpirationAfterManufacture("ManufactureDate")]


        // Foreign Key
        [ForeignKey("Supplier")]
        [Required(ErrorMessage = "Please select a supplier.")]        
        public int? SupplierId { get; set; }
        // Navigation Properties
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<ProductSlot>? ProductSlots { get; set; } = new List<ProductSlot>();
        public virtual ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<Transaction>? Transactions { get; set; } = new List<Transaction>();
    }
}