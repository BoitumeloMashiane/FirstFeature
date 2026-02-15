using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Consumers.Responses
{
    public class CreateOrderResponse
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
