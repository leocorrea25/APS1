using Application.Address.Ports;
using Application.User.Ports;
using Application.User.Request;
using Domain.Order.Ports;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Domain.Order.Requests;


namespace Application.User
{
    public class UserMenager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressManager _addressManager;

        public UserMenager(IUserRepository userRepository, IAddressManager addressManager)
        {
            _userRepository = userRepository;
            _addressManager = addressManager;
        }


        public async Task<Domain.Order.Entities.User?> Authenticate(LoginRequest loginRequest)
        {
            // Verifique se o usuário existe e se a senha está correta
            var user = await _userRepository.GetUserByEmail(loginRequest.Username);
            if (user == null || user.Password != loginRequest.Password)
            {
                return null; // Retorne null se as credenciais forem inválidas
            }

            // Defina uma chave secreta temporária
            string secretKey = "minha_chave_secreta_temporaria_123456";
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
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.IsSeller ? "Seller" : "Buyer")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user; // Retorne o usuário com o token JWT
        }
        async Task<Domain.Order.Entities.User> IUserManager.CreateUser(UserRequest request)
        {
            var address = new Domain.Order.Entities.Address
            {
                PostalCode = request.PostalCodeAddress,
                Number = request.NumberAddress,
                Street = request.Street,
                City = request.City
            };
            address = await _addressManager.CreateAddress(address);

            var user = new Domain.Order.Entities.User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                IsSeller = request.IsSeller,
                Address = address,
                AddressId = address.Id
            };

            // Defina uma chave secreta temporária
            string secretKey = "minha_chave_secreta_temporaria_123456";
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
            user.Token = tokenHandler.WriteToken(token);

            // Salvar o usuário no banco de dados
            user = await _userRepository.CreateUser(user);

            return user; // Retorne o usuário com o token JWT
        }

        async Task<Domain.Order.Entities.User> IUserManager.DeleteUser(int userId)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                return null;
            }

            var result = await _userRepository.DeleteUser(userId);
            return result ? user : null;
        }

        async Task<IEnumerable<Domain.Order.Entities.User>> IUserManager.GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        async Task<Domain.Order.Entities.User> IUserManager.GetUser(int userId)
        {
            return await _userRepository.GetUser(userId);
        }

        async Task<Domain.Order.Entities.User> IUserManager.UpdateUser(Domain.Order.Entities.User request)
        {
            return await _userRepository.UpdateUser(request);
        }
    }
}
