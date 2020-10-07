using System.Threading.Tasks;
using BlogClient.ApiSerices.Interfaces;
using BlogClient.Filters;
using BlogClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {

        private readonly ICategoryApiService _categoryApiService;
        public CategoriesController(ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        [JwtAuthorize]
        public async Task<IActionResult> Index()
        {
            return View(await _categoryApiService.GetAllAsync());
        }

        [JwtAuthorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> Create(CategoryAddModel model)
        {
            if (ModelState.IsValid)
            {
                await _categoryApiService.AddAsync(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }


        [JwtAuthorize]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _categoryApiService.GetByIdAsync(id);
            CategoryUpdateModel model = new CategoryUpdateModel
            {
                Id = result.Id,
                Name = result.Name
            };
            return View(model);
        }

        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> Update(CategoryUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                await _categoryApiService.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
             }
        }

        [JwtAuthorize]
        public async Task<IActionResult> Delete(int id){
            await _categoryApiService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }
}