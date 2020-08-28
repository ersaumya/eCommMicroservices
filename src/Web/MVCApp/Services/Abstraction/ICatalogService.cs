using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Services.Abstraction
{
    public interface ICatalogService
    {
        Task<IEnumerable<Catalog>> GetCatalogs();
        Task<IEnumerable<Catalog>> GetCatalogByCategory(string category);
        Task<Catalog> GetCatalog(string id);
        Task<Catalog> CreateCatalog(Catalog model);
    }
}
