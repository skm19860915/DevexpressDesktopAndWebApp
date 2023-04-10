using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Relationship
    {
        public static int SingleMale = 7;
        public static int SingleFemale = 6;
        public static int NotDefined = 5;
        public static int Husband = 1;
        public static int Wife = 2;
        public Relationship()
        {
            ClassName = "Relationship";
        }
        public string ClassName { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RelationshipID { get; set; }
        public string Description { get; set; }
    }
}
