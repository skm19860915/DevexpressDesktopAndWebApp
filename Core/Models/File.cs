using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class File
    {
        public File()
        {
            Tags = new HashSet<Tag>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public int FileTypeId { get; set; }
        [ForeignKey("FileTypeId")]
        public virtual FileType FileType {get; set; }
        public string ContactId { get; set; }
        [ForeignKey("ContactId")]
        public virtual Contact Owner { get; set; }
        public int Version { get; set; }
        public string Description { get; set; }
        public string URI { get; set; }
        public int? OpportunityId { get; set; }
        [ForeignKey("OpportunityId")]
        public virtual Opportunity Opportunity { get; set; }
        public int? TaskId { get; set; }
        [ForeignKey("TaskId")]
        public virtual Task Task { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public Guid? FileDirectoryGuid { get; set; }
    }
}
