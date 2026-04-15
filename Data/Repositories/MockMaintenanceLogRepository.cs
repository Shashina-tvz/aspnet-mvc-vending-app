using VendingMachineApp.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineApp.Data.Repositories
{
    public class MockMaintenanceLogRepository
    {
        private List<MaintenanceLog> logs = new List<MaintenanceLog>
        {
            new MaintenanceLog { MaintenanceLogId = 1, Description = "Zamjena motora", MaintenanceDate = DateTime.Now.AddDays(-30), Cost = 120, Status = "Završeno", MachineId = 1, TechnicianId = 1 },
            new MaintenanceLog { MaintenanceLogId = 2, Description = "Čišćenje aparata", MaintenanceDate = DateTime.Now.AddDays(-20), Cost = 30, Status = "Završeno", MachineId = 2, TechnicianId = 2 },
            new MaintenanceLog { MaintenanceLogId = 3, Description = "Popravka displeja", MaintenanceDate = DateTime.Now.AddDays(-10), Cost = 80, Status = "U toku", MachineId = 1, TechnicianId = 1 },
            new MaintenanceLog { MaintenanceLogId = 4, Description = "Zamjena brave", MaintenanceDate = DateTime.Now.AddDays(-5), Cost = 50, Status = "Završeno", MachineId = 3, TechnicianId = 3 }
        };
        public List<MaintenanceLog> GetAll() => logs;
        public List<MaintenanceLog> GetByMachineId(int machineId) => logs.Where(x => x.MachineId == machineId).ToList();

        public MaintenanceLog GetById(int id)
        {
            return logs.FirstOrDefault(x => x.MaintenanceLogId == id) ?? new MaintenanceLog();
        }
    }
}