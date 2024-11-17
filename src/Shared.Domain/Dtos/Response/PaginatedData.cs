using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Dtos.Response;

public class PaginatedData<TEntity>(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data) where TEntity : class
{

    public int PageIndex =>  pageIndex;

    public int PageSize => pageSize;

    public long Count => count;

    public IEnumerable<TEntity> Data => data;
}
