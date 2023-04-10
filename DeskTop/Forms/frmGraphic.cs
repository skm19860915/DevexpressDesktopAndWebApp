using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using Desktop.DataServices;
using BlitzerCore.Utilities;
using System.Configuration;

namespace Desktop
{
    public partial class frmGraphic : Form
    {
        public int Id { get; set; }
        public Media Media { get; set; }
        public Graphic Graphic { get; set; }
        public frmGraphic(Media aMedia, Graphic aGraphic)
        {
            Graphic = aGraphic;
            Media = aMedia;
            if (Graphic != null)
                Id = Graphic.ID;
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        void LoadForm()
        {
            Graphic lGraphic = Graphic;
            if (Graphic == null)
                lGraphic = new MediaDataAccess(RepositoryContext.Instance).GetGraphic(Id);

            if (lGraphic == null)
                return;

            Text = $"{lGraphic.ID} - Graphic";
            rdb1600x1200.Checked = lGraphic.MediaFormat == MediaFormats.Size_1600x1200;
            rdb560x460.Checked = lGraphic.MediaFormat == MediaFormats.Size_560x460;
            rdbMPeg.Checked = lGraphic.MediaFormat == MediaFormats.MPEG;
            txtPath.Text = lGraphic.Location;
            pictureBox1.ImageLocation = lGraphic.Location;

        }

        private void frmGraphic_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void rdb1600x1200_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        int Save()
        {
            Graphic lGraphic = Graphic;
            if ( lGraphic == null )
                Graphic = new MediaDataAccess(RepositoryContext.Instance).GetGraphic(Id);
            if (lGraphic == null)
                lGraphic = new Graphic();

            if (rdb1600x1200.Checked)
                lGraphic.MediaFormat = MediaFormats.Size_1600x1200;
            else if (rdb560x460.Checked)
                lGraphic.MediaFormat = MediaFormats.Size_560x460;
            else
                lGraphic.MediaFormat = MediaFormats.MPEG;
            
            Graphic = lGraphic;

            return new MediaDataAccess(RepositoryContext.Instance).Save(Graphic);
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            var appSettings = ConfigurationManager.GetSection("connectionStrings");
            string lAzureCString = ConfigurationManager.ConnectionStrings["Azure"].ConnectionString;

            if (mOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                AzureStorage lStorage = new AzureStorage(lAzureCString, RepositoryContext.Instance);
                string lFileName = mOpenFileDialog.FileName;
                if (System.IO.File.Exists(lFileName))
                {
                    if (Graphic.MediaFormat == MediaFormats.MPEG)
                        Graphic = lStorage.UploadVideoToBlob(lFileName, Media, Graphic.MediaFormat);
                    else
                        Graphic = lStorage.UploadPhotoToBlob(lFileName, Media, Graphic.MediaFormat);

                    // Need to update ID because it is a new image with no ID
                    Id = 0;
                }
            }
            LoadForm();
        }
    }
}
