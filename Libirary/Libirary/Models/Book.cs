using System.ComponentModel.DataAnnotations;

namespace Libirary.Models
{
    public class Book
    {
        [Key]
        public Guid BSN { get; set;}
        [Required(ErrorMessage ="You Must Enter Book Tittle")]
        [MaxLength(60,ErrorMessage ="Book Tittle Cannot Be More Than 60 Charachters")]
        public string bookTitle { get; set;}
        [Required(ErrorMessage = "You Must Enter Author Name")]
        [MaxLength(60, ErrorMessage = "Author name Cannot Be More Than Charachters 60")]
        public string autorName { get; set;}
        [Required(ErrorMessage ="* Enter number of copies")]
        public int  copies { get; set; }
        public int? borrowed { get; set; }
        public int? availableCopiesToborrow { get; set; }
    }
}
