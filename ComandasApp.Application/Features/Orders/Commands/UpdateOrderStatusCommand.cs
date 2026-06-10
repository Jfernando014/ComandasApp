using ComandasApp.Application.Interfaces.Repositories;
using ComandasApp.Application.Interfaces.Services;
using ComandasApp.Domain.Common.Exceptions;
using ComandasApp.Domain.Entities;
using MediatR;

namespace ComandasApp.Application.Features.Orders.Commands
{
    public record UpdateOrderStatusCommand(Guid OrderId, OrderStatus NewStatus) : IRequest;

    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderNotificationService _notificationService;

        public UpdateOrderStatusCommandHandler(IOrderRepository orderRepository, IOrderNotificationService notificationService)
        {
            _orderRepository = orderRepository;
            _notificationService = notificationService;
        }

        public async Task Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order == null)
                throw new DomainException("Pedido no encontrado.");

            order.ChangeStatus(request.NewStatus);

            await _orderRepository.UpdateAsync(order, cancellationToken);
            await _orderRepository.SaveChangesAsync(cancellationToken);

            // Notificar a todos los clientes (ej. a la vista de los meseros o al cliente final)
            await _notificationService.NotifyOrderStatusChangedAsync(order.Id, order.Status.ToString());
        }
    }
}
