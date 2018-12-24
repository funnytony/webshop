using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Domain.DTO.Product;
using WebShop.Domain.Entities;
using WebShop.Models;

namespace WebShop.Interfaces
{
    public interface IProductData
    {
        IEnumerable<ProductDto> GetAll();

        ProductDto GetById(int id);

        void AddNew(ProductDto model);

        void Delete(int id);

        void Update(ProductDto model);

        IEnumerable<SectionDto> GetSections();

        SectionDto GetSectionById(int id);

        IEnumerable<EventDto> GetEvents();

        EventDto GetEventById(int id);

        IEnumerable<ProductDto> GetProducts(ProductFilter filter);

        int GetEventProductCount(int eventId);
    }
}
