using VendingMachineApp.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<MockProductRepository>();
builder.Services.AddSingleton<MockSupplierRepository>();
builder.Services.AddSingleton<MockVendingMachineRepository>();
builder.Services.AddSingleton<MockOrderRepository>();
builder.Services.AddSingleton<MockOrderItemRepository>();
builder.Services.AddSingleton<MockTechnicianRepository>();
builder.Services.AddSingleton<MockTransactionRepository>();
builder.Services.AddSingleton<MockMaintenanceLogRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DashBoard}/{action=Index}/{id?}");

app.Run();
