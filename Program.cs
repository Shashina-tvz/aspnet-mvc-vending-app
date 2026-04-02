using VendingMachineApp.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// --- Inicijalizacija podataka za 3 dobavljača i njihove proizvode ---

// SUPPLIER 1: Coca-Cola Bottling (Beverage)
var supplier1 = new Supplier
{
    SupplierId = 1,
    Name = "Coca-Cola Bottling",
    PhoneNumber = "0800-123-456",
    Email = "info@cocacola.hr",
    Address = "Zagreb, Radnička cesta 1",
    ContactPerson = "Ana Kola",
    RegistrationDate = DateTime.Parse("2010-01-15")
};
var product1_1 = new Product { ProductId = 1, Name = "Coca-Cola", Price = 1.5m, Category = ProductCategory.Beverage, Description = "Gazirani napitak", ReorderThreshold = 10, ManufactureDate = DateTime.Now.AddMonths(-2), ExpirationDate = DateTime.Now.AddMonths(10), Supplier = supplier1, SupplierId = supplier1.SupplierId };
var product1_2 = new Product { ProductId = 2, Name = "Sprite", Price = 1.4m, Category = ProductCategory.Beverage, Description = "Gazirani napitak", ReorderThreshold = 10, ManufactureDate = DateTime.Now.AddMonths(-1), ExpirationDate = DateTime.Now.AddMonths(9), Supplier = supplier1, SupplierId = supplier1.SupplierId };
var product1_3 = new Product { ProductId = 3, Name = "Fanta", Price = 1.4m, Category = ProductCategory.Beverage, Description = "Gazirani napitak", ReorderThreshold = 10, ManufactureDate = DateTime.Now.AddMonths(-1), ExpirationDate = DateTime.Now.AddMonths(9), Supplier = supplier1, SupplierId = supplier1.SupplierId };
supplier1.Products = new List<Product> { product1_1, product1_2, product1_3 };

// SUPPLIER 2: Mars Wrigley (Snack)
var supplier2 = new Supplier
{
    SupplierId = 2,
    Name = "Mars Wrigley",
    PhoneNumber = "0800-555-333",
    Email = "info@mars.com",
    Address = "London, Mars Street 5",
    ContactPerson = "Marko Mars",
    RegistrationDate = DateTime.Parse("2015-06-20")
};
var product2_1 = new Product { ProductId = 4, Name = "Snickers", Price = 1.1m, Category = ProductCategory.Snack, Description = "Čokoladica", ReorderThreshold = 10, ManufactureDate = DateTime.Now.AddMonths(-2), ExpirationDate = DateTime.Now.AddMonths(8), Supplier = supplier2, SupplierId = supplier2.SupplierId };
var product2_2 = new Product { ProductId = 5, Name = "Twix", Price = 1.1m, Category = ProductCategory.Snack, Description = "Čokoladica", ReorderThreshold = 10, ManufactureDate = DateTime.Now.AddMonths(-2), ExpirationDate = DateTime.Now.AddMonths(8), Supplier = supplier2, SupplierId = supplier2.SupplierId };
var product2_3 = new Product { ProductId = 6, Name = "Mars", Price = 1.1m, Category = ProductCategory.Snack, Description = "Čokoladica", ReorderThreshold = 10, ManufactureDate = DateTime.Now.AddMonths(-2), ExpirationDate = DateTime.Now.AddMonths(8), Supplier = supplier2, SupplierId = supplier2.SupplierId };
supplier2.Products = new List<Product> { product2_1, product2_2, product2_3 };

