using ComandasApp.Application.Features.Orders.Commands;
using ComandasApp.Domain.Entities;
using MediatR;

namespace ComandasApp.GraphQL.Mutations
{
    public class OrderMutation
    {
        public async Task<Guid> CreateOrder(
            CreateOrderCommand input,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(input, cancellationToken);
        }

        public async Task<bool> UpdateOrderStatus(
            Guid orderId,
            OrderStatus newStatus,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
        {
            await mediator.Send(new UpdateOrderStatusCommand(orderId, newStatus), cancellationToken);
            return true;
        }
    }
}
