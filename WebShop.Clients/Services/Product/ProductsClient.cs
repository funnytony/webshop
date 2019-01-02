using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WebShop.Clients.Base;
using WebShop.Domain.DTO;
using WebShop.Domain.DTO.Product;
using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.Clients.Services.Product
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration configuration) : base(configuration) => ServiceAddress = "api/products";

        protected sealed override string ServiceAddress { get; set; }

        public SaveResult AddNew(ProductDto model)
        {
            var url = $"{ServiceAddress}/product";
            var response = Post(url, model);
            var result = response.Content.ReadAsAsync<SaveResult>().Result;
            return result;
        }

        public SaveResult Delete(int id)
        {
            var url = $"{ServiceAddress}/{id}";
            var response = Delete(url);
            var result = response.Content.ReadAsAsync<SaveResult>().Result;
            return result;
        }

        public IEnumerable<ProductDto> GetAll()
        {
            var url = $"{ServiceAddress}";
            var result = Get<List<ProductDto>>(url);
            return result;
        }

        public ProductDto GetById(int id)
        {
            var url = $"{ServiceAddress}/{id}";
            var result = Get<ProductDto>(url);
            return result;
        }

        public int GetEventProductCount(int eventId)
        {
            var url = $"{ServiceAddress}/event/{eventId}";
            var result = Get<int>(url);
            return result;
        }

        public IEnumerable<EventDto> GetEvents()
        {
            var url = $"{ServiceAddress}/events";
            var result = Get<List<EventDto>>(url);
            return result;
        }

        public EventDto GetEventById(int eventId)
        {
            var url = $"{ServiceAddress}/events/{eventId}";
            var result = Get<EventDto>(url);
            return result;
        }

        public PagedProductDto GetProducts(ProductFilter filter)
        {
            var url = $"{ServiceAddress}";
            var response = Post(url, filter);
            var result = response.Content.ReadAsAsync<PagedProductDto>().Result;
            return result;

        }

        public IEnumerable<SectionDto> GetSections()
        {
            var url = $"{ServiceAddress}/sections";
            var result = Get<List<SectionDto>>(url);
            return result;

        }

        public SectionDto GetSectionById(int sectionId)
        {
            var url = $"{ServiceAddress}/sections/{sectionId}";
            var result = Get<SectionDto>(url);
            return result;
        }

        public SaveResult Update(ProductDto model)
        {
            var url = $"{ServiceAddress}";
            var response = Put(url, model);
            var result = response.Content.ReadAsAsync<SaveResult>().Result;
            return result;
        }
    }
}
