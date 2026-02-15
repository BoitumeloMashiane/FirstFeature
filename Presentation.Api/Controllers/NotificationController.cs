using Application.Consumers.Requests;
using Application.Consumers.Responses;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(IMediator mediator, ILogger<NotificationController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] SendNotificationRequest request)
        {

            _logger.LogInformation("Received notification request for {Recipient}", request.To);

            try
            {
                var client = _mediator.CreateRequestClient<SendNotificationRequest>();
                var response = await client.GetResponse<SendNotificationResponse>(request);

                if (response.Message.Success)
                {
                    return Ok(new
                    {
                        success = true,
                        message = response.Message.Message,
                        recipient = response.Message.Recipient
                    });
                }
                else
                {
                    return StatusCode(500, new
                    {
                        success = false,
                        message = response.Message.Message,
                        recipient = response.Message.Recipient
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing notification request");
                return StatusCode(500, new { error = "An error occurred processing your request" });
            }
        }
    }
}
