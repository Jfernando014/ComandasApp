using ComandasApp.Application.Interfaces.Repositories;
using ComandasApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComandasApp.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ComandasDbContext _context;

        public OrderRepository(ComandasDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
        {
            await _context.Orders.AddAsync(order, cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetAllActiveOrdersAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.Status != OrderStatus.Delivered && o.Status != OrderStatus.Cancelled)
                .OrderBy(o => o.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
        {
            _context.Orders.Update(order);
            return Task.CompletedTask;
        }
    }
}
