using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibrarySystem.Models;

namespace LibrarySystem.Models
{


    [Table("Member")] // Specify the table name for the derived class
    public class Member : Account
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Barcode { get; set; }
        public DateTime StartMembership { get; set; }
        public DateTime EndMembership { get; set; }
        public string? ImageString { get; set; }
        [NotMapped]
        public IFormFile? ProfileImageFile { get; set; }
      
        [MinLength(2, ErrorMessage = "User name should be at least 2 characters.")]
 
        public string? UserName { get; set; }




        public Member()
        {
           
            StartMembership = DateTime.Now;
        
            EndMembership = StartMembership.AddYears(1);
        }
        public static bool UserRegisteration(Member member, ApplicationDbContext dbContext)
        {

           
            member.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(member.Password);

            
            Member existingUser = dbContext.Member.FirstOrDefault(mem => mem.Barcode == member.Barcode);

            if (existingUser != null)
            {
               
                existingUser.UserName = member.UserName;

                if (existingUser.Email == member.Email)
                    return false;
                else
                {
                    existingUser.Email = member.Email;
                }

                existingUser.ProfileImageFile = member.ProfileImageFile;
                if (member.ImageString == null)
                {
                    existingUser.ImageString = "/images/account_3033143.png";
                }
                else
                {
                    existingUser.ImageString = member.ImageString;
                }

                if (!string.IsNullOrEmpty(member.Password))
                {
                    existingUser.Password = member.Password; 
                }

                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool UserLogin(Member loginMember, ApplicationDbContext dbContext)
        {
            
            Member user = dbContext.Member.SingleOrDefault(mem => mem.Barcode == loginMember.Barcode);

            
            if (user != null && BCrypt.Net.BCrypt.EnhancedVerify(loginMember.Password, user.Password))
            {
                return true; 
            }
            else
            {
                return false;
            }

        }

        public static bool EditAccount(Member editedMember, string newPassword, string confirmPassword, ApplicationDbContext dbContext, string barcodeFromSession)
        {
            
            if (newPassword != confirmPassword)
            {
                
                return false;
            }

            Member existingUser = dbContext.Member.FirstOrDefault(mem => mem.Barcode.ToString() == barcodeFromSession);

            if (existingUser != null)
            {
                
                if (BCrypt.Net.BCrypt.EnhancedVerify(editedMember.Password, existingUser.Password))
                {
                   
                    existingUser.UserName = editedMember.UserName;

                    
                    if (!string.IsNullOrEmpty(newPassword))
                    {
                        existingUser.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(newPassword);
                    }

                    dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    
                    return false;
                }
            }
            else
            {
               
                return false;
            }
        }
    }
}