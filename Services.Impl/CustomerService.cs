using Domain;
using Data.Integrations;
using Data.Repositories;
using Services.Contracts;
using Microsoft.Extensions.Options;

namespace Services.Impl
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerStatusClient _statusClient;

        public CustomerService(
            ICustomerRepository customerRepository,
            ICustomerStatusClient statusClient)
        {
            _customerRepository = customerRepository;
            _statusClient = statusClient;
        }

        public async Task<CustomerSummary> GetCustomerSummaryAsync(
            Guid customerId,
            CancellationToken cancellationToken = default)
        {

            var customer = await _customerRepository.GetById(customerId, cancellationToken);
            var status = await _statusClient.GetStatusAsync(customerId, cancellationToken);

            // Step 4: Build and return domain model
            return new CustomerSummary
            {
                CustomerId = customer.Id,
                Name = customer.Name,
                Status = status
            };
        }
    }
}
