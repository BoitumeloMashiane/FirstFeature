using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Integrations
{
    public interface ICustomerStatusClient
    {
        Task<string> GetStatusAsync(Guid customerId, CancellationToken cancellationToken = default);
    }
}
