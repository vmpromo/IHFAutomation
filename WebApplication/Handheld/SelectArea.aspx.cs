using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Web.Resources;
using IHF.BusinessLayer.Util;
using IHF.BusinessLayer.BusinessClasses.ManualSort;
using IHF.BusinessLayer.DataAccessObjects.ManualSort;
using System.Diagnostics;


namespace IHF.ApplicationLayer.Web.Handheld
{
    public partial class SelectArea : System.Web.UI.Page
    {


        const int PAGE_SIZE = 4;
        const int PREV_NO  = 20;
        const int NXT_NO =21;






        private enum MessageType
        {
            ErrorConfirm = 1,
            SussessConfirm = 2,
            NormalMessage = 3

        }


        ManualSortDAO _manualSort = new ManualSortDAO();

        string _areaId = string.Empty;
        string _loadNo = string.Empty;


        protected void Page_Init(object sender, EventArgs e)
        {


            this.Master.Reset();
            
            
         
           


                if (!Request.QueryString.AllKeys.Contains("aid"))
                {

                    int pageNo = 1; 

                    if (Request.QueryString.AllKeys.Contains("p"))
                    {
                         if (Request.QueryString["p"].ToString()!=string.Empty)
                            pageNo = int.Parse(Request.QueryString["p"].ToString());
                    }

           
                    this.Master.ChildScript = LoadAeas(pageNo);
           
                    ShowMessage(GetMessage("MSMNU1"), MessageType.NormalMessage);
                    
                    this.Master.RegisterStandardScript = true;
                }
                else if (Request.QueryString.AllKeys.Contains("aid") && !Request.QueryString.AllKeys.Contains("ld"))
                {

                    if (Request.QueryString["aid"] != string.Empty)
                    {
                        StringBuilder strScript = new StringBuilder();

                        int lPageNo = 1;

                        if (Request.QueryString.AllKeys.Contains("lp"))
                        {
                            if (Request.QueryString["lp"].ToString() != string.Empty)
                                lPageNo = int.Parse(Request.QueryString["lp"].ToString());
                        }


                        string aid = Request.QueryString["aid"].ToString();
                  
                        strScript = AreasLoad(aid,lPageNo);
                  

                        if (strScript.Length > 0)
                        {
                            this.Master.ChildScript = strScript;
                            ShowMessage(GetMessage("MSMNU2"), MessageType.NormalMessage);

                        }
                        else
                        {
                            this.Master.ChildScript = LoadAeas(1);
                            ShowMessage(GetMessage("MSMNU3"), MessageType.ErrorConfirm);
                            this.Master.RegisterStandardScript = true;

                        }
                    }


                    this.Master.RegisterStandardScript = true;

                }
                else if (Request.QueryString.AllKeys.Contains("aid") && Request.QueryString.AllKeys.Contains("ld"))
                {

                    if (Request.QueryString["aid"] != string.Empty && Request.QueryString["ld"] != string.Empty)
                    {

                        _areaId = Request.QueryString["aid"].ToString();

                        _loadNo = Request.QueryString["ld"].ToString();

                        this.hdnAreaId.Value = _areaId;
                        this.hdnLoadNo.Value = _loadNo;

                        ShowMessage(GetMessage("MSIS1"), MessageType.NormalMessage);
                        this.Master.RegisterStandardScript = true;
                    }

                }
                else
                {
                    this.Master.RegisterStandardScript = true;

                }

                
               
               

        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page.IsPostBack)
            {



                if (Request.QueryString.AllKeys.Contains("aid") && Request.QueryString.AllKeys.Contains("ld"))
                {

                    _areaId = Request.QueryString["aid"].ToString();

                    _loadNo = Request.QueryString["ld"].ToString();

                    this.hdnAreaId.Value = _areaId;
                    this.hdnLoadNo.Value = _loadNo;

                    this.ScanItemSort(_areaId, _loadNo);

                   
                }



               
            }

            ClearValue(true);
           
        }


