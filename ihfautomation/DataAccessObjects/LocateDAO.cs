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
    public class LocateDAO
    {

        #region "private constants"

        private const string Validatechute = "oms_attach_trolley.p_validate_chute";
        private const string ChuteTrolley = "oms_locate.p_chute_trolley";
        private const string ValidateTrolley = "oms_attach_trolley.p_validate_trolley";
        private const string AttachTrolley = "oms_attach_trolley.p_attach_trolley_to_chute";
        private const string LogOnChute = "oms_locate.p_log_on_to_chute";
        private const string FindItem = "oms_locate.p_find_chute_item";
        private const string LocationName = "oms_locate.p_location_name_for_item";
        private const string ValidateLocation = "oms_locate.p_valid_location";
        private const string LocateItem = "oms_locate.p_locate_item";
        private const string ValidateTote = "oms_locate.p_validate_overflow_tote";
        private const string LocateTote = "oms_locate.p_locate_to_overflow_tote";
        private const string LogOffChute = "oms_locate.p_log_off_to_chute";
        private const string GetChuteType = "oms_locate.f_get_chute_type";
        private const string GetChuteArea = "oms_locate.f_get_area";
        private const string ManualAreaitem = "oms_locate.f_sku_manual_single";
        private const string PreValidateLoc = "oms_locate.f_validate_location";
        private const string validateChute = "oms_locate.f_validate_single_chute";


        #endregion

        #region "private variables"

        private DataManager dataManager = new DataManager(Util.DBInstanceEnum.Ora);
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


        public decimal Validate_Chute(string I_chute_barcode, string I_user, string I_terminal_id)
        {
            decimal chute_id = 0;
            Object[] locParams = new Object[] { chute_id, I_chute_barcode, I_user, I_terminal_id };

            chute_id = dataManager.ExecuteReturnMethodDecimal(
                                                Validatechute.ToString(),
                                                locParams);
            return chute_id;
        }

        public decimal Chute_Trolley(string I_chute_barcode, string I_user, string I_terminal_id)
        {
            decimal trolley_id = 0;
            Object[] locParams = new Object[] { trolley_id, I_chute_barcode, I_user, I_terminal_id };

            trolley_id = dataManager.ExecuteReturnMethodDecimal(
                                                ChuteTrolley.ToString(),
                                                locParams);
            return trolley_id;
        }

        public decimal Validate_Trolley(decimal I_chute_id, string I_trolley_barcode, string I_user, string I_terminal_id)
        {
            decimal trolley_id = 0;
            Object[] locParams = new Object[] { trolley_id, I_chute_id, I_trolley_barcode, I_user, I_terminal_id };

            trolley_id = dataManager.ExecuteReturnMethodDecimal(
                                                ValidateTrolley.ToString(),
                                                locParams);

            return trolley_id;
        }

        public void Attach_Trolley(decimal I_chute_id, decimal I_trolley_id, string I_user, string I_terminal_id)
        {

            Object[] locParams = new Object[] { I_chute_id, I_trolley_id, I_user, I_terminal_id };

            dataManager.ExecuteNonQuery(AttachTrolley.ToString(),
                                        locParams);


        }

        public void Log_on_to_Chute(string I_chute_barcode, string I_user_logon)
        {


            Object[] locParams = new Object[] { I_chute_barcode, I_user_logon };

            dataManager.ExecuteNonQuery(LogOnChute.ToString(),
                                        locParams);



        }

        public decimal Find_item(decimal I_chute_id, string I_sku_barcode, string I_user, string I_terminal_id,decimal area_id)
        {
            decimal item_id = 0;
            Object[] locParams = new Object[] { item_id, I_chute_id, I_sku_barcode, I_user, I_terminal_id, area_id };

            item_id = dataManager.ExecuteReturnMethodDecimal(
                                                FindItem.ToString(),
                                                locParams);
            return item_id;
        }


        public string Find_location_name(decimal I_item_id)
        {
            Object[] locParams = new Object[] { I_item_id };

            return dataManager.GetStringforProcedure(
                                                LocationName.ToString(),
                                                locParams);
        }

        public decimal Validate_Location(string I_location_barcode, decimal I_item_id, string I_user, string I_terminal_id)
        {
            decimal location_id = 0;
            Object[] locParams = new Object[] { location_id, I_location_barcode, I_item_id, I_user, I_terminal_id };

            location_id = dataManager.ExecuteReturnMethodDecimal(
                                                ValidateLocation.ToString(),
                                                locParams);

            return location_id;
        }


        public string Locate_item(decimal I_item_id, decimal I_location_id, string I_user_logon, string I_terminal_id)
        {
            Object[] locParams = new Object[] { I_item_id, I_location_id, I_user_logon, I_terminal_id };

            return dataManager.GetStringforProcedure(
                                                LocateItem.ToString(),
                                                locParams);
        }

        public decimal Validate_Tote(string I_tote_barcode, decimal I_item_id, string I_user, string I_terminal_id)
        {
            decimal tote_id = 0;
            Object[] locParams = new Object[] { tote_id, I_tote_barcode, I_item_id, I_user, I_terminal_id };

            tote_id = dataManager.ExecuteReturnMethodDecimal(
                                                ValidateTote.ToString(),
                                                locParams);

            return tote_id;
        }


        public string Locate_tote_item(decimal I_item_id, decimal I_tote_id, string I_user_logon, decimal I_trolley_id, string I_terminal_id)
        {
            Object[] locParams = new Object[] { I_item_id, I_tote_id, I_user_logon, I_trolley_id, I_terminal_id };

            return dataManager.GetStringforProcedure(
                                                LocateTote.ToString(),
                                                locParams);
        }

        public void Log_off_Chute(decimal I_chute_id, string I_user_logon)
        {

            Object[] locParams = new Object[] { I_chute_id, I_user_logon };

            dataManager.ExecuteNonQuery(LogOffChute.ToString(),
                                        locParams);



        }

        public decimal Get_chute_type(decimal chute_id)
        {


            Object[] locParams = new Object[] { chute_id };

            return dataManager.GetValuedecimal(GetChuteType.ToString(),
                                            locParams);
        }


        public decimal Get_Chute_Area(decimal chute_id)
        {


            Object[] locParams = new Object[] { chute_id };

            return dataManager.GetValuedecimal(GetChuteArea.ToString(),
                                            locParams);
        }


        public string OrderForManualArea(decimal areaid,decimal chuteid, decimal itemNumber)
        {

            string retVal ;

            Object[] locParams = new Object[] {areaid,chuteid,itemNumber };

            retVal = dataManager.GetValue(ManualAreaitem.ToString(),
                                                locParams);

            
            return retVal;

        }

        public decimal PreValidateLocation(string I_loc_barcode)
        {


            Object[] locParams = new Object[] { I_loc_barcode };

            return dataManager.GetValuedecimal(PreValidateLoc.ToString(),
                                            locParams);
        }


        public decimal PreValidateChute(string I_chute_barcode)
        {


            Object[] locParams = new Object[] { I_chute_barcode };

            return dataManager.GetValuedecimal(validateChute.ToString(),
                                            locParams);
        }

        #endregion

    }
}
