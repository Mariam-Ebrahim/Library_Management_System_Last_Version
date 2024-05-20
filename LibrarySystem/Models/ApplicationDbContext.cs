using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LibrarySystem.Models
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        // Other code in your ApplicationDbContext class
        public DbSet<Author>? Author { get; set; }
        public DbSet<Member>? Member { get; set; }
        public DbSet<Librarian>? Librarian { get; set; }
        public DbSet<Book>? Book { get; set; }
        public DbSet<Category>? Category { get; set; }
        public DbSet<Service>? Service { get; set; }
        public DbSet<CartItem>? CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            //using Fluent API
            //------------------------------------Composite Key ------------
            modelBuilder.Entity<OrderItem>()
            .HasKey(oi => new { oi.OrderId, oi.BookId });

            base.OnModelCreating(modelBuilder);





        }
      
    }
}
