using ComandasApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComandasApp.Infrastructure.Persistence.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.TableNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(o => o.CustomerName)
                .HasMaxLength(100);

            builder.Property(o => o.Status)
                .HasConversion<string>() // Guarda el Enum como texto en la BD
                .IsRequired()
                .HasMaxLength(30);

            // Relación con OrderItem
            builder.HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Metadata.FindNavigation(nameof(Order.Items))!
                   .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
