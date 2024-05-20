using LibrarySystem.Migrations;
using LibrarySystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibrarySystem.Controllers
{
    public class ServiceController : Controller
    {

        private readonly ApplicationDbContext _context;
        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
           
        }

        public async Task<IActionResult> Index()
        {
            
                return _context.Service != null ?
             View(await _context.Service.ToListAsync()) :
                Problem("Entity set 'ApplicationDbContext.Service'  is null.");

            
           
        }




        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Service == null)
            {
                return NotFound();
            }

            
            if (!int.TryParse(id, out int serviceId))
            {

                return BadRequest("Invalid service ID format.");
            }

            var service = await _context.Service.FindAsync(serviceId);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service)
        {
            if (ModelState.IsValid)
            {
                try
                {
                  
                    _context.Add(service);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Service");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the service. Please try again.");
                    return View(service);
                }
            }

            return View(service);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ServiceId, Name, Description")] Service service)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(service);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(service);
        //}



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ServiceId,Name,ServiceImage,Desciption")] Service service)
        {
            if (id != service.ServiceId.ToString())
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.ServiceId.ToString()))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Service == null)
            {
                return NotFound();
            }

            var service = await _context.Service
                .FirstOrDefaultAsync(m => m.ServiceId.ToString() == id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }
       


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Service == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Service' is null.");
            }

            // Convert string id to int
            if (!int.TryParse(id, out int serviceId))
            {
                return BadRequest("Invalid service ID format.");
            }

            var service = await _context.Service.FindAsync(serviceId);

            if (service != null)
            {
                _context.Service.Remove(service);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }




        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Service == null)
            {
                return NotFound();
            }



            var service = _context.Service.FirstOrDefault(b => b.ServiceId.ToString() == id);
            if (service == null)
            {
                return NotFound();
            }


            return View(service);
        }
        private bool ServiceExists(string id)
        {
            return (_context.Service?.Any(e => e.ServiceId.ToString() == id)).GetValueOrDefault();
        }
    }
}
