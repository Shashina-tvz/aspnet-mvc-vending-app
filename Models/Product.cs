namespace VendingMachineApp.Models
{
    /// <summary>
    /// Complex class representing a product that can be stocked in vending machines
    /// 1-N relationship with ProductSlot
    /// </summary>
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public ProductCategory Category { get; set; }

        public string Description { get; set; } = string.Empty;

        public int ReorderThreshold { get; set; }

        public DateTime ManufactureDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        // Foreign Key
        public int SupplierId { get; set; }

        // Navigation Properties
        public virtual Supplier? Supplier { get; set; }

        public virtual ICollection<ProductSlot>? ProductSlots { get; set; } = new List<ProductSlot>();

        public virtual ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();

        public virtual ICollection<Transaction>? Transactions { get; set; } = new List<Transaction>();
    }
}
