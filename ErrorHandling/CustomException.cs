using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IHF.EnterpriseLibrary.ErrorHandling
{
    public class CustomException:ApplicationException
    {
        private string _exceptionMessage = string.Empty;
        public string ExceptionMessage 
        {
            get { return this._exceptionMessage; }
            set { this._exceptionMessage = value;} 
        }
        
        public CustomException(Exception exception)
        {
            this._exceptionMessage = exception.Message;
        }
    }
}
