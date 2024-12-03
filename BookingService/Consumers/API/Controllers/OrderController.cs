using Application.Order.Ports;
using Domain.Order.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest order)
        {
            var response = await _orderManager.CreateOrder(order);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("{orderId}")]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> GetAllOrders()
        {
            var response = await _orderManager.GetAllOrders();
            return Ok(response);
        }

        [HttpPut("{orderId}")]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> MarkAsCompleted(int orderId)
        {
            // Obter o usuário autenticado
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                // Tente obter o ID do usuário a partir de outro Claim, se necessário
                userIdString = User.FindFirst("sub")?.Value; // Exemplo de outro Claim comum
            }

            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return Unauthorized("Usuário não autenticado ou ID de usuário inválido.");
            }

            // Passar o usuário como parâmetro
            await _orderManager.MarkAsCompleted(orderId, userId);
            return Ok();
        }

        [HttpDelete("{orderId}")]
        [Authorize]
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
