using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CSharp;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Data.SqlClient;

namespace DataBaseLoader
{
    public partial class Form1 : Form
    {
        public class AirPort
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
            public Uri URL { get { return new Uri("Https://www.eze2travel.com/airport"); } }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void FileSelected_ClickedOk(object sender, CancelEventArgs e)
        {
            Excel.Application xlApp;
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            Range range;

            int rCnt;
            int rw = 0;
            int cl = 0;

            xlApp = new Excel.Application();
            //open the excel
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text);
            //get the first sheet of the excel
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

            range = xlWorkSheet.UsedRange;
            // get the total row count
            rw = range.Rows.Count;
            //get the total column count
            cl = range.Columns.Count;

            List<AirPort> lAirPorts = new List<AirPort>();
            // traverse all the row in the excel
            for (rCnt = 2; rCnt <= rw; rCnt++)
            {

                AirPort lAirPort = new AirPort();
                if ((range.Cells[rCnt, 1] as Range).Value2 == null)
                    continue;

                lAirPort.Code = (string)(range.Cells[rCnt, 1] as Range).Value2.ToString();
                if ( (range.Cells[rCnt, 2] as Range).Value2 != null ) 
                    lAirPort.Name = Escape((string)(range.Cells[rCnt, 2] as Range).Value2.ToString());

                if (lAirPort.Name == null || lAirPort.Name.Length == 0)
                    continue;

                if ( (range.Cells[rCnt, 3] as Range).Value2 != null ) 
                    lAirPort.Country = Escape((string)(range.Cells[rCnt, 3] as Range).Value2.ToString());
                lAirPorts.Add(lAirPort);
            }

            //release the resources
            xlWorkBook.Close(true, null, null);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            LoadIntoDB(lAirPorts);
        }

        string Escape (string aInput )
        {
            if (aInput == null)
                return aInput;

            return aInput.Replace("'", "''");
        }

        private void LoadIntoDB(List<AirPort> aData)
        {
            ConnectionStringSettings mySQLConSettings = ConfigurationManager.ConnectionStrings["Blitzer"];

            string ConnectionString = mySQLConSettings.ConnectionString;
            SqlConnection mySQLConn = null;
            try
            {

                mySQLConn = new SqlConnection(ConnectionString);
                mySQLConn.Open();
                foreach (var lAirPort in aData)
                {
                    string lSQL = string.Format("Insert into AirPort (Code, Name, Country, URL) values ( '{0}', '{1}', '{2}', '{3}')", lAirPort.Code, lAirPort.Name, lAirPort.Country, lAirPort.URL);
                    var mySQLCommand = new SqlCommand(lSQL, mySQLConn);
                    var lcount = mySQLCommand.ExecuteNonQuery();
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                mySQLConn.Close();
            }

        }
        private void ImportAirports_Click(object sender, EventArgs e)
        {
            mOpenFileDialog = new OpenFileDialog();
            mOpenFileDialog.ShowDialog();
            txtFilePath.Text = mOpenFileDialog.FileName;
            FileSelected_ClickedOk(null, null);
        }
    }
}
