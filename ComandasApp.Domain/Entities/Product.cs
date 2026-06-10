using ComandasApp.Domain.Common.Exceptions;

namespace ComandasApp.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public bool IsAvailable { get; private set; }

        private Product() { } // Para EF Core

        private Product(string name, string description, decimal price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            IsAvailable = true;
        }

        public static Product Create(string name, string description, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("El nombre del producto no puede estar vacío.");

            if (price < 0)
                throw new DomainException("El precio del producto no puede ser negativo.");

            return new Product(name, description, price);
        }

        public void UpdateDetails(string name, string description, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("El nombre del producto no puede estar vacío.");

            if (price < 0)
                throw new DomainException("El precio del producto no puede ser negativo.");

            Name = name;
            Description = description;
            Price = price;
        }

        public void ToggleAvailability()
        {
            IsAvailable = !IsAvailable;
        }
    }
}
