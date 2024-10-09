using System.ComponentModel.DataAnnotations;

namespace Library_Store.Models
{
    public class AddBookDto
    { 
        [Display(Name = "Book Name")]
        [Required(ErrorMessage = "This Field Can Not Be Empty")]
        public string BookName { get; set; }
        [Display(Name = "Book Price")]
        [Required(ErrorMessage = "This Field Can Not Be Empty")]
        public int BookPrice { get; set; }
        [Display(Name = "Book Quantity")]
        [Required(ErrorMessage = "This Field Can Not Be Empty")]
        public int BookQuantity { get; set; }
        [Display(Name = "Book Category")]
        [Required(ErrorMessage = "This Field Can Not Be Empty")]
        public Categories BookCategory { get; set; }
    }
}
