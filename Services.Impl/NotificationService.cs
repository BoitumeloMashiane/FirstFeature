using Microsoft.Extensions.Logging;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Data.Integrations;
using Data.Repositories;

namespace Services.Impl
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailClient _emailClient;
        private readonly ILogger<NotificationService> _logger;
        public NotificationService(IEmailClient emailClient, ILogger<NotificationService> logger)
        {
            _emailClient = emailClient;
            _logger = logger;
        }
        public Task SendAsync(string to, string subject, string body)
        {
            _logger.LogInformation("Sending notification to {Recipient}", to);
            return _emailClient.SendAsync(to, subject, body);
        }

    }
}