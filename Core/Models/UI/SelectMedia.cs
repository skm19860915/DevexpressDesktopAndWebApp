using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public class SelectMedia
    {
        public SelectMedia(Media aMedia)
        {
            Id = aMedia.Id;
            Title = aMedia.Title;
            if ( aMedia.Category != null )
                Category = aMedia.Category.Name;
            if (aMedia.Country != null)
                Country = aMedia.Country.Name;
            MediaLocation = aMedia.MediaLocation;
            Size560x460 = aMedia.Size560x460 != null;
            Size1600x1200 = aMedia.Size1600x1200 != null;
            Video = aMedia.MPeg != null;
            ForGallery = aMedia.ForGallery;
        }

        public int Id { get; set; }
        public string MediaLocation { get; set; }
        public string Category { get; set; }
        public string Country { get; set; }
        public string Title { get; set; }
        public bool ForGallery { get; set; }
        public bool Size560x460 { get; set; }
        public bool Size1600x1200 { get; set; }
        public bool Video { get; set; }
    }
}
