//
// Name: Enumerations.cs
// Type: Class contains Enumeration for Packing.
// Description: Class contains enumeration using the packing.
//
//$Revision:   1.5  $
//
// Version   Date        Author    Reason
//  1.0      12/07/11    MSalman   Initial Released
//  1.2      04/08/11    MSalman   Updated the enum values.       
//  1.3      19/09/11    MSalman   Updated the enum values.       
//  1.4      10/02/12    M Khan    Enum for EventReason required for logging
//  1.5      13/02/12    M Khan    Added reason for Other error
//  1.6      25/06/12    M Salman  Added Manual Sort Scan Mode. 
//  1.7      25/06/12    M Salman  Changed to int. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IHF.BusinessLayer.Util
{
    public class Enumerations
    {
        public enum Action
        {
            CV,
            OI,
            EI,
            FT,
            RP,
            NB,
            OO,
            MI
        }

        public enum Mode
        {
            MIS,
            SIS

        }

        public enum DType
        {
            DOM,
            INT
        }

        public enum ConsignmentAddressType
        {
            Sender,
            Recipient

        }

        public enum SenderName
        {
            DEFAULT,
            RI

        }

        public enum PdfType
        {
            L,
            D

        }

        public enum MetaPackCallErrorStatus
        {

            MetaPackCallFailed,
            MetaPackCallSuccessul

        }

        public enum ApplicationFunctions
        {
            Returns
        }

        public enum IHFDatabase
        {
            omsdev,
            omstr2,
            omsprd
        }

        public enum SystemParameter
        {
            MIN_DESPATCH_OFFSET_DAYS,
            MAX_DESPATCH_OFFSET_DAYS,
            DESPATCH_SERVICE_INTERVAL
        }

        public enum EventReason
        {
            //Metapack network errors
            RemoteName = 40,
            UnableToConnect = 41,
            ServiceUnavailable = 42,
            UnknownHost = 43,
            OtherError = 44,

            // Metapack calls
            CreateAndAllocateConsignment = 45,
            AllocateConsignment = 46,
            CreateLabelsAsPdf = 47,
            CreateDocumentationAsPdf = 48,
            DeAllocate = 49,
            DeleteConsignment = 50,
            MarkConsignmentAsPrinted = 51,
            MarkConsignmentAsReadyToManifest = 52,
            CreateManifestForFutureDespatch = 53
        }


        public enum ManualSortScanMode
        {
          
            IS= 1,
            PC = 2

        }

    }


}
