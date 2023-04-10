using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class SavedAd
    {
        public enum Statuses {  ACTIVE, DEACTIVE }

        public SavedAd()            
        {
            When = DateTime.Now;
        }

        [Key, Column(Order = 0)]
        public string UserID { get; set; }
        [Key, Column(Order = 1)]
        public int AdID { get; set; }
        public DateTime When { get; set; }
        public Statuses Status { get; set; }
    }
}

