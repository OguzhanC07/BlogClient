using System.ComponentModel.DataAnnotations;

namespace BlogClient.Models
{
    public class CategoryUpdateModel{
        public int Id { get; set; }
        [Required(ErrorMessage="İsim alanı gereklidir")]
        [Display(Name="İsim :")]
        public string Name { get; set; }
    }
}