using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snyggerik.Models
{
    public class Comment
    {
        [Key] // PrimaryKey
        [Display(Name = "Identificator")]
        public int CommentId { get; set; }

        [Required(ErrorMessage ="Comment body is obligate")] // Not Null
        [MaxLength(500)]
        [Display(Name = "Comment")]
        public string CommentBody { get; set; }

        [Required(ErrorMessage = "Date is obligate")]
        [DataType(DataType.Date)]
        [Display(Name = "Create comment date")]
        public DateTime? CommentCreated { get; set; }

        // virtual = hämtar bara information du behöver. <Post> = en Post , Posts är flera Post.
       
        public virtual Post Post { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}