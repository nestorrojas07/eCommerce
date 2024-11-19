using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Enums;

namespace Order.API.Request;

public class CreateOrderRequest
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
}
