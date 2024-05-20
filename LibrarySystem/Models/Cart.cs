using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace LibrarySystem.Models
{
    public class Cart
    {
        private readonly ApplicationDbContext _context;
        public Cart (ApplicationDbContext cart)
        {
            _context = cart;
        }

        [Key]
        public string Id { get; set; }
        public List<CartItem> CartItems { get; set; }
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session; 
            var context =services.GetService<ApplicationDbContext>();
            string CartId = session.GetString("id")?? Guid.NewGuid().ToString();
            session.SetString("id", CartId);
            return new Cart(context) { Id = CartId };
        }
        public CartItem GetCartItem(Book book)
        {
            return _context.CartItems.SingleOrDefault(ci =>ci.Book.ISBN == book.ISBN && ci.CartId == Id);
        }

        public void AddToCart(Book book, int quantity)
        {
            var cartItem = GetCartItem(book);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Book = book,
                 
                    CartId = Id
                };

                _context.CartItems.Add(cartItem);
            }
        
            _context.SaveChanges();
        }

       
       
        public void RemoveFromCart(Book book)
        {
            var cartItem = GetCartItem(book);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
            }
            _context.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = _context.CartItems.Where(ci => ci.CartId == Id);

            _context.CartItems.RemoveRange(cartItems);

            _context.SaveChanges();
        }

        public List<CartItem> GetAllCartItems()
        {
            return CartItems ??
                (CartItems = _context.CartItems.Where(ci => ci.CartId == Id)
                    .Include(ci => ci.Book)
                    .ToList());
        }

       
    }
}