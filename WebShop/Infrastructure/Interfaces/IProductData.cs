using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
