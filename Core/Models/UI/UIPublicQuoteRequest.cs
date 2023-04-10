using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlitzerCore.Models.UI
{
    [NotMapped]
    public class UIPublicQuoteRequest
    {
        public string Departure { get; set; }
        public string Destination { get; set; }
        public string When { get; set; }
        [Required(ErrorMessage = "Please enter Start Date.")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Please enter End Date.")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public string Email { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
    }
}
