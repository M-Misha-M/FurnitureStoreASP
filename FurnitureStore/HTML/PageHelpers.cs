using FurnitureStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FurnitureStore.HTML
{
    public  static class PageHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html ,
           InfoPage page , Func<int,string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            for(int i = 1; i <= page.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == page.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }

    }
}