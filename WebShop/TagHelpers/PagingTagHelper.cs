using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Domain.Models.Page;

namespace WebShop.TagHelpers
{
    public class PagingTagHelper: TagHelper
    {
        private readonly IUrlHelperFactory _helperFactory;

        public PagingTagHelper(IUrlHelperFactory helperFactory)
        {
            _helperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PageViewModel PageModel { get; set; }
        public string PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = _helperFactory.GetUrlHelper(ViewContext);

            var tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");

            for (var i = 1; i <= PageModel.TotalPages; i++)
            {
                var item = CreateTag(i, urlHelper);
                tag.InnerHtml.AppendHtml(item);
            }

            base.Process(context, output);
            output.Content.AppendHtml(tag);
        }

        private TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper)
        {
            var item = new TagBuilder("li");
            var link = new TagBuilder("a");
            if (pageNumber == PageModel.PageNumber)
            {
                item.AddCssClass("active");
            }
            else
            {
                PageUrlValues["page"] = pageNumber;
                link.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
            }
            link.InnerHtml.Append(pageNumber.ToString());
            item.InnerHtml.AppendHtml(link);
            return item;
        }

    }
}
