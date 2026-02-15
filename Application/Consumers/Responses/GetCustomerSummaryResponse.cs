namespace Application.Consumers.Responses
{
    public class GetCustomerSummaryResponse
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
