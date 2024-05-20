using LibrarySystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Models
{
    public class Service
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }
        

        public string? Name { get; set; }

       
        [MinLength(10, ErrorMessage = "Description should be at least 10 characters")]
        public string? Description { get; set; }

       
        public string? ServiceImage {  get; set; }
       
    }
}
