using Catalog.API.Core.Interfaces;
using Catalog.API.DomainModels;
using Catalog.API.Repositories.Abstraction;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filterName = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
            return await _context.Products.Find(filterName).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filterCategory = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            return await _context.Products.Find(filterCategory).ToListAsync();
        }


        public async Task Create(Product product)
        {
            await _context.Products.InsertOneAsync(product);

        }

        public async Task<bool> Update(Product product)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Product> filterId = Builders<Product>.Filter.Eq(p => p.Id,id);
            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filterId);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
