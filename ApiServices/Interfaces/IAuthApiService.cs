using System.Threading.Tasks;
using BlogClient.Models;

namespace BlogClient.ApiSerices.Interfaces
{
    public interface IAuthApiService
    {
        Task<bool> SignIn(AppUserLoginModel model);
    }
}