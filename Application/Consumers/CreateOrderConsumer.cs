using Application.Consumers.Requests;
using Application.Consumers.Responses;
using MassTransit;
using Microsoft.Extensions.Logging;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Consumers
{
    public class CreateOrderConsumer : IConsumer<CreateOrderRequest>
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<CreateOrderConsumer> _logger;

        public CreateOrderConsumer(IOrderService orderService, ILogger<CreateOrderConsumer> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CreateOrderRequest> context)
        {
            var request = context.Message;

            _logger.LogInformation("Processing order creation for {Email}", request.CustomerEmail);

            try
            {
                var order = await _orderService.CreateOrderAsync(
                    request.CustomerId,
                    request.CustomerEmail,
                    request.ProductName,
                    request.Amount);

                var response = new CreateOrderResponse
                {
                    OrderId = order.Id,
                    CustomerId = order.CustomerId,
                    CustomerEmail = order.CustomerEmail,
                    ProductName = order.ProductName,
                    Amount = order.Amount,
                    CreatedAt = order.CreatedAt
                };

                await context.RespondAsync(response);

                _logger.LogInformation("Successfully created order {OrderId}", order.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create order for {Email}", request.CustomerEmail);
                throw;
            }
        }
    }
}
