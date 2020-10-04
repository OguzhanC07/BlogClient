using System.Collections.Generic;
using System.Threading.Tasks;
using BlogClient.Models;

namespace BlogClient.ApiSerices.Interfaces
{
    public interface IBlogApiService{
        Task<List<BlogListModel>> GetAllAsync();
        Task<BlogListModel> GetByIdAsync(int id);
        Task<List<BlogListModel>> GetAllByCategoryId(int id);
        Task AddAsync(BlogAddModel model);
    }
}