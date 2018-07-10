using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using IHF.BusinessLayer.BusinessClasses;
using Oracle.DataAccess.Client;
using System.Data;

using IHF.BusinessLayer.Util;
using IHF.EnterpriseLibrary;
using IHF.EnterpriseLibrary.Data;
using IHF.EnterpriseLibrary.DataServices;

namespace IHF.BusinessLayer.DataAccessObjects
{
    public class RemoveForPackDAO
    {

        #region "private constants"

        private const string LOCATE_ITEM = "oms_remove_for_pack.p_locate_item";
        private const string MOVE_TO_TOTE = "oms_remove_for_pack.p_validate_and_move_to_tote";
        private const string NEXT_ORDER_AND_LOC = "oms_remove_for_pack.f_next_order_and_loc";
        private const string MOVE_INCOMPLETE_TO_TOTE = "oms_remove_for_pack.p_validate_and_move_incomplete";
        private const string ITEMS_NOT_LOCATED = "oms_remove_for_pack.f_items_not_located";
        private const string ITEMS_IN_CHUTE = "oms_remove_for_pack.f_items_left_in_chute";

        #endregion

        #region "private variables"

        private DataManager _dataManager = new DataManager(Util.DBInstanceEnum.Ora);
        //private Trolley trolley = new Trolley();

        #endregion

        #region "private methods"

        private List<Order> StrongOrderList(List<IDataService> listOfResult)
        {
            List<Order> listOfOrders = new List<Order>();

            foreach (Order order in listOfResult)
            {
                listOfOrders.Add((Order)order);
            }
            return listOfOrders;
        }

        #endregion

        #region "Methods available to the presentation layer (web)"

        public void locateitem (decimal itemId, decimal locationId, decimal overflowToteId, decimal trolleyId, string userlogin, string terminalId,
            ref decimal ordernumber, ref string fullyLocatedInd, ref decimal ordervolume, ref string toteService, ref string toteBarcodePrefix, ref string toteTypeName,  ref decimal nextLocId, ref string nextLocBarcode, 
            ref string nextLocLabel,  ref decimal numItems)
        {

            decimal _ordernumber = 0;
            string _fullyLocatedInd = string.Empty;
            string _locationbarcode = string.Empty;
            string _locationlabel = string.Empty;
            decimal _itemcount = 0;
            decimal _ordervolume = 0;
            string _toteservice = string.Empty;
            string _totebarcodeprefix = string.Empty;
            string _totetypename = string.Empty;
            decimal _currlocationid = 0;
            string _currlocbarcode = string.Empty;
            string _currloclabel = string.Empty;

            decimal ? locationIdObj; 
            decimal ? overflowToteIdObj;

            if (locationId == 0)
                locationIdObj = null;
            else
                locationIdObj = locationId;

            if (overflowToteId == 0)
                overflowToteIdObj = null;
            else
                overflowToteIdObj = overflowToteId;


            
            DataSet dsResult = _dataManager.SelectDataSetProcedure(LOCATE_ITEM,
                                                new Object[] {
                                                    itemId,
                                                    locationIdObj,
                                                    overflowToteIdObj,
                                                    trolleyId,
                                                    userlogin,
                                                    terminalId });


            _ordernumber = dsResult.Tables[0].Rows[0]["ordernumber"].Equals(System.DBNull.Value) ? 0 : Convert.ToDecimal(dsResult.Tables[0].Rows[0]["ordernumber"]);
            _fullyLocatedInd = dsResult.Tables[0].Rows[0]["fully_located_ind"].Equals(System.DBNull.Value) ? string.Empty : dsResult.Tables[0].Rows[0]["fully_located_ind"].ToString();
            _ordervolume = dsResult.Tables[0].Rows[0]["order_volume"].Equals(System.DBNull.Value) ? 0 : Convert.ToDecimal(dsResult.Tables[0].Rows[0]["order_volume"]);
            _toteservice = dsResult.Tables[0].Rows[0]["tote_service"].Equals(System.DBNull.Value) ? string.Empty : dsResult.Tables[0].Rows[0]["tote_service"].ToString();
            _totebarcodeprefix = dsResult.Tables[0].Rows[0]["tote_barcode_prefix"].Equals(System.DBNull.Value) ? string.Empty : dsResult.Tables[0].Rows[0]["tote_barcode_prefix"].ToString();
            _totetypename = dsResult.Tables[0].Rows[0]["tote_type_name"].Equals(System.DBNull.Value) ? string.Empty : dsResult.Tables[0].Rows[0]["tote_type_name"].ToString();
            _currlocationid = dsResult.Tables[0].Rows[0]["curr_loc_id"].Equals(System.DBNull.Value) ? 0 : Convert.ToDecimal(dsResult.Tables[0].Rows[0]["curr_loc_id"]);
            _currlocbarcode = dsResult.Tables[0].Rows[0]["curr_loc_barcode"].Equals(System.DBNull.Value) ? string.Empty : dsResult.Tables[0].Rows[0]["curr_loc_barcode"].ToString();
            _currloclabel = dsResult.Tables[0].Rows[0]["curr_trolley_loc_label"].Equals(System.DBNull.Value) ? string.Empty : dsResult.Tables[0].Rows[0]["curr_trolley_loc_label"].ToString();
            _itemcount = dsResult.Tables[0].Rows[0]["curr_loc_item_count"].Equals(System.DBNull.Value) ? 0 : Convert.ToDecimal(dsResult.Tables[0].Rows[0]["curr_loc_item_count"]);


            ordernumber = _ordernumber;
            fullyLocatedInd = _fullyLocatedInd;
            ordervolume = _ordervolume;
            toteService = _toteservice;
            toteBarcodePrefix = _totebarcodeprefix;
            toteTypeName = _totetypename;
            nextLocId = _currlocationid;
            nextLocBarcode = _currlocbarcode;
            nextLocLabel = _currloclabel;
            numItems = _itemcount;
        }


