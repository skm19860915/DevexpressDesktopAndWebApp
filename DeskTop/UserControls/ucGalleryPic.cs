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

namespace Desktop.UserControls
{
    public partial class ucGalleryPic : UserControl
    {
        public Media Media { get; set; }
        public ucGalleryPic()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using ( var lForm = new frmMedia(Media))
            {
                if ( lForm.ShowDialog() == DialogResult.OK)
                {
                    Media = lForm.Media;
                    LoadControl();
                }
            }
        }

        public void LoadControl()
        {
            if (Media == null)
            {
                Logger.LogDebug("ucGalleryPic::LoadControl - Didn't load because media was null");
                return;
            }

            picBox.ImageLocation = Media.MediaLocation;
            lblName.Text = Media.Title;
        }

        private void ucGalleryPic_Load(object sender, EventArgs e)
        {
            LoadControl();
        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }
    }
}
