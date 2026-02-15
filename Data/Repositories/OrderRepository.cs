using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public async Task<Order> SaveAsync(Order order)
        {
            return order;
        }

        public async Task<Order> GetByIdAsync(Guid orderId)
        {
            var order = new Order
            {
                Id = orderId,
                CustomerId = Guid.NewGuid(),
                CustomerEmail = "customer@example.com",
                ProductName = "Sample Product",
                Amount = 20,
                CreatedAt = DateTime.UtcNow
            };

            return order;
        }
    }
}
