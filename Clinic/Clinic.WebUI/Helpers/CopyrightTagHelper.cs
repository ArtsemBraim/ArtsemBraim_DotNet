using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Clinic.WebUI.Helpers
{
    [HtmlTargetElement("copyright")]
    public class CopyrightTagHelper : TagHelper
    {
        private readonly char _copyrightSign = '\u00A9';
        private readonly string _copyrightMessage = "2020 Artem";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "p";
            output.Content.SetContent($"{_copyrightSign} {_copyrightMessage}");
        }
    }
}
