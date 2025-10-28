using BugStore.Application.Reports.Requests;
using BugStore.Application.Reports.Responses;
using MediatR;

namespace BugStore.Application.Reports.Handlers
{
    public class RevenueByPeriodRequestHandler : IRequestHandler<RevenueByPeriodRequest, RevenueByPeriodResponse>
    {
        public Task<RevenueByPeriodResponse> Handle(RevenueByPeriodRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
