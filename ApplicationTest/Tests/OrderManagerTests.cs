using Application.Order.Request;
using Application.Order.Responses;
using Application.Order.Ports;
using Domain.Entities;
using Domain.Ports;
using Moq;
using Application.Order;

namespace ApplicationTest.Tests;

[TestFixture]
class OrderManagerTests
{
    private IQueryable<User> _fakeUsers;
    private IQueryable<Product> _fakeProducts;
    private IQueryable<Order> _fakeOrders;
    private Mock<IUserRepository> _userRepository;
    private Mock<IProductRepository> _productRepository;
    private Mock<IOrderRepository> _orderRepository;
    private Mock<IAddressRepository> _addressRepository;
    private IOrderManager _orderManager;

    [SetUp]
    public void SetUp()
    {
        _fakeUsers = Enumerable.Range(1, 2)
            .Select(i => new User
            {
                Id = i,
                Name = $"User {i}",
                IsSeller = false
            }).AsQueryable();

        _fakeProducts = Enumerable.Range(1, 5)
            .Select(i => new Product
            {
                Id = i,
                Name = $"Product {i}",
                Description = "Awesome product",
                Quantity = 10,
            }).AsQueryable();

        _fakeOrders = Enumerable.Range(1, 9)
            .Select(i => new Order
            {
                Id = i,
                UserId = (i % 2 == 0) ? _fakeUsers.First().Id : _fakeUsers.Last().Id,
                User = (i % 2 == 0) ? _fakeUsers.First() : _fakeUsers.Last(),
                ProductId = i / 2 + 1,
                Product = _fakeProducts.ElementAt(i / 2)
            }).AsQueryable();

        _userRepository = new Mock<IUserRepository>();
        foreach (var user in _fakeUsers.ToList())
        {
            _userRepository.Setup(r => r.GetUser(user.Id))
                    .ReturnsAsync(_fakeUsers.Single(u => u.Id == user.Id));
        }

        _productRepository = new Mock<IProductRepository>();
        foreach (var product in _fakeProducts.ToList())
        {
            _productRepository.Setup(r => r.GetProduct(product.Id))
                    .ReturnsAsync(_fakeProducts.Single(p => p.Id == product.Id));
        }

        _orderRepository = new Mock<IOrderRepository>();
        foreach (var order in _fakeOrders.ToList())
        {
            _orderRepository.Setup(r => r.Get(order.Id))
                    .ReturnsAsync(_fakeOrders.Single(p => p.Id == order.Id));
        }

        _orderRepository.Setup(r => r.GetAll())
                .ReturnsAsync(_fakeOrders.AsEnumerable());

        _addressRepository = new Mock<IAddressRepository>();

        _orderManager = new OrderManager(
            _orderRepository.Object,
            _userRepository.Object,
            _addressRepository.Object,
            _productRepository.Object
        );
    }

    [TestCase(1, 1)]
    [TestCase(2, 2)]
    public async Task ShouldCreateOrder(int userId, int productId)
    {
        var createRequest = new OrderRequest
        {
            UserId = userId,
            DeliveryOption = "retirada",
            ProductId = productId,
            ProductQuantity = 1,
            AdditionalInstructions = "sim",
        };

        var order = await _orderManager.CreateOrder(createRequest);

        Assert.That(order, Is.Not.Null);
    }

    [TestCase(1)]
    public async Task ShouldFetchOrderByUser(int userId)
    {
        var orders = await _orderManager.GetOrdertByUser(userId);

        Assert.That(orders, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(orders.Any(), Is.True);
            Assert.That(orders.All(o => o.UserId == userId), Is.True);
        });
    }
}