        private void ScanItemSort(string areaId, string loadId)
        {

           

            ManualSortScan sortScan = new ManualSortScan();

           
            sortScan = GetParamValues();


            try
            {
                ManualSortScan itemScan = _manualSort.GetItemScan(sortScan);
                
                if (itemScan != null)
                {
                    this.SetItemScanValues(itemScan);

                    this.Master.Reset();


                    if (true == itemScan.PushToChuteOverFlow)
                    {
                        this.ShowMessage(itemScan.ActionMessage, MessageType.NormalMessage);
                    }
                    else if (true == itemScan.ActionResult && itemScan.ActionScan == Enumerations.ManualSortScanMode.IS.ToString())
                    {
                        this.ShowMessage(string.Format(GetMessage("MSIS2"), itemScan.DisplayChute), MessageType.NormalMessage);
                    }
                    else if (false == itemScan.ActionResult && itemScan.ActionScan == Enumerations.ManualSortScanMode.IS.ToString())
                    {
                        this.ShowMessage(itemScan.ActionMessage, MessageType.ErrorConfirm);

                    }
                    else if (true == itemScan.ActionResult && itemScan.ActionScan == Enumerations.ManualSortScanMode.PC.ToString())
                    {
                        ShowMessage(GetMessage("MSPC1"), MessageType.NormalMessage);

                        if (true == itemScan.LoadSorted)
                        {
                            ShowMessage(GetMessage("MSLS1"), MessageType.SussessConfirm);
                        }

                    }
                    else if (false == itemScan.ActionResult && itemScan.ActionScan == Enumerations.ManualSortScanMode.PC.ToString())
                    {
                        this.ShowMessage(itemScan.ActionMessage, MessageType.ErrorConfirm);

                    }


                }

            }
            catch (Exception e)
            {

                this.ShowMessage(GetMessage("MSIS3"), MessageType.ErrorConfirm);

            }

                this.Master.RegisterStandardScript = true;
           
        }


        private ManualSortScan GetParamValues()
        {

            ManualSortScan sortScan = new ManualSortScan();

            if (this.hdnScanMode.Value != string.Empty)
            {
                sortScan.ActionScan = this.hdnScanMode.Value;

            }

            if (this.hdnAreaId.Value != string.Empty)
            {
                sortScan.AreaID = this.hdnAreaId.Value;

            }

            if (this.hdnLoadNo.Value != string.Empty)
            {
                sortScan.LoadNo = this.hdnLoadNo.Value;

            }

            if (this.Master.BarcodeValue != string.Empty)
            {

                sortScan.ScanBarcode = this.Master.BarcodeValue;
            }


            if (this.hdnOrderNo.Value != string.Empty)
            {

                sortScan.OrderNo = this.hdnOrderNo.Value;
            }

            if (this.hdnItemNo.Value != string.Empty)
            {

                sortScan.ItemNo = this.hdnItemNo.Value;
            }

            if (this.hdnChuteId.Value != string.Empty)
            {

                sortScan.ChuteID = this.hdnChuteId.Value;
            }

            return sortScan;

        }

        private void SetItemScanValues(ManualSortScan scanItem)
        {

            if (null != scanItem)
            {
                if (scanItem.ActionResult == true)
                {
                    this.hdnScanMode.Value = (this.hdnScanMode.Value == Enumerations.ManualSortScanMode.IS.ToString()) ?
                                                                               Enumerations.ManualSortScanMode.PC.ToString() :
                                                                               Enumerations.ManualSortScanMode.IS.ToString();
                }
                else
                    this.hdnScanMode.Value = scanItem.ActionScan;


                this.hdnOrderNo.Value = scanItem.OrderNo;
                this.hdnItemNo.Value = scanItem.ItemNo;
                this.hdnAreaId.Value = scanItem.AreaID;
                this.hdnLoadNo.Value = scanItem.LoadNo;
                this.hdnChuteId.Value = scanItem.ChuteID;

            }
        }

        public List<SortArea> GetArea()
        {

            List<SortArea> lstArea = _manualSort.GetAreas();

            return lstArea;
        }


        public StringBuilder LoadAeas(int currentPage)
        {
            int counter = 1;
            int pageCount = 1;

            List<SortArea> lstTotArea = GetArea();

            pageCount = Convert.ToInt32(Math.Ceiling((decimal)( (decimal)lstTotArea.Count / (decimal) PAGE_SIZE)));

            List<SortArea> lstArea = lstTotArea.Skip((currentPage - 1) * PAGE_SIZE).Take(PAGE_SIZE).ToList<SortArea>();

            

            StringBuilder js = new StringBuilder();

            StringBuilder navjs = new StringBuilder();

            

            foreach (SortArea sa in lstArea)
            {

                js.AppendLine(@"  if (barcode.value=='" + counter + "'){");

                js.AppendLine(@"  window.navigate('SelectArea.aspx?aid=" + sa.AreaID.ToString() + "&p=" + currentPage + "');");

                js.AppendLine(@"  return false; ");

                js.AppendLine(@"}");

                HtmlTableCell menuItem = (HtmlTableCell)this.Master.FindControl("menuItem" + counter);

                menuItem.InnerText = counter + " - " + sa.Description;

                menuItem.Visible = true;

                if (int.Parse(this.Master.MaxManuItems) >= counter)
                    counter += 1;
                else
                    break;
            }

            navjs = AreaNavEnableDisable(currentPage, pageCount);

             if (navjs.Length> 0 )
                    js.Append  (navjs.ToString());

            return js;


        }

        public List<SortLoad> GetLoad(string areaId)
        {

            List<SortLoad> lstLoad = _manualSort.GetAreaLoad(areaId);

            return lstLoad;


        }

