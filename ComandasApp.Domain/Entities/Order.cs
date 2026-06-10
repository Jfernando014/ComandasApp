using ComandasApp.Domain.Common.Exceptions;

namespace ComandasApp.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public string TableNumber { get; private set; } = string.Empty;
        public string? CustomerName { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public OrderStatus Status { get; private set; }
        
        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        private Order() { } // Para EF Core

        private Order(string tableNumber, string? customerName)
        {
            Id = Guid.NewGuid();
            TableNumber = tableNumber;
            CustomerName = customerName;
            CreatedAt = DateTime.UtcNow;
            Status = OrderStatus.Pending;
        }

        public static Order Create(string tableNumber, string? customerName = null)
        {
            if (string.IsNullOrWhiteSpace(tableNumber))
                throw new DomainException("El número de mesa es obligatorio.");

            return new Order(tableNumber, customerName);
        }

        public void AddItem(Guid productId, int quantity, decimal unitPrice, string? notes = null)
        {
            if (Status != OrderStatus.Pending && Status != OrderStatus.Preparing)
                throw new DomainException("No se pueden agregar productos a un pedido que ya está listo o cancelado.");

            var item = new OrderItem(productId, quantity, unitPrice, notes);
            _items.Add(item);
        }

        public decimal GetTotalAmount()
        {
            return _items.Sum(i => i.GetTotal());
        }

        public void ChangeStatus(OrderStatus newStatus)
        {
            if (Status == OrderStatus.Cancelled || Status == OrderStatus.Delivered)
                throw new DomainException($"No se puede cambiar el estado de un pedido que ya está en '{Status}'.");

            Status = newStatus;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Delivered)
                throw new DomainException("No se puede cancelar un pedido que ya fue entregado.");
                
            Status = OrderStatus.Cancelled;
        }
    }
}
