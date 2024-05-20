using LibrarySystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace LibrarySystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Cart _cart;

        public OrderController(ApplicationDbContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var cartItems = _cart.GetAllCartItems();
            _cart.CartItems = cartItems;
           

            if (_cart.CartItems.Count == 0)
            {
                ModelState.AddModelError("", "Cart is empty, please add a book first.");
            }

            if (ModelState.IsValid)
            {
                CreateOrder(order);
                _cart.ClearCart();
                return View("CheckoutComplete", order);
            }

            return View(order);
        }

        public IActionResult CheckoutComplete(Order order)
        {
            return View(order);
        }

        public void CreateOrder(Order order)
        {
            order.CheckoutDate = DateTime.Now;
            order.DueDate = DateTime.Now.AddDays(14);
            string BarcodeString = HttpContext.Session.GetString("Barcode");

            if (int.TryParse(BarcodeString, out int BarCode))
            {
                order.Barcode = BarCode;
            }
            




            var cartItems = _cart.CartItems;

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem()
                {
                    
                    BookId = item.Book.ISBN,
                    OrderId = order.Id,
          
                };
                order.OrderItems.Add(orderItem);
             
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
