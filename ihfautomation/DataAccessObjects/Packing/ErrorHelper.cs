//
// Name: ErrorHelper.cs
// Type: class object.
// Description: Exception classes to handle errors.
//$Revision:   1.3$
//
// Version   Date        Author    Reason
//  1.0      28/07/11    MSalman   Initial Release
//  1.1      12/08/11    MSalman   New Error Type added       
//  1.2      07/20/11    MSalman   New Error Type added       
//  1.3      13/02/12    M Khan    Added call oto log exception to the database
//  1.4      24/09/12    J Watt    Error logging done prior to raising exception      
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IHF.BusinessLayer.DataAccessObjects.Packing
{
    public class ConsignmentAllocationFailure : Exception
    {
        public ConsignmentAllocationFailure (string message) : base(message) {}
    }

    public class MetaPackFailure : Exception
    {
        public MetaPackFailure(string message) : base(message) { }
    }

    public class LabelReceivedFailure : Exception
    {
        public LabelReceivedFailure (string message) : base (message) {}
    }

    public class CancelAllocationFailure : Exception
    {
        public CancelAllocationFailure (string message) : base (message) {}
    }

    public class DocumentReceivedFailure : Exception
    {
        public DocumentReceivedFailure(string message) : base(message) { }
    }

    public class PrintDocumentFailure : Exception
    {
        public PrintDocumentFailure(string message) : base(message) { }
    }

    public class SaveDocumentFailure : Exception
    {
        public SaveDocumentFailure(string message) : base(message) { }
    }

    public class SaveLabelFailure : Exception
    {
        public SaveLabelFailure(string message) : base(message) { }
    }
}
