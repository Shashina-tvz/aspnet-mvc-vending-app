using Microsoft.AspNetCore.Mvc;
using VendingMachineApp.Data.Repositories;

namespace VendingMachineApp.Controllers
{

public class DashboardController : Controller
{
    private readonly MockProductRepository _productRepo;
    private readonly MockOrderRepository _orderRepo;
    private readonly MockTechnicianRepository _techRepo;
    private readonly MockVendingMachineRepository _machineRepo;
    private readonly MockSupplierRepository _supplierRepo;
    private readonly MockMaintenanceLogRepository _logRepo;

    public DashboardController(
        MockProductRepository productRepo,
        MockOrderRepository orderRepo,
        MockTechnicianRepository techRepo,
        MockVendingMachineRepository machineRepo,
        MockSupplierRepository supplierRepo,
        MockMaintenanceLogRepository logRepo)
    {
        _productRepo = productRepo;
        _orderRepo = orderRepo;
        _techRepo = techRepo;
        _machineRepo = machineRepo;
        _supplierRepo = supplierRepo;
        _logRepo = logRepo;
    }

    public IActionResult Index()
    {
        ViewBag.ProductCount = _productRepo.GetAll().Count;
        ViewBag.OrderCount = _orderRepo.GetAll().Count;
        ViewBag.TechCount = _techRepo.GetAll().Count;
        ViewBag.MachineCount = _machineRepo.GetAll().Count;
        ViewBag.SupplierCount = _supplierRepo.GetAll().Count;
ViewBag.MaintenanceCount = _logRepo.GetAll().Count;

        return View();
    }
}
}