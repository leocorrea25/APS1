using Application.Product;
using Application.Product.Ports;
using Domain.Entities;
using Domain.Ports;
using Domain.Order.Requests;
using Moq;

namespace ApplicationTest.Tests;

class ProductManagerTests
{
    private IQueryable<User> _fakeSellers;
    private IQueryable<Product> _fakeProducts;
    private Mock<IProductRepository> _productRepository;
    private Mock<IUserRepository> _userRepository;
    private IProductManager _productManager;

    [SetUp]
    public void SetUp()
    {
        _fakeSellers = Enumerable.Range(1, 2)
                .Select(i => new User()
                    {
                        Id = i,
                        Name = $"Seller {i}",
                        IsSeller = true
                    })
                .AsQueryable();

        _fakeProducts = Enumerable.Range(1, 10)
                .Select(i => new Product()
                    {
                        Id = i,
                        Name = $"Product {i}",
                        Description = "A nice product",
                        Price = i * 10,
                        Quantity = i * 10,
                        UserId = (i % 2 == 0) ? _fakeSellers.First().Id : _fakeSellers.Last().Id,
                        User = (i % 2 == 0) ? _fakeSellers.First() : _fakeSellers.Last(),
                    })
                .AsQueryable();
        
        _productRepository = new Mock<IProductRepository>();
        _productRepository.Setup(r => r.GetAllProducts())
                .Returns(Task.FromResult<IEnumerable<Product>>([.. _fakeProducts]));
        
        _userRepository = new Mock<IUserRepository>();
        
        _productManager = new ProductManager(
                _productRepository.Object,
                _userRepository.Object);
    }

    [TestCase(1)]
    [TestCase(2)]
    public async Task ShouldCreateProduct(int sellerId)
    {
        var createRequest = new ProductRequest
        {
            Id = sellerId,
            Name = $"Product {sellerId}",
            Description = "Awesome product",
            Price = 9.99m,
            Quantity = 10
        };

        var product = await _productManager.CreateProduct(createRequest, sellerId);

        Assert.That(product, Is.Not.Null);
        Assert.That(product.User, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(product.User.Id, Is.EqualTo(sellerId));
            Assert.That(product.Id, Is.EqualTo(sellerId));
        });
    }

    [TestCase(1)]
    [TestCase(2)]
    public async Task ShouldFetchSellerProducts(int sellerId)
    {
        _userRepository.Setup(r => r.GetUser(sellerId))
                .Returns(Task.FromResult(_fakeSellers.Single(p => p.Id == sellerId)));

        var products = await _productManager.GetProductByUser(sellerId);

        Assert.That(products, Is.Not.Null);
        Assert.Multiple(() => {
            Assert.That(products.Any, Is.True);
            Assert.That(products.Count(), Is.EqualTo(5));
            Assert.That(products.All(p => p.UserId.Equals(sellerId)), Is.True);
        });
    }
}