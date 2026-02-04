using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain.Orders.Entities;

namespace Sales.Infrastructure.Persistence.Mappings;

public class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("orderitems");
        builder.HasKey(item => item.Id);
        builder.Property(item => item.Id).ValueGeneratedOnAdd();

        builder.Property(item => item.OrderId).IsRequired();
        builder.Property(item => item.ProductId).IsRequired();
        builder.Property(item => item.Quantity)
               .IsRequired()
               .HasPrecision(10, 2);
        builder.Property(item => item.UnitPrice).IsRequired();
        builder.HasIndex(item => item.OrderId);
    }
}