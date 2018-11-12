using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class Product
    {
        public int Id { get; set; }//код товара

        public string Name { get; set; }//название товара

        public string Description { get; set; }//описание товара

        public string FullDescription { get; set; }//подробное описание товара

        public string Appearance { get; set; }//внешний вид

        public int Price { get; set; }//цена

        public bool Sale { get; set; }//распродажа

        public bool New { get; set; }//новые поступления

        public string Image { get; set; }//изображение


    }
}
