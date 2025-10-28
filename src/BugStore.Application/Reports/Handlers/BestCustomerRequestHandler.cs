using BugStore.Application.Reports.Requests;
using BugStore.Application.Reports.Responses;
using MediatR;

namespace BugStore.Application.Reports.Handlers
{
    public class BestCustomerRequestHandler : IRequestHandler<BestCustomerRequest, BestCustomerResponse>
    {
        public Task<BestCustomerResponse> Handle(BestCustomerRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
