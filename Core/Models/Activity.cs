using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models
{
    public enum DifficultyLevel { Easy, Medium, Hard, VeryHard }
    public class Activity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        public int Duration { get; set; }
        public string ExcursionId { get; set; }
        public string Location { get; set; }
        public int ActivityTypeId { get; set; }
        public virtual ActivityType ActivityType { get; set; }
        public string TypeOf { get; set; }
        public string SightSeeing { get; set; }
        public string HighLights { get; set; }
        public string Description { get; set; }
        public string Itinerary { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public List<Media> Medias { get; set; }
    }
}
