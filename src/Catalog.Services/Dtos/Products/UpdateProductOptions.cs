using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Services.Dtos.Products;

public record UpdateProductOptions(
    string? Name,
    string? Description,
    float? Price,
    bool? IsActive
);
