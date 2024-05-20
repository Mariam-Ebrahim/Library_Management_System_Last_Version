using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models
{
    public class Order
    {
       
        public int Id { get; set; }
        public int Barcode { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();

    }
}
