using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Models;

namespace LibrarySystem.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Books(string searchString = null)
        {
            IQueryable<Book> booksQuery = _context.Book.Include(b => b.Author).Include(b => b.Category);

            if (!string.IsNullOrEmpty(searchString))
            {
                booksQuery = booksQuery.Where(b =>
                    b.Title.Contains(searchString) ||
                    b.ISBN.Contains(searchString) ||
                    b.Author.Name.Contains(searchString)
                );
            }

            var books = await booksQuery.ToListAsync();

            return View(books);
        }





        // GET: Books/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            //var book = await _context.Book
            //    .FirstOrDefaultAsync(m => m.ISBN == id);

            var book = _context.Book.Include(b => b.Author).Include(b => b.Category).FirstOrDefault(b => b.ISBN == id);
            if (book == null)
            {
                return NotFound();
            }


            return View(book);
        }



        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (book.Author != null && !string.IsNullOrWhiteSpace(book.Author.Name))
                    {
                        // Check if an author with the same name already exists
                        var existingAuthor = _context.Author.FirstOrDefault(a => a.Name == book.Author.Name);
                        if (existingAuthor != null)
                        {
                            book.Author = existingAuthor;
                        }
                        else
                        {
                            // Create a new author
                            var newAuthor = new Author { Name = book.Author.Name };
                            _context.Author.Add(newAuthor);
                            book.Author = newAuthor;
                        }
                    }

                    if (book.Category != null && !string.IsNullOrWhiteSpace(book.Category.CategoryName))
                    {
                        // Check if a category with the same name already exists
                        var existingCategory = _context.Category.FirstOrDefault(c => c.CategoryName == book.Category.CategoryName);
                        if (existingCategory != null)
                        {
                            book.Category = existingCategory;
                        }
                        else
                        {
                            // Create a new category
                            var newCategory = new Category { CategoryName = book.Category.CategoryName };
                            _context.Category.Add(newCategory);
                            book.Category = newCategory;
                        }
                    }

                    _context.Add(book);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Books", "Books");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the book. Please try again.");
                }
            }

            return View(book);
        }




        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ISBN,Price,Title,Descripttion,NumberOfPages,BookImage,NumberOfCopies")] Book book)
        {
            if (id != book.ISBN)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ISBN))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Books));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = _context.Book.Include(b => b.Author).Include(b => b.Category).FirstOrDefault(b => b.ISBN == id);
               
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Book'  is null.");
            }
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Books));
        }
        private bool BookExists(string id)
        {
            return (_context.Book?.Any(e => e.ISBN == id)).GetValueOrDefault();
        }

    }
}