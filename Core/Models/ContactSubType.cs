using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class ContactSubType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ContactTypeId { get; set; }
        [ForeignKey("ContactTypeId")]
        public ContactType ContactType { get; set; }
        public string Name { get; set; }
    }
}
