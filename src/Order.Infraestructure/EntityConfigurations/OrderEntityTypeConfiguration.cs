using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Enums;
using Order.Domain.Models;

namespace Order.Infraestructure.EntityConfigurations;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Models.OrderModel>
{
    public void Configure(EntityTypeBuilder<Domain.Models.OrderModel> builder)
    {
        builder.ToTable("order");
        builder.Property( o => o.Id)
            .HasColumnName("id")
            .UseIdentityColumn();

        builder.Property<OrderStatus>(o => o.Status)
            .HasConversion<string>();

        builder.Property(o => o.CustomerName)
            .HasColumnName("customer_name")
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(o => o.CustomerEmail)
            .HasColumnName("customer_email")
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(o => o.Total)
            .HasColumnName("total")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(o => o.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.HasMany<OrderDetail>()
            .WithOne()
            .HasForeignKey(od => od.OrderId)
            .IsRequired();
    }
}
