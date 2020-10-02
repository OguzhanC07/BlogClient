using System.Net.Http;
using System.Net.Http.Headers;
using Blogclient.Extensions;
using BlogClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace BlogClient.Filters
{
    public class JwtAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Session.GetString("token");
            if (string.IsNullOrWhiteSpace(token))
            {
                context.Result = new RedirectToActionResult("SignIn", "Account", null);
            }
            else
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer",token);
                var responsseMessage = httpClient.GetAsync("http://localhost:61418/api/Auth/ActiveUser").Result;
                if (responsseMessage.IsSuccessStatusCode)
                {
                    var activeUser = JsonConvert.DeserializeObject<AppUserViewModel>(responsseMessage.Content.ReadAsStringAsync().Result);

                    context.HttpContext.Session.SetObject("activeUser",activeUser);
                }
                else
                {
                    context.Result = new RedirectToActionResult("SignIn", "Account", null);
                }
            }

        }
    }
}