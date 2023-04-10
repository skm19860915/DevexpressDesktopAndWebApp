using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;

namespace Desktop
{
    public partial class frmPicture : Form
    {
        public frmPicture()
        {
            InitializeComponent();
        }

        private void frmPicture_Load(object sender, EventArgs e)
        {
        }

        private void Dump(string aLoc, Media aMedia)
        {
            Logger.EnterFunction(aLoc);
            if (aMedia.Size560x460 != null)
                Logger.LogDebug("560x460 Path : " + aMedia.Size560x460.Location);
            else
                Logger.LogDebug("560x460 is null ");
            if (aMedia.Size1600x1200 != null)
                Logger.LogDebug("1600x1200 Path : " + aMedia.Size1600x1200.Location);
            else
                Logger.LogDebug("1600x1200 is null ");
            Logger.LeaveFunction(aLoc);
        }
        public void Update(Media aMedia)
        {
            if ( aMedia == null )
            {
                Logger.LogDebug("frmPicture::Update - Media is null");
                return;
            }
            this.Text = aMedia.Title;
            pbxPicture.ImageLocation = aMedia.MediaLocation;

            Dump("frmPicture::Update", aMedia);
            if (aMedia.MediaLocation.Length == 0 || aMedia.MediaLocation.ToUpper()[0] != 'H')
            {
                Text = "Can't Display " + aMedia.MediaLocation;
            }
        }

        int CenteredY ( Form aParent )
        {
            int ParentY = aParent.Location.Y;
            int ParentHeight = aParent.Height;
            int MyHeight = Height;
            double Space = (ParentHeight - MyHeight)/2;
            return Convert.ToInt32( ParentY + Space);

        }

        public void Update(Form aParent)
        {
            this.Location = new Point(aParent.Location.X - this.Width - 10, CenteredY(aParent));
        }

        private void pbxPicture_Click(object sender, EventArgs e)
        {

        }
    }
}
