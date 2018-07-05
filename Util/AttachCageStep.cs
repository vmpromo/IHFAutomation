using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IHF.BusinessLayer.Util
{
    public enum  AttachCageStep
    {
        ActionBarcodeScan = 1,
        CageBarcodeScan
    }

    public enum CageAction
    {
        CageAttach = 1,
        CageDetach = 2,
        CageTypeEnd = 3
    }
}
