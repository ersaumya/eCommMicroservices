using MVCApp.Infrastructure;
using MVCApp.Models;
using MVCApp.Services.Abstraction;
using MVCApp.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MVCApp.Services.Implementation
{
    public class CatalogService: BaseHttpClientWithFactory,ICatalogService
    {
        private readonly IApiSettings _settings;

        public CatalogService(IHttpClientFactory factory, IApiSettings settings)
            : base(factory)
        {
            _settings = settings;
        }

        public async Task<IEnumerable<Catalog>> GetCatalogs()
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                               .SetPath(_settings.CatalogPath)
                               .HttpMethod(HttpMethod.Get)
                               .GetHttpMessage();

            return await SendRequest<IEnumerable<Catalog>>(message);
        }

        public async Task<Catalog> GetCatalog(string id)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                               .SetPath(_settings.CatalogPath)
                               .AddToPath(id)
                               .HttpMethod(HttpMethod.Get)
                               .GetHttpMessage();

            return await SendRequest<Catalog>(message);
        }

        public async Task<IEnumerable<Catalog>> GetCatalogByCategory(string category)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                               .SetPath(_settings.CatalogPath)
                               .AddToPath("GetProductByCategory")
                               .AddToPath(category)
                               .HttpMethod(HttpMethod.Get)
                               .GetHttpMessage();

            return await SendRequest<IEnumerable<Catalog>>(message);
        }

        public async Task<Catalog> CreateCatalog(Catalog catalogModel)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                                .SetPath(_settings.CatalogPath)
                                .HttpMethod(HttpMethod.Post)
                                .GetHttpMessage();

            var json = JsonConvert.SerializeObject(catalogModel);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return await SendRequest<Catalog>(message);
        }
    }
}
