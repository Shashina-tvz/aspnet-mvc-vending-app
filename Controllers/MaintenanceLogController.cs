using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VendingMachineApp.Data;
using VendingMachineApp.Data.Entities;

namespace VendingMachineApp.Controllers
{
    public class MaintenanceLogController : Controller
    {
        private readonly AppDbContext _context;

        public MaintenanceLogController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MaintenanceLogs
        public async Task<IActionResult> Index(int? machineId)
        {
            var logsQuery = _context.MaintenanceLogs
                .Include(l => l.VendingMachine)
                .Include(l => l.Technician)
                .AsQueryable();
            if (machineId.HasValue)
            {
                logsQuery = logsQuery.Where(l => l.MachineId == machineId.Value);
            }
            var logs = await logsQuery.ToListAsync();
            return View(logs);
        }

        // GET: MaintenanceLogs/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var log = await _context.MaintenanceLogs
                .Include(l => l.VendingMachine)
                .Include(l => l.Technician)
                .FirstOrDefaultAsync(l => l.MaintenanceLogId == id);
            if (log == null)
            {
                return NotFound();
            }
            return View(log);
        }

        // GET: MaintenanceLogs/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.VendingMachines = new SelectList(await _context.VendingMachines.ToListAsync(), "MachineId", "MachineNumber");
            ViewBag.Technicians = new SelectList(await _context.Technicians.ToListAsync(), "TechnicianId", "Name");
            return View();
        }

        // POST: MaintenanceLogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MaintenanceLog log)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.VendingMachines = new SelectList(await _context.VendingMachines.ToListAsync(), "MachineId", "MachineNumber", log.MachineId);
                ViewBag.Technicians = new SelectList(await _context.Technicians.ToListAsync(), "TechnicianId", "Name", log.TechnicianId);
                return View(log);
            }
            _context.MaintenanceLogs.Add(log);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: MaintenanceLogs/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var log = await _context.MaintenanceLogs.FindAsync(id);
            if (log == null)
            {
                return NotFound();
            }
            ViewBag.VendingMachines = new SelectList(await _context.VendingMachines.ToListAsync(), "MachineId", "MachineNumber", log.MachineId);
            ViewBag.Technicians = new SelectList(await _context.Technicians.ToListAsync(), "TechnicianId", "Name", log.TechnicianId);
            return View(log);
        }

        // POST: MaintenanceLogs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MaintenanceLog log)
        {
            if (id != log.MaintenanceLogId)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                ViewBag.VendingMachines = new SelectList(await _context.VendingMachines.ToListAsync(), "MachineId", "MachineNumber", log.MachineId);
                ViewBag.Technicians = new SelectList(await _context.Technicians.ToListAsync(), "TechnicianId", "Name", log.TechnicianId);
                return View(log);
            }
            _context.MaintenanceLogs.Update(log);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: MaintenanceLogs/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var log = await _context.MaintenanceLogs
                .Include(l => l.VendingMachine)
                .Include(l => l.Technician)
                .FirstOrDefaultAsync(l => l.MaintenanceLogId == id);
            if (log == null)
            {
                return NotFound();
            }
            return View(log);
        }

        // POST: MaintenanceLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var log = await _context.MaintenanceLogs.FindAsync(id);
            if (log == null)
            {
                return NotFound();
            }
            _context.MaintenanceLogs.Remove(log);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
