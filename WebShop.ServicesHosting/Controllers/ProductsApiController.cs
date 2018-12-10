using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Domain.DTO.Product;
using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/products")]
    public class ProductsApiController : Controller, IProductData
    {
        private readonly IProductData _productData;

        public ProductsApiController(IProductData productData)=>_productData = productData;
        
        [HttpPost("product")]
        public void AddNew([FromBody]ProductDto model)
        {
            _productData.AddNew(model);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _productData.Delete(id);
        }

        [HttpGet]
        public IEnumerable<ProductDto> GetAll()
        {
            return _productData.GetAll();
        }


        [HttpGet("{id}")]
        public ProductDto GetById(int id)
        {
            return _productData.GetById(id);
        }

        [HttpGet("event/{eventId}")]
        public int GetEventProductCount(int eventId)
        {
            return _productData.GetEventProductCount(eventId);
        }

        [HttpGet("events")]
        public IEnumerable<EventDto> GetEvents()
        {
            return _productData.GetEvents();
        }

        [HttpPost]
        public IEnumerable<ProductDto> GetProducts([FromBody]ProductFilter filter)
        {
            return _productData.GetProducts(filter);
        }

        [HttpGet("sections")]
        public IEnumerable<SectionDto> GetSections()
        {
            return _productData.GetSections();
        }

        [HttpPut]
        public void Update([FromBody]ProductDto model)
        {
            _productData.Update(model);
        }
    }
}