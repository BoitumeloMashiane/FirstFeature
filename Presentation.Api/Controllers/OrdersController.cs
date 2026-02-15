using Application.Consumers.Requests;
using Application.Consumers.Responses;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IMediator mediator, ILogger<OrdersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received order creation request for {Email}", request.CustomerEmail);

            try
            {
                var client = _mediator.CreateRequestClient<CreateOrderRequest>();
                var response = await client.GetResponse<CreateOrderResponse>(request, cancellationToken);

                return Ok(new
                {
                    orderId = response.Message.OrderId,
                    customerId = response.Message.CustomerId,
                    customerEmail = response.Message.CustomerEmail,
                    productName = response.Message.ProductName,
                    amount = response.Message.Amount,
                    createdAt = response.Message.CreatedAt
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order");
                return StatusCode(500, new { error = "An error occurred creating your order" });
            }
        }
    }
}
