using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlitzerCore.Utilities;
using Desktop.DataServices;

namespace Desktop
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Logger.Init("Desktop");
            Logger.InitConsummer();
            Logger.ConnectionFactory = new DataServices.ConcreteFactory();

            using ( var lForm = new frmEnvSelect())
            {
                if ( lForm.ShowDialog() == DialogResult.OK)
                {
                    RepositoryContext.Environment = lForm.Environment;
                }
            }

            Logger.ConnectionString = RepositoryContext.Instance.ConnectionString;
            Logger.LogInfo($"{Desktop.BlitzerDesktop.Label} : Starting Application with DB=" + Logger.ConnectionString);

            Application.Run(new BlitzerMainForm());
        }
    }
}
