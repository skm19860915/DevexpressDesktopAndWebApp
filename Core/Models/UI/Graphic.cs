using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace BlitzerCore.Models.UI
{
    public class Graphic
    {
        public int ID { get; set; }
        [Column(TypeName = "NVARCHAR(255)")]
        public string Location { get; set; }  // Url to the Graphic item on the blob server
        public MediaFormats MediaFormat { get; set;}
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
