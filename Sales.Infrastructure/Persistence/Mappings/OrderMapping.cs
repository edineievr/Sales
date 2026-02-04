using Microsoft.EntityFrameworkCore;
using Sales.Domain.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Infrastructure.Repositories;

namespace Sales.Infrastructure.Persistence.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");
            builder.HasKey(order => order.Id);
            builder.Property(order => order.Id).ValueGeneratedOnAdd();
            
            builder.Property(order => order.Status).IsRequired();
            builder.Property(order => order.CreationDate).IsRequired();
            builder.Property(order => order.InvoiceDate);
            builder.Property(order => order.CancelationDate);
            
            builder.HasMany(order => order.Items)
                   .WithOne()
                   .HasForeignKey(item => item.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
