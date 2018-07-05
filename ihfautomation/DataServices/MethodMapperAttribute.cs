using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace IHF.EnterpriseLibrary.DataServices
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MethodMapperAttribute : Attribute
    {
        private string _classMethod;
        private string _databaseMethod;

        public string ClassMethod 
        {
            get { return this._classMethod; }
            set { this._classMethod = value; } 
        }

        public string DatabaseMethod 
        {
            get { return _databaseMethod; }
            set { _databaseMethod = value;} 
        }

        //constructor
        public MethodMapperAttribute(string classMethod, string databaseMethod)
        {
            this._classMethod = classMethod;
            this._databaseMethod = databaseMethod;
        }

        
    }
}
