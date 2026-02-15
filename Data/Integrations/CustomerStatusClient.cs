using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Integrations
{
    public class CustomerStatusClient : ICustomerStatusClient
    {
        public Task<string> GetStatusAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            // Static response – placeholder for external API
            return Task.FromResult("Active");
        }
    }
}
