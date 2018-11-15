using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Infrastructure.Interfaces;
using WebShop.Models;

namespace WebShop.Infrastructure.Implementations
{
    public class ProductData : IProductData
    {
        ProductContext _db;

        public ProductData(ProductContext context) => _db = context;

        public void AddNew(Product model)
        {
            _db.Products.Add(model);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null) _db.Products.Remove(product);
            _db.SaveChanges();
        }

        public void Update (Product model)
        {
            _db.Products.Update(model);
            _db.SaveChanges();
        }

        public IEnumerable<Product> GetAll() => _db.Products.ToList();


        public Product GetById(int id) => _db.Products.FirstOrDefault(p => p.Id == id);
        
    }
}
