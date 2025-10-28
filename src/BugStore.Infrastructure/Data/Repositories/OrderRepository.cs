using BugStore.Domain.Entities;
using BugStore.Domain.Interfaces.Repositories;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BugStore.Infrastructure.Data.Repositories
{
    public class OrderRepository(AppDbContext context) : BaseRepository<Order>(context), IOrderRepository
    {
        public async Task<Order?> GetOrderByIdAsNoTrackingWithIncludesAsync(Guid id, CancellationToken cancellationToken)
            => await _dbSet
                .AsNoTracking()
                .Include(x => x.Customer)
                .Include(x => x.Lines)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public override async Task<(IEnumerable<Order> Items, int TotalCount)> GetPagedAsync(
            Expression<Func<Order, bool>> filter,
            int page,
            int pageSize,
            Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy,
            CancellationToken cancellationToken)
        {
            IQueryable<Order> query = _dbSet
                .AsExpandableEFCore()
                .AsNoTracking()
                .Include(x => x.Customer)
                .Include(x => x.Lines)
                .ThenInclude(x => x.Product);

            if (filter != null)
                query = query.Where(filter);

            var totalCount = await query.CountAsync(cancellationToken);

            if (orderBy != null)
                query = orderBy(query);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        public async Task<IEnumerable<Order>> GetPagedOrderAsNotrackingWithIncludesAsync(Expression<Func<Order, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet
                .AsExpandableEFCore()
                .AsNoTracking()
                .Include(x => x.Customer)
                .Include(x => x.Lines)
                .ThenInclude(x => x.Product)
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }
    }
}
