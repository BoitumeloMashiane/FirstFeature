using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Guid customerId, string customerEmail, string productName, decimal amount);
    }
}
