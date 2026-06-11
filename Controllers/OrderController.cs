using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VendingMachineApp.Data;
using VendingMachineApp.Data.Entities;

namespace VendingMachineApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        [AllowAnonymous]
        public async Task<IActionResult> Index(string status)
        {
            var ordersQuery = _context.Orders
            .Include(o => o.Supplier)
            .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                ordersQuery = ordersQuery.Where(o => o.Status == status);
            }
            var orders = await ordersQuery.ToListAsync();
            return View(orders);
        }

        // GET: Orders/Search - AJAX search endpoint
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Search(string term)
{
    var query = _context.Orders
        .Include(o => o.Supplier)
        .AsQueryable();

    if (!string.IsNullOrWhiteSpace(term))
    {
        term = term.ToLower();

        query = query.Where(o =>
            o.OrderId.ToString().Contains(term) ||
            (o.Supplier != null && o.Supplier.Name.ToLower().Contains(term)) ||
            o.Status.ToLower().Contains(term)
        );
    }

    var orders = await query.ToListAsync();

    return PartialView("_OrderTable", orders);
}

        // GET: Orders/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Supplier)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Suppliers = new SelectList(await _context.Suppliers.ToListAsync(), "SupplierId", "Name");
            ViewBag.Products = await _context.Products.ToListAsync();
            
            return View();
        }

        // POST: Orders/Create
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Suppliers = new SelectList(await _context.Suppliers.ToListAsync(), "SupplierId", "Name", order.SupplierId);
                return View(order);
            }
            order.Status = "Pending";
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

//SUPPLIER AUTOCOMPLETE
        [HttpGet]
public async Task<IActionResult> SupplierAutocomplete(string term)
{
    var suppliers = await _context.Suppliers
        .Where(s => string.IsNullOrEmpty(term) ||
                    s.Name.Contains(term))
        .Select(s => new
        {
            id = s.SupplierId,
            name = s.Name
        })
        .ToListAsync();

    return Json(suppliers);
}

        // GET: Orders/Edit/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Supplier)
                .FirstOrDefaultAsync(o => o.OrderId == id);
                
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.Suppliers = new SelectList(await _context.Suppliers.ToListAsync(), "SupplierId", "Name", order.SupplierId);
            return View(order);
        }

        // POST: Orders/Edit/5

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Suppliers = new SelectList(await _context.Suppliers.ToListAsync(), "SupplierId", "Name", order.SupplierId);
                return View(order);
            }
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Orders/Delete/5

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Supplier)
                .FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders
        .Include(o => o.OrderItems)
        .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

             // 1. prvo briši child entitete (OrderItems)
             _context.OrderItems.RemoveRange(order.OrderItems);

            // 2. onda briši parent (Order)
            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

// AJAX endpoint to get products by supplier for dynamic dropdown in order form
        [HttpGet]
        public async Task<IActionResult> ProductsBySupplier(int supplierId)
        {
            var products = await _context.Products
                .Where(p => p.SupplierId == supplierId)
                .ToListAsync();

            return Json(products);
        }
    }
}
