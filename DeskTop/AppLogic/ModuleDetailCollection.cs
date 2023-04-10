using System;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using DevExpress.XtraNavBar;
using DevExpress.XtraEditors;
using System.ComponentModel;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.Utils.Menu;
using System.Drawing;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraCharts;
using Desktop.BaseControls;
using BlitzerCore.Models;

namespace Desktop.AppLogic
{
    public class ModuleDetailCollection : CollectionBase
    {
        ModuleBase owner;
        public ModuleDetailCollection(ModuleBase owner)
        {
            this.owner = owner;
        }
        public ModDetailBase this[int index]
        {
            get
            {
                if (List.Count > index)
                    return List[index] as ModDetailBase;
                return null;
            }
        }
        ModDetailBase IsDetailExist(ModDetailBase value)
        {
            foreach (ModDetailBase detail in List)
                if (detail.ID == value.ID)
                    return detail;
            return null;
        }
        public bool IsDetailExist(object id)
        {
            foreach (ModDetailBase detail in List)
                if (detail.ID == id)
                {
                    owner.ActiveDetailControl = detail;
                    return true;
                }
            return false;
        }
        public bool IsDirtyDetailExist()
        {
            foreach (ModDetailBase detail in List)
                if (detail.Dirty) return true;
            return false;
        }
        public void Add(ModDetailBase value)
        {
            //ModDetailBase existDetail = IsDetailExist(value);
            //if (existDetail == null)
            //{
            //    List.Add(value);
            //    owner.ActiveDetailControl = value;
            //}
            //else
            //{
            //    value.Dispose();
            //    owner.ActiveDetailControl = existDetail;
            //}
            //owner.RibbonMenuController.CalcCloseAllDetails();
        }
        public void Remove(ModDetailBase value)
        {
            int index = List.IndexOf(value);

            if (List.Contains(value))
                List.Remove(value);

            if (index != -1 && List.Count > 0)
            {
                if (index >= List.Count)
                    index = List.Count - 1;
                owner.ActiveDetailControl = (ModDetailBase)List[index];
            }
            else
                owner.ActiveDetailControl = null;

            if (value != null)
                value.Dispose();
            owner.RibbonMenuController.CalcCloseAllDetails();
        }
        public void RemoveAll(bool closeEditing)
        {
            for (int i = List.Count - 1; i >= 0; i--)
            {
                ModDetailBase temp = this[i];
                if (temp.Dirty && !closeEditing) continue;
                List.RemoveAt(i);
                temp.Dispose();
            }
            owner.ActiveDetailControl = null;
            owner.RibbonMenuController.CalcCloseAllDetails();
        }
        internal void HideDetails()
        {
            foreach (ModDetailBase detail in List)
                detail.Hide();
        }
    }
}
