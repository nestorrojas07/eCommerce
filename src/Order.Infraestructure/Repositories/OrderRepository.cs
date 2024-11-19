using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Contracts.Repositories;
using Order.Domain.Models;
using Shared.Domain.Dtos.Response;
using Shared.Domain.Enums;
using Shared.Domain.Exceptions;

namespace Order.Infraestructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderContext _context;

    public OrderRepository(OrderContext context)
    {
        _context = context;
    }

    public async Task<float> CalculateTotalAsycn(long id, CancellationToken cancellationToken = default)
    {
        var order = await GetByIdAsync(id);
        if (order == null)
            throw new KeyNotFoundException($"order {id} not found");
        if (order.Status != OrderStatus.Open)
            throw new DomainException("Order not open");

        var total = await _context.OrderDetails.Where(x => x.OrderId == id).SumAsync(x => x.UnitValue * x.Quatity);
        order.Total = total;
        await UpdateAsync(order);

        return total;
    }

    public async Task<bool> ChangeStatusAsycn(long id, OrderStatus newStatus, CancellationToken cancellationToken = default)
    {
        var order = await GetByIdAsync(id);
        if (order == null) 
            throw new KeyNotFoundException($"order {id} not found");
        order.Status = newStatus;
        await UpdateAsync(order);

        return true;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var order = await _context.Orders.SingleOrDefaultAsync(e => e.Id == id);
        if (order != null && order.Status != OrderStatus.Approved)
        {
            order.Status = OrderStatus.Cancelled;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<PaginatedData<OrderModel>> GetAsync(int pageNumber = 0, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var totalItems = await _context.Orders
           .LongCountAsync();

        var items = await _context.Orders
         .OrderBy(e => e.Id)
         .Skip((pageNumber - 1) * pageSize)
         .Take(pageSize)
         .ToListAsync();

        return new PaginatedData<OrderModel>(pageNumber, pageSize, totalItems, items);
    }

    public async Task<OrderModel?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var order = await _context.Orders.SingleOrDefaultAsync(e => e.Id == id);
        if (order != null)
            return order;

        throw new KeyNotFoundException($"Order {id} not found");
    }

    public async Task<OrderModel> SaveAsync(OrderModel order, CancellationToken cancellationToken = default)
    {
        order.CreatedAt = DateTime.UtcNow;
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<bool> UpdateAsync(OrderModel order, CancellationToken cancellationToken = default)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();

        return true;
    }
}
