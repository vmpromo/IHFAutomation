//
// Name: EventType.cs
// Type: Enum
// Description: enums for Event Tyes.
//$Revision:   1.13$
//
// Version   Date        Author    Reason
//  1.12     13/02/12    M Khan    Changed metapack error code names
//  1.13     27/04/12    M Khan    event code for saving metapack documents
//  1.16     30/05/14    S Green   Changes for Remove For Pack

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IHF.BusinessLayer.Util
{
    public enum EventType
    {


        Cancellation = 4,

        //locate events

        ScanChuteForAttach = 9,
        ScanTrolleyForAttach = 10,
        ScanItemForLocate = 11,
        ScanLocation = 12,
        ScanOFTForAttach = 13,
        TrolleyAutoDetach = 14,
        LogOnChuteForLocate = 15,
        LogOffChuteForLocate = 16,
        TrolleyAttachLocate = 53,
        LocateItem = 54,

        // Packing Types.
        TrolleyScanforPack = 17,
        FailToteScanforReOpenPack = 18,
        OpenforPackOrderSelection = 19,
        ParcelLabelBarcodeforRePack = 20,
        EnterOrderNoForRePack = 21,
        ScanItemtoPack = 22,
        CreateNewParcelScan = 23,
        LimitedQuantityItem = 24,
        CreateandAllocateCarrierService = 25,
        PackDocumentationCreated = 26,
        PackLabelsCreated = 27,
        PackLabelsPrint = 28,
        PackDocumentationPrint = 29,
        DespatchNoteCreated = 30,
        DespatchNotePrint = 31,
        OrderPacked = 32,
        TrolleyPackComplete = 33,
        CancelConsignmentMetaPack = 34,
        ReprintPackParperworkRequeted = 35,
        FailOrdertoTote = 36,
        FailOrderThresholdWarning = 37,
        ItemScantoFailedOrdertote = 38,
        FailToteScanFailOrder = 39,
        ItemTransferedFailOrderTote = 40,
        ProduceFailedToteOrderReport = 41,
        PrintFailedToteOrderReport = 42,
        ErrorToPackOrderReport = 43,
        PrintErrortoPackOrderReport = 44,
        ExcessItemMode = 45,
        ExcessItemScantoFailtote = 46,
        FailedToteScanExcessItemReasoncode = 47,

        CageDespatchedForCarrier = 48,
        RemoveCageFailure = 49,
        ReadyToManifestServiceFailure = 50,
        CreateManifestServiceFailure = 51,
        CageRemoved = 51,
        ClearTote = 55,

        // manual induct

        LoadSelectionForMI = 56,
        ScanSkuForMI = 57,
        ItemAssignedForOrderMI = 58,
        ItemReservedForMI = 59,
        ScanChuteForMI = 60,
        NonSortableItemMI = 61,
        ItemPushedToChute = 62,
        MIRequestCancelled = 63,

        // Manual detach trolley
        MDLogonChute = 64,
        MDTrolleyScan = 65,
        MDLogoffChute = 66,

        // trolley config

        CreateModifyTrolley = 67,
        PrintTrolleyLabel = 68,

        // Metapack errors and performance logging
        NetworkOrServiceDown = 69,
        E10                  = 121,
        E20                  = 122, 
        E30                  = 123,
        E40                  = 124,
        MetapackPerformance  = 125,

        //event for saving metapack documents
        SavePackDocuments    = 140,

        // manual sort
        ManualSortLoadRelease = 144,

        MetapPackAllocationFail = 149,
        
        // remove for pack
        RescanLocation = 163


    }
}
