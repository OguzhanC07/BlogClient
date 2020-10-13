using BlogClient.ApiSerices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogClient.ViewComponents
{
    public class GetLastFiveBlog : ViewComponent
    {
        private readonly IBlogApiService _blogApiService;
        public GetLastFiveBlog(IBlogApiService blogApiService)
        {
            _blogApiService=blogApiService;
        }
        public IViewComponentResult Invoke()
        {

            return View(_blogApiService.GetLastFiveAsync().Result);
        }
    }
}