﻿using Application.Address.Ports;
using Application.Product.Ports;
using Domain.Entities;
using Domain.Ports;
using Domain.Order.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Product
{
    public class ProductManager : IProductManager
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public ProductManager(IProductRepository productRepository, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Product>> GetProductByUser(int id)
        {
            var user = await _userRepository.GetUser(id);
            if (user == null)
            {
                return null;
            }

            if (user.IsSeller == false)
            {
                return null;
            }

            var products = await _productRepository.GetAllProducts();
            return products.Where(p => p.UserId == id);
            
        }

        async Task<Domain.Entities.Product> IProductManager.CreateProduct(ProductRequest productRequest, int UserId)
        {
            var user = await _userRepository.GetUser(UserId);
            if (user == null)
            {
                return null;
            }

            if(user.IsSeller == false)
            {
                return null;
            }

            var product = new Domain.Entities.Product
            {
                Name = productRequest.Name,
                Description = productRequest.Description,
                Price = productRequest.Price,
                Quantity = productRequest.Quantity,
                UserId = UserId
            };

            await _productRepository.CreateProduct(product);

            return product;
        }

        async Task<Domain.Entities.Product> IProductManager.DeleteProduct(int productId)
        {
            var product = await _productRepository.GetProduct(productId);
            if (product == null)
            {
                return null;
            }

            var result = await _productRepository.DeleteProduct(productId);
            return result ? product : null;
        }

        async Task<IEnumerable<Domain.Entities.Product>> IProductManager.GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        async Task<Domain.Entities.Product> IProductManager.GetProduct(int productId)
        {
            return await _productRepository.GetProduct(productId);
        }

        async Task<Domain.Entities.Product> IProductManager.UpdateProduct(ProductRequest product, int userId)
        {
            var existingProduct = await _productRepository.GetProduct(product.Id);
            if (existingProduct == null)
            {
                return null;
            }

            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                return null;
            }

            if (user.IsSeller == false)
            {
                return null;
            }

            if (existingProduct.UserId != userId) 
            {
                return null;
            }

            // Atualize as propriedades do produto existente
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;

            return await _productRepository.UpdateProduct(existingProduct);
        }
    }
}
