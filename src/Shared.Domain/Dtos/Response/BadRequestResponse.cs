using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Dtos.Response;

public record BadRequestResponse(
    string Status,
    IDictionary<string, string[]> Errors);
