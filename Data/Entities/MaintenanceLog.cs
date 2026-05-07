namespace VendingMachineApp.Data.Entities
{
    /// <summary>
    /// Class representing maintenance logs for vending machines
    /// 1-N relationship with VendingMachine
    /// N-1 relationship with Technician
    /// </summary>
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class MaintenanceLog
    {
        [Key]
        public int MaintenanceLogId { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        public DateTime MaintenanceDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;
        // Foreign Keys
        [ForeignKey("VendingMachine")]
        public int MachineId { get; set; }
        [ForeignKey("Technician")]
        public int TechnicianId { get; set; }
        // Navigation Properties
        public virtual VendingMachine? VendingMachine { get; set; }
        public virtual Technician? Technician { get; set; }
    }
}