using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OnlineTests.WebUI.HtmlHelpers
{
    public static class LevelHelpers
    {
        public static MvcHtmlString LavelLinks(this HtmlHelper html, int? selectedLevel, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= 10; i++)
            {
                TagBuilder t = new TagBuilder("a");
                t.MergeAttribute("href", pageUrl(i));
                t.InnerHtml = i.ToString();
                if (i == (int)selectedLevel)
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