using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.DataServices;

namespace IHF.BusinessLayer.BusinessClasses
{
    class Carrier : IDataService
    {
        public string CarrierId { get; set; }
        public string CarrerDescr { get; set; }
    }
}
