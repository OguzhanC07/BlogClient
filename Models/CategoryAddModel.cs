using System.ComponentModel.DataAnnotations;

namespace BlogClient.Models
{
    public class CategoryAddModel
    {
        [Required(ErrorMessage="Ad alanı gereklidir.")]
        [Display(Name="İsim : ")]
        public string Name { get; set; }
    }
}