using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models
{
    public class Resort : Hotel
    {
        public double? BeachRating { get; set; }
    }
}
