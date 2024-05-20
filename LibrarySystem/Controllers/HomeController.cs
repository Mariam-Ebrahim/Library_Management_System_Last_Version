using LibrarySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;


namespace LibrarySystem.Controllers
{
    public class HomeController : Controller
    {
      
        private static string _User { get; set; }

        ApplicationDbContext db;
        public HomeController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {

            var result = db.Category.ToList();
            return View(result);

        }

     
        public IActionResult Category(int id)
        {
            var result = db.Book.Where(x => x.Category.CategoryId == id).ToList();
            return View(result);
        }
        public async Task<IActionResult> Books(string searchString)
        {
            var books = db.Book.Select(b => b);

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b =>
                    b.Title.Contains(searchString) ||
                    b.Author.Name.Contains(searchString)
                );
            }


            return View(await books.ToListAsync());
        }
        public IActionResult Service()
        {
            var result = db.Service.ToList();
            return View(result);
            
        }
		public IActionResult Index0()
		{
			return View();
		}
		public IActionResult BookDetail()
        {
            return View();
        }

       
    

        public IActionResult AddBook()
        {
            return View();
        }
    
    }
}
