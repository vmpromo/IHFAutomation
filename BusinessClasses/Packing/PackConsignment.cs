// Name: PackConsignment.cs
// Type: Business Entity Class for Packing.
// Description: Class contains properties, and method user
//              to call database functions.
//
//$Revision:   1.2  $
//
// Version   Date        Author    Reason
//  1.0      12/07/11    MSalman   Initial Released
//  1.1      04/08/11    MSalman   New field updated.
//  1.2      25/08/11    MSalman   New field is added.       
//  1.3      21/09/11    MSalman   New field is added.       
//  1.4      23/09/11    MSalman   New field is added.  
//  1.5      21/10/11    MSalman   Delivery Instructions field added        
//  1.9      03/04/12    M Khan    Added properties for Personal and recipient address.
//  1.10     18/09/12    J Watt    Additional properties for 8x4 label
//  1.11     28/11/12    J Watt    Store order addtional properties
//  1.12     25/01/13    J Watt    Added additional properties for multi currency
//  1.13     24/05/13    J Watt    Intermediate checkin
//  1.14     13/06/13    J Watt    Click and collect intermedieate checkin
//  1.15     19/06/13    J Watt    CloneInd - using system
//  1.16     19/06/13    J Wat     Added property for collect+ store
//  1.17     28/07/14    S Green   Added property for delivery to store label tags
//  1.18     31/07/17    M. Smart  Added Nominated Day Delivery (NDD) and carrier collection date

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IHF.BusinessLayer.BusinessClasses.Packing;
using IHF.EnterpriseLibrary.DataServices;


namespace IHF.BusinessLayer.BusinessClasses.Packing
{
    public class PackConsignment : IDataService
    {
        #region Private Memeber

        private const string GET_CONSIGNMENT_TO_PACK = "OMS_PACK.F_GET_PARCEL";

        private const string GET_CONSIGNMENT_CODE    = "OMS_PACK.F_GET_CONSIGNMENT_CODE";


        private List<PackConsignment> _consignment = new List<PackConsignment>();




        #endregion

        #region Functions Mapping

        public enum ClassMethods
        {
            GetPackConsignment,
            GetConsignmentCode

        }
        #endregion

        #region LocalProperties

        public string ConsignmentCode { get; set; }

        public string  OrderNo { get; set; }

        public string SenderName { get; set; }

        public decimal WarehouseId { get; set; }

        public string SenderCode { get; set; }

        public string CustomerName { get; set; }

        //Package_Id
        public string ParcelBagID { get; set; }

        //Sum of the product weight in that bag.
        public string ParcelWeight { get; set; }

        public string Sku { get; set; }

        public string ItemVolume { get; set; }

        public string ItemWeight { get; set; }

        public string ItemLength { get; set; }

        public string ItemWidth { get; set; }

        public string ItemHeight { get; set; }

        public string CountryCode { get; set; }

        public string FabricComposition { get; set; }

        public string ProductDescription { get; set; }

        public double ProductValue { get; set; }

        public string LiquidType { get; set; }

        public long ProductQuantity { get; set; }

        public double ConsignmentWeight { get; set; }

        public DateTime DeliverByDate { get; set; }

        public string SenderAddress { get; set; }

        public string SenderPhone { get; set; }

        public string SenderEmailAdderss { get; set; }

        public string RecipientAddress { get; set; }

        public string RecipientPhone { get; set; }

        public string RecipientEmailAddress { get; set; }

        public string DestinationCode { get; set; }

        public string ServiceGroup { get; set; }

        public string TariffCode { get; set; }

        public string ProductTypeDescription { get; set; }

        public string DeliveryInstructions { get; set; }

