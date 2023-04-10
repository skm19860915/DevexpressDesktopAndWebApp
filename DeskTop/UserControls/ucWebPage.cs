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
    public partial class ucWebPage : UserControl
    {
        public Page UIPage { get; set; }
        public event EventHandler SaveButtonClicked;
        public ucWebPage()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void Dump(string aName)
        {
            try
            {
                Logger.EnterFunction(aName);
                Logger.LogValue("UIPage Admin Name", UIPage.BlockTitle);
                Logger.LogValue("UIPage Page Title", UIPage.Title);
                if (UIPage.HeaderImage != null)
                {
                    var lLoc = "HeaderImage";
                    Logger.EnterFunction(lLoc);
                    Logger.LogValue("Header Block ID", UIPage.HeaderImage.Id);
                    if (UIPage.HeaderImage.Media != null)
                    {
                        Logger.LogValue("Media ID", UIPage.HeaderImage.Media.Id);
                        if (UIPage.HeaderImage.Media.Size1600x1200 != null)
                            Logger.LogValue("Size1600x1200 ID", UIPage.HeaderImage.Media.Size1600x1200ID);
                        else
                            Logger.LogDebug("Size1600x1200 is null");
                    }
                    else
                        Logger.LogDebug("Media is null");
                    Logger.LeaveFunction(lLoc);
                }
                else
                    Logger.LogInfo("HeaderImage is null");
                if (UIPage.MainImage != null)
                {
                    Logger.LogValue("Main Block ID", UIPage.MainImage.Id);
                    if (UIPage.MainImage.Media != null)
                        Logger.LogValue("Media ID", UIPage.MainImage.Media.Id);
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


        void Dump(Block aBlock, string aMesg)
        {
            Logger.EnterFunction(aMesg);
            try
            {
                if (UIPage.HeaderImage.Media != null)
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
            }
            finally
            {
                Logger.LeaveFunction(aMesg);
            }

        }

        private void LoadHeaderImage()
        {
            if (UIPage.HeaderImage != null && UIPage.HeaderImage.Media != null && UIPage.HeaderImage.Media.Size1600x1200 != null)
                pictureBox1.ImageLocation = UIPage.HeaderImage.Media.Size1600x1200.Location;
            else
                pictureBox1.ImageLocation = "";
        }

        private void LoadMainImage()
        {
            if (UIPage.MainImage != null && UIPage.MainImage.Media != null)
                pictureBox1.ImageLocation = UIPage.MainImage.Media.MediaLocation;
        }

        void LoadBlockList()
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();

            if (UIPage.Blocks == null)
                UIPage.Blocks = new List<PageToBlockMap>();

            foreach (var lBlockMap in UIPage.Blocks.OrderBy(x => x.Block.OrderId))
                if (lBlockMap.Block != null)
                    listView1.Items.Add(new ListViewItem(new string[] { lBlockMap.Block.Id.ToString(), lBlockMap.Block.Title, lBlockMap.Block.BlockTitle }));
            listView1.EndUpdate();
        }

        void InitPage()
        {
            if (UIPage.HeaderImage == null)
                UIPage.HeaderImage = new Block();
            if (UIPage.MainImage == null)
                UIPage.MainImage = new Block();
        }

        public void LoadPage()
        {
            InitPage();
            txtBlockTitle.Text = UIPage.BlockTitle;
            txtTitle.Text = UIPage.Title;
            txtPageTitle.Text = UIPage.PageTitle;
            
            if (UIPage.HeaderImage == null)
                UIPage.HeaderImage = new Block() { Title = UIPage.Title + " Header Block" };
            txtHeader.Text = UIPage.HeaderImage.BlockTitle;
            txtCaption.Text = UIPage.HeaderImage.Caption;

            if (UIPage.CenterContent == null)
                UIPage.CenterContent = new Content() { Header = UIPage.Title };

            txtConclusion.Text = UIPage.CenterContent.p1;
            txtSummary.Text = UIPage.CenterContent.Summary;
            cbxPublished.Checked = UIPage.Published;
            if (UIPage.CenterContent.Video != null)
                txtVideo.Text = UIPage.CenterContent.Video.Location;
            Text = $"{UIPage.Id} - {UIPage.Title}";
            LoadHeaderImage();
            LoadBlockList();
            Dump("ucWebPage::Load()");

        }

        public void Save()
        {
            UIPage.Title = txtTitle.Text;
            UIPage.PageTitle = txtPageTitle.Text;
            UIPage.HeaderImage.BlockTitle = txtHeader.Text;
            UIPage.HeaderImage.Caption = txtCaption.Text;
            UIPage.CenterContent.Summary = txtSummary.Text;
            UIPage.BlockTitle = txtBlockTitle.Text;
            UIPage.Published = cbxPublished.Checked;
            UIPage.CenterContent.p1 = txtConclusion.Text;
            Dump("frmUIPagePage::Save()");
        }


        private void lnkPick_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Dump(UIPage.HeaderImage, "frmUIPagePage::Change - HeaderImage Entry");
            Dump(UIPage.MainImage, "frmUIPagePage::Change - MainImage Entry");
            using (var lForm = new frmMediaSelect(UIPage.HeaderImage))
            {
                if (lForm.ShowDialog() == DialogResult.OK)
                {
                    if (rdbHeader.Checked == true)
                    {
                        UIPage.HeaderImage.MediaID = lForm.Media.Id;
                        UIPage.HeaderImage.Media = lForm.Media;
                        LoadHeaderImage();
                    }
                    else
                    {
                        UIPage.MainImage.MediaID = lForm.Media.Id;
                        UIPage.MainImage.Media = lForm.Media;
                        LoadMainImage();
                    }
                    Dump(UIPage.HeaderImage, "frmUIPagePage::Change - HeaderImage Updated");
                    Dump(UIPage.MainImage, "frmUIPagePage::Change - MainImage Update");
                }
            }
        }

        private void rdbHeader_Click(object sender, EventArgs e)
        {
            rdbMain.Checked = false;
            rdbHeader.Checked = true;
            LoadHeaderImage();
        }

        private void rdbMain_CheckedChanged(object sender, EventArgs e)
        {
            rdbHeader.Checked = false;
            rdbMain.Checked = true;
            LoadMainImage();
        }

        private void linkHeader_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (UIPage.HeaderImage.Media != null)
            {
                using (var lForm = new frmMedia(UIPage.HeaderImage.Media.Id))
                {
                    if (lForm.ShowDialog() == DialogResult.OK)
                    {
                    }
                }
                LoadHeaderImage();
            }
            else
                Logger.LogDebug("UIPage HeadeImage is null");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (UIPage.MainImage.Media != null)
            {
                using (var lForm = new frmMedia(UIPage.MainImage.Media.Id))
                {
                    if (lForm.ShowDialog() == DialogResult.OK)
                    {

                    }
                }
                LoadMainImage();
            }
            else
                Logger.LogDebug("UIPage MainImage is null");

        }

        protected virtual void OnSaveButtonClicked(EventArgs e)
        {
            SaveButtonClicked?.Invoke(this, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            OnSaveButtonClicked(EventArgs.Empty);
        }

        private void ucWebPage_Load(object sender, EventArgs e)
        {

        }
    }
}
