namespace VendingMachineApp.Data.Entities
{
    /// <summary>
    /// Join class representing items in an order
    /// Creates N-N relationship between Order and Product
    /// </summary>
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        [VendingMachineApp.Data.Validation.GreaterThanZero]
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }
        // Foreign Keys
        [ForeignKey("Order")]
        [Required]
        public int OrderId { get; set; }
        [ForeignKey("Product")]
        [Required]
        public int ProductId { get; set; }
        // Navigation Properties
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}