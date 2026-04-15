using VendingMachineApp.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineApp.Data.Repositories
{
    public class MockProductRepository
    {
        private List<Product> products = new List<Product>
        {
            // Coca-Cola Bottling Hrvatska
            new Product { ProductId = 1, Name = "Coca-Cola", Price = 2.5m, Category = ProductCategory.Beverage, Description = "Gazirani napitak", ReorderThreshold = 10, ManufactureDate = DateTime.Now.AddMonths(-2), ExpirationDate = DateTime.Now.AddMonths(10), SupplierId = 1 },
            new Product { ProductId = 8, Name = "Fanta", Price = 2.4m, Category = ProductCategory.Beverage, Description = "Gazirani napitak", ReorderThreshold = 10, ManufactureDate = DateTime.Now.AddMonths(-2), ExpirationDate = DateTime.Now.AddMonths(10), SupplierId = 1 },
            new Product { ProductId = 9, Name = "Sprite", Price = 2.4m, Category = ProductCategory.Beverage, Description = "Gazirani napitak", ReorderThreshold = 10, ManufactureDate = DateTime.Now.AddMonths(-2), ExpirationDate = DateTime.Now.AddMonths(10), SupplierId = 1 },

            // Mars Wrigley Hrvatska
            new Product { ProductId = 3, Name = "Snickers", Price = 1.5m, Category = ProductCategory.Snack, Description = "Čokoladica", ReorderThreshold = 10, ManufactureDate = DateTime.Now.AddMonths(-2), ExpirationDate = DateTime.Now.AddMonths(8), SupplierId = 2 },
            new Product { ProductId = 4, Name = "Mars", Price = 1.6m, Category = ProductCategory.Snack, Description = "Čokoladica", ReorderThreshold = 10, ManufactureDate = DateTime.Now.AddMonths(-2), ExpirationDate = DateTime.Now.AddMonths(8), SupplierId = 2 },
            new Product { ProductId = 11, Name = "7Days Croissant", Price = 1.8m, Category = ProductCategory.Snack, Description = "Snack croissant", ReorderThreshold = 7, ManufactureDate = DateTime.Now.AddMonths(-1), ExpirationDate = DateTime.Now.AddMonths(5), SupplierId = 2 },

            // Hrusty d.o.o.
            new Product { ProductId = 5, Name = "Hrusty čips classic", Price = 2.0m, Category = ProductCategory.Chips, Description = "Krompir čips", ReorderThreshold = 8, ManufactureDate = DateTime.Now.AddMonths(-3), ExpirationDate = DateTime.Now.AddMonths(8), SupplierId = 3 },
            new Product { ProductId = 10, Name = "Doritos", Price = 2.2m, Category = ProductCategory.Chips, Description = "Tortilla čips", ReorderThreshold = 8, ManufactureDate = DateTime.Now.AddMonths(-3), ExpirationDate = DateTime.Now.AddMonths(8), SupplierId = 3 },

            // Snack&Go d.o.o.
            new Product { ProductId = 6, Name = "Sandwich", Price = 3.5m, Category = ProductCategory.Sandwich, Description = "Sendvič", ReorderThreshold = 3, ManufactureDate = DateTime.Now.AddDays(-2), ExpirationDate = DateTime.Now.AddDays(5), SupplierId = 4 },
            new Product { ProductId = 7, Name = "Muesli Bar", Price = 1.2m, Category = ProductCategory.MuesliBar, Description = "Muesli bar", ReorderThreshold = 6, ManufactureDate = DateTime.Now.AddMonths(-1), ExpirationDate = DateTime.Now.AddMonths(7), SupplierId = 4 }
        };
        public List<Product> GetAll() => products;
        public List<Product> GetByCategory(ProductCategory category) => products.Where(x => x.Category == category).ToList();

        public Product GetById(int id)
        {
            return products.FirstOrDefault(x => x.ProductId == id) ?? new Product();
        }
    }
}