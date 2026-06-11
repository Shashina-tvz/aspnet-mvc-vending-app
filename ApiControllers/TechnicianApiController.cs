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
    public class TechnicianApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TechnicianApiController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // MAPPING
        // =========================
        private static TechnicianDTO ToDTO(Technician t)
        {
            return new TechnicianDTO
            {
                TechnicianId = t.TechnicianId,
                Name = t.Name,
                Email = t.Email,
                PhoneNumber = t.PhoneNumber
            };
        }

        // =========================
        // GET: api/technician
        // (with optional search)
        // =========================

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TechnicianDTO>>> GetAll([FromQuery] string? search)
        {
            var query = _context.Technicians.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t =>
                    t.Name.Contains(search) ||
                    t.Email.Contains(search));
            }

            var technicians = await query.ToListAsync();

            return Ok(technicians.Select(ToDTO));
        }

        // =========================
        // GET: api/technician/5
        // =========================

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TechnicianDTO>> GetById(int id)
        {
            var technician = await _context.Technicians
                .FirstOrDefaultAsync(t => t.TechnicianId == id);

            if (technician == null)
                return NotFound();

            return Ok(ToDTO(technician));
        }

        // =========================
        // POST: api/technician
        // =========================

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<ActionResult<TechnicianDTO>> Create(Technician model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Technicians.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = model.TechnicianId },
                ToDTO(model)
            );
        }

        // =========================
        // PUT: api/technician/5
        // =========================
        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}")]
        public async Task<ActionResult<TechnicianDTO>> Update(int id, Technician model)
        {
            if (id != model.TechnicianId)
                return BadRequest("ID mismatch");

            var technician = await _context.Technicians
                .FirstOrDefaultAsync(t => t.TechnicianId == id);

            if (technician == null)
                return NotFound();

            technician.Name = model.Name;
            technician.Email = model.Email;
            technician.PhoneNumber = model.PhoneNumber;
            technician.LicenseNumber = model.LicenseNumber;
            technician.HireDate = model.HireDate;

            await _context.SaveChangesAsync();

            return Ok(ToDTO(technician));
        }

        // =========================
        // DELETE: api/technician/5
        // =========================

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var technician = await _context.Technicians
                .FirstOrDefaultAsync(t => t.TechnicianId == id);

            if (technician == null)
                return NotFound();

            _context.Technicians.Remove(technician);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}