using Application.Address;
using Application.Address.Ports;
using Application.User;
using Application.User.Ports;
using Application.User.Request;
using Domain.Entities;
using Domain.Ports;
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
        var mockAddressManager = new Mock<IAddressManager>();
        mockAddressManager.Setup(m => m.CreateAddress(It.IsAny<Address>()))
            .ReturnsAsync(new Address
            {
                Id = 1,
                PostalCode = 81110070,
                Number = 123,
                Street = "Rua dos Bobos",
                City = "Curitiba"
            });

        _addressManager = mockAddressManager.Object;

        _userRepository = new Mock<IUserRepository>();
        _userManager = new UserManager(
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

        var userVal = await _userManager.Authenticate(new UserLoginRequest
        {
            Email = email,
            Password = password
        });

        var user = userVal.Data;

        Assert.That(user, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(user.Id, Is.EqualTo(123));
            Assert.That(user.Email, Is.EqualTo(email));
            Assert.That(user.Token, Is.Not.Empty);
        });
    }

    [Test]
    public async Task ShouldCreateUser()
    {
        var request = new CreateUserRequest
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

        var userVal = await _userManager.CreateUser(request);

        var user = userVal.Data;

        Assert.That(user, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(user.Name, Is.EqualTo("John Doe"));
            Assert.That(user.Email, Is.EqualTo("johndoe@email.com"));
            Assert.That(user.Password, Is.EqualTo("P4ssw0rd!123"));
            Assert.That(user.PhoneNumber, Is.EqualTo("41999998888"));
            Assert.That(user.IsSeller, Is.False);
            Assert.That(user.Address, Is.Not.Null);
            Assert.That(user.Address.Street, Is.EqualTo("Rua dos Bobos"));
            Assert.That(user.Address.City, Is.EqualTo("Curitiba"));
            Assert.That(user.Address.PostalCode, Is.EqualTo(81110070));
            Assert.That(user.Address.Number, Is.EqualTo(123));
            Assert.That(user.Token, Is.Not.Empty);
        });
    }
}