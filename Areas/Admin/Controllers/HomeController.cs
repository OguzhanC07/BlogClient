using BlogClient.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller{
        
        [JwtAuthorize]
        public IActionResult Index(){
            return View();
        }
    }
}