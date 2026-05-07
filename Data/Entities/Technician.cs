namespace VendingMachineApp.Data.Entities
{
    /// <summary>
    /// Class representing a technician who performs maintenance
    /// 1-N relationship with MaintenanceLog
    /// </summary>
    using System.ComponentModel.DataAnnotations;
    public class Technician
    {
        
        [Key]
        public int TechnicianId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string LicenseNumber { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        // Navigation Properties
        public virtual ICollection<MaintenanceLog>? MaintenanceLogs { get; set; } = new List<MaintenanceLog>();
    }
}