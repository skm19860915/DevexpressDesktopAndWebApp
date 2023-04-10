using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BlitzerCore.Models
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int TripID { get; set; }
        [ForeignKey("TripID")]
        public virtual Trip Trip { get; set; }

        public virtual string ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual Contact Client { get; set; }
        public Invoice()
        {
            InvoiceDate = DateTime.Now;
        }
    }
}

