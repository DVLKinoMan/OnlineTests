using OnlineTests.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OnlineTests.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder t = new TagBuilder("a");
                t.MergeAttribute("href", pageUrl(i));
                t.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    t.AddCssClass("selected");
                    t.AddCssClass("btn-primary");
                }
                t.AddCssClass("btn btn-default");
                result.Append(t.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}