using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlitzerCore.Business;
using BlitzerCore.Utilities;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using Desktop.DataServices;
using Desktop.UserControls;
using BlitzerCore.DataAccess;

namespace Desktop
{
    public partial class frmTile : Form
    {
        public Tile Tile { get; set; }
        public frmTile()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;

            Tile.Category = (comboBox1.SelectedItem as Category);
            Tile.CategoryID = Tile.Category.Id;
            LoadGallery();
        }

        private void lnkSelectPics_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using ( var lForm = new frmMediaSelect(Tile))
            {
                if ( lForm.ShowDialog() == DialogResult.OK )
                {
                    frmTile_Load(null, EventArgs.Empty);
                }
            }
        }

        private void frmTile_Load(object sender, EventArgs e)
        {
            var lCategories = new CategoryDataAccess(RepositoryContext.Instance).GetAll();
            comboBox1.DataSource = lCategories;

            if ( Tile.CategoryID == null )
            {
                Logger.LogWarning($"frmTile::Load - Can't load form because Category is empty which yields no pictures");
                return;
            }

            comboBox1.SelectedItem = lCategories.Where(x => x.Id == Tile.CategoryID);
            LoadGallery();
        }

        void LoadGallery()
        {
            // Get all the Media Items for the Tile Category
            if (Tile.CategoryID == null)
            {
                Logger.LogWarning($"frmTile::Load - Can't load Gallery because Category is empty which yields no pictures");
                return;
            }

            if (Tile.ResortID == null)
            {
                Logger.LogWarning($"frmTile::Load - Can't load Gallery because ResortID is empty which yields no pictures");
                return;
            }

            var lMedias = new ResortPageBusiness(RepositoryContext.Instance).GetGalleryPhotos(Tile.ResortID.Value, Tile.CategoryID.Value);

            LoadGalleryPic(ucGalleryPic1, 0, lMedias);
            LoadGalleryPic(ucGalleryPic2, 1, lMedias);
            LoadGalleryPic(ucGalleryPic3, 2, lMedias);
            LoadGalleryPic(ucGalleryPic4, 3, lMedias);
            LoadGalleryPic(ucGalleryPic5, 4, lMedias);
            LoadGalleryPic(ucGalleryPic6, 5, lMedias);
            LoadGalleryPic(ucGalleryPic7, 6, lMedias);
            LoadGalleryPic(ucGalleryPic8, 7, lMedias);

        }

        private void LoadGalleryPic(ucGalleryPic aGalleryPic, int aIndex, List<Media> lMedias)
        {
            if ( lMedias.Count() - 1 < aIndex)
            {
                Logger.LogDebug($"Didn't load Gallery pic as position aIndex because there were only {lMedias.Count()} photos");
                return;
            }

            aGalleryPic.Media = lMedias[aIndex];
            aGalleryPic.LoadControl();
        }
    }
}
