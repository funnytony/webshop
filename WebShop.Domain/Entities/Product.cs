using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using WebShop.Domain.Entities.Base;

namespace WebShop.Domain.Entities
{
    public class Product: OrderedEntity
    {
        

        public string Description { get; set; }//описание товара

        public string FullDescription { get; set; }//подробное описание товара

        public string Appearance { get; set; }//внешний вид

        public int Price { get; set; }//цена

        public bool Sale { get; set; }//распродажа

        public bool New { get; set; }//новые поступления

        public string ImageUrl { get; set; }//изображение

        public int? SectionId { get; set; }

        [ForeignKey("SectionId")]
        public virtual Section Section { get; set; }

        public int? EventId { get; set; }

        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }

        public bool IsDelete { get; set; }

    }
}
