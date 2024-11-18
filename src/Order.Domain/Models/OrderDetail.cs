using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Models;

public class OrderDetail
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quatity { get; set; }
    public int UnitValue { get; set; }
    public float Total { get; set; }
    public DateTime CreatedAt { get; set; }
}
