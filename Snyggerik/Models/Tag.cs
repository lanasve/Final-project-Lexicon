using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snyggerik.Models
{
    public class Tag
    {
        [Key]
        [Display(Name = "Identificator")]
        public int TagId { get; set; }

        [Required(ErrorMessage ="Tags name is required")]
        public string Name { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}