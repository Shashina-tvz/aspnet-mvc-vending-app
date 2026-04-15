using Microsoft.AspNetCore.Mvc;
using VendingMachineApp.Data.Entities;
using VendingMachineApp.Data.Repositories;
using System.Linq;

namespace VendingMachineApp.Controllers
{
    public class TechnicianController : Controller
    {
        private readonly MockTechnicianRepository _technicianRepo;

        public TechnicianController(MockTechnicianRepository technicianRepo)
        {
            _technicianRepo = technicianRepo;
        }

        public IActionResult Index()
        {
            var technicians = _technicianRepo.GetAll();
            return View(technicians);
        }

        public IActionResult Details(int id)
        {
            var technician = _technicianRepo.GetById(id);
            if (technician == null) return NotFound();
            return View(technician);
        }
    }
}
