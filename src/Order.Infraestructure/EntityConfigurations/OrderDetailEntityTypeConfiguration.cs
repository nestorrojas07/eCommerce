using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Enums;

namespace Order.Infraestructure.EntityConfigurations;

public class OrderDetailEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Models.OrderDetail>
{
    public void Configure(EntityTypeBuilder<Domain.Models.OrderDetail> builder)
    {
        builder.ToTable("order_detail");

        builder.Property(o => o.Id)
            .HasColumnName("id")
            .UseIdentityColumn();

        builder
            .Property(o => o.OrderId)
            .HasColumnName("order_id")
            .IsRequired();
        builder.HasIndex(o => o.OrderId).IsDescending(false);

        builder.Property(o => o.ProductId)
            .HasColumnName("product_id")
            .IsRequired();

        builder.Property(o => o.ProductName)
            .HasColumnName("product_name")
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(o => o.UnitValue)
            .HasColumnName("unit_value")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(o => o.Total)
            .HasColumnName("total")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(o => o.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();
    }
}
