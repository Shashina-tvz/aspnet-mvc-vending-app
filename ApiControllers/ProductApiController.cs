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
    public class ProductApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductApiController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // MAPPING
        // =========================
        private static ProductDTO ToDTO(Product p)
        {
            return new ProductDTO
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Supplier = p.Supplier == null ? null : new SupplierDTO
                {
                    SupplierId = p.Supplier.SupplierId,
                    Name = p.Supplier.Name,
                    Email = p.Supplier.Email,
                    PhoneNumber = p.Supplier.PhoneNumber,
                    Address = p.Supplier.Address,
                    ContactPerson = p.Supplier.ContactPerson,
                    RegistrationDate = p.Supplier.RegistrationDate
                }
            };
        }

        // =========================
        // GET ALL + SEARCH
        // =========================
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll([FromQuery] string? search)
        {
            var query = _context.Products
                .Include(p => p.Supplier)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p =>
                    p.Name.Contains(search) ||
                    p.Description.Contains(search));
            }

            var products = await query.ToListAsync();

            return Ok(products.Select(ToDTO));
        }

        // =========================
        // GET BY ID
        // =========================

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
                return NotFound();

            return Ok(ToDTO(product));
        }

        // =========================
        // POST (CREATE)
        // =========================

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create(Product model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Products.Add(model);
            await _context.SaveChangesAsync();

            // reload supplier for DTO (ako treba)
            await _context.Entry(model)
                .Reference(p => p.Supplier)
                .LoadAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = model.ProductId },
                ToDTO(model)
            );
        }

        // =========================
        // PUT (UPDATE)
        // =========================
        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDTO>> Update(int id, Product model)
        {
            if (id != model.ProductId)
                return BadRequest("ID mismatch");

            var product = await _context.Products
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
                return NotFound();

            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.SupplierId = model.SupplierId;

            await _context.SaveChangesAsync();

            return Ok(ToDTO(product));
        }

        // =========================
        // DELETE
        // =========================

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}