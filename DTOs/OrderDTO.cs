namespace VendingMachineApp.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = string.Empty;

        // 🔥 UGNJEŽĐENI SUPPLIER
        public SupplierDTO? Supplier { get; set; }

        // 🔥 LISTA UGNJEŽĐENIH ITEMS
        public List<OrderItemDTO> OrderItems { get; set; } = new();
    }
}