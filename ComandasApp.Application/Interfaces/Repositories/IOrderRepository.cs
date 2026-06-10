using ComandasApp.Domain.Entities;

namespace ComandasApp.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Order>> GetAllActiveOrdersAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Order order, CancellationToken cancellationToken = default);
        Task UpdateAsync(Order order, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
