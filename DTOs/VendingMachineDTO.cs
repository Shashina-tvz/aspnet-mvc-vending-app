using VendingMachineApp.Data.Entities;

namespace VendingMachineApp.DTOs
{
    public class VendingMachineDTO
    {
        public int MachineId { get; set; }

        public int MachineNumber { get; set; }

        public string Address { get; set; } = string.Empty;        

        public MachineStatus Status { get; set; }

    }
}