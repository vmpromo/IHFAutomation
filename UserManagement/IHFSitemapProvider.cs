// Name: IHFSitemapProvider.cs
// Type: Site Map Data source provider
// Description: This class is used to get site map data from database.
//
//$Revision:   1.0  $
//
// Version   Date        Author    Reason
//  1.0      18/03/07    ITMK      Released version
//       

using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Collections.Specialized;
using System.Web.SessionState;
using System.Diagnostics;

namespace IHF.Security.UserManagement
{
    class IHFSitemapProvider : StaticSiteMapProvider
    {
        private SiteMapNode rootNode = null;
        private string applicationName = "";
        string UserName = string.Empty;

        public IHFSitemapProvider()
        {
        }

        public string ApplicationName
        {
            get { return this.applicationName; }
            set { this.applicationName = value; }
        }

        public override void Initialize(string name, NameValueCollection attributes)
        {
            base.Initialize(name, attributes);

            this.applicationName = attributes[Definitions.CONFIG_APPLICATION_NAME];

            // if user name lost then get the name from session.
            if (System.Web.HttpContext.Current.User.Identity.Name == null)
            {
                UserName = (String)System.Web.HttpContext.Current.Session["User"];
            }
            else
            {
                UserName = System.Web.HttpContext.Current.User.Identity.Name;
            }

            // LoadSiteMap();

        }

        private DataSet RemoveParentsWithNoChildren(DataSet dst)
        {
            DataSet olddataset = new DataSet();
            DataSet dataset = new DataSet();

            dataset.Tables.Add(new SitemapDAO().CreateNewSiteMapTable(UserName + "1"));
            olddataset = dst;

            foreach (DataRow row in dst.Tables[0].Rows)
            {
                if ((bool)row[2] == true)
                {
                    bool found = false;
                    foreach (DataRow row2 in olddataset.Tables[0].Rows)
                    {
                        if (row[0].ToString() == row2[1].ToString())
                            found = true;
                    }
                    if (found)
                    {
                        DataRow newRow = dataset.Tables[0].NewRow();
                        for (int i = 0; i < 5; i++)
                            newRow[i] = row[i];

                        dataset.Tables[0].Rows.Add(newRow);
                    }
                }
                else
                {
                    DataRow newRow = dataset.Tables[0].NewRow();
                    for (int i = 0; i < 5; i++)
                        newRow[i] = row[i];

                    dataset.Tables[0].Rows.Add(newRow);
                }
            }


            return dataset;
        }

        private void LoadSiteMap()
        {
            //  UserName = string.Empty;

            if (System.Web.HttpContext.Current.User.Identity.Name == null)
            {
                UserName = (String)System.Web.HttpContext.Current.Session["User"];
            }
            else
            {
                UserName = System.Web.HttpContext.Current.User.Identity.Name;
                System.Web.HttpContext.Current.Session["User"] = UserName;
            }
            DataSet dst = null;

            dst = new SitemapDAO().GetSiteMap(this.applicationName, UserName);


            int rowCount = 0;
            do
            {
                rowCount = dst.Tables[0].Rows.Count;
                dst = RemoveParentsWithNoChildren(dst);
            } while (rowCount != dst.Tables[0].Rows.Count);

            if (rowCount != 0)
            {
                this.rootNode = this.CreateNode(dst.Tables[0].Rows[0]);
                this.Clear();
                this.AddNode(this.rootNode);

                // Since the SiteMap class is static, make sure that it is
                // not modified while the site map is built.
                lock (this)
                {

                    ProcessNode(dst, dst.Tables[0].Rows[0], this.rootNode);
                }
                System.Web.HttpContext.Current.Session["SiteMapRoot"] = this.rootNode;

            }
        }

        private void ProcessNode(DataSet dst, DataRow row, SiteMapNode parentNode)
        {
            int parentID = Int32.Parse(row[0].ToString());
            DataView dv = new DataView(dst.Tables[0], string.Format("ParentID = {0}", parentID), string.Empty, DataViewRowState.CurrentRows);
            foreach (DataRowView dvrow in dv)
            {
                SiteMapNode node = CreateNode(dvrow.Row);
                this.AddNode(node, parentNode);
                ProcessNode(dst, dvrow.Row, node);
            }

        }

        private SiteMapNode CreateNode(DataRow row)
        {
            SiteMapNode node = new SiteMapNode(this, row[0].ToString(), row[4].ToString(), row[3].ToString());

            return node;
        }

        public override SiteMapNode BuildSiteMap()
        {
            // if the site map is already built then return the previously built root node
            bool blnSiteMapAlreadyBuilt = System.Web.HttpContext.Current.Session["SiteMapRoot"] != null;
            if (blnSiteMapAlreadyBuilt)
                return (SiteMapNode)System.Web.HttpContext.Current.Session["SiteMapRoot"];

            // build the sitemap
            this.Clear();
            LoadSiteMap();
            return (SiteMapNode)System.Web.HttpContext.Current.Session["SiteMapRoot"];
        }



        public override SiteMapNode RootNode
        {

            get
            {
                if ((System.Web.HttpContext.Current.Session["SiteMapRoot"] == null) ||
                    (!((SiteMapNode)System.Web.HttpContext.Current.Session["SiteMapRoot"]).HasChildNodes))
                {

                    this.Clear();
                    LoadSiteMap();

                }

                return (SiteMapNode)System.Web.HttpContext.Current.Session["SiteMapRoot"];

            }

        }



        protected override SiteMapNode GetRootNodeCore()
        {
            if (System.Web.HttpContext.Current.Session["SiteMapRoot"] == null)
            {
                this.Clear();
                LoadSiteMap();
            }
            return (SiteMapNode)System.Web.HttpContext.Current.Session["SiteMapRoot"];
        }

        public override SiteMapNode FindSiteMapNode(string rawUrl)
        {
            return base.FindSiteMapNode(rawUrl);
        }
    }
}
