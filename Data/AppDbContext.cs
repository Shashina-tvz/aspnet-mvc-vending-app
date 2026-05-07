using Microsoft.EntityFrameworkCore;
using VendingMachineApp.Data.Entities;

namespace VendingMachineApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<VendingMachine> VendingMachines { get; set; }
        public DbSet<ProductSlot> ProductSlots { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<MaintenanceLog> MaintenanceLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade from Order

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict from Product  

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Coca-Cola",
                    Price = 2.5m,
                    Category = ProductCategory.Beverage,
                    Description = "Gazirani napitak",
                    ReorderThreshold = 10,
                    ManufactureDate = new DateTime(2025, 03, 01),
                    ExpirationDate = new DateTime(2027, 03, 01),
                    SupplierId = 1
                },
                new Product
                {
                    ProductId = 8,
                    Name = "Fanta",
                    Price = 2.4m,
                    Category = ProductCategory.Beverage,
                    Description = "Gazirani napitak",
                    ReorderThreshold = 10,
                    ManufactureDate = new DateTime(2025, 03, 01),
                    ExpirationDate = new DateTime(2026, 05, 15),
                    SupplierId = 1
                },
                new Product
                {
                    ProductId = 9,
                    Name = "Sprite",
                    Price = 2.4m,
                    Category = ProductCategory.Beverage,
                    Description = "Gazirani napitak",
                    ReorderThreshold = 10,
                    ManufactureDate = new DateTime(2025, 03, 01),
                    ExpirationDate = new DateTime(2027, 04, 05),
                    SupplierId = 1
                }
            );  


            modelBuilder.Entity<Supplier>().HasData(
                new Supplier
                {
                    SupplierId = 1,
                    Name = "Coca-Cola Bottling Hrvatska",
                    PhoneNumber = "0800-123-456",
                    Email = "info@cocacola.hr",
                    Address = "Radnička cesta 1, 10000 Zagreb",
                    ContactPerson = "Ana Kola",
                    RegistrationDate = new DateTime(2014, 01, 01)
                }
            );        
        }

    }
}