using System;
using System.Collections.Generic;
using System.Text;

namespace ProblemStatement1
{
    internal interface IProductRepository
    {
        List<Product> GetAllProducts();
        void AddProduct(Product product);
        bool DeleteProduct(int productId);
        bool UpdateProduct(Product product);
    }
}
