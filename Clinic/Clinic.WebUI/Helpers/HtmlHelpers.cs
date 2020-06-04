using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Clinic.WebUI.Helpers
{
    public static class HtmlHelpers
    {
        private const string _defaultActionName = "Index";

        public static HtmlString Menu(this IHtmlHelper html, string controllerName)
        {
            var lower = controllerName.ToLower();
            var sb = new StringBuilder();
            sb.Append("<li class=\"nav-item my-auto\">");
            sb.Append($"<a class=\"nav-link text-dark\" href=\"/{controllerName}/Index\">View {lower}</a>");
            sb.Append("</li>");

            return new HtmlString(sb.ToString());
        }

        public static HtmlString Menu(this IHtmlHelper html, string controllerName, string actionName)
        {
            var lower = controllerName.ToLower();
            var sb = new StringBuilder();
            sb.Append("<li class=\"nav-item my-auto\">");
            sb.Append($"<a class=\"nav-link text-dark\" href=\"/{controllerName}/{actionName}\">{actionName}</a>");
            sb.Append("</li>");

            return new HtmlString(sb.ToString());
        }
    }
}
