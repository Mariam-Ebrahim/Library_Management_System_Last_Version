using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Models
{
    public class OrderItem
    {
       
        public int OrderId { get; set; }
        public string BookId { get; set; }
        public Order Order { get; set; }
        public Book Book { get; set; }
    }
}
