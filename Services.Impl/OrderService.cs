using Domain;
using Microsoft.Extensions.Logging;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Impl
{
    public class OrderService : IOrderService
    {
        private readonly INotificationService _notifications;
        private readonly ILogger<OrderService> _logger;

        public OrderService(INotificationService notifications, ILogger<OrderService> logger)
        {
            _notifications = notifications;
            _logger = logger;
        }

        public async Task<Order> CreateOrderAsync(Guid customerId, string customerEmail, string productName, decimal amount)
        {
            _logger.LogInformation("Creating order for {Email}", customerEmail);

            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                CustomerEmail = customerEmail,
                ProductName = productName,
                Amount = amount,
                CreatedAt = DateTime.UtcNow
            };

            await _notifications.SendAsync(
                customerEmail,
                "Order Confirmation",
                $"Your order for {productName} (${amount}) has been created.");

            _logger.LogInformation("Order {OrderId} created and notification sent to {Email}", order.Id, customerEmail);

            return order;
        }
    }
}
