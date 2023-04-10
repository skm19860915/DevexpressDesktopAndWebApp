using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class UserLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationID { get; set; }
        public DateTime When { get; set; }

        [Required(ErrorMessage = "Please enter Latitude")]
        [Display(Name = "Latitude")]
        public string Latitude { get; set; }

        [Required(ErrorMessage = "Please enter Longitude.")]
        [Display(Name = "Longitude")]
        public string Longitude { get; set; }

        [Required(ErrorMessage = "Please enter UserID.")]
        [Display(Name = "UserID")]
        public string UserID { get; set; }
    }
}
