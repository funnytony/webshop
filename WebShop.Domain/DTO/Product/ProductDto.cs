using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Domain.Entities;
using WebShop.Domain.Entities.Base;

namespace WebShop.Domain.DTO.Product
{
    public class ProductDto : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public string Description { get; set; }//описание товара

        public string FullDescription { get; set; }//подробное описание товара

        public string Appearance { get; set; }//внешний вид

        public int Price { get; set; }//цена

        public bool Sale { get; set; }//распродажа

        public bool New { get; set; }//новые поступления

        public string ImageUrl { get; set; }//изображение

        public EventDto Event { get; set; }//событие

        public SectionDto Section { get; set; }//секции
    }
}
