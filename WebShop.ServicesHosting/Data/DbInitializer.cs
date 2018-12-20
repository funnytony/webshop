using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.DAL.Context;
using WebShop.Domain.Entities;

namespace WebShop.Data
{
    public static class DbInitializer
    {
        public static void Initialize(WebShopContext context)
        {
            context.Database.EnsureCreated();
            

            if (!context.Sections.Any())
            {
                var sections = new List<Section>(){
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
                using (var trans = context.Database.BeginTransaction())
                {
                    foreach (var section in sections)
                    {
                        context.Sections.Add(section);
                    }

                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Sections] ON");
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Sections] OFF");
                    trans.Commit();
                }
            }

            if (!context.Events.Any())
            {
                var events = new List<Event>() {

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
                using (var trans = context.Database.BeginTransaction())
                {
                    foreach (var event1 in events)
                    {
                        context.Events.Add(event1);
                    }

                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Events] ON");
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Events] OFF");
                    trans.Commit();
                }
            }

            if (!context.Products.Any())
            {
                var products = new List<Product>() {
                new Product
                    {
                        Id = 1,
                        Name = "Ягодный торт",
                        Description = "Торт с ягодами на день рождения",
                        FullDescription = "Внутри сочный красный бархат, кремчиз и прослойка из свежей клубники",
                        Appearance = "Торт оформлен свежими ягодами по краю. В центре надпись с поздравлением",
                        Price = 20,
                        Sale = false,
                        New = false,
                        ImageUrl = "tort.jpg",
                        Order = 1,
                        SectionId = 2,
                        EventId = 1
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Пряники-снежинки",
                        Description = "Имбирные пряники",
                        FullDescription = "Нежные имбирные пряники с сахарной глазурью",
                        Appearance = "Пряники в виде снежинок подаются в коробке на бумажной подложке",
                        Price = 30,
                        Sale = true,
                        New = false,
                        ImageUrl = "pryaniki.jpg",
                        Order = 1,
                        SectionId = 6
                    },
                    new Product
                    {
                        Id = 3,
                        Name = "Капкейки",
                        Description = "Сладкие капкейки",
                        FullDescription = "Шоколадные с апельсиновым курдом и ванильные со сливочной смородиной",
                        Appearance = "Оформлены кусочками апельсина с шоколадом и мятой, а так же венскими вафлями",
                        Price = 40,
                        Sale = false,
                        New = true,
                        ImageUrl = "cupcake.jpg",
                        Order = 1,
                        SectionId = 5
                    }
            };

                using (var trans = context.Database.BeginTransaction())
                {
                    foreach (var product in products)
                    {
                        context.Products.Add(product);
                    }

                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Products] ON");
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Products] OFF");
                    trans.Commit();
                }
            }

        }
    }
}
