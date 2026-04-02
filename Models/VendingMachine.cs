namespace VendingMachineApp.Models
{
    /// <summary>
    /// Complex class representing a vending machine
    /// 1-N relationship with ProductSlot and Transaction
    /// </summary>
    public class VendingMachine
    {
        public int MachineId { get; set; }

        public int MachineNumber { get; set; }

        public string Address { get; set; } = string.Empty;

        public int Capacity { get; set; }

        public MachineStatus Status { get; set; }

        public DateTime ManufacturedDate { get; set; }

        public DateTime LastMaintenanceDate { get; set; }

        public decimal CurrentBalance { get; set; }

        // Navigation Properties
        public virtual ICollection<ProductSlot>? ProductSlots { get; set; } = new List<ProductSlot>();

        public virtual ICollection<Transaction>? Transactions { get; set; } = new List<Transaction>();

        public virtual ICollection<MaintenanceLog>? MaintenanceLogs { get; set; } = new List<MaintenanceLog>();
    }
}
