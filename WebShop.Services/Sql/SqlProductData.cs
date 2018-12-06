using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.DAL.Context;
using WebShop.Domain.Entities;
using WebShop.Infrastructure.Interfaces;
using WebShop.Models;

namespace WebShop.Infrastructure.Sql
{
    public class SqlProductData : IProductData
    {
        private readonly WebShopContext _context;

        public SqlProductData(WebShopContext context) => _context = context;

        public void AddNew(Product model)
        {
            _context.Products.Add(model);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null) _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAll() => _context.Products.ToList();
        

        public Product GetById(int id) => _context.Products.Include(p=>p.Event).Include(p=>p.Section).FirstOrDefault(p => p.Id == id);        

        public int GetEventProductCount(int eventId) => _context.Products.Count(p => p.EventId.HasValue && p.EventId == eventId);        

        public IEnumerable<Event> GetEvents() => _context.Events.ToList();        

        public IEnumerable<Product> GetProducts(ProductFilter filter)
        {
            var products = _context.Products.Include("Event").Include(p=>p.Section).AsQueryable();
            if (filter.SectionId.HasValue) products = products.Where(p => p.SectionId.Equals(filter.SectionId));
            if (filter.EventId.HasValue) products = products.Where(p => p.EventId.HasValue && p.EventId.Equals(filter.EventId));
            return products.ToList();
        }

        public IEnumerable<Section> GetSections() => _context.Sections.ToList();        

        public void Update(Product model)
        {
            _context.Products.Update(model);
            _context.SaveChanges();
        }
    }
}
