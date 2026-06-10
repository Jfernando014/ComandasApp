using ComandasApp.Application.Interfaces.Repositories;
using ComandasApp.Domain.Entities;
using HotChocolate.Types;

namespace ComandasApp.GraphQL.Queries
{
    public class OrderQuery
    {
        [UseSorting]
        public async Task<IEnumerable<Order>> GetActiveOrders([Service] IOrderRepository repository, CancellationToken cancellationToken)
        {
            return await repository.GetAllActiveOrdersAsync(cancellationToken);
        }

        public async Task<Order?> GetOrderById(Guid id, [Service] IOrderRepository repository, CancellationToken cancellationToken)
        {
            return await repository.GetByIdAsync(id, cancellationToken);
        }
    }
}
