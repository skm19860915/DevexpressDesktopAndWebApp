using DevExpress.LookAndFeel;
using DevExpress.Utils.DragDrop;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebPageTourApp
{
    public partial class Form2 : Form
    {
        public DataTable _selectPageList { get; set; }
        public bool isTourStarted { get; set; }
        public Form2()
        {
            InitializeComponent();
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Link");
            table.Columns.Add("Name");
            table.Columns.Add("Description");
            table.Columns.Add("Tags");

            _selectPageList = table;
            HandleBehaviorDragDropEvents();
        }
        public DataTable GetPagesList()
        {

            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Link");
            table.Columns.Add("Name");
            table.Columns.Add("Description");
            table.Columns.Add("Tags");


            for (int i = 0; i < 100; i++)
            {
                table.Rows.Add(new object[] { i, "https://google.com", "Name " + i, "Description " + i, "tag1,tag2" });

            }
            return table;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            WebPagesGrid.DataSource = GetPagesList();

            WebPageView.Columns[0].Visible = false;
            SetStartTourButtonStatus();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            var selectedRowIndex = WebPageView.GetSelectedRows();
            var table = WebPagesGrid.DataSource as DataTable;
            var selectedRow = table.Rows[selectedRowIndex[0]];

            if (_selectPageList == null) _selectPageList = new DataTable();

            if (_selectPageList.Select("ID = " + selectedRow["ID"].ToString()).ToList().Any())
            {
                if (MessageBox.Show("This is already exists. Do you want to add this page one more time?", "Duplicate Page", MessageBoxButtons.YesNo) == DialogResult.No) return;
            }

            DataRow newRow = _selectPageList.NewRow();

            newRow["ID"] = selectedRow["ID"];
            newRow["Link"] = selectedRow["Link"];
            newRow["Name"] = selectedRow["Name"];
            newRow["Description"] = selectedRow["Description"];
            newRow["Tags"] = selectedRow["Tags"];
            
            _selectPageList.Rows.Add(newRow);

            _selectedPageGrid.DataSource = _selectPageList;
            _selectedPageGrid.RefreshDataSource();
            _selectedPageView.Columns[0].Visible = false;
        }

        private void SetStartTourButtonStatus()
        {
            if (isTourStarted)
            {
                this.btnTakeUserForDemo.Text = "Stop Tour";
                this.btnTakeUserForDemo.Appearance.BackColor = System.Drawing.Color.Lime;
                this.btnTakeUserForDemo.Appearance.BackColor2 = System.Drawing.Color.Lime;
            }
            else
            {
                this.btnTakeUserForDemo.Text = "Start Tour";
                this.btnTakeUserForDemo.Appearance.BackColor = System.Drawing.Color.White;
                this.btnTakeUserForDemo.Appearance.BackColor2 = System.Drawing.Color.White;
            }
        }
        


        private void textEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (textEdit1.Text.Length > 15)
            {
                MessageBox.Show("Please enter name with character count less 15", "Name Validation");
                return;
            }
            btnTakeUserForDemo.Text = "Click here " + textEdit1.Text + " to start your tour";
        }

        private void btnTakeUserForDemo_Click(object sender, EventArgs e)
        {
            if (_selectPageList == null || _selectPageList.Rows.Count <= 0) return;

            isTourStarted = !isTourStarted;
            //System.Diagnostics.Process.Start(_selectPageList.FirstOrDefault().Link);
            SetStartTourButtonStatus();

        }

       
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = _selectedPageView.FocusedRowHandle;

            if (index <= 0) return;

            _selectedPageView.DeleteRow(index);
            _selectedPageView.FocusedRowHandle = index - 1;

            _selectedPageView.Columns[0].Visible = false;
        }

        private void WebPageGrid_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var selectedRowIndex = WebPageView.GetSelectedRows();
            var table = WebPagesGrid.DataSource as DataTable;
            var selectedRow = table.Rows[selectedRowIndex[0]];


            webBrowser1.Navigate(selectedRow["Link"].ToString());
        }

        private void WebPagesGrid_Click(object sender, EventArgs e)
        {

        }
        
        void Delete_MenuButtonEvent(object sender, EventArgs e) {
            btnDelete_Click(null, null);
            deleteMenu.HidePopup();
        }
        void Add_MenuButtonEvent(object sender, EventArgs e)
        {
            gridView1_DoubleClick(null,null);
            addMenu.HidePopup();
        }
        void Edit_MenuButtonEvent(object sender, EventArgs e)
        {
            addMenu.HidePopup();

            //var newForm = new AddWebPageForm();

            //var selectedRowIndex = WebPageView.GetSelectedRows();
            //var table = WebPagesGrid.DataSource as DataTable;
            //var selectedRow = table.Rows[selectedRowIndex[0]];

            //newForm.SetPageData(selectedRow);
            
            //newForm.FormBorderStyle = FormBorderStyle.FixedDialog;

            //newForm.MaximizeBox = false;
            //newForm.MinimizeBox = false;
            
            //newForm.ShowDialog();
        }

        DXPopupMenu deleteMenu;
        DXPopupMenu addMenu;

        private void WebPageView_MouseUp(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                addMenu = new DXPopupMenu();
                addMenu.Items.Add(new DXMenuItem("Add for Demo", new EventHandler(Add_MenuButtonEvent), null, null, null, null));
                addMenu.Items.Add(new DXMenuItem("Edit", new EventHandler(Edit_MenuButtonEvent), null, null, null, null));

                // Display a DXPopupMenu as a regular menu
                UserLookAndFeel lf = UserLookAndFeel.Default;
                Control parentControl = this;
                Point pt = Cursor.Position;
             //   pt.X = pt.X - 20;
                pt.Y = pt.Y - 30;

                addMenu.MenuViewType = MenuViewType.Menu;
                addMenu.ShowPopup(parentControl, pt);
                //or
                ((IDXDropDownControl)addMenu).Show(new SkinMenuManager(lf), parentControl, pt);
                
            }
        }
        private void _selectedPageView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                deleteMenu = new DXPopupMenu();
                deleteMenu.Items.Add(new DXMenuItem("Delete", new EventHandler(Delete_MenuButtonEvent), null, null, null, null));

                // Display a DXPopupMenu as a regular menu
                UserLookAndFeel lf = UserLookAndFeel.Default;
                Control parentControl = this;
                Point pt = Cursor.Position;

                //pt.X = pt.X - 20;
                pt.Y = pt.Y - 30;
                //...
                deleteMenu.MenuViewType = MenuViewType.Menu;
                deleteMenu.ShowPopup(parentControl, pt);
                //or
                ((IDXDropDownControl)deleteMenu).Show(new SkinMenuManager(lf), parentControl, pt);

            }

        }


        public void HandleBehaviorDragDropEvents()
        {
            DragDropBehavior gridControlBehavior = behaviorManager1.GetBehavior<DragDropBehavior>(this._selectedPageView);
            gridControlBehavior.DragDrop += Behavior_DragDrop;
            gridControlBehavior.DragOver += Behavior_DragOver;
        }
        private void Behavior_DragOver(object sender, DragOverEventArgs e)
        {
            DragOverGridEventArgs args = DragOverGridEventArgs.GetDragOverGridEventArgs(e);
            e.InsertType = args.InsertType;
            e.InsertIndicatorLocation = args.InsertIndicatorLocation;
            e.Action = args.Action;
            Cursor.Current = args.Cursor;
            args.Handled = true;
        }
        private void Behavior_DragDrop(object sender, DevExpress.Utils.DragDrop.DragDropEventArgs e)
        {
            GridView targetGrid = e.Target as GridView;
            GridView sourceGrid = e.Source as GridView;
            if (e.Action == DragDropActions.None || targetGrid != sourceGrid)
                return;
            DataTable sourceTable = sourceGrid.GridControl.DataSource as DataTable;

            Point hitPoint = targetGrid.GridControl.PointToClient(Cursor.Position);
            GridHitInfo hitInfo = targetGrid.CalcHitInfo(hitPoint);

            int[] sourceHandles = e.GetData<int[]>();

            int targetRowHandle = hitInfo.RowHandle;
            int targetRowIndex = targetGrid.GetDataSourceRowIndex(targetRowHandle);

            List<DataRow> draggedRows = new List<DataRow>();
            foreach (int sourceHandle in sourceHandles)
            {
                int oldRowIndex = sourceGrid.GetDataSourceRowIndex(sourceHandle);
                DataRow oldRow = sourceTable.Rows[oldRowIndex];
                draggedRows.Add(oldRow);
            }

            int newRowIndex;

            switch (e.InsertType)
            {
                case InsertType.Before:
                    newRowIndex = targetRowIndex > sourceHandles[sourceHandles.Length - 1] ? targetRowIndex - 1 : targetRowIndex;
                    for (int i = draggedRows.Count - 1; i >= 0; i--)
                    {
                        DataRow oldRow = draggedRows[i];
                        DataRow newRow = sourceTable.NewRow();
                        newRow.ItemArray = oldRow.ItemArray;
                        sourceTable.Rows.Remove(oldRow);
                        sourceTable.Rows.InsertAt(newRow, newRowIndex);
                    }
                    break;
                case InsertType.After:
                    newRowIndex = targetRowIndex < sourceHandles[0] ? targetRowIndex + 1 : targetRowIndex;
                    for (int i = 0; i < draggedRows.Count; i++)
                    {
                        DataRow oldRow = draggedRows[i];
                        DataRow newRow = sourceTable.NewRow();
                        newRow.ItemArray = oldRow.ItemArray;
                        sourceTable.Rows.Remove(oldRow);
                        sourceTable.Rows.InsertAt(newRow, newRowIndex);
                    }
                    break;
                default:
                    newRowIndex = -1;
                    break;
            }
            int insertedIndex = targetGrid.GetRowHandle(newRowIndex);
            targetGrid.FocusedRowHandle = insertedIndex;
            targetGrid.SelectRow(targetGrid.FocusedRowHandle);
        }
    }
}
