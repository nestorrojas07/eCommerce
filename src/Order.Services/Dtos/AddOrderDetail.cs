using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Enums;

namespace Order.Domain.Models;

public class AddOrderDetail
{
    public long ProductId { get; set; }
    public int Quatity { get; set; }
}
