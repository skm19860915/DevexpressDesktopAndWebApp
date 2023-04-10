using BlitzerCore.Models.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace BlitzerCore.Models.UI
{
    public enum MediaFormats { Size_1920x1080, Size_1600x1200, Size_1024x640, Size_560x460, ICON, IMOVIE, MPEG }
    public class Media
    {
        public Media()
        {
            Title = Title;
        }
        public int Id { get; set; }
        public string Title { get; set; }  // title that the Page Admin Sees
        public string Description { get; set; }
        public string MediaLocation { 
            get
            {
                if (RemoteLocation(ThumbNail))
                    return ThumbNail.Location;
                else if (RemoteLocation(Size560x460))
                    return Size560x460.Location;
                else if (RemoteLocation(Size1600x1200))
                    return Size1600x1200.Location;
                else if (RemoteLocation(MPeg))
                    return MPeg.Location;

                return "";
            }
        }
        public string ImagePath
        {
            get
            {
                if (URLPath(ThumbNail))
                    return ThumbNail.Location;
                else if (URLPath(Size560x460))
                    return Size560x460.Location;
                else if (URLPath(Size1600x1200))
                    return Size1600x1200.Location;
                else if (URLPath(MPeg))
                    return MPeg.Location;

                return "";
            }
        }

        private bool RemoteLocation ( Graphic lGraphic )
        {
            if (lGraphic != null && lGraphic.Location != null && lGraphic.Location.Length > 0  && lGraphic.Location.ToUpper()[0] == 'H')
                return true;

            return false;
        }

        private bool URLPath(Graphic lGraphic)
        {
            if (lGraphic != null && lGraphic.Location != null && lGraphic.Location.Length > 0)
                return true;

            return false;
        }

        public int? Size1920x1080ID { get; set; }
        [ForeignKey("Size1920x1080ID")]
        public virtual Photo Size1920x1080 { get; set; }
        public int? Size1600x1200ID { get; set; }
        [ForeignKey("Size1600x1200ID")]
        public virtual Photo Size1600x1200 { get; set; }
        public int? Size1024x640ID { get; set; }
        [ForeignKey("Size1024x640ID")]
        public virtual Photo Size1024x640 { get; set; }
        public int? Size560x460ID { get; set; }
        [ForeignKey("Size560x460ID")]
        public virtual Photo Size560x460 { get; set; }
        public int? MPegID { get; set; }
        [ForeignKey("MPegID")]
        public virtual Video MPeg { get; set; }
        public int? ThumbNailID { get; set; }
        [ForeignKey("ThumbNailID")]
        public virtual Photo ThumbNail { get; set; }
        public int? PageID { get; set; }  // This is a piece of media for this hotel
        [ForeignKey("PageID")]
        public virtual Page Resort { get; set; }
        public int? CategoryID { get; set; }  // This is the category for the picture
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
        public int? CountryID { get; set; }  // This is the category for the picture
        [ForeignKey("CountryID")]
        public virtual Country Country { get; set; }
        public bool ForGallery { get; set; }
        [NotMapped]
        [Display(Name = "File")]
        public IFormFile TempNewFile { get; set; }
        [EnumDataType(typeof(MediaFormats))]
        [NotMapped]
        public MediaFormats TempNewFileFormat { get; set; }
    }
}
