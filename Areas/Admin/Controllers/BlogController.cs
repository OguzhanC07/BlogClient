using System.Threading.Tasks;
using BlogClient.ApiSerices.Interfaces;
using BlogClient.Filters;
using BlogClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {

        private readonly IBlogApiService _blogApiService;
        public BlogController(IBlogApiService blogApiService)
        {
            _blogApiService = blogApiService;
        }

        [JwtAuthorize]
        public async Task<IActionResult> Index()
        {
            return View(await _blogApiService.GetAllAsync());
        }

        [JwtAuthorize]
        public IActionResult Create()
        {
            return View(new BlogAddModel());
        }

        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> Create(BlogAddModel model)
        {
            if (ModelState.IsValid)
            {
                await _blogApiService.AddAsync(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [JwtAuthorize]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var blogList = await _blogApiService.GetByIdAsync(id);
            
            return View(new BlogUpdateModel{
                Id=blogList.Id,
                Title=blogList.Title,
                Description=blogList.Description,
                ShortDesc=blogList.ShortDesc
            });
        }

        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> Update(BlogUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                await _blogApiService.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [JwtAuthorize]
        public async Task<IActionResult> Delete(int id){
            await _blogApiService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}