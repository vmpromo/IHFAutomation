//
// Name: BookingCodeParamsDAO.cs
// Type: DAO class
// Description: contains the class variables, methods and pl/sql calls
//              associated with the BookingCodeParams entities
//
//$Revision:   1.0  $
//
// Version   Date        Author     Reason
//  1.0      05/02/15    M Cackett  Initial Release

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Text.RegularExpressions;
using System.Net;
using System.Configuration;
using IHF.EnterpriseLibrary.Data;
using IHF.BusinessLayer.BusinessClasses.Packing;
using IHF.BusinessLayer.BusinessClasses.ActivityLog;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.BusinessLayer.DataAccessObjects.ActivityLog;
using IHF.BusinessLayer.Util;
using Com.MetaPack.DeliveryManager;
using System.Data;
using Oracle.DataAccess.Client;
using IHF.BusinessLayer.BusinessClasses;
using IHF.BusinessLayer.BusinessClasses.Stack;
using IHF.EnterpriseLibrary.DataServices;



namespace IHF.BusinessLayer.DataAccessObjects.Packing
{
    class BookingCodeParamsDAO
    {
        #region Private Memebers

        protected DateTime? timeBeforeCall = null;
        protected DateTime? timeAfterCall = null;

        protected DataManager _dal = new DataManager(Util.DBInstanceEnum.Ora);

        protected BookingCodeParams _bookingcodeparams = new BookingCodeParams();

        #endregion

        #region Local Functions

        public IList<BookingCodeParams> GetBookingCodeParams(string serviceGroupId)
        {

            IList<BookingCodeParams> _list = ((BookingCodeParams)_dal.Get(BookingCodeParams.ClassMethods.GetBookingCodeParams.ToString()
                                                                                , this._bookingcodeparams
                                                                                , new object[] { serviceGroupId })[0]).BookingCodeInfo;


            return _list;
                                


        }

        #endregion




    }
}
