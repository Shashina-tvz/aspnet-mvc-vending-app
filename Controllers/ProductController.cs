using Microsoft.AspNetCore.Mvc;
using VendingMachineApp.Data.Entities;
using VendingMachineApp.Data.Repositories;
using System.Linq;

namespace VendingMachineApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly MockProductRepository _productRepo;
        private readonly MockSupplierRepository _supplierRepo;

        public ProductController(MockProductRepository productRepo, MockSupplierRepository supplierRepo)
        {
            _productRepo = productRepo;
            _supplierRepo = supplierRepo;
        }

        public IActionResult Index()
        {
            var products = _productRepo.GetAll();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _productRepo.GetById(id);
            if (product == null) return NotFound();
            product.Supplier = _supplierRepo.GetById(product.SupplierId);
            return View(product);
        }
    }
}
