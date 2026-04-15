namespace VendingMachineApp.Data.Entities
{
    /// <summary>
    /// Complex class representing a product supplier
    /// 1-N relationship with Product
    /// </summary>
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        // Navigation Properties
        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
        public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();
    }
}