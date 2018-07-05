using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using IHF.BusinessLayer.BusinessClasses;
using IHF.BusinessLayer.BusinessClasses.Stack;
using IHF.EnterpriseLibrary.DataServices;
using IHF.EnterpriseLibrary.Data;

using IHF.BusinessLayer.BusinessClasses.ManualSort;

namespace IHF.BusinessLayer.DataAccessObjects.ManualSort
{
    public class ManualSortDAO
    {

        SortArea _sortArea = new SortArea();

        SortLoad _sortAreaLoad = new SortLoad();

        ManualSortScan _sortScan = new ManualSortScan();

        ManualSingleLocate _singleLocate = new ManualSingleLocate();


        protected DataManager _dal = new DataManager(Util.DBInstanceEnum.Ora);

        public List<SortArea> GetAreas()
        {

            List<SortArea> _list = ((SortArea)_dal.Get(SortArea.ClassMethods.GetManualAreas.ToString()
                                                                          , this._sortArea
                                                                          , new object[] { })[0]).SortAreaInfo;

            return _list;
        }

        public List<SortLoad> GetAreaLoad(string areaID)
        {

            List<SortLoad> _list = ((SortLoad)_dal.Get(SortLoad.ClassMethods.GetAreaLoad.ToString()
                                                                          , this._sortAreaLoad
                                                                          , new object[] {areaID })[0]).SortLoadInfo;

            return _list;
        }




        public ManualSortScan GetItemScan(ManualSortScan itemscan)
        {

            ManualSortScan maualSortItem = (ManualSortScan)_dal.Get(ManualSortScan.ClassMethods.GetItemScan.ToString()
                                                                          , this._sortScan
                                                                          , new object[] {
                                                                                           itemscan.AreaID,
                                                                                           itemscan.LoadNo,
                                                                                           itemscan.ChuteID,
                                                                                           itemscan.OrderNo,
                                                                                           itemscan.ItemNo,
                                                                                           itemscan.ActionScan,
                                                                                           itemscan.ScanBarcode,
                                                                                           Shared.CurrentUser })[0];

            return maualSortItem;
        }




        public ManualSingleLocate  GetSingleLocate(ManualSingleLocate  scanLocate)
        {

            ManualSingleLocate maualSortItem = (ManualSingleLocate)_dal.Get(ManualSingleLocate.ClassMethods.GetSingleLocate.ToString()
                                                                          , this._singleLocate
                                                                          , new object[] {

                                                                                           scanLocate.ChuteID,
                                                                                           scanLocate.TrolleyID,
                                                                                           scanLocate.ScanMode,
                                                                                           scanLocate.ScanBarcode,
                                                                                           Shared.CurrentUser })[0];

            return maualSortItem;
        }

    }
}
