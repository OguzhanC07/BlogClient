using System.Threading.Tasks;
using BlogClient.ApiSerices.Interfaces;
using BlogClient.Enums;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BlogClient.TagHelpers
{
    [HtmlTargetElement("getblogimage")]
    public class ImageTagHelper : TagHelper
    {
        private readonly IImageApiService _imageApiService;
        public ImageTagHelper(IImageApiService imageApiService)
        {
            _imageApiService = imageApiService;
        }
        public int Id { get; set; }
        public BlogImageType BlogImageType { get; set; } = BlogImageType.BlogHome;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var blob = await _imageApiService.GetBlogImageByIdAsync(Id);
            string html = string.Empty;

            if (BlogImageType == BlogImageType.BlogHome)
            {
                html = $"<img class='card-img-top' src='{blob}'>";
            }
            else
            {
                html = $"<img class='img-fluid-rounded' src='{blob}'>";
            }


            output.Content.SetHtmlContent(html);
        }
    }
}