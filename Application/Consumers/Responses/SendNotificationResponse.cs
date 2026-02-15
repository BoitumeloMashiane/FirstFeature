using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Consumers.Responses
{
    public class SendNotificationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Recipient { get; set; } = string.Empty;
    }
}
