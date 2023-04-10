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
    public partial class ucTilePic : UserControl
    {
        public Tile Tile { get; set; }
        public ucTilePic()
        {
            InitializeComponent();
        }

        private void picTile_Click(object sender, EventArgs e)
        {
            using (var lForm = new frmTile())
            {
                lForm.Tile = Tile;
                if (lForm.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private void ucTilePic_Load(object sender, EventArgs e)
        {
            if (Tile == null )
            {
                Logger.LogDebug("ucTilePic::Load - Failed to load Tile because it was null");
                return;
            }

            if (Tile.Category != null)
                lblCategory.Text = Tile.Category.Name;
            else
                lblCategory.Text = "No Category Selected";
            if ( Tile.Media != null )
                picTile.ImageLocation = Tile.Media.MediaLocation;
        }
    }
}
