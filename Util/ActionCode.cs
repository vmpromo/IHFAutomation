using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IHF.BusinessLayer.Util
{
    public enum ActionCode
    {
        NoStock = 10,
        SellableReturn = 30,
        DamagedReturn = 40,
        MispickReturn = 50,
        CustomerServicesReturn = 70
    }
}