        public StringBuilder AreasLoad(string areaId, int currentPage )
        {
            int counter = 1;
            int pageCount = 1;

            List<SortLoad> lstAreaLoad = GetLoad(areaId);


            pageCount = Convert.ToInt32(Math.Ceiling((decimal)((decimal)lstAreaLoad.Count / (decimal)PAGE_SIZE)));

            List<SortLoad> lstLoads = lstAreaLoad.Skip((currentPage - 1) * PAGE_SIZE).Take(PAGE_SIZE).ToList<SortLoad>();

            StringBuilder js = new StringBuilder();

            StringBuilder navjs = new StringBuilder();



            foreach (SortLoad sl in lstLoads)
            {
                js.AppendLine(@"  if (barcode.value=='" + counter + "'){");

                js.AppendLine(@"  window.navigate('SelectArea.aspx?aid=" + areaId + "&ld=" + sl.PickLoadNo + "&lp="+currentPage+"');");

                js.AppendLine(@"  return false; ");

                js.AppendLine(@"}");

                HtmlTableCell menuItem = (HtmlTableCell)this.Master.FindControl("menuItem" + counter);

                menuItem.InnerText = counter + " - " + sl.PickLoadNo;

                menuItem.Visible = true;


                if (int.Parse(this.Master.MaxManuItems) > counter)
                    counter += 1;
                else
                    break;
            }


            navjs = LoadNavEnableDisable(areaId, currentPage, pageCount);

            if (navjs.Length > 0)
                js.Append(navjs.ToString());
            

            return js;


        }

        public StringBuilder AreaNavEnableDisable(int currentPage, int pageCount)
        {

            StringBuilder js = new StringBuilder();


            if (currentPage > 1)
            {
                js.AppendLine(@"  if (barcode.value == '" + PREV_NO+ "'){");

                js.AppendLine(@"  window.navigate('SelectArea.aspx?p=" + (currentPage - 1) + "');");

                js.AppendLine(@"  return false; ");

                js.AppendLine(@"}");


                HtmlTableCell prevMenuItem = (HtmlTableCell)this.Master.FindControl("menuPrevItem");

                prevMenuItem.InnerText = PREV_NO+" - Prev";

                prevMenuItem.Visible = true;
          
            }


             if (currentPage < pageCount)
             {


                 js.AppendLine(@"  if (barcode.value == '" + NXT_NO + "'){");

                 js.AppendLine(@"  window.navigate('SelectArea.aspx?p=" + (currentPage + 1) + "');");

                 js.AppendLine(@"  return false; ");

                 js.AppendLine(@"}");


                 HtmlTableCell prevMenuItem = (HtmlTableCell)this.Master.FindControl("menuNextItem");

                 prevMenuItem.InnerText = NXT_NO +" - Next" ;

                 prevMenuItem.Visible = true;

             
             }



            return js;
        }


        public StringBuilder LoadNavEnableDisable(string areaId , int currentAPage, int pageCount)
        {

            StringBuilder js = new StringBuilder();


            if (currentAPage > 1)
            {
                js.AppendLine(@"  if (barcode.value == '" + PREV_NO + "'){");

                js.AppendLine(@"  window.navigate('SelectArea.aspx?aid=" + areaId + "&lp=" + (currentAPage - 1) + "');");

                js.AppendLine(@"  return false; ");

                js.AppendLine(@"}");


                HtmlTableCell prevMenuItem = (HtmlTableCell)this.Master.FindControl("menuPrevItem");

                prevMenuItem.InnerText = PREV_NO + " - Prev";

                prevMenuItem.Visible = true;

            }


            if (currentAPage < pageCount)
            {


                js.AppendLine(@"  if (barcode.value == '" + NXT_NO + "'){");

                js.AppendLine(@"  window.navigate('SelectArea.aspx?aid=" + areaId + "&lp=" + (currentAPage + 1) + "');");

                js.AppendLine(@"  return false; ");

                js.AppendLine(@"}");


                HtmlTableCell prevMenuItem = (HtmlTableCell)this.Master.FindControl("menuNextItem");

                prevMenuItem.InnerText = NXT_NO + " - Next";

                prevMenuItem.Visible = true;


            }



            return js;
        }


        private void ShowMessage(string msg, MessageType type)
        {

            switch (type)
            {
                case MessageType.SussessConfirm:
                    this.Master.SuccessMessage = msg;
                    this.Master.DisplayMessage = true;
                    break;
                case MessageType.ErrorConfirm:
                    this.Master.ErrorMessage = msg;
                    this.Master.DisplayMessage = true;
                    break;
                case MessageType.NormalMessage:
                    this.Master.MessageBoard = msg;
                    break;

            }

        }

        private string GetMessage(string msgKey)
        {

            return GetGlobalResourceObject("AppMessages", msgKey).ToString();
        
        }


        private void ClearValue(bool clear)
        {

            if (true == clear)
                this.Master.BarcodeValue = string.Empty;
         
        }

    }
}