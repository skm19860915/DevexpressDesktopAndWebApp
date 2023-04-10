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
using Desktop.DataServices;
using Desktop.UserControls;

namespace Desktop
{
    public partial class frmPanel : Form
    {
        BlitzerCore.Models.UI.Panel Panel = null;
        int ResortId { get; set; }
        public frmPanel(BlitzerCore.Models.UI.Panel aPanel, int aResortId)
        {
            InitializeComponent();
            Panel = aPanel;
            ResortId = aResortId;

            InitControls();
        }

        void InitControls()
        {
            if (Panel.Tiles == null) Panel.Tiles = new List<Tile>();
            for (int i = 0; i < 4 - Panel.Tiles.Count(); i++)
                Panel.Tiles.Add(new Tile() { ResortID = ResortId });

            InitTile(ucTilePic1, 0);
            InitTile(ucTilePic2, 1);
            InitTile(ucTilePic3, 2);
            InitTile(ucTilePic4, 3);
        }

        void InitTile(ucTilePic aTile, int aIndex)
        {
            Tile lTile = Panel.Tiles[aIndex];
            if (lTile.ResortID == null)
                lTile.ResortID = ResortId;

            aTile.Tile = Panel.Tiles[aIndex];
        }

        private string GetTitleImage (int aIndex)
        {
            if (Panel.Tiles == null) return "";
            if (Panel.Tiles.Count <= aIndex) return "";
            if (Panel.Tiles[aIndex].Media == null) return "";
            if (Panel.Tiles[aIndex].Media.MediaLocation != null)
                return Panel.Tiles[aIndex].Media.MediaLocation;
            return "";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void frmTiles_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void ucTilePic1_Load(object sender, EventArgs e)
        {

        }
    }
}
