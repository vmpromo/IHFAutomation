using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.DataServices;

namespace IHF.BusinessLayer.BusinessClasses
{

    class Cagetype : IDataService
    {
        public string CageTypeId {get; set;}
        public string CageTypeDescr { get; set; }
        public string CarrierId { get; set; }


    }
}
