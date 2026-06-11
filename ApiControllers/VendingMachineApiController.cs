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
    public class VendingMachineApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VendingMachineApiController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // MAPPING
        // =========================
        private static VendingMachineDTO ToDTO(VendingMachine m)
        {
            return new VendingMachineDTO
            {
                MachineId = m.MachineId,
                MachineNumber = m.MachineNumber,
                Address = m.Address,
                Status = m.Status
            };
        }

        // =========================
        // GET: api/vendingmachine
        // =========================

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendingMachineDTO>>> GetAll([FromQuery] string? search)
        {
            var query = _context.VendingMachines.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(m =>
                    m.Address.Contains(search) ||
                    m.MachineNumber.ToString().Contains(search));
            }

            var machines = await query.ToListAsync();

            return Ok(machines.Select(ToDTO));
        }

        // =========================
        // GET: api/vendingmachine/5
        // =========================

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<VendingMachineDTO>> GetById(int id)
        {
            var machine = await _context.VendingMachines
                .FirstOrDefaultAsync(m => m.MachineId == id);

            if (machine == null)
                return NotFound();

            return Ok(ToDTO(machine));
        }

        // =========================
        // POST: api/vendingmachine
        // =========================
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<ActionResult<VendingMachineDTO>> Create(VendingMachine model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.VendingMachines.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = model.MachineId },
                ToDTO(model)
            );
        }

        // =========================
        // PUT: api/vendingmachine/5
        // =========================
        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}")]
        public async Task<ActionResult<VendingMachineDTO>> Update(int id, VendingMachine model)
        {
            if (id != model.MachineId)
                return BadRequest("ID mismatch");

            var machine = await _context.VendingMachines
                .FirstOrDefaultAsync(m => m.MachineId == id);

            if (machine == null)
                return NotFound();

            machine.MachineNumber = model.MachineNumber;
            machine.Address = model.Address;
            machine.Status = model.Status;
            machine.Capacity = model.Capacity;
            machine.ManufacturedDate = model.ManufacturedDate;
            machine.LastMaintenanceDate = model.LastMaintenanceDate;
            machine.CurrentBalance = model.CurrentBalance;

            await _context.SaveChangesAsync();

            return Ok(ToDTO(machine));
        }

        // =========================
        // DELETE: api/vendingmachine/5
        // =========================
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var machine = await _context.VendingMachines
                .FirstOrDefaultAsync(m => m.MachineId == id);

            if (machine == null)
                return NotFound();

            _context.VendingMachines.Remove(machine);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}