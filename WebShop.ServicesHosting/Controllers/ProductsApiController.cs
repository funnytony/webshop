using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Domain.DTO;
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
        public SaveResult AddNew([FromBody]ProductDto model)
        {
            return _productData.AddNew(model);
        }

        [HttpDelete("{id}")]
        public SaveResult Delete(int id)
        {
            return _productData.Delete(id);
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

        [HttpGet("events/{eventId}")]
        public EventDto GetEventById(int eventId)
        {
            return _productData.GetEventById(eventId);
        }

        [HttpPost]
        public PagedProductDto GetProducts([FromBody]ProductFilter filter)
        {
            return _productData.GetProducts(filter);
        }

        [HttpGet("sections")]
        public IEnumerable<SectionDto> GetSections()
        {
            return _productData.GetSections();
        }

        [HttpGet("sections/{sectionId}")]
        public SectionDto GetSectionById(int sectionId)
        {
            return _productData.GetSectionById(sectionId);
        }

        [HttpPut]
        public SaveResult Update([FromBody]ProductDto model)
        {
            return _productData.Update(model);
        }
    }
}