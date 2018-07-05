using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IHF.EnterpriseLibrary.ErrorHandling
{
    public class ConfigurationFileException:ApplicationException{
        private string _exceptionMessage = string.Empty;

        public ConfigurationFileException(string message) {
            _exceptionMessage = message;
        }
        public string ExceptionMessage{
            get { return this._exceptionMessage; }
            set { this._exceptionMessage = value; }
        }
    }
}
