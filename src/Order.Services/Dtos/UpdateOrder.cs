using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Enums;

namespace Order.Services.Dtos;

public class UpdateOrder
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
}
