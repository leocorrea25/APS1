using Application.Address.Ports;
using Application.User.Ports;
using Application.User.Request;
using Domain.Ports;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Domain.Order.Requests;
using System.ComponentModel.DataAnnotations;
using Application.Struct;


namespace Application.User
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressManager _addressManager;

        public UserManager(IUserRepository userRepository, IAddressManager addressManager)
        {
            _userRepository = userRepository;
            _addressManager = addressManager;
        }


        private ValidationResult<Domain.Entities.User> Error(Exception ex)
        {
            return new ValidationResult<Domain.Entities.User>(null, false, ex);
        }

        private ValidationResult<Domain.Entities.User> Success(Domain.Entities.User user)
        {
            return new ValidationResult<Domain.Entities.User>(user, true, null);
        }

        private ValidationResult<IEnumerable<Domain.Entities.User>> SuccessList(IEnumerable<Domain.Entities.User> users)
        {
            return new ValidationResult<IEnumerable<Domain.Entities.User>>(users, true, null);
        }


        public async Task<ValidationResult<Domain.Entities.User>> Authenticate(UserLoginRequest loginRequest)
        {
            // Verifique se o usuário existe e se a senha está correta
            var user = await _userRepository.GetUserByEmail(loginRequest.Email);
            if (user == null || user.Password != loginRequest.Password)
            {
                return Error(new ValidationException("Invalid username or password."));
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

            return Success(user); // Retorne o usuário com o token JWT
        }
        async Task<ValidationResult<Domain.Entities.User>> IUserManager.CreateUser(CreateUserRequest request)
        {
            try
            {
                var emailCheck = await _userRepository.GetUserByEmail(request.Email);
                if (emailCheck != null)
                {
                    return Error(new ValidationException("Email already in use."));
                }


                var address = new Domain.Entities.Address
                {
                    PostalCode = request.PostalCodeAddress,
                    Number = request.NumberAddress,
                    Street = request.Street,
                    City = request.City
                };
                address = await _addressManager.CreateAddress(address);

                var user = new Domain.Entities.User
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

                return Success(user); // Retorne o usuário com o token JWT
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        async Task<ValidationResult<Domain.Entities.User>> IUserManager.DeleteUser(int userId)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                return Error(new ValidationException("User not found."));
            }

            var result = await _userRepository.DeleteUser(userId);

            if (!result)
            {
                return Error(new ValidationException("Failed to delete user."));
            }

            return Success(user);
        }

        async Task<ValidationResult<IEnumerable<Domain.Entities.User>>> IUserManager.GetAllUsers()
        {
            var result = await _userRepository.GetAllUsers();

            return SuccessList(result);
        }

        async Task<ValidationResult<Domain.Entities.User>> IUserManager.GetUser(int userId)
        {
            var result = await _userRepository.GetUser(userId);

            if (result == null)
            {
                return Error(new ValidationException("User not found."));
            }

            return Success(result);
        }

        async Task<ValidationResult<Domain.Entities.User>> IUserManager.UpdateUser(UpdateUserRequest request)
        {

            System.Console.WriteLine("UpdateUserRequest: " + request);
            try
            {

                var currentUser = await _userRepository.GetUser(request.Id);

                if (currentUser == null)
                {
                    return Error(new ValidationException("User not found."));
                }

                System.Console.WriteLine("inside the try");
                if (request.Address != null)
                {
                    var address = new Domain.Entities.Address
                    {
                        PostalCode = request.Address.PostalCode,
                        Number = request.Address.Number,
                        Street = request.Address.Street,
                        City = request.Address.City
                    };
                    var newAddress = await _addressManager.CreateAddress(address);

                    currentUser.Address = newAddress;
                }

                currentUser.Name = request.Name ?? currentUser.Name;
                currentUser.Email = request.Email ?? currentUser.Email;
                currentUser.PhoneNumber = request.PhoneNumber ?? currentUser.PhoneNumber;
                currentUser.IsSeller = request.IsSeller ?? currentUser.IsSeller;


                var newUser = await _userRepository.UpdateUser(currentUser);

                if (newUser == null)
                {
                    return Error(new ValidationException("Failed to update user."));
                }

                System.Console.WriteLine("updated repository");

                return Success(newUser);
            }
            catch (Exception ex)
            {
                return Error(ex);

            }
        }
    }
}
