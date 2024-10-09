using System.ComponentModel.DataAnnotations;

namespace Library_Store.Models
{
    public class LogingUser
    {
        [Display(Name = "User Email")]
        [Required(ErrorMessage = "This Field Can Not Be Empty")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [Display(Name = "User Password")]
        [Required(ErrorMessage = "This Field Can Not Be Empty")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
        
    }
}
