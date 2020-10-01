using System.Threading.Tasks;
using BlogClient.ApiSerices.Interfaces;
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
        public async Task<IActionResult> Index(){
            return View(await _blogApiService.GetAllAsync());
        }

        public async Task<IActionResult> PostDetail(int id){
            return View(await _blogApiService.GetByIdAsync(id));
        }
    }
}