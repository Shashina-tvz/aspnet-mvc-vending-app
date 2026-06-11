using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace VendingMachineApp.TagHelpers
{
    [HtmlTargetElement("date-time", Attributes = ForAttributeName)]
    public class DateTimePickerTagHelper : TagHelper
    {
        private const string ForAttributeName = "asp-for";

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; } = default!;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var fullName = For.Name;
            var value = For.Model as DateTime?;

            output.TagName = "div";
            output.Attributes.SetAttribute("class", "datetime-wrapper");

            var displayValue = value.HasValue
                ? value.Value.ToString("dd.MM.yyyy HH:mm")
                : "";

            var isoValue = value.HasValue
                ? value.Value.ToString("O")
                : "";

            output.Content.SetHtmlContent($@"
                <input type='text'
                       class='datetime-picker dark-input'
                       value='{displayValue}'
                       autocomplete='off' />

                <input type='hidden'
                       name='{fullName}'
                       class='datetime-value'
                       value='{isoValue}' />
            ");
        }
    }
}