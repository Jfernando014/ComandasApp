namespace ComandasApp.Contracts.Orders
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string TableNumber { get; set; } = string.Empty;
        public string? CustomerName { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }

    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string? Notes { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
