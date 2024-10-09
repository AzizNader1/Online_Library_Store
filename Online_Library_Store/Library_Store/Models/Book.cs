using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library_Store.Models
{
    public enum Categories
    {
        [Display(Name = "Self Development")] SelfDevelopment,
        [Display(Name = "Science")] Science,
        [Display(Name = "Psychology")] Psychology,
        [Display(Name = "Political Science")] PoliticalScience,
        [Display(Name = "Plays")] Plays,
        [Display(Name = "Novels")] Novels,
        [Display(Name = "Medicain")] Medicain,
        [Display(Name = "History")] History,
        [Display(Name = "Geography")] Geography,
        [Display(Name = "Fantasy")] Fantasy,
        [Display(Name = "Business")] Business

    }
    public class Book
    {
        public int BookId { get; set; }
        [Display(Name ="Book Name")]
        [Required(ErrorMessage ="This Field Can Not Be Empty")]
        public string BookName { get; set; }
        [Display(Name = "Book Price")]
        [Required(ErrorMessage = "This Field Can Not Be Empty")]
        public int BookPrice { get; set; }
        [Display(Name = "Book Quantity")]
        [Required(ErrorMessage = "This Field Can Not Be Empty")]
        public int BookQuantity { get; set; }
        [Display(Name = "Book Image")]
        [Required(ErrorMessage = "This Field Can Not Be Empty")]
        public string BookImage { get; set; }
        [Display(Name = "Book Category")]
        [Required(ErrorMessage = "This Field Can Not Be Empty")]
        public Categories BookCategory { get; set; }
    }
}
