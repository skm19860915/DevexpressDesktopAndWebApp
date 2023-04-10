using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;
using Desktop.DataServices;
using BlitzerCore.Utilities;
using System.Configuration;

namespace Desktop
{
    public partial class frmMedia : Form
    {
        int MediaId { get; set; }
        public Media Media { get; set; }
        public frmMedia(Media aMedia)
        {
            InitializeComponent();
            Media = aMedia;
            if (Media == null)
                Media = new Media();
            EnableAddButtons();
        }
        void EnableAddButtons()
        {
            btnUpload.Enabled = txtTitle.Text.Length > 0;
            if (btnUpload.Enabled)
                lblRequired.Text = "";
            else
                lblRequired.Text = "Requires Title";
            EnableSaveButton();
        }

        public frmMedia(int aID)
        {
            InitializeComponent();
            MediaId = aID;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void LoadMedia()
        {
            if (Media == null)
                Media = new MediaDataAccess(RepositoryContext.Instance).Get(MediaId);

            if (Media == null)
                Media = new Media();
        }

        private void Dump(string aName )
        {
            try
            {
                Logger.EnterFunction(aName);
                Logger.LogValue("Media Name", Media.Title);
                Logger.LogValue("Media Id", Media.Id);
                if (Media.Size560x460 != null)
                    Logger.LogValue("560x460 ID", Media.Size560x460.ID);
                else
                    Logger.LogInfo("560x460 is null");
                if (Media.Size1600x1200 != null)
                    Logger.LogValue("1600x1200 ID", Media.Size1600x1200.ID);
                else
                    Logger.LogInfo("1600x1200 is null");
                if (Media.MPeg != null)
                    Logger.LogValue("MPEG ID", Media.MPeg.ID);
                else
                    Logger.LogInfo("MPEG is null");
            }
            finally
            {
                Logger.LeaveFunction(aName);
            }

        }

        private void frmMedia_Load(object sender, EventArgs e)
        {

            LoadMedia();

            cmbCategory.DataSource = new CategoryDataAccess(RepositoryContext.Instance).GetAll();
            cmbLocation.DataSource = new CountryDataAccess(RepositoryContext.Instance).GetAll();
            cmbResort.DataSource = new ResortPageDataAccess(RepositoryContext.Instance).GetAll();

            if (Media == null)
            {
                Logger.LogInfo("frmMedia openned with null media");
                return;
            }
            if (Media.Id > 0)
                this.Text = $"{Media.Id} - {Media.Title}";
            else
                Text = "New Media Item";

            Dump("frmMedia_Load");
            txtTitle.Text = Media.Title;
            LoadImage();
            cbx560x460.Checked = Media.Size560x460 != null;
            cbx1024x640.Checked = Media.Size1024x640 != null;
            cbx1600x1200.Checked = Media.Size1600x1200 != null;
            cbxMPEG.Checked = Media.MPeg != null;


            cmbCategory.SelectedItem = Media.Category;
            cmbLocation.SelectedItem = Media.Country;
            cmbResort.SelectedItem = Media.Resort;
            EnableSaveButton();
        }

        void LoadImage()
        {
            picThumbnail.ImageLocation = Media.MediaLocation;
        }

        public void Save()
        {
            LoadMedia();

            Media.Title = txtTitle.Text;
            if (cmbCategory.SelectedValue != null)
                Media.CategoryID = (int)cmbCategory.SelectedValue;
            if (cmbLocation.SelectedValue != null)
                Media.CountryID = (int)cmbLocation.SelectedValue;
            Media.Resort = cmbResort.SelectedItem as Page;
            new MediaDataAccess(RepositoryContext.Instance).Save(Media);
            Dump("frmMedia::Save");
        }

        void MediaIDAssert()
        {
            if (Media.Id == 0)
            {
                MessageBox.Show("Can't set graphic name", "Media ID = 0", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

        }

        private void linkVideo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Media.MPeg == null)
            {
                if (Media.Id == 0)
                    Save();
                Media.MPeg = UploadGraphic(MediaFormats.MPEG) as Video;
                Logger.LogInfo($"Saved new video with to Blob at {Media.MPeg.Location}");
            }
            if (Media.MPeg != null)
            {
                using (var lForm = new frmGraphic(Media, Media.MPeg))
                {
                    if (lForm.ShowDialog() == DialogResult.OK)
                    {
                        Media.MPeg = lForm.Graphic as Video;
                        LoadImage();
                    }
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableSaveButton();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
        }

        private Graphic UploadGraphic(MediaFormats aFormat)
        {
            MediaIDAssert();
            var appSettings = ConfigurationManager.GetSection("connectionStrings");
            string lAzureCString = ConfigurationManager.ConnectionStrings["Azure"].ConnectionString;

            if (mOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                AzureStorage lStorage = new AzureStorage(lAzureCString, RepositoryContext.Instance);
                string lFileName = mOpenFileDialog.FileName;
                Logger.LogInfo($"Uploading Graphic file {lFileName} to Media ID = ${Media.Id}");
                if (System.IO.File.Exists(lFileName))
                {
                    Graphic lGraphic = null;
                    if (aFormat == MediaFormats.MPEG)
                        lGraphic = lStorage.UploadVideoToBlob(lFileName, Media, aFormat);
                    else
                        lGraphic = lStorage.UploadPhotoToBlob(lFileName, Media, aFormat);

                    Logger.LogInfo($"Saved new graphic {aFormat} in blob at {lGraphic.Location}");

                    return lGraphic;
                }
            }

            return null;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cbx560x460_CheckedChanged(object sender, EventArgs e)
        {
            EnableAddMedia();
        }

        private void txtTitle_KeyDown(object sender, KeyEventArgs e)
        {
            EnableAddMedia();
        }

        private void EnableAddMedia()
        {
            var lStatus = cbx1024x640.Checked || cbx1600x1200.Checked || cbx560x460.Checked || cbxMPEG.Checked;
            lStatus = lStatus && txtTitle.TextLength > 0;
            btnUpload.Enabled = true;
        }

        private void cbx1024x640_CheckedChanged(object sender, EventArgs e)
        {
            EnableAddMedia();
        }

        private void cbx1600x1200_CheckedChanged(object sender, EventArgs e)
        {
            EnableAddMedia();
        }

        private void cbxMPEG_CheckedChanged(object sender, EventArgs e)
        {
            EnableAddMedia();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Media.Size560x460 == null)
            {
                if (Media.Id == 0)
                    Save();
                Media.Size560x460 = UploadGraphic(MediaFormats.Size_1600x1200) as Photo;
                Logger.LogInfo($"Saved new 560x460 image with to blob at {Media.MPeg.Location}");
            }
            if (Media.Size560x460 != null)
            {
                using (var lForm = new frmGraphic(Media, Media.Size560x460))
                {
                    if (lForm.ShowDialog() == DialogResult.OK)
                    {
                        Media.Size560x460 = lForm.Graphic as Photo;
                        LoadImage();
                    }
                }
            }

        }

        private void link1600x1200_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Media.Size1600x1200 == null)
            {
                if (Media.Id == 0)
                    Save();
                Media.Size1600x1200 = UploadGraphic(MediaFormats.Size_1600x1200) as Photo;
            }

            if (Media.Size1600x1200 != null)
            {
                using (var lForm = new frmGraphic(Media, Media.Size1600x1200))
                {
                    if (lForm.ShowDialog() == DialogResult.OK)
                    {
                        Media.Size1600x1200 = lForm.Graphic as Photo;
                        LoadImage();
                    }
                }
            }
        }

        private void EnableSaveButton()
        {
            bool TitleCheck = txtTitle.Text != null && txtTitle.Text.Length > 0;
            bool CategoryCheck = cmbCategory.SelectedValue != null;
            bool LocationCheck = cmbLocation.SelectedValue != null;

            btnSave.Enabled = (TitleCheck && CategoryCheck && LocationCheck);
            if (!TitleCheck && !CategoryCheck && !LocationCheck)
                lblSaveVal.Text = "Requires Title, Category and location";
            else if (TitleCheck && !CategoryCheck && !LocationCheck)
                lblSaveVal.Text = "Requires Category and Location";
            else if (!TitleCheck && CategoryCheck && !LocationCheck)
                lblSaveVal.Text = "Requires Title and Location";
            else if (!TitleCheck && !CategoryCheck && LocationCheck)
                lblSaveVal.Text = "Requires Title and Category";
            else if (TitleCheck && CategoryCheck && !LocationCheck)
                lblSaveVal.Text = "Requires Location";
            else if (TitleCheck && !CategoryCheck && LocationCheck)
                lblSaveVal.Text = "Requires Category";
            else
                lblSaveVal.Text = "";
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableSaveButton();
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            EnableSaveButton();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
