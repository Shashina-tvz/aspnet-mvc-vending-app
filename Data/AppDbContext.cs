using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VendingMachineApp.Data.Entities;

namespace VendingMachineApp.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
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
                // Coca-Cola Bottling Hrvatska
            new Product { ProductId = 1, Name = "Coca-Cola", Price = 2.5m, Category = ProductCategory.Beverage, Description = "Gazirani napitak", ReorderThreshold = 10, ManufactureDate  = new DateTime(2025, 03, 01), ExpirationDate = new DateTime(2027, 02, 27), SupplierId = 1 },
            new Product { ProductId = 8, Name = "Fanta", Price = 2.4m, Category = ProductCategory.Beverage, Description = "Gazirani napitak", ReorderThreshold = 10, ManufactureDate  = new DateTime(2026, 02, 05), ExpirationDate = new DateTime(2028, 01, 04), SupplierId = 1 },
            new Product { ProductId = 9, Name = "Sprite", Price = 2.4m, Category = ProductCategory.Beverage, Description = "Gazirani napitak", ReorderThreshold = 10, ManufactureDate = new DateTime(2024, 12, 28), ExpirationDate = new DateTime(2026, 05, 27), SupplierId = 1 },

            // Mars Wrigley Hrvatska
            new Product { ProductId = 3, Name = "Snickers", Price = 1.5m, Category = ProductCategory.Snack, Description = "Čokoladica", ReorderThreshold = 10, ManufactureDate = new DateTime(2025, 09, 01), ExpirationDate = new DateTime(2026, 08, 27), SupplierId = 2 },
            new Product { ProductId = 4, Name = "Mars", Price = 1.6m, Category = ProductCategory.Snack, Description = "Čokoladica", ReorderThreshold = 10, ManufactureDate = new DateTime(2026, 04, 28), ExpirationDate = new DateTime(2027, 05, 27), SupplierId = 2 },
            new Product { ProductId = 2, Name = "7Days Croissant", Price = 1.8m, Category = ProductCategory.Snack, Description = "Snack croissant", ReorderThreshold = 7, ManufactureDate = new DateTime(2025, 11, 28), ExpirationDate = new DateTime(2026, 06, 27), SupplierId = 2 },

            // Hrusty d.o.o.
            new Product { ProductId = 5, Name = "Hrusty čips classic", Price = 2.0m, Category = ProductCategory.Chips, Description = "Krompir čips", ReorderThreshold = 8, ManufactureDate = new DateTime(2024, 10, 28), ExpirationDate = new DateTime(2026, 05, 27), SupplierId = 3 },
            new Product { ProductId = 10, Name = "Doritos", Price = 2.2m, Category = ProductCategory.Chips, Description = "Tortilla čips", ReorderThreshold = 8, ManufactureDate = new DateTime(2025, 09, 28), ExpirationDate = new DateTime(2026, 12, 27), SupplierId = 3 },

            // Snack&Go d.o.o.
            new Product { ProductId = 6, Name = "Sandwich", Price = 3.5m, Category = ProductCategory.Sandwich, Description = "Sendvič", ReorderThreshold = 3, ManufactureDate = new DateTime(2026, 05, 19), ExpirationDate = new DateTime(2026, 05, 30), SupplierId = 4 },
            new Product { ProductId = 7, Name = "Muesli Bar", Price = 1.2m, Category = ProductCategory.MuesliBar, Description = "Muesli bar", ReorderThreshold = 6, ManufactureDate = new DateTime(2025, 11, 28), ExpirationDate = new DateTime(2026, 10, 27), SupplierId = 4 }
            );  


            modelBuilder.Entity<Supplier>().HasData(
               new Supplier {
                SupplierId = 1,
                Name = "Coca-Cola Bottling Hrvatska",
                PhoneNumber = "0800-123-456",
                Email = "info@cocacola.hr",
                Address = "Radnička cesta 1, 10000 Zagreb",
                ContactPerson = "Ana Kola",
                RegistrationDate = new DateTime(2012, 05, 01)
            },
            new Supplier {
                SupplierId = 2,
                Name = "Mars Wrigley Hrvatska",
                PhoneNumber = "0800-555-333",
                Email = "info@mars.com",
                Address = "Savska cesta 32, 10000 Zagreb",
                ContactPerson = "Marko Mars",
                RegistrationDate = new DateTime(2018, 04, 05)
            },
            new Supplier {
                SupplierId = 3,
                Name = "Hrusty d.o.o.",
                PhoneNumber = "0800-444-555",
                Email = "info@hrusty.hr",
                Address = "Hrusty Lane 7, 10000 Zagreb",
                ContactPerson = "Hrvoje Čips",
                RegistrationDate = new DateTime(2013, 08, 01)
            },
            new Supplier {
                SupplierId = 4,
                Name = "Snack&Go d.o.o.",
                PhoneNumber = "0800-777-888",
                Email = "info@snackgo.hr",
                Address = "Ulica Snackova 15, 21000 Split",
                ContactPerson = "Petra Grickalica",
                RegistrationDate = new DateTime(2020, 01, 01)
            }
            );  

            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, OrderDate = new DateTime(2026, 03, 01), DeliveryDate  = new DateTime(2026, 04, 01), TotalAmount = 100.0m, Status = "Delivered", SupplierId = 1 },
                new Order { OrderId = 2, OrderDate = new DateTime(2026, 02, 01), DeliveryDate = new DateTime(2026, 02, 06), TotalAmount = 150.0m, Status = "Delivered", SupplierId = 2 },
                new Order { OrderId = 3, OrderDate = new DateTime(2026, 02, 11), DeliveryDate = new DateTime(2026, 02, 16), TotalAmount = 200.0m, Status = "Delivered", SupplierId = 3 },
                new Order { OrderId = 4, OrderDate = new DateTime(2026, 04, 26), DeliveryDate = null, TotalAmount = 80.0m, Status = "Pending", SupplierId = 4 }
                
            );     

            modelBuilder.Entity<MaintenanceLog>().HasData(
                new MaintenanceLog { MaintenanceLogId = 1, Description = "Zamjena motora", MaintenanceDate = new DateTime(2025, 08, 07), Cost = 120, Status = "Završeno", MachineId = 1, TechnicianId = 1 },
                new MaintenanceLog { MaintenanceLogId = 2, Description = "Čišćenje aparata", MaintenanceDate = new DateTime(2025, 12, 08), Cost = 30, Status = "Završeno", MachineId = 2, TechnicianId = 2 },
                new MaintenanceLog { MaintenanceLogId = 3, Description = "Popravka displeja", MaintenanceDate = new DateTime(2026, 05, 09), Cost = 80, Status = "U toku", MachineId = 1, TechnicianId = 1 },
                new MaintenanceLog { MaintenanceLogId = 4, Description = "Zamjena brave", MaintenanceDate = new DateTime(2026, 04, 10), Cost = 50, Status = "Završeno", MachineId = 3, TechnicianId = 3 }
            ); 

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { OrderItemId = 1, Quantity = 10, UnitPrice = 2.5m, SubTotal = 25.0m, OrderId = 1, ProductId = 1 },
                new OrderItem { OrderItemId = 2, Quantity = 5, UnitPrice = 1.5m, SubTotal = 7.5m, OrderId = 2, ProductId = 3 },
                new OrderItem { OrderItemId = 3, Quantity = 8, UnitPrice = 2.0m, SubTotal = 16.0m, OrderId = 3, ProductId = 2 },
                new OrderItem { OrderItemId = 4, Quantity = 3, UnitPrice = 3.5m, SubTotal = 10.5m, OrderId = 4, ProductId = 6 },
                new OrderItem { OrderItemId = 5, Quantity = 6, UnitPrice = 2.2m, SubTotal = 13.2m, OrderId = 2, ProductId = 10 }
            );

            modelBuilder.Entity<Technician>().HasData(
                new Technician { TechnicianId = 1, Name = "Ivan Ivić", LicenseNumber = "LIC001", Email = "ivan@firma.com", PhoneNumber = "061-111-111", HireDate = new DateTime(2019, 01, 10) },
                new Technician { TechnicianId = 2, Name = "Ana Aničić", LicenseNumber = "LIC002", Email = "ana@firma.com", PhoneNumber = "062-222-222", HireDate = new DateTime(2022, 05, 15) },
                new Technician { TechnicianId = 3, Name = "Marko Marković", LicenseNumber = "LIC003", Email = "marko@firma.com", PhoneNumber = "063-333-333", HireDate = new DateTime(2024, 06, 01) }
            );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction { TransactionId = 1, ProductNumberEntered = 1, AmountPaid = 2.5m, TotalPrice = 2.5m, TransactionDate =new DateTime(2026, 05, 12), Status = TransactionStatus.Successful, QuantityDispensed = 1, MachineId = 1, ProductId = 1 },
                new Transaction { TransactionId = 2, ProductNumberEntered = 2, AmountPaid = 1.5m, TotalPrice = 1.5m, TransactionDate = new DateTime(2026, 05, 13), Status = TransactionStatus.Failed, QuantityDispensed = 0, MachineId = 2, ProductId = 3 },
                new Transaction { TransactionId = 3, ProductNumberEntered = 3, AmountPaid = 2.0m, TotalPrice = 2.0m, TransactionDate = new DateTime(2026, 05, 14), Status = TransactionStatus.Successful, QuantityDispensed = 1, MachineId = 1, ProductId = 2 },
                new Transaction { TransactionId = 4, ProductNumberEntered = 4, AmountPaid = 3.5m, TotalPrice = 3.5m, TransactionDate = new DateTime(2026, 05, 15), Status = TransactionStatus.Successful, QuantityDispensed = 1, MachineId = 3, ProductId = 6 }
            );

            modelBuilder.Entity<VendingMachine>().HasData(
                new VendingMachine { MachineId = 1, MachineNumber = 101, Address = "Ilica 15, Zagreb", Capacity = 50, Status = MachineStatus.Active, ManufacturedDate = new DateTime(2022, 06, 01), LastMaintenanceDate = new DateTime(2025, 05, 01), CurrentBalance = 100.0m },
                new VendingMachine { MachineId = 2, MachineNumber = 102, Address = "Avenija Dubrovnik 10, Zagreb", Capacity = 60, Status = MachineStatus.Maintenance, ManufacturedDate = new DateTime(2021, 03, 10), LastMaintenanceDate = new DateTime(2025, 04, 15), CurrentBalance = 200.0m },
                new VendingMachine { MachineId = 3, MachineNumber = 103, Address = "Savska cesta 50, Zagreb", Capacity = 40, Status = MachineStatus.OutOfService, ManufacturedDate = new DateTime(2023, 01, 01), LastMaintenanceDate = new DateTime(2025, 01, 01), CurrentBalance = 50.0m },
                new VendingMachine { MachineId = 4, MachineNumber = 104, Address = "Korzo 5, Rijeka", Capacity = 70, Status = MachineStatus.Active, ManufacturedDate = new DateTime(2020, 01, 01), LastMaintenanceDate = new DateTime(2025, 01, 01), CurrentBalance = 300.0m },
                new VendingMachine { MachineId = 5, MachineNumber = 105, Address = "Trg bana Jelačića 1, Zagreb", Capacity = 55, Status = MachineStatus.Inactive, ManufacturedDate = new DateTime(2019, 01, 01), LastMaintenanceDate = new DateTime(2025, 01, 01), CurrentBalance = 0.0m },
                new VendingMachine { MachineId = 6, MachineNumber = 106, Address = "Riva 8, Split", Capacity = 80, Status = MachineStatus.Active, ManufacturedDate = new DateTime(2018, 01, 01), LastMaintenanceDate = new DateTime(2025, 01, 01), CurrentBalance = 500.0m }
            );
        }

    }
}