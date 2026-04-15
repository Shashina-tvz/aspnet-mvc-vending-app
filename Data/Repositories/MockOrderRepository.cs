using VendingMachineApp.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineApp.Data.Repositories
{
    public class MockOrderRepository
    {
        private List<Order> orders = new List<Order>
        {
            new Order { OrderId = 1, OrderDate = DateTime.Now.AddDays(-30), DeliveryDate = DateTime.Now.AddDays(-25), TotalAmount = 100.0m, Status = "Delivered", SupplierId = 1 },
            new Order { OrderId = 2, OrderDate = DateTime.Now.AddDays(-20), DeliveryDate = DateTime.Now.AddDays(-15), TotalAmount = 150.0m, Status = "Delivered", SupplierId = 2 },
            new Order { OrderId = 3, OrderDate = DateTime.Now.AddDays(-10), DeliveryDate = DateTime.Now.AddDays(-5), TotalAmount = 200.0m, Status = "Delivered", SupplierId = 3 },
            new Order { OrderId = 4, OrderDate = DateTime.Now.AddDays(-5), DeliveryDate = null, TotalAmount = 80.0m, Status = "Pending", SupplierId = 4 },
            new Order { OrderId = 5, OrderDate = DateTime.Now.AddDays(-2), DeliveryDate = null, TotalAmount = 50.0m, Status = "Pending", SupplierId = 5 }
        };
        public List<Order> GetAll() => orders;
        public List<Order> GetByStatus(string status) => orders.Where(x => x.Status == status).ToList();

        public Order GetById(int id)
        {
            return orders.FirstOrDefault(x => x.OrderId == id) ?? new Order();
        }
    }
}