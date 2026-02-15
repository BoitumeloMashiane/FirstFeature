using Domain;

namespace Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetById(Guid customerId, CancellationToken cancellationToken = default);
    }
}
