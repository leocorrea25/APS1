using Application.Order.Request;
using Application.Order.Responses;
using Application.Order.Ports;
using Domain.Order.Entities;
using Domain.Order.Ports;
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
                IsSeller = i % 2 == 0
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

        System.Console.WriteLine(_fakeOrders.Any());
        System.Console.WriteLine(_fakeProducts.Any());
        System.Console.WriteLine(_fakeUsers.Any());

        _userRepository = new Mock<IUserRepository>();
        foreach (var user in _fakeUsers.ToList())
        {
            _userRepository.Setup(r => r.GetUser(user.Id))
                    .Returns(Task.FromResult(_fakeUsers.Single(u =>
                        u.Id == user.Id)));
        }

        _productRepository = new Mock<IProductRepository>();
        foreach (var product in _fakeProducts.ToList())
        {
            _productRepository.Setup(r => r.GetProduct(product.Id))
                    .Returns(Task.FromResult(_fakeProducts.Single(p =>
                        p.Id == product.Id)));
        }

        _orderRepository = new Mock<IOrderRepository>();
        foreach (var order in _fakeOrders.ToList())
        {
            _orderRepository.Setup(r => r.Get(order.Id))
                    .Returns(Task.FromResult(_fakeOrders.Single(p =>
                        p.Id == order.Id)));
        }

        _orderRepository.Setup(r => r.GetAll())
                .Returns(Task.FromResult(_fakeOrders.AsEnumerable()));

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
            DeliveryOption = "entrega",
            ProductId = productId,
            ProductQuantity = 1,
            AdditionalInstructions = "",
        };

        var order = await _orderManager.CreateOrder(createRequest);

        Assert.That(order, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(order.UserId, Is.EqualTo(userId));
            Assert.That(order.ProductId, Is.EqualTo(productId));
            Assert.That(order.IsCompleted, Is.True);
        });
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

    [TestCase(1, 2)]
    [TestCase(2, 2)]
    public async Task ShouldMarkOrderAsCompleted(int orderId, int userId)
    {
        var result = await _orderManager.MarkAsCompleted(orderId, userId);

        Assert.That(result, Is.True);
    }
}
