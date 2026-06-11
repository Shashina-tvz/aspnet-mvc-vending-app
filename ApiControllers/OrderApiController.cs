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
    public class OrderApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderApiController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // MAPPING
        // =========================
        private static OrderDTO ToDTO(Order o)
        {
            return new OrderDTO
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                DeliveryDate = o.DeliveryDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status,

                Supplier = o.Supplier == null ? null : new SupplierDTO
                {
                    SupplierId = o.Supplier.SupplierId,
                    Name = o.Supplier.Name,
                    Email = o.Supplier.Email,
                    PhoneNumber = o.Supplier.PhoneNumber,
                    Address = o.Supplier.Address,
                    ContactPerson = o.Supplier.ContactPerson,
                    RegistrationDate = o.Supplier.RegistrationDate
                },

                OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                {
                    OrderItemId = oi.OrderItemId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    SubTotal = oi.SubTotal,

                    Product = oi.Product == null ? null : new ProductDTO
                    {
                        ProductId = oi.Product.ProductId,
                        Name = oi.Product.Name,
                        Price = oi.Product.Price,
                        Supplier = null // 👈 ovdje NAMJERNO gasimo dalje ugniježđenje
                    }
                }).ToList()
            };
        }

        // =========================
        // GET ALL + SEARCH
        // =========================

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAll([FromQuery] string? search)
        {
            var query = _context.Orders
                .Include(o => o.Supplier)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(o =>
                    o.Status.Contains(search) ||
                    o.Supplier.Name.Contains(search));
            }

            var orders = await query.ToListAsync();

            return Ok(orders.Select(ToDTO));
        }

        // =========================
        // GET BY ID
        // =========================

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Supplier)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                return NotFound();

            return Ok(ToDTO(order));
        }

        // =========================
        // POST (CREATE)
        // =========================
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Create(Order model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Orders.Add(model);
            await _context.SaveChangesAsync();

            await _context.Entry(model)
                .Reference(o => o.Supplier)
                .LoadAsync();

            await _context.Entry(model)
                .Collection(o => o.OrderItems)
                .Query()
                .Include(oi => oi.Product)
                .LoadAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = model.OrderId },
                ToDTO(model)
            );
        }

        // =========================
        // PUT (UPDATE)
        // =========================
        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDTO>> Update(int id, Order model)
        {
            if (id != model.OrderId)
                return BadRequest("ID mismatch");

            var order = await _context.Orders
                .Include(o => o.Supplier)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                return NotFound();

            order.OrderDate = model.OrderDate;
            order.DeliveryDate = model.DeliveryDate;
            order.TotalAmount = model.TotalAmount;
            order.Status = model.Status;
            order.SupplierId = model.SupplierId;

            await _context.SaveChangesAsync();

            return Ok(ToDTO(order));
        }

        // =========================
        // DELETE
        // =========================
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                return NotFound();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}