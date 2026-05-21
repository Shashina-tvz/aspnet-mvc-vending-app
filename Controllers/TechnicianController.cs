using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendingMachineApp.Data;
using VendingMachineApp.Data.Entities;

namespace VendingMachineApp.Controllers
{
    public class TechnicianController : Controller
    {
        private readonly AppDbContext _context;

        public TechnicianController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Technicians
        public async Task<IActionResult> Index(string name)
        {
            var techniciansQuery = _context.Technicians
            .AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                techniciansQuery = techniciansQuery
                .Where(t => t.Name.Contains(name));
            }

            var technicians = await techniciansQuery.ToListAsync();
            return View(technicians);
        }

        // GET: Technicians/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var technician = await _context.Technicians
                .Include(t => t.MaintenanceLogs)
                .FirstOrDefaultAsync(t => t.TechnicianId == id);

            if (technician == null)
                return NotFound();
            
            return View(technician);
        }

        // GET: Technicians/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Technicians/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Technician technician)
        {
            if (!ModelState.IsValid)
            {
                return View(technician);
            }
            _context.Technicians.Add(technician);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Technicians/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var technician = await _context.Technicians
                .FirstOrDefaultAsync(t => t.TechnicianId == id);

            if (technician == null)            
                return NotFound();
            
            return View(technician);
        }

        // POST: Technicians/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Technician technician)
        {
            if (id != technician.TechnicianId)            
                return BadRequest();
            
            if (!ModelState.IsValid)            
                return View(technician);
            
            var existing = await _context.Technicians
                .FirstOrDefaultAsync(t => t.TechnicianId == id);

            if (existing == null)
                return NotFound();

            existing.Name = technician.Name;
            existing.LicenseNumber = technician.LicenseNumber;
            existing.Email = technician.Email;
            existing.PhoneNumber = technician.PhoneNumber;
            existing.HireDate = technician.HireDate;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Technicians/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var technician = await _context.Technicians
                .FirstOrDefaultAsync(t => t.TechnicianId == id);

            if (technician == null)            
                return NotFound();
            
            return View(technician);
        }

        // POST: Technicians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var technician = await _context.Technicians
            .FindAsync(id);

            if (technician == null)  
                return NotFound();
            
            
            _context.Technicians.Remove(technician);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
