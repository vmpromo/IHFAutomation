using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IHF.BusinessLayer.Util
{
    public enum RemoveForPackStep
    {
        Initial = 1,  // Validate Chute
        ScanSku,
        ScanLocation, // Trolley location or overflow tote
        ReScanLocation,
        ScanTote,
        ScanSinglesChute
    }
}
