using Application.User.Ports;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Domain.Order.Requests;
using Application.User.Request;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IConfiguration _configuration;

        public UserController(IUserManager userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginRequest loginRequest)
        {
            var userVal = await _userManager.Authenticate(loginRequest);

            if (!userVal.IsValid)
            {
                return Unauthorized();
            }

            var user = userVal.Value;

            // Carregar a chave secreta a partir da configuração
            string? secretKey = _configuration["JwtSettings:SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException(nameof(secretKey), "Secret key cannot be null or empty.");
            }

            // Gerar o token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.IsSeller ? "Seller" : "Buyer")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> CreateUser([FromBody] CreateUserRequest userRequest)
        {
            var userVal = await _userManager.CreateUser(userRequest);
            if (!userVal.IsValid)
            {
                return BadRequest(userVal.Message);
            }

            var user = userVal.Value;

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var userVal = await _userManager.DeleteUser(id);
            if (!userVal.IsValid)
            {
                return NotFound();
            }
            return Ok(userVal.Value);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var usersVal = await _userManager.GetAllUsers();

            if (!usersVal.IsValid)
            {
                return NotFound();
            }

            return Ok(usersVal.Value);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var userVal = await _userManager.GetUser(id);

            if (!userVal.IsValid)
            {
                return NotFound();
            }

            return Ok(userVal.Value);
        }

        [HttpPut()]
        [Authorize]
        public async Task<ActionResult<User>> UpdateUser([FromBody] UpdateUserRequest request)
        {
            Console.WriteLine("Updating user");
            var updatedUserVal = await _userManager.UpdateUser(request);

            if (!updatedUserVal.IsValid)
            {
                return BadRequest(updatedUserVal.Message);
            }
            return Ok(updatedUserVal.Value);
        }
    }
}
