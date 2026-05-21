using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendingMachineApp.Data;
using VendingMachineApp.Data.Entities;

namespace VendingMachineApp.Controllers
{
    public class SupplierController : Controller
    {
        private readonly AppDbContext _context;

        public SupplierController(AppDbContext context)
        {
            _context = context;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            var suppliers = await _context.Suppliers
                .ToListAsync();

            return View(suppliers);
        }

        // DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var supplier = await _context.Suppliers
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.SupplierId == id);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return View(supplier);
            }

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

         // EDIT GET
        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(s => s.SupplierId == id);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

         // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Supplier supplier)
        {
            if (id != supplier.SupplierId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(supplier);
            }

            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        // DELETE GET
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(s => s.SupplierId == id);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }
        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(s => s.SupplierId == id);

            if (supplier == null)
            {
                return NotFound();
            }

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}