using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public enum RelationShips { Unknown, Primary, Husband, Wife, Father, Mother, Son, Daughter, Boyfriend, Girlfriend, Partner, GrandMother, GrandFather, GrandChild, Other }

    public class HouseHoldMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string MemberId { get; set; }
        [ForeignKey("MemberId")]
        public Contact Member { get; set; }
        public RelationShips Relationship { get; set; }
        public int HouseHoldID { get; set; }
        [ForeignKey("HouseHoldID")]
        public virtual HouseHold HouseHold { get; set; }
    }
}
