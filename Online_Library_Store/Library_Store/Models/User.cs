using System.ComponentModel.DataAnnotations;

namespace Library_Store.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Display(Name = "User Email")]
        [Required(ErrorMessage = "This Field Can Not Be Empty")]
        public string UserEmail { get; set; }
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "This Field Can Not Be Empty")]
        public string UserName { get; set; }
        [Display(Name = "User Password")]
        [Required(ErrorMessage = "This Field Can Not Be Empty")]
        public string UserPassword { get; set; }
        [Display(Name = "User Image")]
        [Required(ErrorMessage = "This Field Can Not Be Empty")]
        public string UserImage { get; set; }
    }
}
