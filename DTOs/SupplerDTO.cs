namespace VendingMachineApp.DTOs
{
    public class SupplierDTO
    {
        public int SupplierId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
    }
}