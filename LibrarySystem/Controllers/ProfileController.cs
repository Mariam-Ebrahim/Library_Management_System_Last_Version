using LibrarySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Controllers
{
    public class ProfileController : Controller
    {
       
        private readonly ApplicationDbContext _dbContext;
        public ProfileController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
           
                 string barcode= HttpContext.Session.GetString("Barcode");
            
               
                Member member = _dbContext.Member.FirstOrDefault(mem => mem.Barcode.ToString() == barcode);

                if (member != null)
                {
                   
                    return View(member);
                }

                
                return NotFound("Member not found.");
               
            
        }
        public IActionResult Account()

        {
            return View();
        }

        [HttpPost]
        public IActionResult Account(Member editedMember, string newPassword, string confirmPassword)
        {
            string barcode = HttpContext.Session.GetString("Barcode");

            if (Member.EditAccount(editedMember, newPassword, confirmPassword, _dbContext, barcode))
            {
                
                return RedirectToAction("Index", "Home");
            }
            else
            {
                
                ViewData["WrongMessage"] = "Editing failed. Please check your current password or ensure that the new password matches the confirm password.";

                return View(editedMember);
            }
        }
    }
}