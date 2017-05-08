using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snyggerik.Models
{
    public class Blog
    {
        [Key] // PrimaryKey
        public int BlogId { get; set; }

        [Required(ErrorMessage ="Title is required.")] // Not Null
        [MaxLength(200, ErrorMessage ="Max lenght is 50 chars")] // char (50)
        [Display(Name = "Title of Blog")]
        public string BlogTitle { get; set; }

        [Required(ErrorMessage ="Description is required")] // Not Null
        [MaxLength(5000, ErrorMessage = "Max lenght is 1000 chars")] // char (1000)
        [Display(Name = "Blog description")]
        public string BlogBody { get; set; }

        [Required(ErrorMessage = "No Date")]
        [DataType(DataType.Date)]
        [Display(Name = "Blog create date")]
        public DateTime? BlogCreated { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public virtual ApplicationUser User { get; set; }

    }
}