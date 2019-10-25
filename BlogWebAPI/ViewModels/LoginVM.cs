using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebAPI.ViewModels
{
    public class LoginVM
    {

        [EmailAddress]
        [DataType(DataType.EmailAddress, ErrorMessage = "The {0} field is not a valid e-mail address.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "The {0} field is not a valid e-mail address.")]
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

    }
}
