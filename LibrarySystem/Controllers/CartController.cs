using LibrarySystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Cart _cart;

        public CartController(ApplicationDbContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }
        public IActionResult Index()
        {
            var items = _cart.GetAllCartItems();
            _cart.CartItems = items;

            return View(_cart);
        }
        public IActionResult AddToCart(string id)
        {
            var isLoggedIn = HttpContext.Session.GetString("IsLoggedIn");
            var isRegistered = HttpContext.Session.GetString("IsRegistered");
            var selectedBook = GetBookById(id);
            if ( (isLoggedIn == "true" || isRegistered == "true") && (selectedBook != null) )
            {
                _cart.AddToCart(selectedBook, 1);
                return RedirectToAction("Books", "Home");
            }
            return RedirectToAction("Login", "Authentication");
        }

        public IActionResult RemoveFromCart(string id)
        {
            var selectedBook = GetBookById(id);

            if (selectedBook != null)
            {
                _cart.RemoveFromCart(selectedBook);
            }

            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            _cart.ClearCart();

            return RedirectToAction("Index");
        }

        public Book GetBookById(string id)
        {
            return _context.Book.FirstOrDefault(b => b.ISBN == id);
        }
    
    }
}

