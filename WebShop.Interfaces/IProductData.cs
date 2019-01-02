using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Domain.DTO;
using WebShop.Domain.DTO.Product;
using WebShop.Domain.Entities;
using WebShop.Models;

namespace WebShop.Interfaces
{
    public interface IProductData
    {
        IEnumerable<ProductDto> GetAll();

        ProductDto GetById(int id);

        SaveResult AddNew(ProductDto model);

        SaveResult Delete(int id);

        SaveResult Update(ProductDto model);

        IEnumerable<SectionDto> GetSections();

        SectionDto GetSectionById(int id);

        IEnumerable<EventDto> GetEvents();

        EventDto GetEventById(int id);

        PagedProductDto GetProducts(ProductFilter filter);

        int GetEventProductCount(int eventId);
    }
}