        public void movetotote(string locationbarcode, string totebarcode, string username, string terminalid, ref decimal nextlocid, ref string nextlocbarcode, ref string nextloclabel, ref string totelabel, ref decimal itemcount)
        {
            decimal _nextlocid = 0;
            string _nextlocbarcode = string.Empty;
            string _nextloclabel = string.Empty;
            decimal _itemcount = 0;
            string _totelabel = string.Empty;


            DataSet dsResult = _dataManager.SelectDataSetProcedure(MOVE_TO_TOTE,
                                    new Object[] { 
                                        locationbarcode,
                                        totebarcode,
                                        username,
                                        terminalid });

            _nextlocid = dsResult.Tables[0].Rows[0]["next_loc_id"].Equals(System.DBNull.Value) ? 0 : Convert.ToDecimal(dsResult.Tables[0].Rows[0]["next_loc_id"]);
            _nextlocbarcode = dsResult.Tables[0].Rows[0]["next_loc_barcode"].Equals(System.DBNull.Value) ? string.Empty : dsResult.Tables[0].Rows[0]["next_loc_barcode"].ToString();
            _nextloclabel = dsResult.Tables[0].Rows[0]["next_loc_label"].Equals(System.DBNull.Value) ?  string.Empty : dsResult.Tables[0].Rows[0]["next_loc_label"].ToString();
            _itemcount = dsResult.Tables[0].Rows[0]["next_loc_item_count"].Equals(System.DBNull.Value) ? 0 :   Convert.ToDecimal(dsResult.Tables[0].Rows[0]["next_loc_item_count"]);
            _totelabel =  dsResult.Tables[0].Rows[0]["tote_label"].Equals(System.DBNull.Value) ? string.Empty :  dsResult.Tables[0].Rows[0]["tote_label"].ToString();

            nextlocid = _nextlocid;
            nextlocbarcode = _nextlocbarcode;
            nextloclabel = _nextloclabel;
            itemcount = _itemcount;
            totelabel = _totelabel;

        }


