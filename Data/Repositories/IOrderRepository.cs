using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> SaveAsync(Order order);
        Task<Order> GetByIdAsync(Guid orderId);
    }
}
