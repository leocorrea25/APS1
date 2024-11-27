using Application.Order.Ports;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderManager _orderManager;

        public OrderController(ILogger<OrderController> logger,
            IOrderManager orderManager)
        {
            _logger = logger;
            _orderManager = orderManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Domain.Order.Entities.Order order)
        {
            var response = await _orderManager.CreateOrder(order);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            var response = await _orderManager.GetOrder(orderId);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var response = await _orderManager.GetAllOrders();
            return Ok(response);
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] Domain.Order.Entities.Order order)
        {
            if (orderId != order.Id)
            {
                return BadRequest("Order ID mismatch");
            }

            var response = await _orderManager.UpdateOrder(order);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPatch("{orderId}/complete")]
        public async Task<IActionResult> MarkAsCompleted(int orderId)
        {
            await _orderManager.MarkAsCompleted(orderId);
            return Ok();
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var response = await _orderManager.DeleteOrder(orderId);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
