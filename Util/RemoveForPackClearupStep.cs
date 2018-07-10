using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IHF.BusinessLayer.Util
{
    public enum RemoveForPackClearupStep
    {
        Initial = 1,  // Validate Chute
        ScanLocation, // Trolley location or overflow tote
        ScanTote
    }

}
