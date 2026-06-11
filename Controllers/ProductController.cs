using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VendingMachineApp.Data;
using VendingMachineApp.Data.Entities;

namespace VendingMachineApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [Route("products/all")]
        public async Task<IActionResult> Index(ProductCategory? category)
        {
            var products = await _context.Products
                .Include(p => p.Supplier)
                .ToListAsync();
            return View(products);
        }

        // GET: Products/Search - AJAX search endpoint

        [AllowAnonymous]
        [HttpGet]        
        public async Task<IActionResult> Search(string term)
        {
            var query = _context.Products
                .Include(p => p.Supplier)
                .AsQueryable();

            var products = await query.ToListAsync();

            if (!string.IsNullOrWhiteSpace(term))
            {
                term = term.ToLower();

                products = products.Where(p =>
                    p.Name.ToLower().Contains(term) ||
                    p.Supplier!.Name.ToLower().Contains(term) ||
                    (p.Category.HasValue &&
                    p.Category.Value.ToString().ToLower().Contains(term))
                ).ToList();
            }

            return PartialView("_ProductTable", products);
        }

        [Authorize]
        [Route("products/info/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [Authorize(Roles = "Admin,Manager")]
        [Route("products/new")]
        // CREATE GET
        public async Task<IActionResult> Create()
        {
            ViewBag.Suppliers = new SelectList(
                await _context.Suppliers.ToListAsync(),
                "SupplierId",
                "Name");

            return View(new Product());
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]       
        [ValidateAntiForgeryToken]
        [Route("products/new")]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Suppliers = new SelectList(
                    await _context.Suppliers.ToListAsync(),
                    "SupplierId",
                    "Name");

                return View(product);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { highlightId = product.ProductId });
        }

        [HttpGet]
        public async Task<IActionResult> SupplierAutocomplete(string term)
        {
            var query = _context.Suppliers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(term))
            {
                term = term.ToLower();

                query = query.Where(s =>
                    s.Name.ToLower().Contains(term));
            }

            var results = await query
                .Select(s => new
                {
                    id = s.SupplierId,
                    name = s.Name
                })
                .Take(10)
                .ToListAsync();

            return Json(results);
        }

       [HttpGet]
        public IActionResult CategoryAutocomplete(string term)
        {
            var all = Enum.GetValues(typeof(ProductCategory))
                .Cast<ProductCategory>()
                .Select(c => new
                {
                    id = (int)c,
                    name = c.ToString()!
                })
                .ToList();

            if (!string.IsNullOrWhiteSpace(term))
            {
                term = term.ToLower();

                all = all.Where(c =>
                    c.name.ToLower().Contains(term))
                    .ToList();
            }

            return Json(all.Take(10));
        }

        [Authorize(Roles = "Admin,Manager")]
        [Route("products/update/{id}")]
         // EDIT GET
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Suppliers = new SelectList(
                await _context.Suppliers.ToListAsync(),
                "SupplierId",
                "Name",
                product.SupplierId);

            return View(product);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]        
        [ValidateAntiForgeryToken]
        [Route("products/update/{id}")]
        //POST EDIT
          public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Suppliers = new SelectList(
                    await _context.Suppliers.ToListAsync(),
                    "SupplierId",
                    "Name",
                    product.SupplierId);

                return View(product);
            }

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return Redirect("/products/all");;
        }

        [Authorize(Roles = "Admin")]
        [Route("products/remove/{id}")]
        // DELETE GET
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("products/remove/{id}")]
        // POST DELETE
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Redirect("/products/all");
        }
    }
}
