using Microsoft.AspNetCore.Mvc;

namespace BlogClient.ViewComponents{
    public class Search : ViewComponent{
        public IViewComponentResult Invoke(string s){
            ViewBag.SearchString= s;
            return View();
        }
    }
}