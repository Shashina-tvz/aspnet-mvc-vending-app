using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var suppliers = await _context.Suppliers
                .OrderBy(s => s.Name)
                .ToListAsync();

            return View(suppliers);
        }

        // SEARCH (AJAX)
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Search(string term)
        {
            var query = _context.Suppliers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(term))
            {
                term = term.ToLower();

                query = query.Where(s =>
                    s.Name.ToLower().Contains(term) ||
                    s.Address.ToLower().Contains(term) ||
                    s.Email.ToLower().Contains(term) ||
                    s.PhoneNumber.ToLower().Contains(term)
                );
            }

            var result = await query
                .OrderBy(s => s.Name)
                .ToListAsync();

            return PartialView("_SupplierTable", result);
        }

        // DETAILS
        [Authorize]
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
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Create()
        {
            return View(new Supplier());
        }

        // CREATE POST
        [Authorize(Roles = "Admin,Manager")]
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

            return RedirectToAction("Index", new { highlightId = supplier.SupplierId });
        }

        // EDIT GET
        [Authorize(Roles = "Admin,Manager")]
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

        [Authorize(Roles = "Admin,Manager")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        public async Task<IActionResult> Products(int id)
{
    var supplier = await _context.Suppliers
        .Include(s => s.Products)
        .FirstOrDefaultAsync(s => s.SupplierId == id);

    if (supplier == null)
        return NotFound();

    return View(supplier);
}
    }
}