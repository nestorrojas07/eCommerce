using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Order.API.Mappers;
using Order.API.Request;
using Order.Domain.Models;
using Order.Services.Services;
using Shared.Domain.Dtos;
using Shared.Domain.Dtos.Response;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger, OrderService orderService)
        {
            _orderService = orderService;
            _logger = logger;
        }

        
        [HttpPost("")]
        public async Task<ActionResult<OrderModel>> CreateAsync([FromBody] CreateOrderRequest request, [FromServices] IValidator<CreateOrderRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new BadRequestResponse("BadRequest", validationResult.ToDictionary()));
            }

            return Ok(await _orderService.CreateOrderAsync(request.ToModelOrder()));
        }

        [HttpGet("")]
        public async Task<ActionResult<PaginatedData<OrderModel>>> GetOrdersAsync([FromQuery] int pageNumber = 1, [FromQuery]  int pageSize = 10)
        {
            return Ok(await _orderService.GetOrdersAsync(pageNumber, pageSize));
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderModel>> GetOrderByIdAsync([FromRoute] int orderId)
        {
            return Ok(await _orderService.GetOrderByIdAsync(orderId));
        }

        [HttpPut("{orderId}")]
        public async Task<ActionResult<OrderModel>> UpdateOrderAsync([FromRoute] int orderId, [FromBody] UpdateOrderRequest request, [FromServices] IValidator<UpdateOrderRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new BadRequestResponse("BadRequest", validationResult.ToDictionary()));
            }
            return Ok(await _orderService.UpdateOrderAsync(orderId, request.ToModelUpdateOrder()));
        }

        [HttpPut("{orderId}/cancel")]
        public async Task<ActionResult<OrderModel>> CancellOrder([FromRoute] int orderId)
        {
            return Ok(await _orderService.CancellOrder(orderId));
        }

        [HttpPut("{orderId}/approve")]
        public async Task<ActionResult<OrderModel>> ApproveOrderAsync([FromRoute] int orderId)
        {
            return Ok(await _orderService.ApproveOrderAsync(orderId));
        }

        [HttpGet("healt")]
        public async Task<ActionResult<string>> Health()
        {
            return Ok("Ok");
        }

        [HttpGet("{orderId}/items")]
        public async Task<ActionResult<PaginatedData<OrderDetail>>> GetItemAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await _orderService.GetItemAsync(pageNumber, pageSize));
        }

        [HttpPost("{orderId}/items")]
        public async Task<ActionResult<OrderDetail>> AddItemAsync([FromRoute] int orderId, [FromBody] AddOrderDetailRequest request, [FromServices] IValidator<AddOrderDetailRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new BadRequestResponse("BadRequest", validationResult.ToDictionary()));
            }

            return Ok(await _orderService.AddItemAsync(orderId, request.ToAddOrderDetail()));
        }

        [HttpPut("{orderId}/items")]
        public async Task<ActionResult<OrderModel>> UpdateItemAsync([FromRoute] int orderId, [FromBody] UpdateOrderDetailRequest request, [FromServices] IValidator<UpdateOrderDetailRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new BadRequestResponse("BadRequest", validationResult.ToDictionary()));
            }

            var updateDetail = request.ToUpdateOrderDetail();
            return Ok(await _orderService.UpdateItemAsync(orderId, updateDetail));
        }

        [HttpPut("{orderId}/items/{orderDetailId}")]
        public async Task<ActionResult<OrderModel>> RemoveItemAsync([FromRoute] int orderId, [FromRoute] int orderDetailId)
        {
            return Ok(await _orderService.RemoveItemAsync(orderId, orderDetailId));
        }
    }
}
