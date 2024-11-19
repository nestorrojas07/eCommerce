using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Enums;

namespace Order.API.Request;

public class AddOrderDetailRequest
{
    public long ProductId { get; set; }
    public int Quatity { get; set; }
}
