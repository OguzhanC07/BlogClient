using System.ComponentModel.DataAnnotations;

namespace BlogClient.Models
{
    public class AppUserLoginModel
    {
        [Required(ErrorMessage="Kullanıcı adı gereklidir.")]
        public string UserName { get; set; }
        [Required(ErrorMessage="Şifre alanı gereklidir.")]
        public string Password { get; set; }
    }
}