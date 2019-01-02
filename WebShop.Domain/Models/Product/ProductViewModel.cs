using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebShop.Domain.Entities.Base;
using WebShop.Domain.Models.BreadCrumbs;

namespace WebShop.Models
{
    public class ProductViewModel : OrderedEntity
    {
        public string Description { get; set; }//описание товара

        public string FullDescription { get; set; }//подробное описание товара

        public string Appearance { get; set; }//внешний вид

        [Required, Display(Name = "Цена")]
        public int Price { get; set; }//цена

        public bool Sale { get; set; }//распродажа

        public bool New { get; set; }//новые поступления

        [Required, Display(Name = "Изображение")]
        public string ImageUrl { get; set; }//изображение

        [Required]
        public int? SectionId { get; set; }

        [Display(Name = "Категория")]
        public string Section { get; set; }

        public int? EventId { get; set; }

        [Display(Name = "Событие")]
        public string Event { get; set; }

        public SelectList Sections { get; set; }

        public SelectList Events { get; set; }

        public BreadcrumbHelperViewModel BreadcrumbHelper { get; set; }
    }
}
