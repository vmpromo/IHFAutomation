//
// Name: ActivityLogEnum.cs
// Type: Enumeration class
// Description: Collection of enumeration types using for 
//              Activity logging.
//
//$Revision:   1.0 $
//
// Version   Date        Author    Reason
//  1.0      01/08/11    MSalman   Initial Release
//  1.1      01/08/11    MSalman   Updated to be a object. 
//  1.2      16/08/11    MSalman   New Types are added.             
//  1.14     30/05/15    S Green   Changes for Remove For Pack

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IHF.BusinessLayer.Util
{
    public class ActivityLogEnum
    {
        public enum AppSystem
        {
            
            MDS = 1,
            IHF = 2,
            IHFDAS = 3,
            OMSRET = 4
        }

        public enum ApplicationID
        {

            General = 1,
            Configuration = 2,
            LoadInterface = 3,
            LoadRelease = 4,
            AutoInduction = 5,
            ManualInduction = 6,
            AttachAndLocate = 7,
            ManualTrolleyDetach = 8,
            Pack = 9,
            DespatchLaneCageChange = 10,
            DespatchLaneAssignment = 11,
            AutoCaging = 12,
            ManualCaging = 13,
            CageParcelRemove = 14,
            DespatchCage = 15,
            Manifest = 16,
            Payment = 17,
            CustomerEmail = 18,
            ItemCancellation = 19,
            ClearExcessTotes = 20,
            UserConfiguration = 21,
            ReprintPackPaperWork = 22,
            Returns = 23,
            OrderItemCancellation = 24,
            ForceLocate = 25,
            MoveFTToNewLocation = 26,
            PreWork = 27,
            Reprint = 28,
            UserManager = 29,
            ManualSort = 30,
            RemoveForPack=33

        }


        public enum ModuleID
        {

            General = 1,
            AttachAndlocate = 2,
            ManualTrolleyDetach = 3,
            Pack = 4,
            OpenForPack = 5,
            ExcessItem = 6,
            FailOrder = 7,
            RePrintPackpaperwork = 8,
            DespatchLaneCageChange = 9,
            DespatchLaneAssignment = 10,
            Autocaging = 11,
            ManualCaging = 12,
            CageParcelRemove = 13,
            CagesReadyForDespatch = 14,
            CagesRemovedFromReadyToDespatch = 15,
            DespatchCarrierCages = 16,
            TrolleyAndLocationsConfig = 17,
            CagesConfig = 18,
            ChutesConfig = 19,
            TrolleyTypesCongif = 20,
            DespatchLanesConfig = 21,
            DevicesConfig = 22,
            WorkstationsConfig = 23,
            FailedTotesConfig = 24,
            OverflowTotesConfig = 25,
            CarriersConfig = 26,
            CarrierServicesConfig = 27,
            LoadInterface = 28,
            LoadRelease = 29,
            AutoInduction = 30,
            ManualInductToChute = 31,
            Prework = 32,
            MSLoadRelease = 33,
            RemoveForPack = 43,
            RemoveForPackClrup = 44


        }

        public enum ResultCd
        {

            Success = 0,
            //locate
            LogonFailed = 1,
            ChuteScanFailed = 2,
            TrolleyScanFailed = 4,
            SkuValidationFailed = 5,
            ItemvalidationFailed = 6,
            LocationValidationFailed = 7,
            OFTValidationFailed = 8,
            FailedToLocate = 9,
            FailedToLocateInOT = 10,
            FailedToAttach = 11,
            FailedToDetach = 12,
            InvalidChuteType = 13,
            //manual induct
            LoadValidationFailedMI = 14,
            SKUValidationFailedMI = 15,
            RequestFailedMI = 16,
            CancelRequestFailedMI = 17,
            PutToChuteFailed = 18,
            ScanChuteFailedMI = 19,
            // manual detach
            MDChuteScanFailed = 20,
            MDTrolleyScanFailed = 21,
            MDFailed = 22,
            //trolley config
            CreateModifyFailed = 23,
            PrintTrolleyLabelFailed = 24,

            // manual sort
            IncorrectRelAction = 59,
            MSLoadReleaseFailed = 60

        }



    }
}
