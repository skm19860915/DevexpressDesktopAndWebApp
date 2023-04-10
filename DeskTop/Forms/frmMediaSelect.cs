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
    
namespace Desktop
{
    public partial class frmMediaSelect : Form
    {
        Block Block { get; set; }
        public Media Media { get; internal set; }
        frmPicture ChildPicture { get; set; }

        public frmMediaSelect(Block aBlock)
        {
            InitializeComponent();
            Block = aBlock;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void frmMediaSelect_Load(object sender, EventArgs e)
        {
            LoadForm();
            ChildPicture = new frmPicture();
            ChildPicture.Show();
            ChildPicture.Update(this);
        }

        void LoadForm(Media aMedia)
        {
            this.grdMedias.DataSource = new MediaDataAccess(RepositoryContext.Instance).GetAllFlatten();

            if (aMedia == null)
                return;
        }

        void LoadForm()
        {

            this.grdMedias.DataSource = new MediaDataAccess(RepositoryContext.Instance).GetAllFlatten();
        }

        private void mediaBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void grdMedias_Click(object sender, EventArgs e)
        {
            SelectMedia();
        }

        private void SelectMedia()
        {
            SelectMedia lSelect = null;

            foreach (int i in this.gridVw_SelectMedia.GetSelectedRows())
            {
                lSelect = gridVw_SelectMedia.GetRow(i) as SelectMedia;
            }

            if (lSelect != null)
            {
                Media = new MediaDataAccess(RepositoryContext.Instance).Get(lSelect.Id);
                if ( ChildPicture != null )
                    ChildPicture.Update(Media);
                Logger.LogDebug($"frmMediaSelect() - Media ID Selected [{Media.Id}]");
            }
        }

        private void grdMedias_DoubleClick(object sender, EventArgs e)
        {
            Logger.LogDebug("frmMediaSelect::GridDoubleClicked");
            SelectMedia lSelect = null;

            foreach (int i in this.gridVw_SelectMedia.GetSelectedRows())
            {
                lSelect = gridVw_SelectMedia.GetRow(i) as SelectMedia;
            }

            if (lSelect == null)
                return;

            Media = new MediaDataAccess(RepositoryContext.Instance).Get(lSelect.Id);

            using (var lForm = new frmMedia(lSelect.Id))
            {
                if (lForm.ShowDialog() == DialogResult.OK)
                {
                    LoadForm();
                }
            }
        }

        private void btnSave_Clicked(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Select Button Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            SelectMedia lSelect = null;

            foreach (int i in this.gridVw_SelectMedia.GetSelectedRows())
            {
                lSelect = gridVw_SelectMedia.GetRow(i) as SelectMedia;
            }

            if (lSelect == null)
                return;
            Logger.LogDebug($"frmMediaSelect::SelectButtonClicked - Media [{lSelect.Id}] Selected");
            Media = new MediaDataAccess(RepositoryContext.Instance).Get(lSelect.Id);
        }

        private void frmMediaSelect_Move(object sender, EventArgs e)
        {

        }

        private void frmMediaSelect_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChildPicture.Close();
            ChildPicture.Dispose();
        }

        private void gridVw_SelectMedia_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void gridVw_SelectMedia_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SelectMedia();
        }

        private void btnAddMedia_Clicked(object sender, EventArgs e)
        {
            Logger.LogDebug($"frmMediaSelect::AddMedia Clicked Create new Media ");
            using (var lForm = new frmMedia(new Media() ))
            {
                if (lForm.ShowDialog() == DialogResult.OK)
                {
                    lForm.Save();
                    Media = lForm.Media;
                    Logger.LogDebug($"frmMediaSelect::AddMedia Save Clicked - New Media ID = {Media.Id}");
                }
                LoadForm(Media);
                SelectMedia(Media);
            }
        }

        private void SelectMedia(Media aMedia)
        {
            int lIndex = gridVw_SelectMedia.RowCount - 1;
            var lRow = gridVw_SelectMedia.GetDataRow(lIndex);
            //if ( lRow.ItemArray[0].)
            gridVw_SelectMedia.SelectRow(lIndex);
        }
    }
}
