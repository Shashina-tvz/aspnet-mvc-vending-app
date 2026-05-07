namespace VendingMachineApp.Data.Entities
{
    /// <summary>
    /// Class representing a purchase order from a supplier
    /// 1-N relationship with Supplier
    /// N-N relationship with Product through OrderItem
    /// </summary>
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Order
    {
        
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;
        // Foreign Key
        [ForeignKey("Supplier")]
        [Required]
        public int SupplierId { get; set; }
        // Navigation Properties
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
    }
}