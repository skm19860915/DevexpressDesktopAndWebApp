using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models
{
    public class RelationshipMap
    {
        public int Id { get; set; }
        public string PrimaryId { get; set; }
        [ForeignKey("PrimaryId")]
        public Contact Primary { get; set; }
        public int RelationshipId { get; set; }
        [ForeignKey("RelationshipId")]
        public Relationship Relationship { get; set; }
        public string TargetId { get; set; }
        [ForeignKey("TargetId")]
        public Contact Target { get; set; }
    }
}
