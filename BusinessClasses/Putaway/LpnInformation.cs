// Name: LpnInformation.cs
// Type: class file 
// Description: Business class for LPN
//
//$Revision:   1.2  $
//
// Version   Date        Author     Reason
//  1.0      08/05/18    A Petrescu Initial Revision
//  1.1      08/06/18    M Cacket   Added sku.
//                       S Remedios
//                       S Patel
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IHF.BusinessLayer.BusinessClasses.Putaway
{
    public class LpnInformation
    {
        public string OrderNumber { get; set; }
        public string ItemNumber { get; set; }
        public string ActualLocation { get; set; }
        public int? ActionCode { get; set; }
        public string Sku { get; set; }
    }
}
