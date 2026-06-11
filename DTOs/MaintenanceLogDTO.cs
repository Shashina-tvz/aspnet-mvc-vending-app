namespace VendingMachineApp.DTOs
{
    public class MaintenanceLogDTO
    {
        public int MaintenanceLogId { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTime MaintenanceDate { get; set; }

        public decimal Cost { get; set; }

        public string Status { get; set; } = string.Empty;

        public TechnicianDTO? Technician { get; set; }

        public VendingMachineDTO? VendingMachine { get; set; }
    }
}