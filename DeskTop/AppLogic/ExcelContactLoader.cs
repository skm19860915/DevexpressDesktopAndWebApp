using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronXL;

//https://ironsoftware.com/csharp/excel/examples/excel-worksheets/

namespace Desktop.AppLogic
{
    public class ExcelContact
    {
        string cTITLE = "A";
        string cFIRST = "B";
        string cLAST = "C";
        string cSTREET = "F";
        string cCITY = "G";
        string cSTATE = "H";
        string cZIP = "I";
        string cPHONE = "K";
        string cMOBILE = "M";
        string cEMAIL = "N";

        public string Title { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        private WorkSheet Sheet { get; set; }
        public ExcelContact(WorkSheet aSheet, string aRow)
        {
            Sheet = aSheet;
            Parse(aRow);
        }

        void Parse(string aRow)
        {
            Title = Sheet[cTITLE + aRow].StringValue;
            First = Sheet[cFIRST + aRow].StringValue;
            Last = Sheet[cLAST + aRow].StringValue;
            Street = Sheet[cSTREET + aRow].StringValue;
            City = Sheet[cCITY + aRow].StringValue;
            State = Sheet[cSTATE + aRow].StringValue;
            Zip = Sheet[cZIP + aRow].StringValue;
            Phone = Sheet[cPHONE + aRow].StringValue;
            Mobile = Sheet[cMOBILE + aRow].StringValue;
            Email = Sheet[cEMAIL + aRow].StringValue;

        }
    }
    public class ExcelContactLoader
    {
        public List<ExcelContact> Contacts { get; set; }

        public List<ExcelContact> Load()
        {
            WorkBook workbook = WorkBook.Load(@"C:\Users\redop\source\repos\Blitzer\Data\Database\SalesForceContacts.xlsx");
            WorkSheet sheet = workbook.WorkSheets.First();

            Contacts = new List<ExcelContact>();

            for (int i = 0; i < sheet.RowCount; i++)
                Contacts.Add(new ExcelContact(sheet, (i+1).ToString()));

            return Contacts;
        }
    }
}
