using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class MarketingAd
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MainPictureURL { get; set; }
        public string MainVideoURL { get; set; }
    }
}
