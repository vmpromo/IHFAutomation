using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.DataServices;

namespace IHF.BusinessLayer.BusinessClasses
{
    class SNumCode:IDataService
    {
        #region "private variables"
        private string _codeType;
        private int _code;
        private string _charShortTranslation;
        private string _charTranslation;
        #endregion

        #region "properties"
        public string Codetype 
        {
            get { return _codeType; }
            set { _codeType = value; } 
        }

        public int Code 
        {
            get { return _code; }
            set { _code = value; } 
        }

        public string ShortDescrption 
        {
            get { return _charShortTranslation; }
            set { _charShortTranslation = value; } 
        }

        public string LongDescription 
        {
            get { return _charTranslation; }
            set { _charTranslation = value; }
        }
        #endregion

    }
}
