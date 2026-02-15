using Microsoft.AspNetCore.Mvc;
using Application.Consumers.Requests;
using Application.Consumers.Responses;
using MassTransit.Mediator;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("summary")]
        public async Task<IActionResult> GetSummary(
          [FromQuery] Guid customerId, CancellationToken cancellationToken)
        {
            var request = new GetCustomerSummaryRequest
            {
                CustomerId = customerId
            };
            var client = _mediator.CreateRequestClient<GetCustomerSummaryRequest>();
            var response = await client.GetResponse<GetCustomerSummaryResponse>(request, cancellationToken);
            return Ok(new
            {
                response.Message.CustomerId,
                response.Message.Name,
                response.Message.Status
            });
        }
    }
}
