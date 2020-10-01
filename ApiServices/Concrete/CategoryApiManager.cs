using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BlogClient.ApiSerices.Interfaces;
using BlogClient.Models;
using Newtonsoft.Json;

namespace BlogClient.ApiSerices.Concrete
{
    public class CategoryApiManager : ICategoryApiService
    {
        private readonly HttpClient _httpClient;
        public CategoryApiManager(HttpClient httpClient)
        {
            _httpClient=httpClient;
           _httpClient.BaseAddress = new Uri("http://localhost:61418/api/categories/");
        }
        public async Task<List<CategoryListModel>> GetAllAsync()
        {
            var responseMessage = await _httpClient.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CategoryListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            else
            {
                return null;
            }
        }
    }
}