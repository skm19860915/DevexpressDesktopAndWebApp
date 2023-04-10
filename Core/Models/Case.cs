using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Case : Task
    {
        public string Resolution { get; set; }
        public int CaseStatusID { get; set; }
        public virtual CaseStatus CaseStatus {get; set; }
    }
}
