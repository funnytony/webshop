using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Domain.Models.BreadCrumbs;
using WebShop.Interfaces;

namespace WebShop.ViewComponents
{
    public class BreadCrumbsViewComponent: ViewComponent
    {
        private readonly IProductData _productData;

        public BreadCrumbsViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        public async Task<IViewComponentResult> InvokeAsync(BreadCrumbType type, int id, BreadCrumbType fromType)
        {
            if (!Enum.IsDefined(typeof(BreadCrumbType), type))
            {
                throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(BreadCrumbType));
            }

            if (!Enum.IsDefined(typeof(BreadCrumbType), fromType))
            {
                throw new InvalidEnumArgumentException(nameof(fromType), (int)fromType, typeof(BreadCrumbType));
            }

            switch (type)
            {
                case BreadCrumbType.Section:
                    var section = _productData.GetSectionById(id);
                    return View(new List<BreadCrumbViewModel>()
                    {
                        new BreadCrumbViewModel()
                        {
                            BreadCrumbType = type,
                            Id = id.ToString(),
                            Name = section.Name
                        }
                    });
                case BreadCrumbType.Event:
                    var brand = _productData.GetEventById(id);
                    return View(new List<BreadCrumbViewModel>()
                    {
                        new BreadCrumbViewModel()
                        {
                            BreadCrumbType = type,
                            Id = id.ToString(),
                            Name = brand.Name
                        }
                    });
                case BreadCrumbType.Item:
                    var crumbs = GetItemBreadCrumbs(id, fromType, type);
                    return View(crumbs);
                case BreadCrumbType.None:
                default:
                    return View(new List<BreadCrumbViewModel>());
            }

        }

        private IEnumerable<BreadCrumbViewModel> GetItemBreadCrumbs(int id, BreadCrumbType fromType, BreadCrumbType type)
        {
            var item = _productData.GetById(id);
            var crumbs = new List<BreadCrumbViewModel>();

            if (fromType == BreadCrumbType.Section)
            {
                crumbs.Add(new BreadCrumbViewModel()
                {
                    BreadCrumbType = fromType,
                    Id = item.Section.Id.ToString(),
                    Name = item.Section.Name
                });
            }
            else
            {
                crumbs.Add(new BreadCrumbViewModel()
                {
                    BreadCrumbType = fromType,
                    Id = item.Event.Id.ToString(),
                    Name = item.Event.Name
                });
            }

            crumbs.Add(new BreadCrumbViewModel()
            {
                BreadCrumbType = type,
                Id = item.Id.ToString(),
                Name = item.Name
            });
            return crumbs;
        }


    }
}
