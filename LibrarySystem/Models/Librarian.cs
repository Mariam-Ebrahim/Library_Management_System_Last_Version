using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibrarySystem.Models;

namespace LibrarySystem.Models
{



    [Table("Librarian")] 
    public class Librarian : Account
    {

         [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         public int LibrarianId { get; set; }



        public static bool LibrarianLogin(Librarian loginLibrarian, ApplicationDbContext dbContext)
        {

            Librarian librarian = dbContext.Librarian.SingleOrDefault(lib => lib.LibrarianId == loginLibrarian.LibrarianId);


            if (librarian != null && BCrypt.Net.BCrypt.EnhancedVerify(loginLibrarian.Password, librarian.Password))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
