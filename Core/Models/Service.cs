using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlitzerCore.Models
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceID { get; set; }
        public string Name { get; set; }
        public int? ParentID { get; set; }
        [ForeignKey("ParentID")] 
        public virtual Service ParentService { get; set; }
    }
}
