using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlitzerCore.Models.UI
{
    public class Page
    {
        public Page()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }  // Title seen in the admin page (Index) list of pages
        public string PageTitle { get; set; }  // Page Title seen by Travelers
        public string PageCaption { get; set; }  // Title seen in the admin page (Index) list of pages
        public int? CountryId { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
        public int? HeaderImageID { get; set; }
        [ForeignKey("HeaderImageID")]
        public virtual Block HeaderImage { get; set; } // Image at the top of the page 
        public int? ContentID { get; set; }
        [ForeignKey("ContentID")]
        public virtual Content CenterContent { get; set; }
        public string BlockTitle { get; set; }
        public int? MainImageID { get; set; }
        [ForeignKey("MainImageID")]
        public virtual Block MainImage { get; set; }
        public string ImageURL {
            get
            {
                if (HeaderImage != null && HeaderImage.Media != null)
                    return HeaderImage.Media.ImagePath;

                return "";
            }
        }
        public List<PageToBlockMap> Blocks { get; set; }
        public bool Published { get; set; }
        public DateTime? PublishedOn { get; set; }
        public string AuthorID { get; set; }
        [ForeignKey("AuthorID")]
        public virtual Contact Author { get; set; }
        public string MyUrl
        {
            get
            {
                if (OverRideUrl != null)
                    return OverRideUrl;

                if (Controller != null && Action != null)
                    return "/" + Controller + "/" + Action;

                switch (PageTypeId)
                {
                    case 1:
                        if (CenterContent == null ||
                            CenterContent.Header == "Update the Header")
                            return null;

                        return "/ResortPage/Details/" + Id;
                    case 2:
                        return "/Country/Details/" + Id;
                    case 3:
                        return "/Ranking/Details/" + Id;
                    case 4:
                        return "/Gallary/Details/" + Id;
                    default:
                        return null;
                }
            }
        }

        public string OverRideUrl { get; set; }

        public int PageTypeId { get; set; }
        public string Controller { get; set; }  //Controller used for Redirect
        public string Action { get; set; }  // Action on the controller for the redirect

        public virtual PageType PageType { get; set; }
        [NotMapped]
        public List<SelectListItem> PageTypes { get; set; }
    }
}
