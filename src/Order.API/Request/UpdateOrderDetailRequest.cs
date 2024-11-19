using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Enums;

namespace Order.API.Request;

public class UpdateOrderDetailRequest
{
    public long Id { get; set; }
    public int Quatity { get; set; }
}
