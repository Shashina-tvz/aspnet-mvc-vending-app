namespace VendingMachineApp.Data.Entities
{
    /// <summary>
    /// Class representing maintenance logs for vending machines
    /// 1-N relationship with VendingMachine
    /// N-1 relationship with Technician
    /// </summary>
    public class MaintenanceLog
    {
        public int MaintenanceLogId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime MaintenanceDate { get; set; }
        public decimal Cost { get; set; }
        public string Status { get; set; } = string.Empty;
        // Foreign Keys
        public int MachineId { get; set; }
        public int TechnicianId { get; set; }
        // Navigation Properties
        public virtual VendingMachine? VendingMachine { get; set; }
        public virtual Technician? Technician { get; set; }
    }
}