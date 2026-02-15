using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public async Task<Customer> GetById(Guid customerId, CancellationToken cancellationToken = default)
        {
            // Static data – placeholder until database is implemented
            var customer = new Customer
            {
                Id = customerId,
                Name = "John Doe",
                Email = "john.doe@example.com"
            };
            return await Task.FromResult(customer);
        }
    }
}
