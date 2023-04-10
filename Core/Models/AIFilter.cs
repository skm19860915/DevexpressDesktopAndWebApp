using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class AIFilter {

        protected int _AIFilterID;
        protected string _AIName; 

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public virtual int AIFilterID { get { return _AIFilterID; } set { _AIFilterID = value; } }
        public virtual string Name { get { return _AIName; } set { _AIName = value; } }
        public virtual string Description { get; set; }
        public virtual IEnumerable<QuoteRequestResort> Apply(IEnumerable<QuoteRequestResort> aInput) { return aInput; }
        public virtual IEnumerable<FlightItinerary> Apply(IEnumerable<FlightItinerary> aInput) { return aInput; }
    }
}
