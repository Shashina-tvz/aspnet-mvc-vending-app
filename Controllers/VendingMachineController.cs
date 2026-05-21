using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VendingMachineApp.Data;
using VendingMachineApp.Data.Entities;

namespace VendingMachineApp.Controllers
{
    public class VendingMachineController : Controller
    {
        private readonly AppDbContext _context;

        public VendingMachineController(AppDbContext context)
        {
            _context = context;
        }

        // GET: VendingMachines
        public async Task<IActionResult> Index(int? machineNumber)
        {
            var machinesQuery = _context.VendingMachines
                .Include(vm => vm.ProductSlots)
                .Include(vm => vm.Transactions)
                .Include(vm => vm.MaintenanceLogs)
                .AsQueryable();

            if (machineNumber.HasValue)
            {
                machinesQuery = machinesQuery
                .Where(vm => vm.MachineNumber == machineNumber.Value);
            }
            var machines = await machinesQuery.ToListAsync();
            return View(machines);
        }

        // GET: VendingMachines/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var machine = await _context.VendingMachines
                .Include(vm => vm.ProductSlots)
                    .ThenInclude(ps => ps.Product)
                .Include(vm => vm.Transactions)
                .Include(vm => vm.MaintenanceLogs)
                .FirstOrDefaultAsync(vm => vm.MachineId == id);
            if (machine == null)            
                return NotFound();
            
            return View(machine);
        }

        // GET: VendingMachines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VendingMachines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VendingMachine machine)
        {
            if (!ModelState.IsValid)            
                return View(machine);
            
            _context.VendingMachines.Add(machine);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: VendingMachines/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var machine = await _context.VendingMachines.FindAsync(id);
            if (machine == null)            
                return NotFound();
            
            return View(machine);
        }

        // POST: VendingMachines/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VendingMachine machine)
        {
            if (id != machine.MachineId)            
                return BadRequest();
            
            if (!ModelState.IsValid)            
                return View(machine);
            
            _context.VendingMachines.Update(machine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: VendingMachines/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var machine = await _context.VendingMachines
                .FirstOrDefaultAsync(vm => vm.MachineId == id);
            if (machine == null)            
                return NotFound();
            
            return View(machine);
        }

        // POST: VendingMachines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var machine = await _context.VendingMachines.FindAsync(id);
            if (machine == null)            
                return NotFound();
            
            _context.VendingMachines.Remove(machine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
