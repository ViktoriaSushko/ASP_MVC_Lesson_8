using ASP_MVC_Lesson_8.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Primitives;
using System.Text;

namespace ASP_MVC_Lesson_8.Helpers
{
    public class PagerTagHelper:TagHelper
    {
        public int CurrentPage{ get; set; }
        public int PageCount { get; set; }
        public int ItemsPerPage { get; set; }
        public string? Href { get; set; }
        public IEnumerable<Cat?> Items { get; set; } 
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul class='pagination'>");
            for (int i = 1; i <=PageCount; i++)
            {
                sb.Append("<li class='page-item'>");
                string isActive = i == CurrentPage ? "active" : "";
                sb.Append($"<a href='{Href ?? ""}?page={i}' class='page-link {isActive}'>{i}</a>");
                sb.Append("</li>");
            }
            sb.Append("</ul");
            output.Content.SetHtmlContent(sb.ToString());

        }
    }
}
