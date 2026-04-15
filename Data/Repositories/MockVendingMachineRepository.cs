using VendingMachineApp.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineApp.Data.Repositories
{
    public class MockVendingMachineRepository
    {
        private List<VendingMachine> machines = new List<VendingMachine>
        {
            new VendingMachine { MachineId = 1, MachineNumber = 101, Address = "Adresa 1", Capacity = 50, Status = MachineStatus.Active, ManufacturedDate = DateTime.Now.AddYears(-2), LastMaintenanceDate = DateTime.Now.AddMonths(-1), CurrentBalance = 100.0m },
            new VendingMachine { MachineId = 2, MachineNumber = 102, Address = "Adresa 2", Capacity = 60, Status = MachineStatus.Maintenance, ManufacturedDate = DateTime.Now.AddYears(-3), LastMaintenanceDate = DateTime.Now.AddMonths(-2), CurrentBalance = 200.0m },
            new VendingMachine { MachineId = 3, MachineNumber = 103, Address = "Adresa 3", Capacity = 40, Status = MachineStatus.OutOfService, ManufacturedDate = DateTime.Now.AddYears(-1), LastMaintenanceDate = DateTime.Now.AddMonths(-3), CurrentBalance = 50.0m },
            new VendingMachine { MachineId = 4, MachineNumber = 104, Address = "Adresa 4", Capacity = 70, Status = MachineStatus.Active, ManufacturedDate = DateTime.Now.AddYears(-4), LastMaintenanceDate = DateTime.Now.AddMonths(-4), CurrentBalance = 300.0m },
            new VendingMachine { MachineId = 5, MachineNumber = 105, Address = "Adresa 5", Capacity = 55, Status = MachineStatus.Inactive, ManufacturedDate = DateTime.Now.AddYears(-5), LastMaintenanceDate = DateTime.Now.AddMonths(-5), CurrentBalance = 0.0m },
            new VendingMachine { MachineId = 6, MachineNumber = 106, Address = "Adresa 6", Capacity = 80, Status = MachineStatus.Active, ManufacturedDate = DateTime.Now.AddYears(-6), LastMaintenanceDate = DateTime.Now.AddMonths(-6), CurrentBalance = 500.0m }
        };
        public List<VendingMachine> GetAll() => machines;
        public VendingMachine GetByMachineNumber(int machineNumber) => machines.FirstOrDefault(x => x.MachineNumber == machineNumber)?? new VendingMachine();

        public VendingMachine GetById(int id)
        {
            return machines.FirstOrDefault(x => x.MachineId == id) ?? new VendingMachine();
        }
    }
}