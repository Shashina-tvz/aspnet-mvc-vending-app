using Microsoft.AspNetCore.Mvc;
using VendingMachineApp.Data.Entities;
using VendingMachineApp.Data.Repositories;
using System.Linq;

namespace VendingMachineApp.Controllers
{
    public class SupplierController : Controller
    {
        private readonly MockSupplierRepository _supplierRepo;

        public SupplierController(MockSupplierRepository supplierRepo)
        {
            _supplierRepo = supplierRepo;
        }

       public IActionResult Index(string name)
        {
            var suppliers = string.IsNullOrEmpty(name)
                ? _supplierRepo.GetAll()
                : _supplierRepo.GetByName(name);

            return View(suppliers);
        }

        public IActionResult Details(int id)
        {
            var supplier = _supplierRepo.GetById(id);
            if (supplier == null) return NotFound();
            return View(supplier);
        }
    }
}
