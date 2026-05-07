namespace VendingMachineApp.Data.Entities
{
    /// <summary>
    /// Complex class representing a product that can be stocked in vending machines
    /// 1-N relationship with ProductSlot
    /// </summary>
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Required]
        public ProductCategory Category { get; set; }
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        public int ReorderThreshold { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        // Foreign Key
        [ForeignKey("Supplier")]
        [Required]
        public int SupplierId { get; set; }
        // Navigation Properties
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<ProductSlot>? ProductSlots { get; set; } = new List<ProductSlot>();
        public virtual ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<Transaction>? Transactions { get; set; } = new List<Transaction>();
    }
}