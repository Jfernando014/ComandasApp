using ComandasApp.Application.Interfaces.Repositories;
using ComandasApp.Application.Interfaces.Services;
using ComandasApp.Domain.Entities;
using MediatR;

namespace ComandasApp.Application.Features.Orders.Commands
{
    public record CreateOrderCommand(string TableNumber, string? CustomerName, List<CreateOrderItemInput> Items) : IRequest<Guid>;

    public record CreateOrderItemInput(Guid ProductId, int Quantity, decimal UnitPrice, string? Notes);

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderNotificationService _notificationService;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IOrderNotificationService notificationService)
        {
            _orderRepository = orderRepository;
            _notificationService = notificationService;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Crear entidad
            var order = Order.Create(request.TableNumber, request.CustomerName);

            // Agregar items
            foreach (var item in request.Items)
            {
                order.AddItem(item.ProductId, item.Quantity, item.UnitPrice, item.Notes);
            }

            // Persistir
            await _orderRepository.AddAsync(order, cancellationToken);
            await _orderRepository.SaveChangesAsync(cancellationToken);

            // Emitir evento SignalR
            await _notificationService.NotifyOrderCreatedAsync(order.Id, order.TableNumber);

            return order.Id;
        }
    }
}
