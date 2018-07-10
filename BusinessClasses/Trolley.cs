using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.DataServices;

namespace IHF.BusinessLayer.BusinessClasses
{
    public class Trolley : IDataService
    {
        private decimal _id;
        private decimal _classtype;
        private decimal _statuscd;
        private decimal _seq;
        private DateTime _createdOn;
        private string _barcode;
        private string _trolleyLabel;
        private string _createdBy;
        private decimal _trolleytype;
        private string _deleteInd;
        private DateTime _lastChangedOn;
        private string _lastchangedBy;
        
        
        public decimal ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public decimal Classtype
        {
            get { return _classtype; }
            set { _classtype = value; }
        }

        public decimal Status
        {
            get { return _statuscd; }
            set { _statuscd = value; }
        }

        public DateTime CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }

        public string Barcode
        {
            get { return _barcode; }
            set { _barcode = value; }
        }

        public string TrolleyLabel
        {
            get { return _trolleyLabel; }
            set { _trolleyLabel = value; }
        }

        public string CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        public DateTime LastChangedOn
        {
            get { return _lastChangedOn; }
            set { _lastChangedOn = value; }
        }

        public string LastChangedBy
        {
            get { return _lastchangedBy; }
            set { _lastchangedBy = value; }
        }

        public decimal Type
        {
            get { return _trolleytype; }
            set { _trolleytype = value; }
        }


        public string IsInternational
        {
            get { return _deleteInd; }
            set { _deleteInd = value;}
        }

        //public abstract decimal Save();
    }
}
