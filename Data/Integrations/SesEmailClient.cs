using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;


namespace Data.Integrations
{
    public class SesEmailClient : IEmailClient
    {
        private readonly ILogger<SesEmailClient> _logger;
        public SesEmailClient(ILogger<SesEmailClient> logger)
        {
            _logger = logger;
        }
        public Task SendAsync(string to, string subject, string body)
        {
            _logger.LogInformation("Sending email via SES to {Recipient}", to);
            // AWS SES integration
            return Task.CompletedTask;
        }
    }
}
