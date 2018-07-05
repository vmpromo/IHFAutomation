//
// Name: PackAddress.cs
// Type: Entity Class 
// Description: Business Entity class of a type Address.
//
//$Revision:   1.0  $
//
// Version   Date        Author    Reason
//  1.0      12/07/11    MSalman   Initial Released

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IHF.BusinessLayer.BusinessClasses.Packing
{
    public class PackAddress
    {

        public AddressType Type { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }

        public string  CountryCode { get; set; }

        public string PostCode { get; set; }


    }


    public enum AddressType
    {
        SenderAddress = 0,
        RecipientAddress = 1

    }
}
