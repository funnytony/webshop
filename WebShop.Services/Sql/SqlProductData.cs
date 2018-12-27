using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.DAL.Context;
using WebShop.Domain.DTO.Product;
using WebShop.Domain.Entities;
using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.Infrastructure.Sql
{
    public class SqlProductData : IProductData
    {
        private readonly WebShopContext _context;

        public SqlProductData(WebShopContext context) => _context = context;

        public void AddNew(ProductDto model)
        {
            _context.Products.Add(new Product()
            {
                Name = model.Name,
                Description = model.Description,
                FullDescription = model.FullDescription,
                Appearance = model.Appearance,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                Order = model.Order,
                New = model.New,
                Sale = model.Sale,
                SectionId = model.Section!=null?model.Section.Id:0,
                EventId = model.Event!=null?model.Event.Id:0                
            });
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Products.Include(p=>p.Section).Include(p=>p.Event).FirstOrDefault(p=>p.Id == id);
            if (product != null) _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public IEnumerable<ProductDto> GetAll()
        {
            return _context.Products.Select(p=> new ProductDto() {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                FullDescription = p.FullDescription,
                Appearance = p.Appearance,
                Price = p.Price,
                Order = p.Order,
                New = p.New,
                Sale = p.Sale,
                ImageUrl = p.ImageUrl,
                Event = p.EventId.HasValue? new EventDto() { Id = p.Event.Id, Name = p.Event.Name}:null,
                Section = p.SectionId.HasValue? new SectionDto() { Id = p.Section.Id, Name = p.Section.Name}: null
            }).ToList();
        }


        public ProductDto GetById(int id)
        {
            return _context.Products.Include(p => p.Event).Include(p => p.Section).Select(p => new ProductDto()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                FullDescription = p.FullDescription,
                Appearance = p.Appearance,
                Price = p.Price,
                Order = p.Order,
                New = p.New,
                Sale = p.Sale,
                ImageUrl = p.ImageUrl,
                Event = p.EventId.HasValue ? new EventDto() { Id = p.Event.Id, Name = p.Event.Name } : null,
                Section = p.SectionId.HasValue ? new SectionDto() { Id = p.Section.Id, Name = p.Section.Name } : null
            }).FirstOrDefault(p => p.Id == id);
        }

        public int GetEventProductCount(int eventId) => _context.Products.Count(p => p.EventId.HasValue && p.EventId == eventId);

        public IEnumerable<EventDto> GetEvents()
        {
            return _context.Events.Select(e => new EventDto() {
                Id = e.Id,
                Name = e.Name,
                Order = e.Order
            }).ToList();
        }

        public EventDto GetEventById(int id)
        {
            var events = _context.Events.FirstOrDefault(e => e.Id == id);
            if (events == null)
                return new EventDto();
            return new EventDto()
            {
                Id = events.Id,
                Name = events.Name,
                Order = events.Order
            };
        }

        public PagedProductDto GetProducts(ProductFilter filter)
        {
            var products = _context.Products.Include("Event").Include(p=>p.Section).AsQueryable();
            if (filter.SectionId.HasValue) products = products.Where(p => p.SectionId.Equals(filter.SectionId));
            if (filter.EventId.HasValue) products = products.Where(p => p.EventId.HasValue && p.EventId.Equals(filter.EventId));
            var model = new PagedProductDto
            {
                TotalCount = products.Count()
            };

            if (filter.PageSize.HasValue)
            {
                model.Products = products.OrderBy(p => p.Order).Skip((filter.Page - 1) * filter.PageSize.Value).Take(filter.PageSize.Value).
                    Select(p => new ProductDto()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        FullDescription = p.FullDescription,
                        Appearance = p.Appearance,
                        Price = p.Price,
                        Order = p.Order,
                        New = p.New,
                        Sale = p.Sale,
                        ImageUrl = p.ImageUrl,
                        Event = p.EventId.HasValue ? new EventDto() { Id = p.Event.Id, Name = p.Event.Name } : null,
                        Section = p.SectionId.HasValue ? new SectionDto() { Id = p.Section.Id, Name = p.Section.Name } : null
                    }).ToList();
            }
            else
            {
                model.Products = products.OrderBy(p => p.Order).
                   Select(p => new ProductDto()
                   {
                       Id = p.Id,
                       Name = p.Name,
                       Description = p.Description,
                       FullDescription = p.FullDescription,
                       Appearance = p.Appearance,
                       Price = p.Price,
                       Order = p.Order,
                       New = p.New,
                       Sale = p.Sale,
                       ImageUrl = p.ImageUrl,
                       Event = p.EventId.HasValue ? new EventDto() { Id = p.Event.Id, Name = p.Event.Name } : null,
                       Section = p.SectionId.HasValue ? new SectionDto() { Id = p.Section.Id, Name = p.Section.Name } : null
                   }).ToList();
            }


                return model;
        }

        public IEnumerable<SectionDto> GetSections()
        {
            return  _context.Sections.Select(s=>new SectionDto() {
                Id = s.Id,
                Name = s.Name,
                Order = s.Order,
                ParentId = s.ParentId
            }).ToList();
        }

        public SectionDto GetSectionById(int id)
        {
            var section = _context.Sections.FirstOrDefault(s => s.Id == id);
            if (section == null)
                return new SectionDto();
            return new SectionDto()
            {
                Id = section.Id,
                Name = section.Name,
                Order = section.Order,
                ParentId = section.ParentId
            };
        }

        public void Update(ProductDto model)
        {
            _context.Products.Update(new Product() {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                FullDescription = model.FullDescription,
                Appearance = model.Appearance,
                Price = model.Price,
                Order = model.Order,
                New = model.New,
                Sale = model.Sale,
                ImageUrl = model.ImageUrl,
                Event = model.Event!=null?new Event() { Id= model.Event.Id, Name = model.Event.Name, Order = model.Event.Order}:null,
                Section = model.Section != null ? new Section() { Id = model.Section.Id, Name = model.Section.Name, Order = model.Section.Order } : null
            });
            _context.SaveChanges();
        }

        

        
    }
}
