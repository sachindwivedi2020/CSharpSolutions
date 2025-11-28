using System;
using System.Collections.Generic;
using System.Text;

namespace ProblemStatement1
{
    internal class ProductService 
    {
       private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public List<Product> GetAllProducts()=> _productRepository.GetAllProducts();

        public void AddProduct(Product product)
        {
            ValidateProduct(product);
            _productRepository.AddProduct(product);
        }
        public bool UpdateProduct(Product product)
        {
            ValidateProduct(product);
            return _productRepository.UpdateProduct(product);
        }
        public bool DeleteProduct(int productId)
        {
            if (productId < 0)
                throw new ArgumentException("ProductId must not be negative.", nameof(productId));

            return _productRepository.DeleteProduct(productId);
        }
        // Validates a full Product object (Id, Name, AvailableQuantity)
        private void ValidateProduct(Product product)
        {
            //ProductId must not be negative
            if (product.Id < 0)
                throw new ArgumentException("ProductId must not be negative.", nameof(product.Id));

            //Product name must not be empty
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                throw new ArgumentException("Product name cannot be empty.");
            }
            //Available quantity must not be zero or negative
            if (product.AvailableQuantity < 0)
            {
                throw new ArgumentException("Available quantity cannot be negative.");
            }
        }

    }
}
