using Data.Repositories;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Tests.Fakes
{
    public class FakeOrderRepository : IOrderRepository
    {
        public List<Order> SavedOrders { get; } = new();

        public Task<Order> SaveAsync(Order order)
        {
            SavedOrders.Add(order);
            return Task.FromResult(order);
        }

        public Task<Order> GetByIdAsync(Guid orderId)
        {
            var order = SavedOrders.FirstOrDefault(o => o.Id == orderId);
            return Task.FromResult(order);
        }
    }
}