        public string Gs1Barcode { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime CustCollectionDate { get; set; }

        public string SourceCode { get; set; }

        public string StoreName { get; set; }

        public string StoreNum { get; set; }

        public string CloneInd { get; set; }

        public string CollectPlusStore { get; set; }

        public string DestType { get; set; }

        public string CurrencyCode { get; set; }

        public double CurrencyRate { get; set; }

        public string StoreDelivOrdTypeTag { get; set; }

        // Nominated Delivery Precise time slot reference and carrier collection date
        public string NddSlotTokenId { get; set; }
        public DateTime CarrierCollectionDate { get; set; }
      
        
        public List<PackConsignment> PackConsignmentInfo
        {
            get
            {
                return _consignment;
            }
            set
            {
                _consignment = value;
            }
        }

        #endregion

        #region Local Functions

        [MethodMapper("GetPackConsignment", PackConsignment.GET_CONSIGNMENT_TO_PACK)]
        public IList<IDataService> GetPackConsignment(IDataReader reader)
        {

            IList<IDataService> lst = new List<IDataService>();

            List<PackConsignment> items = new List<PackConsignment>();


            while (reader.Read())
            {
                PackConsignment obj = new PackConsignment();

                obj.OrderNo            = reader["ORDERNUMBER"].ToString()?? string.Empty;
                obj.DeliverByDate      = Convert.ToDateTime(reader["DELIVERBYDATE"].ToString());
                obj.SenderName         = reader["SENDERNAME"].ToString() ?? string.Empty;
                obj.WarehouseId        = reader["WAREHOUSEID"].ToString() == string.Empty ? 1 : decimal.Parse(reader["WAREHOUSEID"].ToString());
                obj.SenderAddress      = reader["SENDERADDRESS"].ToString() ?? string.Empty;
                obj.CustomerName       = reader["CUSTOMERNAME"].ToString() ?? string.Empty;
                obj.RecipientAddress   = reader["CUSTOMERADDRESS"].ToString() ?? string.Empty;
                obj.ParcelBagID        = reader["PACKAGE_ID"].ToString() ?? string.Empty;
                obj.Sku                = reader["SKU"].ToString() ?? string.Empty;
                obj.ItemVolume         = reader["ITEM_VOLUME"].ToString() ?? string.Empty;
                obj.ItemWeight         = reader["ITEM_WEIGHT"].ToString() ?? string.Empty;
                obj.ItemLength         = reader["ITEM_LENGTH"].ToString() ?? string.Empty;
                obj.ItemWidth          = reader["ITEM_WIDTH"].ToString() ?? string.Empty;
                obj.ItemHeight         = reader["ITEM_HEIGHT"].ToString() ?? string.Empty;
                obj.CountryCode        = reader["COUNTRY_CODE"].ToString() ?? string.Empty;
                obj.FabricComposition  = reader["FABRIC_COMPOSITION"].ToString() ?? string.Empty;
                obj.ProductDescription = reader["PRODUCT_DESCRIPTION"].ToString() ?? string.Empty;
                obj.LiquidType         = reader["LIQUID_TYPE"].ToString() ?? string.Empty;
                obj.ProductQuantity    = reader["PRODUCT_QUANTITY"].ToString()==""? 0 : (long)((decimal)reader["PRODUCT_QUANTITY"]);
                obj.ProductValue       = reader["ITEM_PRICE"].ToString() == "" ? 0 : Convert.ToDouble(reader["ITEM_PRICE"].ToString());
                obj.DestinationCode    = reader["DESTINATION_CODE"].ToString() ?? string.Empty;
                obj.ServiceGroup       = reader["SERVICE_GROUP"].ToString() ?? string.Empty;
                obj.TariffCode         = reader["HARMONISATIONCODE"].ToString() ?? string.Empty;
                obj.ProductTypeDescription = reader["PRODUCTTYPEDESCRIPTION"].ToString() ?? string.Empty;
                obj.RecipientPhone         = reader["CONTACTPHONENUMBER"].ToString() ?? string.Empty;
                obj.SenderPhone            = reader["SENDERPHONENUMBER"].ToString() ?? string.Empty;
                obj.DeliveryInstructions   = reader["DELIVERYINSTRUCTIONS1"].ToString() ?? string.Empty;
                obj.SenderEmailAdderss     = reader["PersonalEmailAddress"].ToString() ?? string.Empty;
                obj.RecipientEmailAddress  = reader["RecipientEmailAddress"].ToString() ?? string.Empty;
                obj.SourceCode             = reader["SOURCECODE"].ToString() ?? string.Empty;
                obj.Gs1Barcode             = reader["GS1_BARCODE"].ToString();
                obj.OrderDate              = Convert.ToDateTime(reader["ORDERDATE"].ToString());
                string custCollection      = reader["CUST_COLLECTION_DTM"].ToString();
                if (string.IsNullOrEmpty(custCollection))
                    obj.CustCollectionDate = obj.DeliverByDate;
                else
                    obj.CustCollectionDate = Convert.ToDateTime(reader["CUST_COLLECTION_DTM"].ToString());
                obj.StoreName              = reader["STORENAME"].ToString();
                obj.StoreNum               = reader["STORENUM"].ToString();
                obj.DestType               = reader["DEST_TYPE"].ToString();
                obj.CurrencyCode           = reader["CURRENCYCODE"].ToString() ?? string.Empty;
                obj.CurrencyRate           = reader["CURRENCYRATE"].ToString() == "" ? 0 : Convert.ToDouble(reader["CURRENCYRATE"].ToString());
                obj.SenderCode             = reader["SENDERCODE"].ToString() ?? string.Empty;
                obj.CloneInd               = reader["CLONE_IND"].ToString() ?? string.Empty;
                obj.CollectPlusStore       = reader["COLLECTPLUSSTORE"].ToString() ?? string.Empty;
                obj.StoreDelivOrdTypeTag   = reader["STORE_DELIV_ORD_TYPE_TAG"].ToString() ?? string.Empty;
                obj.NddSlotTokenId         = reader["NDD_SLOT_TOKEN_ID"].ToString() ?? string.Empty;
                obj.CarrierCollectionDate  = Convert.ToDateTime(reader["EARLIEST_COLLECT_DTM"].ToString());

                items.Add(obj);

            }
            this.PackConsignmentInfo = items;
            lst.Add(this);
            reader.Close();

            return lst;
        }


        [MethodMapper("GetConsignmentCode", PackConsignment.GET_CONSIGNMENT_CODE)]
        public IList<IDataService> GetConsignmentCode(IDataReader reader)
        {

            IList<IDataService> lst = new List<IDataService>();

            if (reader.Read())
            {
                this.ConsignmentCode = reader["CONSIGNMENTCODE"].ToString() ?? string.Empty;

            }
            lst.Add(this);
            reader.Close();

            return lst;
        }



        #endregion



    }
}
