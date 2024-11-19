using Microsoft.EntityFrameworkCore;
using Order.Domain.Contracts.Repositories;
using Order.Domain.Models;
using Shared.Domain.Dtos.Response;
using Shared.Domain.Enums;
using Shared.Domain.Exceptions;

namespace Order.Infraestructure.Repositories;

public class OrderDetailRepository : IOrderDetailRepository
{

    private readonly OrderContext _context;

    public OrderDetailRepository(OrderContext context)
    {
        _context = context;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var order = await _context.OrderDetails.SingleOrDefaultAsync(e => e.Id == id);
        if (order != null)
        {
            _context.OrderDetails.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }
        return false;
    }

    public async Task<PaginatedData<OrderDetail>> GetAsync(int pageNumber = 1, int pageSize = 100, CancellationToken cancellationToken = default)
    {
        var totalItems = await _context.OrderDetails
           .LongCountAsync();

        var items = await _context.OrderDetails
         .OrderBy(e => e.Id)
         .Skip((pageNumber - 1) * pageSize)
         .Take(pageSize)
         .ToListAsync();

        return new PaginatedData<OrderDetail>(pageNumber, pageSize, totalItems, items);
    }

    public async Task<OrderDetail?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var item = await _context.OrderDetails.SingleOrDefaultAsync(e => e.Id == id);
        if (item != null)
            return item;

        throw new KeyNotFoundException($"Order Detail {id} not found");
    }

    public async Task<OrderDetail> SaveAsync(OrderDetail item, CancellationToken cancellationToken = default)
    {
        item.CreatedAt = DateTime.UtcNow;
        await _context.OrderDetails.AddAsync(item);
        await _context.SaveChangesAsync();

        return item;
    }

    public async Task<bool> UpdateAsync(OrderDetail item, CancellationToken cancellationToken = default)
    {
        _context.OrderDetails.Update(item);
        await _context.SaveChangesAsync();

        return true;
    }
}
