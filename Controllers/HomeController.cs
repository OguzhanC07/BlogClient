using System.Threading.Tasks;
using BlogClient.ApiSerices.Interfaces;
using BlogClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogApiService _blogApiService;
        public HomeController(IBlogApiService blogApiService)
        {
            _blogApiService=blogApiService;
        }
        public async Task<IActionResult> Index(int? categoryId, string s){
            if(categoryId.HasValue){
                ViewBag.ActiveCategory= categoryId;
                return View(await _blogApiService.GetAllByCategoryId((int)categoryId));
            }
            if(!string.IsNullOrWhiteSpace(s)){
                ViewBag.SearchString = s;
                return View(await _blogApiService.SearchAsync(s));
            }

            return View(await _blogApiService.GetAllAsync());
        }

        public async Task<IActionResult> PostDetail(int id){
            ViewBag.Comments = await _blogApiService.GetCommentsAsync(id,null);
            return View(await _blogApiService.GetByIdAsync(id));
        }

        public async Task<IActionResult> AddComment(CommentAddModel model){
            await _blogApiService.AddCommentAsync(model);
            return RedirectToAction("PostDetail" ,new {id=model.BlogId});
        }
    }
}