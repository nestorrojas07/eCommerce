using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Domain.Contracts.Repositories;
using Order.Domain.Models;
using Order.Services.Dtos;
using Shared.Domain.Dtos.Response;
using Shared.Domain.Exceptions;

namespace Order.Services.Services;

public class OrderService 
{
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(
        IOrderDetailRepository orderDetailRepository,
        IOrderRepository orderRepository , 
        IProductRepository productRepository
        )
    {
        _orderDetailRepository = orderDetailRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }




    #region Order Services
    public async Task<OrderModel> CreateOrderAsync(CreateOrder createOrder)
    {
        var order = new OrderModel()
        {
            CreatedAt = DateTime.Now,
            CustomerName = createOrder.CustomerName,
            CustomerEmail = createOrder.CustomerEmail,
            Status = Shared.Domain.Enums.OrderStatus.Open,
            Total = 0
        };

        return await _orderRepository.SaveAsync(order);

    }

    public async Task<PaginatedData<OrderModel>> GetOrdersAsync(int pageNumber = 0, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        return await _orderRepository.GetAsync(pageNumber, pageSize, cancellationToken);

    }

    public async Task<OrderModel?> GetOrderByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _orderRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<OrderModel> UpdateOrderAsync(long id, UpdateOrder orderupdate, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
        if (order == null)
            throw new KeyNotFoundException($"product {id} not found");

        if (!string.IsNullOrEmpty(orderupdate.CustomerName))
            order.CustomerName = orderupdate.CustomerName;

        if (!string.IsNullOrEmpty(orderupdate.CustomerEmail))
            order.CustomerEmail = orderupdate.CustomerEmail;
       
        await _orderRepository.UpdateAsync(order, cancellationToken);
        await _orderRepository.CalculateTotalAsycn(id, cancellationToken);
        return order;
    }

    public async Task<bool> CancellOrder(long id, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
        if (order == null)
            throw new KeyNotFoundException($"Order {id} not found");

        return await _orderRepository.ChangeStatusAsycn(id, Shared.Domain.Enums.OrderStatus.Cancelled, cancellationToken);
    }

    public async Task<bool> ApproveOrderAsync(long id, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
        if (order == null)
            throw new KeyNotFoundException($"Order {id} not found");
        if (order.Status != Shared.Domain.Enums.OrderStatus.Open)
            throw new DomainException($"Order in a diferent status to Open");

        return await _orderRepository.ChangeStatusAsycn(id, Shared.Domain.Enums.OrderStatus.Approved, cancellationToken);
    }

    #endregion

    #region OrderDetail region

    public async Task<PaginatedData<OrderDetail>> GetItemAsync(long orderId, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
            throw new KeyNotFoundException($"Order {orderId} not found");

        return await _orderDetailRepository.GetAsync(pageNumber, pageSize, cancellationToken);
    }

    public async Task<OrderDetail> AddItemAsync(long orderId, AddOrderDetail orderDetail)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
            throw new KeyNotFoundException($"Order {orderId} not found");

        if (order.Status != Shared.Domain.Enums.OrderStatus.Open)
            throw new DomainException($"Order {orderId} is not Open");

        var product = await _productRepository.GetByIdAsync(orderDetail.ProductId);
        if (product == null)
            throw new KeyNotFoundException($"Order {orderDetail.ProductId} not found");

        var detail = new OrderDetail() {
            OrderId = orderId,
            ProductId = orderDetail.ProductId,
            ProductName = product.Name,
            Quatity = orderDetail.Quatity,
            CreatedAt = DateTime.UtcNow,
            UnitValue = product.Price,
            Total = orderDetail.Quatity * product.Price,
        };
        var model = await _orderDetailRepository.SaveAsync(detail);
        await _orderRepository.CalculateTotalAsycn(orderId);

        return model;
    }

    public async Task<OrderDetail> UpdateItemAsync(long orderId, UpdateOrderDetail orderDetail)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
            throw new KeyNotFoundException($"Order {orderId} not found");

        if (order.Status != Shared.Domain.Enums.OrderStatus.Open)
            throw new DomainException($"Order {orderId} is not Open");


        var detail = await _orderDetailRepository.GetByIdAsync(orderDetail.Id);
        if (detail == null)
            throw new KeyNotFoundException($"item {orderDetail.Id} not found");

        detail.Quatity = orderDetail.Quatity;
        detail.Total = orderDetail.Quatity * detail.UnitValue;

        var model = await _orderDetailRepository.UpdateAsync(detail);
        await _orderRepository.CalculateTotalAsycn(orderId);

        return detail;
    }

    public async Task<bool> RemoveItemAsync(long orderId, long orderDetailId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
            throw new KeyNotFoundException($"Order {orderId} not found");

        if (order.Status != Shared.Domain.Enums.OrderStatus.Open)
            throw new DomainException($"Order {orderId} is not Open");


        var detail = await _orderDetailRepository.GetByIdAsync(orderDetailId);
        if (detail == null)
            throw new KeyNotFoundException($"item {orderDetailId} not found");



        var model = await _orderDetailRepository.DeleteAsync(orderDetailId);
        await _orderRepository.CalculateTotalAsycn(orderId);

        return true;
    }

    #endregion




}
