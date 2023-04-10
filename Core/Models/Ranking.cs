using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models.UI;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Ranking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RankingPageId { get; set; }
        [ForeignKey("RankingPageId")]
        public virtual UIRanking RankingPage { get; set; }
        public int Position { get; set; }
        public int ResortPageId { get; set; }
        [ForeignKey("ResortPageId")]
        public virtual UIResortPage ResortPage { get; set; }
    }
}
