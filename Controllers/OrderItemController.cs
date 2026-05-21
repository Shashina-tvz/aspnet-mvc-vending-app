using Microsoft.AspNetCore.Mvc;
using VendingMachineApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using VendingMachineApp.Data;


namespace VendingMachineApp.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly AppDbContext _context;

        public OrderItemController(AppDbContext context)
        {
            _context = context;
        }

        // ADD ITEM (GET)
        public async Task<IActionResult> Add(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
                return NotFound();

            ViewBag.Products = await _context.Products.ToListAsync();

            return View(order);
        }

        // ADD ITEM (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int orderId, int productId, int quantity)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
                return NotFound();

            var item = new OrderItem
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity
            };

            _context.OrderItems.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Order", new { id = orderId });
        }

        // REMOVE ITEM
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _context.OrderItems.FindAsync(id);

            if (item == null)
                return NotFound();

            int orderId = item.OrderId;

            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Order", new { id = orderId });
        }
    }
}
