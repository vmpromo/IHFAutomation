using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Dispatcher;
using IHF.BusinessLayer.DataAccessObjects;

namespace IHF.ApplicationLayer.Web.Code.WcfBehaviours
{
    public class WcfErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            new LoggerDAO().LogException(error);
            return false;
        }

        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
        }
    }
}