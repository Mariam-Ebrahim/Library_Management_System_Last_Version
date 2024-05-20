using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models
{
    public enum _Gender { Male, Female };
    public abstract class Account
    {
        //[Required(ErrorMessage = "National ID is required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "NationalId must contain only digits")]
        [MinLength(14, ErrorMessage = "NationalId must be 14 digits long")]
        [MaxLength(14, ErrorMessage = "NationalId must be 14 digits long")]
        public virtual string? NationalId { get; set; }
        //[Required(ErrorMessage = "First Name is required")]
        [MinLength(2, ErrorMessage = "First Name should be at least 2 characters")]
        public virtual string? FirstName { get; set; }
        //[Required(ErrorMessage = "LastName is required")]
        [MinLength(2, ErrorMessage = "Last Name must be 14 digits long")]
        public virtual string? LastName { get; set; }

        [DataType(DataType.Password)]
        public virtual string? Password { get; set; }
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public virtual string? Email { get; set; }
        //[Required(ErrorMessage = "Gender is required")]
        public virtual _Gender? Gender { get; set; }
        //[Required]
        public virtual string? Street { get; set; }
        //[Required(ErrorMessage = "City is required")]
        [MinLength(2, ErrorMessage = "City should be at least 2 characters")]
        public virtual  string? City { get; set; }

        //[Required(ErrorMessage = "Country is required")]
        [MinLength(2, ErrorMessage = "Country should be at least 2 characters")]
        public virtual string? Country { get; set; }
    }
}
