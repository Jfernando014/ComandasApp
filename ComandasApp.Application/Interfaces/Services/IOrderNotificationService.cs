namespace ComandasApp.Application.Interfaces.Services
{
    public interface IOrderNotificationService
    {
        Task NotifyOrderCreatedAsync(Guid orderId, string tableNumber);
        Task NotifyOrderStatusChangedAsync(Guid orderId, string newStatus);
    }
}
