using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Order.Infraestructure.EntityConfigurations;

namespace Order.Infraestructure;

public class OrderContext : DbContext
{
    public OrderContext(DbContextOptions<OrderContext> options, IConfiguration configuration) : base(options)
    {
    }

    public DbSet<Order.Domain.Models.OrderModel> Orders { get; set; }
    public DbSet<Order.Domain.Models.OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new OrderEntityTypeConfiguration());

        builder.ApplyConfiguration(new OrderDetailEntityTypeConfiguration());
    }


}
