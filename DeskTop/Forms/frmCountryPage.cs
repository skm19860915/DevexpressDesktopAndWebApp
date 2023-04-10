using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;
using BlitzerCore.Business;
using Desktop.DataServices;
using BlitzerCore.Utilities;
using System.Configuration;

namespace Desktop
{
    public partial class CountryPage : Form
    {
        UICountry Country { get; set; }

        public CountryPage(int aCountryId)
        {
            InitializeComponent();
            Country = new CountryPageDataAccess(RepositoryContext.Instance).Get(aCountryId);
            //Logger.LogInfo(Country.PageTitle);
            listView1.View = View.Details;
            listView1.Columns.Add("ID");
            listView1.Columns.Add("Title");
            listView1.Columns[1].Width = 250;
            listView1.Columns.Add("Header");
            listView1.Columns[2].Width = 300;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void Save()
        {
            Country.Title = txtTitle.Text;
            Country.PageTitle = txtPageTitle.Text;
            Country.HeaderImage.BlockTitle = txtHeader.Text;
            Country.HeaderImage.Caption = txtCaption.Text;
            Country.CenterContent.Summary = txtSummary.Text;
            Country.BlockTitle = txtBlockTitle.Text;
            Country.Published = cbxPublished.Checked;
            try
            {
                new CountryPageDataAccess(RepositoryContext.Instance).Save(Country);
                Dump("frmCountryPage::Save()");
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to Save Country", e);
            }
        }

        private void Dump(string aName)
        {
            try
            {
                Logger.EnterFunction(aName);
                Logger.LogValue("Country Admin Name", Country.BlockTitle);
                Logger.LogValue("Country Page Title", Country.Title);
                if (Country.HeaderImage != null)
                {
                    var lLoc = "HeaderImage";
                    Logger.EnterFunction(lLoc);
                    Logger.LogValue("Header Block ID", Country.HeaderImage.Id);
                    if (Country.HeaderImage.Media != null)
                    {
                        Logger.LogValue("Media ID", Country.HeaderImage.Media.Id);
                        if (Country.HeaderImage.Media.Size1600x1200 != null)
                            Logger.LogValue("Size1600x1200 ID", Country.HeaderImage.Media.Size1600x1200ID);
                        else
                            Logger.LogDebug("Size1600x1200 is null");
                    }
                    else
                        Logger.LogDebug("Media is null");
                    Logger.LeaveFunction(lLoc);
                }
                else
                    Logger.LogInfo("HeaderImage is null");
                if (Country.MainImage != null)
                {
                    Logger.LogValue("Main Block ID", Country.MainImage.Id);
                    if (Country.MainImage.Media != null)
                        Logger.LogValue("Media ID", Country.MainImage.Media.Id);
                    else
                        Logger.LogDebug("Media is null");
                }
                else
                    Logger.LogInfo("MainImage is null");
            }
            finally
            {
                Logger.LeaveFunction(aName);
            }
        }

        private void CountryPage_Load(object sender, EventArgs e)
        {
            txtBlockTitle.Text = Country.BlockTitle;
            txtTitle.Text = Country.Title;
            txtPageTitle.Text = Country.PageTitle;
            txtHeader.Text = Country.HeaderImage.BlockTitle;
            txtCaption.Text = Country.HeaderImage.Caption;
            txtSummary.Text = Country.CenterContent.Summary;
            cbxPublished.Checked = Country.Published;
            if (Country.CenterContent.Video != null)
                txtVideo.Text = Country.CenterContent.Video.Location;
            Text = $"{Country.Id} - {Country.Title}";
            LoadHeaderImage();
            LoadBlockList();
            Dump("frmCountryPage::Load()");
        }

        void LoadBlockList()
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();
            foreach (var lBlockMap in Country.Blocks.OrderBy(x => x.Block.OrderId))
                if (lBlockMap.Block != null)
                    listView1.Items.Add(new ListViewItem(new string[] { lBlockMap.Block.Id.ToString(), lBlockMap.Block.Title, lBlockMap.Block.BlockTitle }));
            listView1.EndUpdate();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        int CreateBlock(int aPosition)
        {
            if (Country.Blocks == null)
                Country.Blocks = new List<BlitzerCore.Models.PageToBlockMap>();

            if (aPosition - Country.Blocks.Count() > 0)
            {
                for (int i = 0; i < (aPosition - Country.Blocks.Count()); i++)
                {
                    Country.Blocks.Add(new BlitzerCore.Models.PageToBlockMap() { Block = new Block() { BlockTitle = Country.Title + " Block" } });
                }

                new CountryPageDataAccess(RepositoryContext.Instance).Save(Country);
            }

            return Country.Blocks[aPosition - 1].Block.Id;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btnBlock2_Click(object sender, EventArgs e)
        {
        }

        private void btnBlock3_Click(object sender, EventArgs e)
        {
        }

        private void btnSelectVideo_Click(object sender, EventArgs e)
        {
            var appSettings = ConfigurationManager.GetSection("connectionStrings");
            string lAzureCString = ConfigurationManager.ConnectionStrings["Azure"].ConnectionString;

            AzureStorage lStorage = new AzureStorage(lAzureCString, RepositoryContext.Instance);
            if (Country.CenterContent.Video == null)
                Country.CenterContent.Video = new Video() { MediaFormat = MediaFormats.MPEG };

            if (mOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string lFileName = mOpenFileDialog.FileName;
                if (System.IO.File.Exists(lFileName))
                {
                    try
                    {
                        WaitIndicator.StartWaitingIndicator(this, DevExpress.Utils.Animation.WaitingAnimatorType.Ring);
                        Country.CenterContent.Video = lStorage.UploadVideoToBlob(lFileName, Country.CenterContent, BlitzerCore.Models.UI.MediaFormats.MPEG) as Video;
                        txtVideo.Text = Country.CenterContent.Video.Location;
                    }
                    finally
                    {
                        WaitIndicator.StopWaitingIndicator();
                    }
                }
            }
        }

        private void rdbHeader_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void LoadHeaderImage()
        {
            if (Country.HeaderImage.Media != null && Country.HeaderImage.Media.Size1600x1200 != null)
                pictureBox1.ImageLocation = Country.HeaderImage.Media.Size1600x1200.Location;
            else
                pictureBox1.ImageLocation = "";
        }

        private void rdbMain_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void LoadMainImage()
        {
            if (Country.MainImage.Media != null)
                pictureBox1.ImageLocation = Country.MainImage.Media.MediaLocation;
        }

        void Dump(Block aBlock, string aMesg )
        {
            Logger.EnterFunction(aMesg);
            try
            {
                if (Country.HeaderImage.Media != null)
                {
                    if (aBlock != null)
                    {
                        Logger.LogValue("Block ID", aBlock.Id);

                        if (aBlock.Media != null)
                            Logger.LogValue("Media ID", aBlock.Media.Id);
                        else
                            Logger.LogDebug("Media is null");
                    }
                    else
                        Logger.LogDebug("Block is null");
                }
            }  finally
            {
                Logger.LeaveFunction(aMesg);
            }

    }

    private void lnkPick_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Dump(Country.HeaderImage, "frmCountryPage::Change - HeaderImage Entry");
            Dump(Country.MainImage, "frmCountryPage::Change - MainImage Entry");
            using (var lForm = new frmMediaSelect(Country.HeaderImage))
            {
                if (lForm.ShowDialog() == DialogResult.OK)
                {
                    if (rdbHeader.Checked == true)
                    {
                        Country.HeaderImage.MediaID = lForm.Media.Id;
                        Country.HeaderImage.Media = lForm.Media;
                        LoadHeaderImage();
                    }
                    else
                    {
                        Country.MainImage.MediaID = lForm.Media.Id;
                        Country.MainImage.Media = lForm.Media;
                        LoadMainImage();
                    }
                    Dump(Country.HeaderImage, "frmCountryPage::Change - HeaderImage Updated");
                    Dump(Country.MainImage, "frmCountryPage::Change - MainImage Update");
                }
            }
        }

        private void rdbHeader_Click(object sender, EventArgs e)
        {
            rdbMain.Checked = false;
            rdbHeader.Checked = true;
            LoadHeaderImage();
        }

        private void rdbMain_Click(object sender, EventArgs e)
        {
            rdbHeader.Checked = false;
            rdbMain.Checked = true;
            LoadMainImage();

        }

        private void imageListBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddBlock_Click(object sender, EventArgs e)
        {
            var lBlock = new BlockBusiness(null).Create(Country.Title);
            using (var lForm = new frmBlock(lBlock))
            {
                if (lForm.ShowDialog() == DialogResult.OK)
                {
                    lForm.Save();
                    Country.Blocks.Add(new BlitzerCore.Models.PageToBlockMap() { BlockId = lForm.Block.Id });
                    try
                    {
                        new CountryPageDataAccess(RepositoryContext.Instance).Save(Country);
                    }
                    catch (Exception e1)
                    {
                        Logger.LogException("Failed to Save Country", e1);
                    }
                    LoadBlockList();
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_Click(object sender, EventArgs e)
        {
            var lSelected = listView1.SelectedIndices;
            if (lSelected.Count == 0)
                return;

            int lBlockId = int.Parse(listView1.Items[lSelected[0]].Text);
            //int lBlockId = Country.Blocks[lSelected[0]].Block.Id;

            using (var lForm = new frmBlock(lBlockId))
            {
                if (lForm.ShowDialog() == DialogResult.OK)
                {
                    lForm.Save();
                }
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            var lSelected = listView1.SelectedIndices;
            if (lSelected.Count == 0)
                return;

            int lBlockId = int.Parse(listView1.Items[lSelected[0]].Text);

            using (var lForm = new frmBlock(lBlockId))
            {
                if (lForm.ShowDialog() == DialogResult.OK)
                {
                    lForm.Save();
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void linkHeader_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Country.HeaderImage.Media != null)
            {
                using (var lForm = new frmMedia(Country.HeaderImage.Media.Id))
                {
                    if (lForm.ShowDialog() == DialogResult.OK)
                    {
                    }
                }
                LoadHeaderImage();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Country.MainImage.Media != null)
            {
                using (var lForm = new frmMedia(Country.MainImage.Media.Id))
                {
                    if (lForm.ShowDialog() == DialogResult.OK)
                    {

                    }
                }
                LoadMainImage();
            }
        }
    }
}
