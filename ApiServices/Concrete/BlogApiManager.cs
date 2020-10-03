using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blogclient.Extensions;
using BlogClient.ApiSerices.Interfaces;
using BlogClient.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BlogClient.ApiSerices.Concrete
{
    public class BlogApiManager : IBlogApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BlogApiManager(IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:61418/api/blogs/");
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<BlogListModel>> GetAllAsync()
        {
            var responseMessage = await _httpClient.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            else
            {
                return null;
            }
        }

        public async Task<BlogListModel> GetByIdAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync($"{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BlogListModel>(await responseMessage.Content.ReadAsStringAsync());
            }
            else
            {
                return null;
            }
        }

        public async Task<List<BlogListModel>> GetAllByCategoryId(int id)
        {
            var responseMessage = await _httpClient.GetAsync($"GetAllByCategoryId/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            else
            {
                return null;
            }
        }


        public async Task AddAsync(BlogAddModel model)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();
            if (model.Image!=null)
            {
                var bytes =await System.IO.File.ReadAllBytesAsync(model.Image.FileName);
                //resimi bytlera çevirme işlemi
                ByteArrayContent byteContent = new ByteArrayContent(bytes);

                //content type kaybetmemek için
                byteContent.Headers.ContentType= new MediaTypeHeaderValue(model.Image.ContentType);

                //resimi byte şeklinde ekleme, türünü yazma ve dosya adını yazma
                formData.Add(byteContent,nameof(BlogAddModel.Image),model.Image.FileName);
            }
            

            //Sessiondan gelen user bilgilerini alma
            var user = _httpContextAccessor.HttpContext.Session.GetObject<AppUserViewModel>("activeUser");
            model.AppUserId=user.Id;

            //string verileri ekleme
            formData.Add(new StringContent(model.AppUserId.ToString()),nameof(BlogAddModel.AppUserId));

            formData.Add(new StringContent(model.ShortDesc), nameof(BlogAddModel.ShortDesc));

            formData.Add(new StringContent(model.Description),nameof(BlogAddModel.Description));

            formData.Add(new StringContent(model.Title),nameof(BlogAddModel.Title));
        
        
            //jwt authorize işlemi -- Bearer + sessiondan gelen token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));
        
            await _httpClient.PostAsync("",formData);
        }
    }
}