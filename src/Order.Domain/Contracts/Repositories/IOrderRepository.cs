using Order.Domain.Models;
using Shared.Domain.Dtos.Response;
using Shared.Domain.Enums;

namespace Order.Domain.Contracts.Repositories;

public interface IOrderRepository
{
    public Task<OrderModel> SaveAsync(OrderModel order, CancellationToken cancellationToken = default);
    public Task<OrderModel?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    public Task<PaginatedData<OrderModel>> GetAsync(int pageNumber = 0, int pageSize = 10, CancellationToken cancellationToken = default);

    public Task<bool> UpdateAsync(OrderModel order, CancellationToken cancellationToken = default);
    public Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);

    public Task<bool> ChangeStatusAsycn(long id,OrderStatus newStatus, CancellationToken cancellationToken = default);
    public Task<float> CalculateTotalAsycn(long id, CancellationToken cancellationToken = default);

}
