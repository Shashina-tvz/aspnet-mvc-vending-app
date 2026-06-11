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
    public class SupplierApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SupplierApiController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // MAPPING
        // =========================
        private static SupplierDTO ToDTO(Supplier s)
        {
            return new SupplierDTO
            {
                SupplierId = s.SupplierId,
                Name = s.Name,
                PhoneNumber = s.PhoneNumber,
                Email = s.Email,
                Address = s.Address,
                ContactPerson = s.ContactPerson,
                RegistrationDate = s.RegistrationDate
            };
        }

        // =========================
        // GET: api/supplier
        // (with optional search)
        // =========================

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetAll([FromQuery] string? search)
        {
            var query = _context.Suppliers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s =>
                    s.Name.Contains(search) ||
                    s.Email.Contains(search));
            }

            var suppliers = await query.ToListAsync();

            return Ok(suppliers.Select(ToDTO));
        }

        // =========================
        // GET: api/supplier/5
        // =========================

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDTO>> GetById(int id)
        {
            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(s => s.SupplierId == id);

            if (supplier == null)
                return NotFound();

            return Ok(ToDTO(supplier));
        }

        // =========================
        // POST: api/supplier
        // =========================

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<SupplierDTO>> Create(Supplier model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Suppliers.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = model.SupplierId },
                ToDTO(model)
            );
        }

        // =========================
        // PUT: api/supplier/5
        // =========================

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<SupplierDTO>> Update(int id, Supplier model)
        {
            
            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(s => s.SupplierId == id);

            if (supplier == null)
                return NotFound();

            if (id != model.SupplierId)
            return BadRequest("ID mismatch");


            supplier.Name = model.Name;
            supplier.PhoneNumber = model.PhoneNumber;
            supplier.Email = model.Email;
            supplier.Address = model.Address;
            supplier.ContactPerson = model.ContactPerson;
            supplier.RegistrationDate = model.RegistrationDate;

            await _context.SaveChangesAsync();

            return Ok(ToDTO(supplier));
        }

        // =========================
        // DELETE: api/supplier/5
        // =========================
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(s => s.SupplierId == id);

            if (supplier == null)
                return NotFound();

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}