using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.DataAccess.Client;
using IHF.EnterpriseLibrary.Data;
using IHF.BusinessLayer.Util;

namespace IHF.BusinessLayer.DataAccessObjects
{
    [Serializable]
    public class MainMenu
    {
        
        #region "private variables"

        private decimal _web_page_id;
        private decimal _web_page_parent_id;
        private string _page_child_ind;
        private string _caption;
        private string _url;
        
        #endregion





        public decimal Id 
        { 
            get { return _web_page_id; } 
            set { _web_page_id = value; } 
        }


        public decimal Parent_Id 
        { 
            get { return _web_page_parent_id; } 
            set { _web_page_parent_id = value; } 
        }
        
        
        
        public string PageChildInd 
        { 
            get { return _page_child_ind; } 
            set { _page_child_ind = value; } 
        }
        
        public string Caption 
        { get { return _caption; } 
            set { _caption = value; } 
        }
        
        public string Url 
        { 
            get { return _url; } 
            set { _url = value; } 
        }
        
        
        public List<MainMenu> SubMenu = new List<MainMenu>();


        public MainMenu(decimal mainMenuId, string userLogon, string application)
        {
            _web_page_id = mainMenuId;

            DataTable allMenus = null;

            if (HttpContext.Current.Session["Menu"] != null)
                allMenus = (DataTable)HttpContext.Current.Session["Menu"];
            else
                allMenus = GetMenuItems(userLogon, application);

            SetMenuItemsForId(_web_page_id, allMenus);
        }

        private MainMenu()
        {
        }

        private DataTable GetMenuItems(string userLogon, string application)
        {
            MainMenuDAO mainMenuDao = new MainMenuDAO();
            
            DataTable allMenus = mainMenuDao.GetMenus(userLogon, application);
            HttpContext.Current.Session["Menu"] = allMenus;

            return allMenus;
        }

        private void SetMenuItemsForId(decimal parentMenuId, DataTable sourceTable)
        {
            //**************comment out a fix for filtering destop menus********************
            //DataRow[] menuItems = null; ;
            //MainMenu menuItem;
            //if (parentMenuId != (int)HandheldInput.Root)
            //{
            //    //outer if to identify if any dektop menu
            //    DataRow[] rows = sourceTable.Select("web_page_id = " + parentMenuId);
            //    DataRow mainRow = rows.Length > 0 ? rows[0] : null;

            //    if ((mainRow != null) && (int.Parse(mainRow[1].ToString()) == (int)HandheldInput.Root))
            //    {
            //        menuItems = sourceTable.Select("web_page_parent_id = " + parentMenuId);
            //    }
            //}
            //else
            //{
            //    menuItems = sourceTable.Select("web_page_parent_id = " + parentMenuId);
            //}
            //for (int i = 0; (menuItems != null && i < menuItems.Length); i++)
            //{
            //    menuItem = new MainMenu();
            //    menuItem._web_page_id = int.Parse(menuItems[i][0].ToString());
            //    menuItem._web_page_parent_id = int.Parse(menuItems[i][1].ToString());
            //    menuItem._page_child_ind = menuItems[i][2].ToString();
            //    menuItem._caption = menuItems[i][3].ToString();
            //    menuItem._url = menuItems[i][4].ToString();

            //    this.SubMenu.Add(menuItem);
            //}
            //**************comment out a fix for filtering destop menus********************

            //DataRow[] menuItems = null; ;
            MainMenu menuItem;

            DataRow[] menuItems = sourceTable.Select("web_page_parent_id = " + parentMenuId);

            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItem = new MainMenu();
                menuItem._web_page_id = int.Parse(menuItems[i][0].ToString());
                menuItem._web_page_parent_id = int.Parse(menuItems[i][1].ToString());
                menuItem._page_child_ind = menuItems[i][2].ToString();
                menuItem._caption = menuItems[i][3].ToString();
                menuItem._url = menuItems[i][4].ToString();

                this.SubMenu.Add(menuItem);
            }
        }

    }
}