using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebAPI.ViewModels
{
    public class BlogVM
    {
        public int Id { get; set; }
        public string Author { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "This field is required.")]
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ImageUrl { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
