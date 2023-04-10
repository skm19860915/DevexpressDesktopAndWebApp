using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public class Content
    {
        public Content()
        {
            Paragraphs = new List<Paragraph>();
        }
        public int Id { get; set; }
        public string Header { get; set; }
        public string Caption { get; set; }
        public string Summary { get; set; }
        public Video Video { get; set; }
        public Photo Photo { get; set; }
        public List<Paragraph> Paragraphs { get; set; }
        public string p1 { get; set; }
        public string p2 { get; set; }
        public string p3 { get; set; }
        public string p4 { get; set; }
    }
}
