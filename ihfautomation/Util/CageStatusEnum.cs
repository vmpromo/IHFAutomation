using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IHF.BusinessLayer.Util
{
    public enum CageStatus
    {
        Created = 0,
        Available = 10,
        Attached = 20,
        AttachedInUse = 30,
        Detached = 40,
        Loaded = 50,
        Despatched = 60,
        Cancelled = 70
    }
}

