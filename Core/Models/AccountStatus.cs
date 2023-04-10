using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class AccountStatus
    {
        [Key]
        public int StatusID { get; set; }
        public string Description { get; set; }
    }
}
