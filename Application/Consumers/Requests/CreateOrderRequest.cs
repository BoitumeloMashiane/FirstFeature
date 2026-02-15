using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Consumers.Requests
{
    public sealed class CreateOrderRequest
    {
        public Guid CustomerId { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
