using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.DataAccess.Client;
using IHF.EnterpriseLibrary.Data;

namespace webPageForHandhelds
{
    public class Menu
    {
        #region "private constants"

        private const string SITEMAP = "OMS_USER.F_GET_USER_SITE_MAP";

        #endregion

        #region "private variables"

        private DataManager dataManager = 
            new DataManager(IHF.BusinessLayer.Util.DBInstanceEnum.Ora);

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
        
        
        public List<Menu> SubMenu = new List<Menu>();


        public Menu(decimal mainMenuId)
        {
            _web_page_id = mainMenuId;

            GetMenuItems(_web_page_id);
        }

        public Menu() { }

        private void GetMenuItems(decimal webPageId)
        {
            DataSet dataset = dataManager.ExecuteDataset(SITEMAP, new object[] { "itmk", "IHF" });

            DataRow[] menuItems = dataset.Tables[0].Select("web_page_parent_id = " + webPageId);

            Menu menuItem;
            
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItem = new Menu(decimal.Parse(menuItems[i][0].ToString()));
                menuItem.Id = int.Parse(menuItems[i][0].ToString());
                menuItem._web_page_parent_id = int.Parse(menuItems[i][1].ToString());
                menuItem._page_child_ind = menuItems[i][2].ToString();
                menuItem._caption = menuItems[i][3].ToString();
                menuItem._url = menuItems[i][4].ToString();

                this.SubMenu.Add(menuItem);
            }
        }

    }

    //public class MenuItem : Menu
    //{
    //    public MenuItem ()
    //    {}
    //}

}