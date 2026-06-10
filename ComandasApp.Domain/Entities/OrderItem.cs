using ComandasApp.Domain.Common.Exceptions;

namespace ComandasApp.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; } = null!;
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public string? Notes { get; private set; }

        private OrderItem() { } // Para EF Core

        internal OrderItem(Guid productId, int quantity, decimal unitPrice, string? notes)
        {
            if (quantity <= 0)
                throw new DomainException("La cantidad debe ser mayor a cero.");

            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Notes = notes;
        }

        public decimal GetTotal() => Quantity * UnitPrice;
    }
}
