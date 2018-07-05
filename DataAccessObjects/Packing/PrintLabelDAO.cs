//
// Name: PrintLabelDAO.cs
// Type: ADO class
// Description: contains functionality for calling PL/SQL procedures
//              to generate the data for printing
//
//$Revision:   1.1  $
//
// Version   Date        Author    Reason
//  1.0      12/10/17    APetrescu Initial Release
//  1.1      12/07/17    APetrescu Added this header; cleaned up redundant using statements


using System;
using IHF.EnterpriseLibrary.Data;

namespace IHF.BusinessLayer.DataAccessObjects.Packing
{
    public class PrintLabelDAO
    {
        private const string GenerateBarcodesProc = "rm_returns_label.p_create_order_barcodes";
        private const string GetDespatchNoteTypeFct = "oms_print_util.f_get_despatch_note_type";
        private DataManager _dataManager;

        public PrintLabelDAO(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public int GetDespatchNoteType(string orderNo)
        {
            using (var dataReader = _dataManager.ExecuteReader(GetDespatchNoteTypeFct, new object[] {orderNo}))
            {
                if (!dataReader.Read())
                {
                    throw new ApplicationException("Unable to find despatch note type for order " + orderNo);
                }

                return int.Parse(dataReader[0].ToString());
            }
        }

        public void GenerateDespatchNoteBarcodes(string orderNo)
        {
            _dataManager.ExecuteNonQuery(GenerateBarcodesProc, new object[] { orderNo });
        }
    }
}
