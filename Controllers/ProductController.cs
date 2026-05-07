using Microsoft.AspNetCore.Mvc;
using VendingMachineApp.Data.Entities;
using VendingMachineApp.Data.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VendingMachineApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepo;
        private readonly SupplierRepository _supplierRepo;

        public ProductController(ProductRepository productRepo, SupplierRepository supplierRepo)
        {
            _productRepo = productRepo;
            _supplierRepo = supplierRepo;
        }

        [Route("products/all")]
        public async Task<IActionResult> Index(ProductCategory? category)
        {
            var products = category.HasValue
                ? await _productRepo.GetByCategoryAsync(category.Value)
                : await _productRepo.GetAllAsync();
            return View(products);
        }

        [Route("products/info/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null) return NotFound();
            product.Supplier = await _supplierRepo.GetByIdAsync(product.SupplierId);
            return View(product);
        }

        
        [Route("products/new")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("products/new")]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepo.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [Route("products/update/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("products/update/{id}")]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ProductId) return NotFound();
            if (ModelState.IsValid)
            {
                await _productRepo.UpdateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [Route("products/remove/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("products/remove/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
