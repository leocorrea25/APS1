using Application.Address;
using Application.Address.Ports;
using Application.User;
using Application.User.Ports;
using Application.User.Request;
using Domain.Order.Entities;
using Domain.Order.Ports;
using Domain.Order.Requests;
using Moq;

namespace ApplicationTest.Tests;

class UserManagerTests
{
    private User _fakeUser;
    private Mock<IUserRepository> _userRepository;
    private Mock<IAddressRepository> _addressRepository;
    private IAddressManager _addressManager;
    private IUserManager _userManager;

    [SetUp]
    public void SetUp()
    {
        _addressRepository = new Mock<IAddressRepository>();
        _addressManager = new AddressManager(_addressRepository.Object);

        _userRepository = new Mock<IUserRepository>();
        _userManager = new UserMenager(
                _userRepository.Object,
                _addressManager);
        
        _fakeUser = new User()
        {
            Id = 123,
            Name = "Zé",
            Email = "ze@gmail.com",
        };
    }

    [TestCase("ze@gmail.com", "P4ssw0rd!123")]
    [TestCase("ze@gmail.com", "Gh78Nk*)a_")]
    public async Task ShouldAuthenticate(string email, string password)
    {
        _fakeUser.Password = password;
        _userRepository.Setup(r => r.GetUserByEmail(email))
                .Returns(Task.FromResult(_fakeUser));

        var user = await _userManager.Authenticate(new LoginRequest
        {
            Username = email,
            Password = password
        });

        Assert.That(user, Is.Not.Null);
        Assert.Multiple(() => {
            Assert.That(user.Id, Is.EqualTo(123));
            Assert.That(user.Email, Is.EqualTo(email));
            Assert.That(user.Token, Is.Not.Empty);
        });
    }

    [Test]
    public async Task ShouldCreateUser()
    {
        var request = new UserRequest
        {
            Name = "John Doe",
            Email = "johndoe@email.com",
            City = "Curitiba",
            IsSeller = false,
            PostalCodeAddress = 81110070,
            Street = "Rua dos Bobos",
            NumberAddress = 123,
            PhoneNumber = "41999998888",
            Password = "P4ssw0rd!123",
        };

        var user = await _userManager.CreateUser(request);

        Assert.That(user, Is.Not.Null);
        Assert.That(user.Name, Is.EqualTo("John Doe"));
    }
}