using BugStore.Application.Reports.Responses;
using BugStore.Domain.Dtos;
using MediatR;

namespace BugStore.Application.Reports.Requests
{
    public class BestCustomerRequest : PagedRequestDto, IRequest<PagedResponseDto<IEnumerable<BestCustomerResponseDto>>>;
}
