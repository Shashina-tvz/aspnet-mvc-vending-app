using Microsoft.AspNetCore.Mvc;
using VendingMachineApp.Data.Entities;
using VendingMachineApp.Data.Repositories;
using System.Linq;

namespace VendingMachineApp.Controllers
{
    public class MaintenanceLogController : Controller
    {
        private readonly MockMaintenanceLogRepository _logRepo;
        private readonly MockVendingMachineRepository _machineRepo;

        public MaintenanceLogController(MockMaintenanceLogRepository logRepo, MockVendingMachineRepository machineRepo)
        {
            _logRepo = logRepo;
            _machineRepo = machineRepo;
        }

        public IActionResult Index()
        {
            var logs = _logRepo.GetAll();
            return View(logs);
        }

        public IActionResult Details(int id)
        {
            var log = _logRepo.GetById(id);
            if (log == null) return NotFound();
            log.VendingMachine = _machineRepo.GetById(log.MachineId);
            return View(log);
        }
    }
}