        public void nextorderandloc(decimal chuteid, decimal ? currentordernumber, ref decimal nextordernumber, ref string toteservice, ref decimal nextlocid, ref string nextlocbarcode, ref string nextloclabel, ref decimal nextlocitemcount, ref string totebarcodeprefix, ref string totetypename, ref decimal failedtoteid, ref string failedtotelabel, ref string failedtotebarcode)
        {

            decimal _nextordernumber = 0;
            string  _toteservice  = string.Empty;
            decimal _nextlocid = 0;
            string  _nextlocbarcode = string.Empty;
            string  _nextloclabel = string.Empty;
            decimal _nextlocitemcount = 0;
            string  _totebarcodeprefix = string.Empty;
            string  _totetypename = string.Empty;
            decimal _failedtoteid = 0;
            string  _failedtotelabel = string.Empty;
            string  _failedtotebarcode = string.Empty;


            DataSet dsResult = _dataManager.ExecuteDataset(NEXT_ORDER_AND_LOC,
                                    new Object[] { 
                                        chuteid,
                                        currentordernumber });

            _nextordernumber = dsResult.Tables[0].Rows[0]["next_ordernumber"].Equals(System.DBNull.Value) ? 0 : Convert.ToDecimal(dsResult.Tables[0].Rows[0]["next_ordernumber"]);
            _toteservice = dsResult.Tables[0].Rows[0]["tote_service"].Equals(System.DBNull.Value) ? string.Empty : dsResult.Tables[0].Rows[0]["tote_service"].ToString();
            _nextlocid = dsResult.Tables[0].Rows[0]["next_loc_id"].Equals(System.DBNull.Value) ? 0 : Convert.ToDecimal(dsResult.Tables[0].Rows[0]["next_loc_id"]);
            _nextlocbarcode = dsResult.Tables[0].Rows[0]["next_loc_barcode"].Equals(System.DBNull.Value) ? string.Empty : dsResult.Tables[0].Rows[0]["next_loc_barcode"].ToString();
            _nextloclabel = dsResult.Tables[0].Rows[0]["next_loc_label"].Equals(System.DBNull.Value) ? string.Empty : dsResult.Tables[0].Rows[0]["next_loc_label"].ToString();
            _nextlocitemcount = dsResult.Tables[0].Rows[0]["next_loc_item_count"].Equals(System.DBNull.Value) ? 0 : Convert.ToDecimal(dsResult.Tables[0].Rows[0]["next_loc_item_count"]);
            _totebarcodeprefix = dsResult.Tables[0].Rows[0]["tote_barcode_prefix"].Equals(System.DBNull.Value) ? string.Empty : dsResult.Tables[0].Rows[0]["tote_barcode_prefix"].ToString();
            _totetypename = dsResult.Tables[0].Rows[0]["tote_type_name"].Equals(System.DBNull.Value) ? string.Empty : dsResult.Tables[0].Rows[0]["tote_type_name"].ToString();
            _failedtoteid = dsResult.Tables[0].Rows[0]["failed_tote_id"].Equals(System.DBNull.Value) ? 0 : Convert.ToDecimal(dsResult.Tables[0].Rows[0]["failed_tote_id"]);
            _failedtotelabel = dsResult.Tables[0].Rows[0]["failed_tote_label"].Equals(System.DBNull.Value) ? string.Empty : dsResult.Tables[0].Rows[0]["failed_tote_label"].ToString();
            _failedtotebarcode = dsResult.Tables[0].Rows[0]["failed_tote_barcode"].Equals(System.DBNull.Value) ? string.Empty : dsResult.Tables[0].Rows[0]["failed_tote_barcode"].ToString();


            nextordernumber = _nextordernumber;
            toteservice = _toteservice;
            nextlocid = _nextlocid;
            nextlocbarcode = _nextlocbarcode;
            nextloclabel = _nextloclabel;
            nextlocitemcount = _nextlocitemcount;
            totebarcodeprefix = _totebarcodeprefix;
            totetypename = _totetypename;
            failedtoteid = _failedtoteid;
            failedtotelabel = _failedtotelabel;
            failedtotebarcode = _failedtotebarcode;

        }


        public void moveincompletetotote(decimal chuteid, decimal currentordernumber, string locationbarcode, string totebarcode, string username, string terminalid, ref decimal nextordernumber)
        {

            decimal _nextordernumber = 0;

            DataSet dsResult = _dataManager.SelectDataSetProcedure(MOVE_INCOMPLETE_TO_TOTE,
                                    new Object[] { 
                                        chuteid,
                                        currentordernumber,
                                        locationbarcode,
                                        totebarcode,
                                        username,
                                        terminalid});

            _nextordernumber = dsResult.Tables[0].Rows[0]["next_ordernumber"].Equals(System.DBNull.Value) ? 0 : Convert.ToDecimal(dsResult.Tables[0].Rows[0]["next_ordernumber"]);
            
            nextordernumber = _nextordernumber;

        }



        public void itemsnotlocated(decimal itemId, ref decimal itemCount)
        {

            decimal _itemcount = 0;

            DataSet dsResult = _dataManager.ExecuteDataset(ITEMS_NOT_LOCATED,
                                    new Object[] {
                                        itemId});

            _itemcount = Convert.ToDecimal(dsResult.Tables[0].Rows[0]["item_count"]);

            itemCount = _itemcount;

        }



        public void itemsinchute(decimal chuteId, ref string itemsInChute)
        {

            string _itemsinchute = "F";

            DataSet dsResult = _dataManager.ExecuteDataset(ITEMS_IN_CHUTE,
                                    new Object[] {
                                        chuteId});

            _itemsinchute = dsResult.Tables[0].Rows[0]["items_found"].ToString();

            itemsInChute = _itemsinchute;

        }



        #endregion

    }
}
