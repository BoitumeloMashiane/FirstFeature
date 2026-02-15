namespace Application.Consumers.Requests
{
    public sealed class GetCustomerSummaryRequest
    {
        public Guid CustomerId { get; set; }
    }
}
