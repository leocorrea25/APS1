using Application.Product.Ports;
using Domain.Entities;
using Domain.Order.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductManager productManager) : ControllerBase
    {
        private readonly IProductManager _productManager = productManager;

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductRequest productRequest)
        {
            // Obter o ID do usuário autenticado
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("Usuário não autenticado.");
            }

            if (!int.TryParse(userIdString, out int userId))
            {
                return Unauthorized("ID de usuário inválido.");
            }

            // Passar o ID do usuário para o método CreateProduct
            var product = await _productManager.CreateProduct(productRequest, userId);
            if (product == null)
            {
                return BadRequest("User not found or invalid data.");
            }
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _productManager.DeleteProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _productManager.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productManager.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] ProductRequest product)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("Usuário não autenticado.");
            }

            if (!int.TryParse(userIdString, out int userId))
            {
                return Unauthorized("ID de usuário inválido.");
            }

            if (id != product.Id)
            {
                return BadRequest("Product ID mismatch.");
            }

            var updatedProduct = await _productManager.UpdateProduct(product, userId);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return Ok(updatedProduct);
        }

        [HttpGet("user-products")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Product>>> GetUserProducts()
        {
            // Obter o ID do usuário autenticado
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("Usuário não autenticado.");
            }

            if (!int.TryParse(userIdString, out int userId))
            {
                return Unauthorized("ID de usuário inválido.");
            }

            // Buscar os produtos do usuário
            var products = await _productManager.GetProductByUser(userId);
            if (products == null || !products.Any())
            {
                return NotFound("Nenhum produto encontrado para este usuário.");
            }

            return Ok(products);
        }
    }
}
