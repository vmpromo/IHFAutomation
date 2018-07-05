// Name: PackRequest.cs
// Type: Business Entity Class for Packing.
// Description: Class contains properties used screen packing.
//
//$Revision:   1.10$
//
// Version   Date        Author    Reason
//  1.0      12/07/11    MSalman   Initial Released
//  1.1      20/07/11    MSalman   New property added.
//  1.2      01/08/11    MSalman   New Status field added.
//  1.3      01/09/11    MSalman   New Status field added.
//  1.4      05/09/11    MSalman   New Status field added.
//  1.5      09/09/11    MSalman   New Status field added.
//  1.6      15/09/11    MSalman   New Status field added.
//  1.10     03/04/12    M Khan    Added transport mode property.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IHF.BusinessLayer.BusinessClasses.Packing
{
    public class PackRequest
    {

        public string Barcode { get; set; }

        public string UserName { get; set; }

        public string HostName { get; set; }

        public string TrolleyId { get; set; }

        public string ToteId { get; set; }

        public string PreviousToteId{ get; set; }

        public string ContainerNO { get; set; }

        public string ActionId { get; set; }

        public string PreviousActionId { get; set; }

        public string PackMode { get; set; }

        public string PreviousPackMode { get; set; }

        public string DestinationType { get; set; }

        public string ExcessItemInd { get; set; }

        public string OrderNo { get; set; }

        public string  PreviousOrderNo { get; set; }

        public string ReasonCode { get; set; }

        public string InOrder { get; set; }

        public string Docs { get; set; }

        public string EndOfTrolley { get; set; }

        public string PreviousCurrentItem { get; set; }

        public string PreviousTotalItem { get; set; }

        public string PreviousContainerLabel { get; set; }

        public string PreviousLocation { get; set; }

        public string PreviousTotalParcelBag { get; set; }

        public string PreviousOrderCount { get; set; }

        public string MissingItemToteId { get; set; }

        public string TransportMode { get; set; }
    }

}
