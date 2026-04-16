using Microsoft.AspNetCore.Mvc;
using VendingMachineApp.Data.Entities;
using VendingMachineApp.Data.Repositories;
using System.Linq;

namespace VendingMachineApp.Controllers
{
    public class VendingMachineController : Controller
    {
        private readonly MockVendingMachineRepository _machineRepo;
        private readonly MockProductRepository _productRepo;

        public VendingMachineController(MockVendingMachineRepository machineRepo, MockProductRepository productRepo)
        {
            _machineRepo = machineRepo;
            _productRepo = productRepo;
        }

        public IActionResult Index(int? machineNumber)
        {
            if (machineNumber.HasValue)
            {
                var machine = _machineRepo.GetByMachineNumber(machineNumber.Value);
                if (machine == null)
                    return NotFound();
                return View(new List<VendingMachine> { machine });
            }
            var machines = _machineRepo.GetAll();
            return View(machines);
        }

        public IActionResult Details(int id)
        {
            var machine = _machineRepo.GetById(id);
            if (machine == null)
                return NotFound();

            if (machine.ProductSlots != null)
            {
                foreach (var slot in machine.ProductSlots)
                {
                    slot.Product = _productRepo.GetById(slot.ProductId);
                }
            }

            return View(machine);
        }
    }
}
