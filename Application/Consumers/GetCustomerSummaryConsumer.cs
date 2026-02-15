using Application.Consumers.Requests;
using Application.Consumers.Responses;
using MassTransit;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Consumers
{
    public class GetCustomerSummaryConsumer : IConsumer<GetCustomerSummaryRequest>
    {
        private readonly ICustomerService _customerService;
        public GetCustomerSummaryConsumer(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public async Task Consume(ConsumeContext<GetCustomerSummaryRequest> context)
        {
            var summary = await _customerService.GetCustomerSummaryAsync(
              context.Message.CustomerId, context.CancellationToken);

            var response = new GetCustomerSummaryResponse
            {
                CustomerId = summary.CustomerId,
                Name = summary.Name,
                Status = summary.Status
            };
            await context.RespondAsync(response);
        }
    }
}
