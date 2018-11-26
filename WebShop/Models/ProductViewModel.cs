using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Domain.Entities.Base;

namespace WebShop.Models
{
    public class ProductViewModel : OrderedEntity
    {
        public string Description { get; set; }//описание товара

        public string FullDescription { get; set; }//подробное описание товара

        public string Appearance { get; set; }//внешний вид

        public int Price { get; set; }//цена

        public bool Sale { get; set; }//распродажа

        public bool New { get; set; }//новые поступления

        public string ImageUrl { get; set; }//изображение

        public int? SectionId { get; set; }

        public int? EventId { get; set; }
    }
}
