using BugStore.Domain.Entities;
using BugStore.Domain.Interfaces.Repositories;

namespace BugStore.Infrastructure.Data.Repositories
{
    public class CustomerRepository(AppDbContext context) : BaseRepository<Customer>(context), ICustomerRepository;
}
