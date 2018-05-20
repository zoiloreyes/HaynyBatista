using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using HaynyBatista.Models;

namespace HaynyBatista.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
                                              PagingInfo pagingInfo,
                                              Func<int,string> pageUrl)
        {
            StringBuilder resultado = new StringBuilder();
            TagBuilder buttonGroup = new TagBuilder("div");
            buttonGroup.AddCssClass("btn-group");
            buttonGroup.Attributes.Add("role", "toolbar");
            
            for(int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if(i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("bg-purple");
                }
                tag.AddCssClass("btn btn-default");
                resultado.Append(tag.ToString());
            }
            buttonGroup.InnerHtml = resultado.ToString();
            return MvcHtmlString.Create(buttonGroup.ToString());
        }
    }
}