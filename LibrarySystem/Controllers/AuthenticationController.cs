using LibrarySystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers
{
    public class AuthenticationController : Controller
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthenticationController(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Register(Member member)
        {

            if (member.ProfileImageFile != null && member.ProfileImageFile.FileName != null)
            {
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
                var fileExtension = Path.GetExtension(member.ProfileImageFile.FileName)?.ToLower();


                if (fileExtension != null && !allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("ProfileImageFile", "Please upload only images with the following extensions: .jpg, .jpeg, .png");
                }
                
                var imagePath = "/images/";
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + member.ProfileImageFile.FileName;

                // Use the IWebHostEnvironment service to get the wwwroot path
                var webRootPath = _webHostEnvironment.WebRootPath;

                // Combine the wwwroot path with the images folder and unique file name
                var filePath = Path.Combine(webRootPath, "images", uniqueFileName);

                // Ensure the directory exists before attempting to save the file
                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    member.ProfileImageFile.CopyTo(stream);
                }

               
                member.ImageString = Path.Combine(imagePath, uniqueFileName);
            }
            if (Member.UserRegisteration(member, _dbContext))
            {
               
                HttpContext.Session.SetString("Barcode", member.Barcode.ToString());

                
                HttpContext.Session.SetString("IsRegistered", "true");

                return RedirectToAction("Index", "Home");
            }
            else
            {
                
                ViewData["WrongMessage"] = "Registration failed. Please check your Barcode  or email ";

                
                HttpContext.Session.Remove("IsRegistered");

                return View(member);
            }
        }


        [HttpGet]
        public IActionResult Login()

        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Member member)
        {
            if (Member.UserLogin(member, _dbContext))
            {
                HttpContext.Session.SetString("Barcode", member.Barcode.ToString());
                HttpContext.Session.SetString("UserType", "Member");

                HttpContext.Session.SetString("IsLoggedIn", "true");

               
                return RedirectToAction("Index", "Home");
            }
            else
            {
               
                ViewData["Error"] = "Login failed. Please check your username and password.";

                
                HttpContext.Session.Remove("IsLoggedIn");

                return View();
            }
        }

        public IActionResult Logout()
        {
            
            HttpContext.Session.Clear();

            
            return RedirectToAction("Index", "Home");
        }
    }
}


