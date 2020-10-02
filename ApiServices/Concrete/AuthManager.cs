using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlogClient.ApiSerices.Interfaces;
using BlogClient.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BlogClient.ApiSerices.Concrete
{
    public class AuthManager : IAuthApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _accessor;
        public AuthManager(HttpClient httpClient,IHttpContextAccessor accessor)
        {
            _accessor=accessor;
            _httpClient = httpClient;
            httpClient.BaseAddress = new System.Uri("http://localhost:61418/api/Auth/");
        }


        public async Task<bool> SignIn(AppUserLoginModel model)
        {

            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent= new StringContent(jsonData,Encoding.UTF8,"application/json");

            var responseMessage = await _httpClient.PostAsync("SignIn",stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                var accessToken = JsonConvert.DeserializeObject<AccessTokenModel>(await responseMessage.Content.ReadAsStringAsync());
                _accessor.HttpContext.Session.SetString("token", accessToken.Token);           
            
                return true;
            }
            return false;
        }
    }
}