using System;
using System.Reflection;
using System.Collections;
using DevExpress.XtraNavBar;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Ribbon;
using Desktop.BaseControls;
using BlitzerCore.Utilities;

namespace Desktop.AppLogic
{
    public class ModuleInfo
    {
        public readonly string ModName;
        public readonly string Caption;
        readonly DevExpress.XtraNavBar.NavBarItem mNavBar;
        RibbonPage RibbonPage { get; set; }
        bool wasShown;
        public ModuleBase Module { get; set; }
        public Type ModuleType { get; set; }

        public ModuleInfo(string aModName, string aNavCaption, Type aModType, NavBarItem aNavItem, RibbonPage aRibbonPage)
        {
            this.ModName = aModName;
            this.mNavBar = aNavItem;
            RibbonPage = aRibbonPage;
            Caption = aNavCaption;
            mNavBar.Caption = Caption;
            ModuleType = aModType;
        }
        public string Name { get { return this.ModName; } }
         public bool WasShown { get { return wasShown; } set { wasShown = value; } }
        public void ModuleDispose()
        {
            if (Module != null /*&& this.module.AllowDispose*/)
            {
                Module.Dispose();
                Module = null;
            }
        }
        public ModuleBase TModule
        {
            get
            {
                if (Module == null)
                {
                    ConstructorInfo constructorInfoObj = ModuleType.GetConstructor(Type.EmptyTypes);
                    if (constructorInfoObj == null)
                        throw new ApplicationException(string.Format(ConstStrings.Get("PublicConstructorError"), ModuleType.FullName));
                    Module = constructorInfoObj.Invoke(null) as ModuleBase;
                }
                return Module;
            }
        }
    }

    public class ModuleInfoCollection : CollectionBase
    {
        public ModuleInfo this[int index]
        {
            get
            {
                if (List.Count > index)
                    return List[index] as ModuleInfo;
                return null;
            }
        }
        public ModuleInfo this[string name]
        {
            get
            {
                foreach (ModuleInfo info in this)
                    if (info.Caption.Equals(name))
                        return info;
                return null;
            }
        }
        public void Add(ModuleInfo value)
        {
            if (!List.Contains(value))
                List.Add(value);
        }
        public void Remove(ModuleInfo value)
        {
            if (List.Contains(value))
                List.Remove(value);
        }
    }


    public class ModulesInfo
    {
        public event EventHandler CurrentModuleChanged;
        static ModulesInfo instance;
        readonly ModuleInfoCollection collection;
        ModuleInfo currentModule;
        public static void ShowModule(DevExpress.XtraBars.Ribbon.RibbonForm parent, string name, PanelControl group)
        {
            Logger.LogError("ModuleInfo::ShowModule called incorrectly.  Should call ViewManager.ShowModule");
        }

        public ModuleInfoCollection Collection { get { return collection; } }
        public ModuleInfo CurrentModuleBase { get { return currentModule; } set { currentModule = value; } }

        public static void Add(string aModName, string aCaption, Type aModType, DevExpress.XtraNavBar.NavBarItem aNavItem, RibbonPage aRibbonPage)
        {
            ModuleInfo item = new ModuleInfo(aModName, aCaption, aModType, aNavItem, aRibbonPage);
            Instance.Collection.Add(item);
        }
        public static int Count { get { return instance.Collection.Count; } }
        public static ModuleInfo GetItem(int index) { return instance.Collection[index]; }
        public static ModuleInfo GetItem(string name) { return instance.Collection[name]; }
        public static void RemoveItem(string name)
        {
            ModuleInfo info = GetItem(name);
            if (info != null)
            {
                Logger.LogTracing($"Removed {name} from ModulesInfo");
                instance.Collection.Remove(info);
            } else
                Logger.LogWarning($"Unable to Remove {name} from ModulesInfo");
        }
        public static ModulesInfo Instance { get { return instance; } }
        public static ModuleInfo CurrentModuleInfo { get { return instance.currentModule; } }
        public static ModuleBase CurrentModule
        {
            get
            {
                if (CurrentModuleInfo != null)
                    return CurrentModuleInfo.TModule;
                return null;
            }
        }
        static ModulesInfo()
        {
            instance = new ModulesInfo();
        }
        public ModulesInfo()
        {
            this.collection = new ModuleInfoCollection();
            this.currentModule = null;
        }
        protected static void RaiseModuleChanged()
        {
            if (Instance.CurrentModuleChanged != null)
                Instance.CurrentModuleChanged(Instance, EventArgs.Empty);
        }
        public static void FillNavBar(NavBarControl navBar)
        {
            FillNavBar(navBar, NavBarGroupStyle.LargeIconsText, NavBarImage.Small);
        }
        public static void FillNavBar(NavBarControl navBar, NavBarGroupStyle groupStyle, NavBarImage groupCaptionImage)
        {
            //if (navBar == null) return;
            //navBar.BeginUpdate();
            //for (int i = 0; i < Count; i++)
            //{
            //    if (GetItem(i).Group == ConstStrings.Get("AboutGroup")) continue;
            //    NavBarItem item = new NavBarItem();
            //    //navBar.Items.Add(item);
            //    //item.Caption = GetItem(i).Name;
            //    //item.SmallImage = GetItem(i).SmallImage;
            //    //item.LargeImage = GetItem(i).LargeImage;
            //    //item.Tag = GetItem(i);
            //    //GetNavBarGroup(navBar, GetItem(i).Group, groupStyle, groupCaptionImage).ItemLinks.Add(new NavBarItemLink(item));
            //}
            //navBar.EndUpdate();
        }
        static NavBarGroup GetNavBarGroup(NavBarControl navBar, string groupName, NavBarGroupStyle groupStyle, NavBarImage groupCaptionImage)
        {
            foreach (NavBarGroup group in navBar.Groups)
                if (group.Caption == groupName) return group;
            NavBarGroup newGroup = navBar.Groups.Add();
            newGroup.Caption = groupName;
            newGroup.Name = groupName;
            newGroup.GroupStyle = groupStyle;
            newGroup.GroupCaptionUseImage = groupCaptionImage;
            return newGroup;
        }
    }

}
