namespace VendingMachineApp.Data.Entities
{
    /// <summary>
    /// Class representing a technician who performs maintenance
    /// 1-N relationship with MaintenanceLog
    /// </summary>
    public class Technician
    {
        public int TechnicianId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        // Navigation Properties
        public virtual ICollection<MaintenanceLog>? MaintenanceLogs { get; set; } = new List<MaintenanceLog>();
    }
}