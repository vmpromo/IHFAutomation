// Name: LocationServiceResponse.cs
// Type: class file 
// Description: Class for the responses from the location service
//
//$Revision:   1.2  $
//
// Version   Date        Author     Reason
//  1.0      01/03/18    A Petrescu Initial Revision
//  1.1      23/03/18    S Patel    ORDS Implementation - two new classes created.
//  1.2      21/05/18    M Cacket   Added classes for ORDS check location.
//                       S Remedios

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IHF.BusinessLayer.DataAccessObjects.Returns.Dto
{

    public class SkuLocation
    {
        public string loc { get; set; }
    }

    public class LocationServiceResponse
    {
        public List<SkuLocation> items { get; set; }
    }

    public class ChkLocation
    {
        public string locn_status { get; set; }
    }

    public class ChkLocationServiceResponse
    {
        public List<ChkLocation> items { get; set; }
    }


}
