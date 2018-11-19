using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Infrastructure.Interfaces;
using WebShop.Models;

namespace WebShop.Infrastructure.Implementations
{
    public class ProductData : IProductData
    {
        ProductContext _db;

        private readonly List<Section> _sections = new List<Section>{
            new Section()
                {
                    Id = 1,
                    Name = "Cake",
                    Order = 0,
                    ParentId = null
                },
                new Section()
                {
                    Id = 2,
                    Name = "Birthday",
                    Order = 0,
                    ParentId = 1
                },
                new Section()
                {
                    Id = 3,
                    Name = "Halloween",
                    Order = 1,
                    ParentId = 1
                },
                new Section()
                {
                    Id = 4,
                    Name = "Wedding",
                    Order = 2,
                    ParentId = 1
                },                
                new Section()
                {
                    Id = 5,
                    Name = "Cupcake",
                    Order = 1,
                    ParentId = null
                },
                new Section()
                {
                    Id = 6,
                    Name = "Gingerbread",
                    Order = 2,
                    ParentId = null
                }
                

            };

        private readonly List<Event> _events = new List<Event> {

            new Event
            {
                Id = 1,
                Name = "Birthday",
                Order = 0
            },

            new Event
            {
                Id = 2,
                Name = "Helloween",
                Order = 1
            },

            new Event
            {
                Id = 3,
                Name = "Wedding",
                Order = 2
            }
        };

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

        public IEnumerable<Section> GetSections() => _sections;


        public IEnumerable<Event> GetEvents() => _events;

        public IEnumerable<Product> GetProducts(ProductFilter filter)
        {
            var products = GetAll();
            if (filter.SectionId.HasValue) products = products.Where(p => p.SectionId.Equals(filter.SectionId)).ToList();
            if (filter.EventId.HasValue) products = products.Where(p =>p.EventId.HasValue && p.EventId.Equals(filter.EventId)).ToList();
            return products;
        }
    }
}
