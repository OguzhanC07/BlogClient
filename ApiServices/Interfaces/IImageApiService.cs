using System.Threading.Tasks;

namespace BlogClient.ApiSerices.Interfaces
{
    public interface IImageApiService
    {
        Task<string> GetBlogImageByIdAsync(int id);
    }
}