// SUPPLIER 3: Hrusty (Chips)
var supplier3 = new Supplier
{
    SupplierId = 3,
    Name = "Hrusty",
    PhoneNumber = "0800-444-555",
    Email = "info@hrusty.hr",
    Address = "Zagreb, Hrusty Lane 7",
    ContactPerson = "Hrvoje Čips",
    RegistrationDate = DateTime.Parse("2017-11-05")
};
var product3_1 = new Product { ProductId = 7, Name = "Hrusty čips classic", Price = 1.6m, Category = ProductCategory.Chips, Description = "Krompir čips", ReorderThreshold = 10, ManufactureDate = DateTime.Now.AddMonths(-2), ExpirationDate = DateTime.Now.AddMonths(7), Supplier = supplier3, SupplierId = supplier3.SupplierId };
var product3_2 = new Product { ProductId = 8, Name = "Hrusty snack", Price = 1.5m, Category = ProductCategory.Chips, Description = "Snack mix", ReorderThreshold = 10, ManufactureDate = DateTime.Now.AddMonths(-2), ExpirationDate = DateTime.Now.AddMonths(7), Supplier = supplier3, SupplierId = supplier3.SupplierId };
var product3_3 = new Product { ProductId = 9, Name = "Hrusty nuts", Price = 1.7m, Category = ProductCategory.Chips, Description = "Kikiriki", ReorderThreshold = 10, ManufactureDate = DateTime.Now.AddMonths(-2), ExpirationDate = DateTime.Now.AddMonths(7), Supplier = supplier3, SupplierId = supplier3.SupplierId };
supplier3.Products = new List<Product> { product3_1, product3_2, product3_3 };

// --- Glavna lista za LINQ upite ---
var suppliers = new List<Supplier> { supplier1, supplier2, supplier3 };
var products = suppliers.SelectMany(s => s.Products ?? new List<Product>()).ToList();
// --- kraj inicijalizacije ---

// --- Primjeri korisnih LINQ upita nad modelom ---

// 1. Najskuplji proizvod
var najskupljiProizvod = products.OrderByDescending(p => p.Price).FirstOrDefault();

if (najskupljiProizvod != null)
    Console.WriteLine($"Najskuplji proizvod: {najskupljiProizvod.Name} ({najskupljiProizvod.Price} EUR)");
else
    Console.WriteLine("Nema proizvoda.");

// 2. Proizvod kojemu je ostalo najmanje do isteka roka
var proizvodNajblizeIsteku = products.OrderBy(p => (p.ExpirationDate - DateTime.Now)).FirstOrDefault();

if (proizvodNajblizeIsteku != null)
    Console.WriteLine($"Proizvod najbliže isteku: {proizvodNajblizeIsteku.Name} (istječe: {proizvodNajblizeIsteku.ExpirationDate:yyyy-MM-dd})");
else
    Console.WriteLine("Nema proizvoda.");

// 3. Supplier koji ima najviše različitih proizvoda
var supplierNajviseProizvoda = suppliers.OrderByDescending(s => (s.Products ?? new List<Product>()).Count).FirstOrDefault();

if (supplierNajviseProizvoda != null)
    Console.WriteLine($"Supplier s najviše proizvoda: {supplierNajviseProizvoda.Name} ({(supplierNajviseProizvoda.Products?.Count ?? 0)} proizvoda)");
else
    Console.WriteLine("Nema dobavljača.");

// 4. Svi proizvodi skuplji od 1.3 EUR
var skupljiProizvodi = products.Where(p => p.Price > 1.3m).ToList();

Console.WriteLine("Proizvodi skuplji od 1.3 EUR:");
foreach (var p in skupljiProizvodi)
    Console.WriteLine($"- {p.Name} ({p.Price} EUR)");
if (!skupljiProizvodi.Any())
    Console.WriteLine("Nema takvih proizvoda.");

// 5. Svi proizvodi sortirani po datumu isteka roka (najprije oni koji istječu uskoro)
var proizvodiPoIsteku = products.OrderBy(p => p.ExpirationDate).ToList();

Console.WriteLine("Proizvodi sortirani po isteku roka:");
foreach (var p in proizvodiPoIsteku)
    Console.WriteLine($"- {p.Name} (istječe: {p.ExpirationDate:yyyy-MM-dd})");
if (!proizvodiPoIsteku.Any())
    Console.WriteLine("Nema proizvoda.");

// 6. Svi dobavljači koji imaju barem jedan proizvod iz kategorije "Beverage"
var suppliersBeverage = suppliers.Where(s => (s.Products ?? new List<Product>()).Any(p => p.Category == ProductCategory.Beverage)).ToList();

Console.WriteLine("Dobavljači s barem jednim pićem:");
foreach (var s in suppliersBeverage)
    Console.WriteLine($"- {s.Name}");
if (!suppliersBeverage.Any())
    Console.WriteLine("Nema takvih dobavljača.");
// --- kraj primjera upita ---

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
