using BlogClient.ApiSerices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogClient.ViewComponents
{
    public class CategoryList : ViewComponent{
        
        private readonly ICategoryApiService _categoryApiService;
        public CategoryList(ICategoryApiService categoryApiService)
        {
            _categoryApiService=categoryApiService;
        }
        
        public IViewComponentResult Invoke(){
            return View(_categoryApiService.GetAllAsync().Result);
        }
    }
}