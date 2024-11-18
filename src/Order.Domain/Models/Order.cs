using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Enums;

namespace Order.Domain.Models;

public class Order
{
    public long Id { get; set; }
    public OrderStatus Status { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public float Total { get; set; }
    public DateTime CreatedAt { get; set; }
}
