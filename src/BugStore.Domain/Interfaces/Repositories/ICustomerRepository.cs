using BugStore.Domain.Dtos;
using BugStore.Domain.Entities;

namespace BugStore.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<(IEnumerable<BestCustomerResponseDto> Items, int TotalCount)> GetPagedBestCustomers(int page, int pageSize, CancellationToken cancellationToken);
    }
}
