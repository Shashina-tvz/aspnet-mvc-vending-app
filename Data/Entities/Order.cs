namespace VendingMachineApp.Data.Entities
{
    /// <summary>
    /// Class representing a purchase order from a supplier
    /// 1-N relationship with Supplier
    /// N-N relationship with Product through OrderItem
    /// </summary>
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        // Foreign Key
        public int SupplierId { get; set; }
        // Navigation Properties
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
    }
}