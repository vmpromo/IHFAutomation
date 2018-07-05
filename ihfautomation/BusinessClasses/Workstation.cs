//
// Name       : Workstation.cs
// Type       : entity class
// Description: properties for workstation entity
//
//$Revision:   1.23$
//
// Version   Date        Author    Reason
//  1.23     12/04/12    M Khan    Added text and value variables

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.DataServices;
using System.Data;

namespace IHF.BusinessLayer.BusinessClasses
{
    public abstract class Workstation:IDataService
    {
        #region "private variables and constants"
        private decimal     _id;
        private decimal     _type;
        private decimal     _status;
        private DateTime    _createdOn;
        private string      _barcode;
        private string      _workstationLabel;
        private string      _createdBy;
        private decimal?     _trolleyId;
        private string      _isInternational;
        private DateTime    _lastChangedOn;
        private string      _lastchangedBy;

        private const string WORKSTATION_BY_ID = "oms_workstation_util.f_get_workstation_by_id";

        #endregion

        #region "properties"
            public decimal   ID 
            {
                get { return _id; }
                set { _id = value; } 
            }

            public decimal   Status 
            {
                get { return _status;}
                set { _status = value; } 
            }

            public DateTime  CreatedOn 
            {
                get { return _createdOn; }
                set { _createdOn = value; } 
            }

            public string    Barcode
            {
                get { return _barcode; }
                set { _barcode = value; }
            }

            public string    WorkstationLabel
            {
                get { return _workstationLabel; }
                set { _workstationLabel = value;}
            }

            public string    CreatedBy
            {
                get { return _createdBy; }
                set { _createdBy = value; }
            }

            public DateTime  LastChangedOn
            {
                get { return _lastChangedOn; }
                set { _lastChangedOn = value; }
            }

            public string    LastChangedBy
            {
                get { return _lastchangedBy; }
                set { _lastchangedBy = value; }
            }

            public decimal Type
            {
                get { return _type; }
                set { _type = value; }
            }

            public decimal? TrolleyID
            {
                get { return _trolleyId; }
                set { _trolleyId = value; }
            }

            public string IsInternational
            {
                get 
                {
                    return _isInternational;
                }
                set
                {
                    _isInternational = value;
                }
            }
        #endregion

        
        #region "enum"
            public enum ClassMethods
            {
                GetWorkstations,
            }
        #endregion

        #region "public methods available for data layer"

        [MethodMapper("GetWorkstations", Workstation.WORKSTATION_BY_ID)]
        public List<IDataService> GetWorkstations(IDataReader dataReader)
        {
            List<IDataService> listOfWorkstations = new List<IDataService>();
            
            if (dataReader.Read())
            {
                this.ID = int.Parse(dataReader[0].ToString());
                this.Type = int.Parse(dataReader[1].ToString());
                this.Status = int.Parse(dataReader[2].ToString());
                this.Barcode = dataReader[3].ToString();
                this.WorkstationLabel = dataReader[4].ToString();
                this.TrolleyID = Convert.IsDBNull(dataReader[5]) == true ? 0 : decimal.Parse(dataReader[5].ToString());
                this.IsInternational = dataReader[6].ToString();

                listOfWorkstations.Add(this);
            }
            return listOfWorkstations;

        }

        #endregion

    }

    public class WorkstationLookup:IDataService
    {
        private const string PACKSTATIONS = "oms_pack.f_packstations";

        public string value;
        public string text;
        public string areaid;

        public WorkstationLookup(string value, string text)
        {
            this.value = value;
            this.text = text;
        }

        public WorkstationLookup()
        {
        }

        #region "enum"
        public enum ClassMethods
        {
            ListPackstations
        }
        #endregion

        [MethodMapper("ListPackstations", WorkstationLookup.PACKSTATIONS)]
        public List<IDataService> ListPackstations(IDataReader dataReader)
        {
            List<IDataService> listOfPackstations = new List<IDataService>();
            
            while (dataReader.Read())
                listOfPackstations.Add( 
                    new WorkstationLookup{
                        value = dataReader[0].ToString(), 
                        text = dataReader[1].ToString()
                    }
                );

            return listOfPackstations;

        }

    }
}
