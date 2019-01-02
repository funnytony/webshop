using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;
using WebShop.Interfaces;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class SiteMapController : Controller
    {
        private readonly IProductData _productData;

        public SiteMapController(IProductData productData)
        {
            _productData = productData;
        }

        public IActionResult Index()
        {
            var nodes = new List<SitemapNode>
            {
                new SitemapNode(Url.Action("Index","Home")),
                new SitemapNode(Url.Action("Shop","Catalog")),
                new SitemapNode(Url.Action("BlogSingle","Home")),
                new SitemapNode(Url.Action("Blog","Home")),
                new SitemapNode(Url.Action("ContactUs","Home"))
            };

            var sections = _productData.GetSections();

            foreach (var section in sections)
            {
                if (section.ParentId.HasValue)
                    nodes.Add(
                        new SitemapNode(
                            Url.Action("Shop",
                            "Catalog", new { sectionId = section.Id })
                            ));
                else if(sections.FirstOrDefault(s=>s.ParentId == section.Id)==null)
                {
                    nodes.Add(
                        new SitemapNode(
                            Url.Action("Shop",
                            "Catalog", new { sectionId = section.Id })
                            ));
                }
            }

            var events = _productData.GetEvents();

            foreach (var Event in events)
            {
                nodes.Add(new SitemapNode(Url.Action("Shop", "Catalog", new { eventId = Event.Id })));
            }

            var products = _productData.GetProducts(new ProductFilter()).Products;

            foreach (var productDto in products)
            {
                nodes.Add(new SitemapNode(Url.Action("ProductDetails", "Catalog", new { id = productDto.Id })));
            }

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));

        }
    }
}