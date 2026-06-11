using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendingMachineApp.Data;
using VendingMachineApp.Data.Entities;
using VendingMachineApp.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace VendingMachineApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceLogApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MaintenanceLogApiController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // MAPPING
        // =========================
        private static MaintenanceLogDTO ToDTO(MaintenanceLog m)
        {
            return new MaintenanceLogDTO
            {
                MaintenanceLogId = m.MaintenanceLogId,
                Description = m.Description,
                MaintenanceDate = m.MaintenanceDate,
                Cost = m.Cost,
                Status = m.Status,

                // 🔥 VendingMachine (UGNIJEŽĐEN)
                VendingMachine = m.VendingMachine == null ? null : new VendingMachineDTO
                {
                    MachineId = m.VendingMachine.MachineId,
                    MachineNumber = m.VendingMachine.MachineNumber,
                    Address = m.VendingMachine.Address,
                    Status = m.VendingMachine.Status
                },

                // 🔥 Technician (UGNIJEŽĐEN)
                Technician = m.Technician == null ? null : new TechnicianDTO
                {
                    TechnicianId = m.Technician.TechnicianId,
                    Name = m.Technician.Name,
                    Email = m.Technician.Email,
                    PhoneNumber = m.Technician.PhoneNumber
                }
            };
        }

        // =========================
        // GET: api/maintenancelog
        // =========================

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaintenanceLogDTO>>> GetAll([FromQuery] string? search)
        {
            var query = _context.MaintenanceLogs
                .Include(m => m.VendingMachine)
                .Include(m => m.Technician)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(m =>
                    m.Description.Contains(search) ||
                    m.Status.Contains(search));
            }

            var logs = await query.ToListAsync();

            return Ok(logs.Select(ToDTO));
        }

        // =========================
        // GET: api/maintenancelog/5
        // =========================

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<MaintenanceLogDTO>> GetById(int id)
        {
            var log = await _context.MaintenanceLogs
                .Include(m => m.VendingMachine)
                .Include(m => m.Technician)
                .FirstOrDefaultAsync(m => m.MaintenanceLogId == id);

            if (log == null)
                return NotFound();

            return Ok(ToDTO(log));
        }

        // =========================
        // POST: api/maintenancelog
        // =========================
        [Authorize (Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<ActionResult<MaintenanceLogDTO>> Create(MaintenanceLog model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.MaintenanceLogs.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = model.MaintenanceLogId },
                ToDTO(model)
            );
        }

        // =========================
        // PUT: api/maintenancelog/5
        // =========================
        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}")]
        public async Task<ActionResult<MaintenanceLogDTO>> Update(int id, MaintenanceLog model)
        {
            if (id != model.MaintenanceLogId)
                return BadRequest("ID mismatch");

            var log = await _context.MaintenanceLogs
                .FirstOrDefaultAsync(m => m.MaintenanceLogId == id);

            if (log == null)
                return NotFound();

            log.Description = model.Description;
            log.MaintenanceDate = model.MaintenanceDate;
            log.Cost = model.Cost;
            log.Status = model.Status;
            log.MachineId = model.MachineId;
            log.TechnicianId = model.TechnicianId;

            await _context.SaveChangesAsync();

            return Ok(ToDTO(log));
        }

        // =========================
        // DELETE: api/maintenancelog
        // =========================
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var log = await _context.MaintenanceLogs
                .FirstOrDefaultAsync(m => m.MaintenanceLogId == id);

            if (log == null)
                return NotFound();

            _context.MaintenanceLogs.Remove(log);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}