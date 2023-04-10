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
using Desktop.DataServices;
using BlitzerCore.Utilities;
using System.Configuration;
using BlitzerCore.Models.UI;

namespace Desktop
{
    public partial class frmBlock : Form
    {
        int BlockId { get; set; }
        public Block Block { get; set; }
        public frmBlock(int? aID = 0)
        {
            InitializeComponent();
            BlockId = aID.Value;
        }

        public frmBlock(Block aBlock)
        {
            InitializeComponent();
            Block = aBlock;
        }
        private void frmBlock_Load(object sender, EventArgs e)
        {
            if (BlockId > 0)
                Block = new BlockDataAccess(RepositoryContext.Instance).Get(BlockId);

            txtTitle.Text = Block.Title;
            txtPageTitle.Text = Block.BlockTitle;
            txtCaption.Text = Block.Caption;
            txtBody.Text = Block.Body;
            txtOrder.Text = Block.OrderId.ToString();
            txtListTitle.Text =  Block.ListTitle;
            cbxPublished.Checked = Block.Published;
            Text = $"{Block.Id} - {Block.Title} Block";
            LoadMediaPicture();
            LoadPage();
        }

        private void LoadMediaPicture()
        {
            if (Block.Media != null)
            {
                if (Block.Media.Size560x460 != null)
                    picBlock.ImageLocation = Block.Media.Size560x460.Location;
                else if (Block.Media.ThumbNail != null)
                    picBlock.ImageLocation = Block.Media.ThumbNail.Location;
                lblPicTitle.Text = Block.Media.Title;
            } else
            {
                lblPicTitle.Text = "No Picture Selected";
            }

        }

        internal void Save()
        {
            var lBlockDA = new BlockDataAccess(RepositoryContext.Instance);
            Block lBlock = Block;
            if ( BlockId > 0 )
                lBlock = lBlockDA.Get(BlockId);

            lBlock.Title = txtTitle.Text;
            lBlock.BlockTitle = txtPageTitle.Text;
            lBlock.Caption = txtCaption.Text;
            lBlock.Body = txtBody.Text;
            lBlock.ListTitle = txtListTitle.Text;
            lBlock.Published = cbxPublished.Checked;
            int lOrder = 10;
            if (int.TryParse(txtOrder.Text, out lOrder))
                lBlock.OrderId = lOrder;

            try
            {
                lBlockDA.Save(lBlock);
                Block = lBlock;
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to Save Block", e);
            }
        }

        private void linkAddPic_Click(object sender, EventArgs e)
        {
            //var appSettings = ConfigurationManager.GetSection("connectionStrings");
            //string lAzureCString = ConfigurationManager.ConnectionStrings["Azure"].ConnectionString;

            //AzureStorage lStorage = new AzureStorage(lAzureCString, RepositoryContext.Instance);
            //if (mOpenFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    string lFileName = mOpenFileDialog.FileName;
            //    if (System.IO.File.Exists(lFileName))
            //    {
            //        lStorage.UploadMediaToBlob(lFileName, Block.Media, BlitzerCore.Models.UI.MediaFormats.Size_560x460);
            //        picBlock.ImageLocation = Block.Media.Size560x460.Location;
            //    }
            //}
        }

        private void linkSelectPage_Click(object sender, EventArgs e)
        {
            using (var lForm = new frmPageSelect())
            {
                if (lForm.ShowDialog() == DialogResult.OK)
                {
                    if (Block.PageMap == null)
                        Block.PageMap = new BlitzerCore.Models.BlockToPageMap();
                    Block.PageMap.BlockId = Block.Id;
                    Block.PageMap.PageId = lForm.PageId;
                    new BlockDataAccess(RepositoryContext.Instance).Save(Block);
                    LoadPage();
                }
            }
        }

        void LoadPage()
        {
            if (Block.PageMap == null || Block.PageMap.Page == null )
                lblPageTitle.Text = "No Page Selected";
            else if (Block.PageMap.Page.HeaderImage != null && Block.PageMap.Page.HeaderImage.Media != null)
            {
                var lImage = Block.PageMap.Page.HeaderImage.Media;
                if (lImage.ThumbNail != null)
                    picPage.ImageLocation = lImage.ThumbNail.Location;
                else if (lImage.Size560x460 != null)
                    picPage.ImageLocation = lImage.Size560x460.Location;
                else if (lImage.Size1600x1200 != null)
                    picPage.ImageLocation = lImage.Size1600x1200.Location;
                lblPageTitle.Text = Block.PageMap.Page.Title;
            } else
            {
                lblPageTitle.Text = Block.PageMap.Page.Title;
            }
        }
        private void linkSelectPic_Click(object sender, EventArgs e)
        {
            using ( var lForm = new frmMediaSelect(Block))
            {
                if (lForm.ShowDialog() == DialogResult.OK)
                {
                    Block.Media = lForm.Media;
                    Save();
                    LoadMediaPicture();
                }
            }
        }

        private void picPage_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void picBlock_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void lblPageTitle_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void lblPicTitle_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtBody_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
