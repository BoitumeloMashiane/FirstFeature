
using Domain;

namespace Services.Contracts
{
    public interface ICustomerService
    {
        Task<CustomerSummary> GetCustomerSummaryAsync(Guid customerId, CancellationToken cancellationToken = default);
    }
}
