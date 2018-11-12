using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop
{
    public static class SampleData
    {
        public static void Initialize(ProductContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {                        
                        Name = "Ягодный торт",
                        Description = "Торт с ягодами на день рождения",
                        FullDescription = "Внутри сочный красный бархат, кремчиз и прослойка из свежей клубники",
                        Appearance = "Торт оформлен свежими ягодами по краю. В центре надпись с поздравлением",
                        Price = 20,
                        Sale = false,
                        New = false,
                        Image = "tort.jpg"
                    },
                    new Product
                    {
                        Name = "Пряники-снежинки",
                        Description = "Имбирные пряники",
                        FullDescription = "Нежные имбирные пряники с сахарной глазурью",
                        Appearance = "Пряники в виде снежинок подаются в коробке на бумажной подложке",
                        Price = 30,
                        Sale = true,
                        New = false,
                        Image = "pryaniki.jpg"
                    },
                    new Product
                    {
                        Name = "Капкейки",
                        Description = "Сладкие капкейки",
                        FullDescription = "Шоколадные с апельсиновым курдом и ванильные со сливочной смородиной",
                        Appearance = "Оформлены кусочками апельсина с шоколадом и мятой, а так же венскими вафлями",
                        Price = 40,
                        Sale = false,
                        New = true,
                        Image = "cupcake.jpg"
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
