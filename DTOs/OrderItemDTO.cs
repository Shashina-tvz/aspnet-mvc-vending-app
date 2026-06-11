namespace VendingMachineApp.DTOs
{
    public class OrderItemDTO
    {
        public int OrderItemId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal SubTotal { get; set; }

        // 🔥 PRODUCT UGNJEŽĐEN
        public ProductDTO? Product { get; set; }
    }
}