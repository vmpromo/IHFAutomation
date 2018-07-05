using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.DataAccess.Client;
using IHF.EnterpriseLibrary.Data;
using IHF.BusinessLayer.Util;
using IHF.BusinessLayer.BusinessClasses.FeatureToggle;

namespace IHF.BusinessLayer.DataAccessObjects
{
    public class MainMenuDto
    {
        public decimal Id { get; set; }
        public decimal Parent_Id { get; set; }
        public string PageChildInd { get; set; }
        public string Caption { get; set; }
        public string Url { get; set; }
    }

    [Serializable]
    public class MainMenu
    {
        private Dictionary<decimal, List<MainMenuDto>> _allMenusByParent;

        public MainMenu(string userLogon, string application)
        {
            if (HttpContext.Current.Session["PagesByParent"] != null)
                _allMenusByParent = (Dictionary<decimal, List<MainMenuDto>>)HttpContext.Current.Session["PagesByParent"];
            else
            {
                _allMenusByParent = GetMenuItems(userLogon, application);
                HttpContext.Current.Session["PagesByParent"] = _allMenusByParent;
            }
        }

        private Dictionary<decimal, List<MainMenuDto>> GetMenuItems(string userLogon, string application)
        {
            MainMenuDAO mainMenuDao = new MainMenuDAO();

            var configSection = FeatureToggleConfigSection.GetConfig();

            var pagesByParent = mainMenuDao
                .GetMenus(userLogon, application)
                .Where(menu => configSection.IsPageEnabledForUser(userLogon, menu.Url))
                .GroupBy(m => m.Parent_Id)
                .ToDictionary(grp => grp.Key, grp => grp.ToList());

            return pagesByParent;
        }

        public List<MainMenuDto> GetPagesForParent(decimal parentId)
        {
            if (_allMenusByParent.ContainsKey(parentId))
                return _allMenusByParent[parentId];
            else
                return new List<MainMenuDto>();
        }
    }
}