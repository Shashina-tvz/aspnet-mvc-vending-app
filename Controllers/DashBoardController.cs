using Microsoft.AspNetCore.Mvc;
using VendingMachineApp.Data.Repositories;

namespace VendingMachineApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ProductRepository _productRepo;
        private readonly OrderRepository _orderRepo;
        private readonly TechnicianRepository _techRepo;
        private readonly VendingMachineRepository _machineRepo;
        private readonly SupplierRepository _supplierRepo;
        private readonly MaintenanceLogRepository _logRepo;

        public DashboardController(
            ProductRepository productRepo,
            OrderRepository orderRepo,
            TechnicianRepository techRepo,
            VendingMachineRepository machineRepo,
            SupplierRepository supplierRepo,
            MaintenanceLogRepository logRepo)
        {
            _productRepo = productRepo;
            _orderRepo = orderRepo;
            _techRepo = techRepo;
            _machineRepo = machineRepo;
            _supplierRepo = supplierRepo;
            _logRepo = logRepo;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ProductCount = (await _productRepo.GetAllAsync()).Count;
            ViewBag.OrderCount = (await _orderRepo.GetAllAsync()).Count;
            ViewBag.TechCount = (await _techRepo.GetAllAsync()).Count;
            ViewBag.MachineCount = (await _machineRepo.GetAllAsync()).Count;
            ViewBag.SupplierCount = (await _supplierRepo.GetAllAsync()).Count;
            ViewBag.MaintenanceCount = (await _logRepo.GetAllAsync()).Count;

            return View();
        }
    }
}