using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snyggerik.Models
{
    public class Post
    {
        [Key]
        public int IdPost { get; set; }

        [Display(Name ="Title")]
        [Required (ErrorMessage ="Title is reqiuired")]
        [StringLength(150, ErrorMessage ="Title length can not be more than 150 char")]
        public string PostTitle { get; set; }

        [Display(Name = "Post")]
        [Required(ErrorMessage = "Post body is reqiuired")]
        [StringLength(5000, ErrorMessage = "Post length can not be more than 5000 char")]
        public string PostBody { get; set; }

        [Display(Name = "Post created")]
        [Required(ErrorMessage = "Create date is reqiuired")]
        [DataType(DataType.Date)]
        public DateTime PostCreated { get; set; }

        [Display(Name = "Views")]
        public int Views { get; set; }

        [Display(Name ="Post tags collection ")]
        public virtual ICollection<PostTag> PostTags { get; set; }

        public virtual  ICollection<Comment> Comments { get; set; }

        public virtual Blog Blog { get; set; }
    }
}