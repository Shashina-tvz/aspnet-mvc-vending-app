using VendingMachineApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using VendingMachineApp.Data;

using System.Globalization;

using Microsoft.AspNetCore.Identity;
using VendingMachineApp.Data.Entities;

using VendingMachineApp.Data.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

//Razor Pages
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Identity konfiguracija
builder.Services.AddDefaultIdentity<AppUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>();


//Google prijava 
var isTesting = builder.Environment.IsEnvironment("Testing");

if (!isTesting)
    {
        builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            });
    }

builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<SupplierRepository>();
builder.Services.AddScoped<VendingMachineRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<OrderItemRepository>();
builder.Services.AddScoped<TechnicianRepository>();
builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<MaintenanceLogRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

var culture = new CultureInfo("hr-HR"); 

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseHttpsRedirection();
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


// Default route configuration
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DashBoard}/{action=Index}/{id?}");

//MapRazorPages
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    await RoleSeeder.SeedRoles(scope.ServiceProvider);
}

app.Run();

public partial class Program { }
