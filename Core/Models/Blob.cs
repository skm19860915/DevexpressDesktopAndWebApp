using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Blob
    {
        public enum BlobType { Image, Video}

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlobID { get; set; }
        public BlobType Type { get; set; }
        public int Order { get; set; }
        public int AdID { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public Uri URL { get; set; }

        [ForeignKey("AdID")]
        public virtual Ad Ad { get; set; }

        internal void Copy(Blob aBlob)
        {
            Type = aBlob.Type;
            Order = aBlob.Order;
            AdID = aBlob.AdID;
            Header = aBlob.Header;
            Description = aBlob.Description;
            URL = aBlob.URL;


        }
    }
}
