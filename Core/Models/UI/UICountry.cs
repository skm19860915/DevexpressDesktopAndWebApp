using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models.UI
{
    public class UICountry : Page
    {
        public List<UIResortPage> Resorts { get; set; }

        public override string ToString()
        {
            return $"ID [{this.Id}] Title = {this.Title}";
        }
    }
}
