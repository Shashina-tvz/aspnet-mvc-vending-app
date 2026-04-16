using Microsoft.AspNetCore.Mvc;
using VendingMachineApp.Data.Entities;
using VendingMachineApp.Data.Repositories;
using System.Linq;

namespace VendingMachineApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly MockOrderRepository _orderRepo;
        private readonly MockOrderItemRepository _orderItemRepo;

        public OrderController(MockOrderRepository orderRepo, MockOrderItemRepository orderItemRepo)
        {
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
        }

        public IActionResult Index(string status)
        {
            var orders = !string.IsNullOrEmpty(status) ? _orderRepo.GetByStatus(status) : _orderRepo.GetAll();
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = _orderRepo.GetById(id);
            if (order == null) return NotFound();
            order.OrderItems = _orderItemRepo.GetAll().Where(oi => oi.OrderId == order.OrderId).ToList();
            return View(order);
        }
    }
}
