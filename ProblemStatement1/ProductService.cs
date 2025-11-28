using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace ProblemStatement1
{
    internal class ProductService
    {
        //private fields
        private readonly IProductRepository _productRepository;

        //constructor
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        //public API
        public List<Product> GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            Console.WriteLine("Available Products:");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Available Quantity: {product.AvailableQuantity}");
            }
            return products;
        }

        public void AddProduct()
        {
            var input = GetProductInput();
            ValidateProduct(input.Id,input.Name,input.Price,input.Quantity);

            //once validated, create product object
            var product = new Product
            {
                Id = int.Parse(input.Id),
                Name = input.Name,
                Price =decimal.Parse( input.Price),
                AvailableQuantity =int.Parse( input.Quantity)
            };
            _productRepository.AddProduct(product);
            Console.WriteLine("Product added successfully.");
        }
        public void UpdateProduct()
        {
            var input = GetProductInput();
            ValidateProduct(input.Id, input.Name, input.Price, input.Quantity);

            //once validated, create product object
            var product = new Product
            {
                Id = int.Parse(input.Id),
                Name = input.Name,
                Price = decimal.Parse(input.Price),
                AvailableQuantity = int.Parse(input.Quantity)
            };
            bool isUpdated = _productRepository.UpdateProduct(product);
            if (isUpdated)
                Console.WriteLine("Product updated successfully.");
            else
                Console.WriteLine("Product not found.");
        }
        public void DeleteProduct(int productId)
        {
            if (productId < 0)
                throw new ArgumentException("ProductId must not be negative.", nameof(productId));

            Console.Write("Enter Product ID to remove: ");
            int id = int.Parse(Console.ReadLine());
            var productResult = _productRepository.DeleteProduct(id);
            if (!productResult)
            {
                Console.WriteLine("Product not found.");
            }
            Console.WriteLine("Product removed successfully.");
        }
        private (string Id, string Name, string Price, string Quantity) GetProductInput()
        {
            Console.Write("Enter Product ID: ");
            string id = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter Product Price: ");
            string price = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter Available Quantity: ");
            string quantity = Console.ReadLine() ?? string.Empty;
            return (id, name, price, quantity);
        }
        private void ValidateProduct(string Id, string Name, string Price, string Quantity)
        {
            //ProductId must not be negative
            if (int.Parse(Id) < 0)
                throw new ArgumentException("ProductId must not be negative.", nameof(Id));

            //Product name must not be empty
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentException("Product name cannot be empty.");
            }
            //Available quantity must not be zero or negative
            if (int.Parse(Quantity) < 0)
            {
                throw new ArgumentException("Available quantity cannot be negative.");
            }
        }

    }
}
