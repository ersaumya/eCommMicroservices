using Catalog.API.DomainModels;
using Catalog.API.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        public Task Create(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProduct(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductByCategory(string CategoryName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
