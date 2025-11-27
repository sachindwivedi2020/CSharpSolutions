using System;
using System.Collections.Generic;
using System.Text;

namespace ProblemStatement1
{
    internal class ProductRepository
    {
        static List<Product> products = new List<Product>()
        {
            new Product { Id = 1, Name = "Laptop", Price = 999.99m, AvailableQuantity = 10 },
            new Product { Id = 2, Name = "Smartphone", Price = 499.99m, AvailableQuantity = 25 },
            new Product { Id = 3, Name = "Tablet", Price = 299.99m, AvailableQuantity = 15 },
            new Product { Id = 4, Name = "Headphones", Price = 89.99m, AvailableQuantity = 50 },
            new Product { Id = 5, Name = "Smartwatch", Price = 199.99m, AvailableQuantity = 30 }
        };
        public List<Product> GetAllProducts()
        {
            return products;
        }
        public void AddProduct(Product product)
        {
            products.Add(product);
        }
        public void DeleteProduct(int productId)
        {
            var product = products.Find(p => p.Id == productId);
            if (product != null)
            {
                products.Remove(product);
            }
        }
        public bool UpdateProduct(Product product)
        {
            // Find the existing product by Id
            var existingProduct = products.Find(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                // Update the properties
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.AvailableQuantity = product.AvailableQuantity;
                return true;
            }
            return false;
        }
    }
}
