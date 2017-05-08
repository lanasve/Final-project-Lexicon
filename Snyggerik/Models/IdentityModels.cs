using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snyggerik.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Required(ErrorMessage = "First name is required")] // Not Null
        [MaxLength(50, ErrorMessage = "Max lenght is 50 chars")] // char (1000)
        [Display(Name = "Users name")]
        public string  FirstName { get; set; }

        [Required(ErrorMessage = "Surname is required")] // Not Null
        [MaxLength(50, ErrorMessage = "Max lenght is 50 chars")] // char (1000)
        [Display(Name = "Users surname")]
        public string LastName { get; set; }

        public virtual  ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        //Blog class. connect blog class and user class
        
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("LexiconConnection", throwIfV1Schema: false)     // SQL database
      //      : base("DefaultConnection", throwIfV1Schema: false)   // Local database
        {
            //add our own tables
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
         public DbSet<Blog> Blogs {get; set;}
         public DbSet<Post> Posts {get; set;}
         public DbSet<PostTag> PostTags  {get; set;}
         public DbSet<Tag> Tags  {get; set;}
         public DbSet<Comment> Comments { get; set; }
       
         

    }
}