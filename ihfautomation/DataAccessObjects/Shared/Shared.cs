//
// Name: Shared.cs
// Type: Shared Class 
// Description: Shared class for getting logged in User Name 
//              ,Client Host name, and client IP address.
//
//$Revision:   1.0  $
//
// Version   Date        Author    Reason
//  1.0      12/07/11    MSalman   Initial Released

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.UI;

namespace IHF.BusinessLayer.DataAccessObjects
{
    public class Shared
    {



        public static string CurrentUser
        {
            get
            {
                return HttpContext.Current.User.Identity.Name.ToString();
            }



        }

        public static string UserIPAddress
        {
            get
            {

                return HttpContext.Current.Request.UserHostAddress.ToString();

            }


        }

        public static string UserHostName
        {

            get
            {

                return Dns.GetHostEntry(
                            HttpContext.Current.Request.ServerVariables["remote_host"]).HostName.ToString();

            }

        }




    }



}