using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.CodeAnalysis.Elfie.Model;
using LibrarySystem.Migrations;

namespace LibrarySystem.Controllers
{
    public class LendingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LendingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            return _context.Orders != null ?
         View(await _context.Orders.ToListAsync()) :
            Problem("Entity set 'ApplicationDbContext.Orders'  is null."); 
        }


        public async Task<IActionResult> Details()
        {

            return _context.OrderItems != null ?
         View(await _context.OrderItems.OrderByDescending(orderItem => orderItem.OrderId).ToListAsync()) :
            Problem("Entity set 'ApplicationDbContext.Orderitems'  is null.");
        }

      


    }
}
