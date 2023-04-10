using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public class SalesData
    {
        public string Month { get; set; }
        public double Existing { get; set; }
        public double Facebook { get; set; }
        public double Instagram { get; set; }
        public double Youtube { get; set; }
        public double Google { get; set; }
        public double Friend { get; set; }
        public double Website { get; set; }
        public double Commission { get; set; }
    }


    public class UIWarRoom
    {
        public string YTDSales { get; set; }
        public string YTDComm { get; set; }
        public string MonthlyP_L { get; set; }
        public List<UITask> FollowUps { get; set; }
        public List<UITask> Tasks { get; set; }
        public List<UITask> Issues { get; set; }
        public List<UIOpportunity> Opportunities { get; set; }
        public IEnumerable<UINote> Notes { get; set; }
        public List<SalesData> Sales { get; set; }
        public string Existing { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Friend { get; set; }
        public string Google { get; set; }
        public string Website { get; set; }
        public string ExistingCSS { get; set; }
        public string FacebookCSS { get; set; }
        public string InstagramCSS { get; set; }
        public string FriendCSS { get; set; }
        public string GoogleCSS { get; set; }
        public string WebsiteCSS { get; set; }
        public string PLCSS { get; set; }
    }
}
