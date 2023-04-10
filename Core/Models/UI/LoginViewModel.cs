using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzerCore.Models.UI
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            ErrorMsgs = new List<ErrorMsg>();
        }
        public string CaptchaValue { get; set; }
        [Required(ErrorMessage = "Please enter email.")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Latitude")]
        public string Latitude { get; set; }

        [Display(Name = "Longitude")]
        public string Longitude { get; set; }
        public string ENV { get; set; }
        public List<ErrorMsg> ErrorMsgs { get; set; }
    }


    public class APILoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
