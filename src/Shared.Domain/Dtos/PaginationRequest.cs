using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Dtos;

public record PaginationRequest(int PageSize = 10, int PageIndex = 1);
