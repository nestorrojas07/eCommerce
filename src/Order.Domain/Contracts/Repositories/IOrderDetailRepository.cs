using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Domain.Models;
using Shared.Domain.Dtos.Response;

namespace Order.Domain.Contracts.Repositories;

public interface IOrderDetailRepository
{
    public Task<OrderDetail> SaveAsync(OrderDetail item, CancellationToken cancellationToken = default);
    public Task<OrderDetail?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    public Task<PaginatedData<OrderDetail>> GetAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    public Task<bool> UpdateAsync(OrderDetail item, CancellationToken cancellationToken = default);
    public Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
}
