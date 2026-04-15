using VendingMachineApp.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineApp.Data.Repositories
{
    public class MockOrderItemRepository
    {
        private List<OrderItem> items = new List<OrderItem>
        {
            new OrderItem { OrderItemId = 1, Quantity = 10, UnitPrice = 2.5m, SubTotal = 25.0m, OrderId = 1, ProductId = 1 },
            new OrderItem { OrderItemId = 2, Quantity = 5, UnitPrice = 1.5m, SubTotal = 7.5m, OrderId = 2, ProductId = 3 },
            new OrderItem { OrderItemId = 3, Quantity = 8, UnitPrice = 2.0m, SubTotal = 16.0m, OrderId = 3, ProductId = 2 },
            new OrderItem { OrderItemId = 4, Quantity = 3, UnitPrice = 3.5m, SubTotal = 10.5m, OrderId = 4, ProductId = 6 },
            new OrderItem { OrderItemId = 5, Quantity = 6, UnitPrice = 2.2m, SubTotal = 13.2m, OrderId = 5, ProductId = 10 }
        };
        public List<OrderItem> GetAll() => items;
        public OrderItem GetByOrderItemId(int id) => items.FirstOrDefault(x => x.OrderItemId == id);

        public OrderItem GetById(int id)
        {
            return items.FirstOrDefault(x => x.OrderItemId == id) ?? new OrderItem();
        }
    }
}