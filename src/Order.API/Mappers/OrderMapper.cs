using Order.API.Request;
using Order.Domain.Models;
using Order.Services.Dtos;

namespace Order.API.Mappers;

public static class OrderMapper
{
    public static CreateOrder ToModelOrder(this CreateOrderRequest request)
    {
        return new CreateOrder
        {
            CustomerEmail = request.CustomerEmail,
            CustomerName = request.CustomerName,
        };
    }

    public static UpdateOrder ToModelUpdateOrder(this UpdateOrderRequest request)
    {
        return new UpdateOrder
        {
            CustomerEmail = request.CustomerEmail,
            CustomerName = request.CustomerName,
        };
    }

    public static AddOrderDetail ToAddOrderDetail(this AddOrderDetailRequest request)
    {
        return new AddOrderDetail
        {
            ProductId = request.ProductId, 
            Quatity = request.Quatity,
        };
    }
    
    public static UpdateOrderDetail ToUpdateOrderDetail(this UpdateOrderDetailRequest request)
    {
        return new UpdateOrderDetail
        {
            Id= request.Id,
            Quatity = request.Quatity
        };
    }
}
