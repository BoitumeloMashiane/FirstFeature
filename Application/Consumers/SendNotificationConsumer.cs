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
    public class SendNotificationConsumer : IConsumer<SendNotificationRequest>
    {
        private readonly INotificationService _notificationService;
        private readonly ILogger<SendNotificationConsumer> _logger;

        public SendNotificationConsumer(INotificationService notificationService, ILogger<SendNotificationConsumer> logger)
        {
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SendNotificationRequest> context)
        {
            var request = context.Message;

            _logger.LogInformation("Processing notification for recipient: {Recipient}", request.To);

            try
            {
                await _notificationService.SendAsync(request.To, request.Subject, request.Body);

                var response = new SendNotificationResponse
                {
                    Success = true,
                    Message = "Notification sent!",
                    Recipient = request.To
                };

                await context.RespondAsync(response);

                _logger.LogInformation("Success: Processed notification for {Recipient}", request.To);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: Failed to process notification for {Recipient}", request.To);

                var response = new SendNotificationResponse
                {
                    Success = false,
                    Message = $"Failed to send notification: {ex.Message}",
                    Recipient = request.To
                };

                await context.RespondAsync(response);
            }
        }
    }
}
