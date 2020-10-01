using System.Collections.Generic;
using System.Threading.Tasks;
using BlogClient.Models;

namespace BlogClient.ApiSerices.Interfaces
{
    public interface ICategoryApiService{
        Task<List<CategoryListModel>> GetAllAsync();
        Task<List<CategoryWithBlogCountModel>> GetAllWithBlogsCountAsync();
        Task<CategoryListModel> GetByIdAsync(int id);
    }
}