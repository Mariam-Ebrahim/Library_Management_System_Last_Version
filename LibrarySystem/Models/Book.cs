using LibrarySystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Models
{
    public class Book
    {
        [Key]  
        public string ISBN { get; set; }
        
        public decimal Price { get; set; }
        
        public string Title { get; set; }
    
        
        [MinLength(10, ErrorMessage = "Description should be at least 10 characters")]

        public string Descripttion { get; set; }
        
        [RegularExpression("^[0-9]*$", ErrorMessage = "Number of copies must be only numbers")]
        public int NumberOfPages { get; set; }
        
        public string?BookImage { get; set; }
        [NotMapped]
        public IFormFile? BookImageFile { get; set; }
       
        public Author? Author { get; set; }
       
        public Category? Category { get; set; }
    }
}