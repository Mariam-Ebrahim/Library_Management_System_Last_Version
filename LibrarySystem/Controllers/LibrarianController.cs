using LibrarySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Controllers
{
    public class LibrarianController : Controller
    {
		private readonly ApplicationDbContext _context;

		public LibrarianController(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Librarian librarian)
        {
            if (Librarian.LibrarianLogin(librarian, _context))
            {
                HttpContext.Session.SetString("LibrarianId", librarian.LibrarianId.ToString());

                return RedirectToAction("Books", "Books");
            }
            else
            {

                ViewData["Error"] = "Login failed. Please check your ID and password.";
                return View();
            }
        }
        public IActionResult LibrarianProfile()
        {
            Librarian librarian = _context.Librarian.FirstOrDefault();
            return View(librarian);
        }


     


        }
}
