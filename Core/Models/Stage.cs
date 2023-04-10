using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Stage
    {
        //public enum Stages { Invalid, Qualification, MeetingScheduled,PriceQuote, Negotiations,Won, Loss}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StageID { get; set; }
        public bool Default { get; set; }
        public string Description { get; set; }
    }
}

