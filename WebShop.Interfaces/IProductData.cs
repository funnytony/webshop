using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Models;

namespace WebShop.Infrastructure.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Product> GetAll();

        Product GetById(int id);

        void AddNew(Product model);

        void Delete(int id);

        void Update(Product model);

        IEnumerable<Section> GetSections();

        IEnumerable<Event> GetEvents();

        IEnumerable<Product> GetProducts(ProductFilter filter);

        int GetEventProductCount(int eventId);
    }
}
