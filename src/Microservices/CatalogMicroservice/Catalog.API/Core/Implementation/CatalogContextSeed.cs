using Catalog.API.DomainModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Core.Implementation
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreConfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPreConfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Name="Iphone 11 pro",
                    Summary="Best flagship phone of 2020",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                    ImageFile="product1.jpeg",
                    Price=117000,
                    Category="Mobile"
                },
                new Product()
                {
                    Name="Samsung s20 ultra",
                    Summary="Best flagship phone of 2020",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                    ImageFile="product2.jpeg",
                    Price=79000,
                    Category="Mobile"
                },
                new Product()
                {
                    Name="Oneplus 8 pro",
                    Summary="Best flagship phone of 2020",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                    ImageFile="product3.jpeg",
                    Price=54000,
                    Category="Mobile"
                },
                new Product()
                {
                    Name="Mi 10",
                    Summary="Best flagship phone of 2020",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                    ImageFile="product4.jpeg",
                    Price=49000,
                    Category="Mobile"
                }

            };
        }
    }
}
