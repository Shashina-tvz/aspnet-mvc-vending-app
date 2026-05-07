using Microsoft.AspNetCore.Mvc;
using VendingMachineApp.Data.Entities;
using VendingMachineApp.Data.Repositories;
using System.Threading.Tasks;
using System.Linq;

namespace VendingMachineApp.Controllers
{
    public class SupplierController : Controller
    {
        private readonly SupplierRepository _supplierRepo;

        public SupplierController(SupplierRepository supplierRepo)
        {
            _supplierRepo = supplierRepo;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var suppliers = string.IsNullOrWhiteSpace(search)
                ? await _supplierRepo.GetAllAsync()
                : (await _supplierRepo.GetAllAsync())
                    .Where(s => s.Name.Contains(search, System.StringComparison.OrdinalIgnoreCase))
                    .ToList();
            return View(suppliers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var supplier = await _supplierRepo.GetByIdAsync(id);
            if (supplier == null) return NotFound();
            return View(supplier);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                await _supplierRepo.AddAsync(supplier);
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _supplierRepo.GetByIdAsync(id);
            if (supplier == null) return NotFound();
            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Supplier supplier)
        {
            if (id != supplier.SupplierId) return NotFound();
            if (ModelState.IsValid)
            {
                await _supplierRepo.UpdateAsync(supplier);
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _supplierRepo.GetByIdAsync(id);
            if (supplier == null) return NotFound();
            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _supplierRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
