using ComandasApp.Application.Interfaces.Services;
using ComandasApp.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ComandasApp.Services
{
    public class OrderNotificationService : IOrderNotificationService
    {
        private readonly IHubContext<OrderHub> _hubContext;

        public OrderNotificationService(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyOrderCreatedAsync(Guid orderId, string tableNumber)
        {
            // Emite un evento a todos los clientes conectados indicando que hay una nueva orden
            await _hubContext.Clients.All.SendAsync("OrderCreated", orderId, tableNumber);
        }

        public async Task NotifyOrderStatusChangedAsync(Guid orderId, string newStatus)
        {
            // Emite un evento a todos indicando que el estado de una orden cambió (ej. de Pendiente a Preparando)
            await _hubContext.Clients.All.SendAsync("OrderStatusChanged", orderId, newStatus);
        }
    }
}
