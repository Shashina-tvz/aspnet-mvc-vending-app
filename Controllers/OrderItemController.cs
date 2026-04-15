using Microsoft.AspNetCore.Mvc;
using VendingMachineApp.Data.Entities;
using VendingMachineApp.Data.Repositories;
using System.Linq;

namespace VendingMachineApp.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly MockOrderItemRepository _orderItemRepo;

        public OrderItemController(MockOrderItemRepository orderItemRepo)
        {
            _orderItemRepo = orderItemRepo;
        }

        public IActionResult Index()
        {
            var items = _orderItemRepo.GetAll();
            return View(items);
        }

        public IActionResult Details(int id)
        {
            var item = _orderItemRepo.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }
    }
}
