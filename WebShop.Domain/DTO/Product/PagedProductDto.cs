using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Domain.DTO.Product
{
    public class PagedProductDto
    {
        public IEnumerable<ProductDto> Products { get; set; }

        public int TotalCount { get; set; }

    }
}
