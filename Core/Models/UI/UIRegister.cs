using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BlitzerCore.Models.UI
{
    public class UIRegister
    {
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Please enter password.")]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please enter confirmation password.")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmationPassword")]
        public string ConfirmationPassword { get; set; }
        public string Id { get; set; }
    }
}
