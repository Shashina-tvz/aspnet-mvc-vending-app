namespace VendingMachineApp.Models
{
    /// <summary>
    /// Join class representing items in an order
    /// Creates N-N relationship between Order and Product
    /// </summary>
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal SubTotal { get; set; }

        // Foreign Keys
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        // Navigation Properties
        public virtual Order? Order { get; set; }

        public virtual Product? Product { get; set; }
    }
}
