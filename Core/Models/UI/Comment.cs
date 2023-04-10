using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int AuthorID { get; set; }
        public Contact Author { get; set; }
        public string Text { get; set; }
        public bool Approved { get; set; }
        public bool Trash { get; set; }
        public int PostID { get; set; } 
        public Post InResponseTo { get; set; }
        public DateTime SubmittedOn { get; set; }

    }
}
