// Name: DataManager.cs
// Type: class file 
// Description: class file for data access
//
//$Revision:   1.26  $
//
// Version   Date        Author     Reason
//  1.0      22/02/11    ITMK       Released version
//  1.1      22/02/11    IT MK      No Change
//  1.2      22/02/11    IT MK      No Change
//  1.3      22/02/11    IT MK      No Change
//  1.4      22/02/11    IT MK      First check in for IHF Automation application
//                                  Includes
//                                  1. application, business and database layers
//                                  2. Implementation of enterprise library
//                                  3. exception handling
//  1.5      22/02/11    IT MK      No Change
//  1.6      15/03/11    IT MK      updated the solution
//  1.7      16/03/11    ITMK       Added checkboolean and getvalue functions
//  1.8      21/07/11    MSalman    Temp func added for oracle blob handling
//  1.9      17/03/11    ITAJ1      No change.
//  1.10     18/03/11    ITAJ1      re checking sln file
//  1.11     18/03/11    IT MK      added provider in security application and corresponding DAOs
//  1.12     25/03/11    IT MK      No Change
//  1.13     25/03/11    IT MK      No Change
//  1.14     08/04/11    ITAJ1      No Change
//  1.15     17/05/11    ITAJ1      ref cursor for procedure
//  1.16     10/06/11    IT MK      oracle data access related test
//  1.17     04/07/11    ITAJ1      added handling for decimal output from procedure
//  1.18     05/07/11    M Khan     Added a function for procedures with ref_cursors as 1st output parameter
//  1.19     21/07/11    ITMS1      Temp function added for oracle blob handling
//  1.20     18/08/11    IT MK      Despatch service created
//  1.21     18/08/11    IT MK      Despatch - Interim code
//  1.22     23/08/11    IT MK      Despatch - Interim version
//  1.23     05/12/11    IT MK      No change.
//  1.24     29/05/13    J Watt     Stop exceptions writing to the windows events
//  1.25     03/01/17    M Cackett  Added function to handle pipelined Oracle functions
//                                  Called from SELECT statement(s).
//  1.26     10/01/17    M Cackett  QA changes - fill in the blanks in the banner.
//  1.27     10/02/17    M Cackett  Set fetch size to make sure pipeReader reads sensible sized blocks for performance
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IHF.EnterpriseLibrary.DataRepository;
using IHF.EnterpriseLibrary.Data;
using IHF.EnterpriseLibrary.DataServices;
using System.Reflection;

using Oracle.DataAccess.Client;
using System.Data;
using IHF.EnterpriseLibrary.ErrorHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;


namespace IHF.EnterpriseLibrary.Data
{
    public class DataManager
    {
        private DataAccess _dataAccess;

        private const char TRUE_CONSTANT = 'T';

        public DataManager(Enum connectionString)
        {
            _dataAccess = new DataAccess(connectionString.ToString()); 
        }

        #region "Private methods"

        private IDataReader SelectReader(string databaseMethod, object[] parameters)
        {
            try
            {
                OracleCommand command = new OracleCommand(databaseMethod);
                command.CommandType = CommandType.StoredProcedure;
                OracleParameter outputParameter = new OracleParameter();

                outputParameter.Direction = ParameterDirection.ReturnValue;
                outputParameter.OracleDbType = OracleDbType.RefCursor;
                command.Parameters.Add(outputParameter);

                if (parameters != null)
                {
                    foreach (object parameter in parameters)
                    {
                        command.Parameters.Add("parameter", parameter);
                    }
                }
                return this._dataAccess.Database.ExecuteReader(command);
            }
            catch (Exception exception)
            {
                //ExceptionPolicy.HandleException(exception, "IHF Application Exception");
                //throw new CustomException(exception);
                throw exception;
            }
            //return this._dataAccess.Database.ExecuteReader(command);
        }

        // the return value of the function is dataset
        private DataSet SelectDataSet(string databaseMethod, object[] parameters)
        {
            try
            {
                OracleCommand command = new OracleCommand(databaseMethod);
                command.CommandType = CommandType.StoredProcedure;
                OracleParameter outputParameter = new OracleParameter();

                outputParameter.Direction = ParameterDirection.ReturnValue;
                outputParameter.OracleDbType = OracleDbType.RefCursor;
                command.Parameters.Add(outputParameter);

                if (parameters != null)
                {
                    foreach (object parameter in parameters)
                    {
                        command.Parameters.Add("parameter", parameter);
                    }
                }
            
                return this._dataAccess.Database.ExecuteDataSet(command);
            }
            catch (Exception exception)
            {
                ExceptionPolicy.HandleException(exception, "IHF Application Exception");
                //throw new CustomException(exception);
                throw exception;
            }
        }

        // the output parameter of the procedure is dataset
        public DataSet SelectDataSetProcedure(string databaseMethod, object[] parameters)
        {
            try
            {
                OracleCommand command = new OracleCommand(databaseMethod);
                command.CommandType = CommandType.StoredProcedure;
                OracleParameter outputParameter = new OracleParameter();

                outputParameter.Direction = ParameterDirection.Output;
                outputParameter.OracleDbType = OracleDbType.RefCursor;
                command.Parameters.Add(outputParameter);

                if (parameters != null)
                {
                    foreach (object parameter in parameters)
                    {
                        command.Parameters.Add("parameter", parameter);
                    }
                }

                return this._dataAccess.Database.ExecuteDataSet(command);
            }
            catch (Exception exception)
            {
                //ExceptionPolicy.HandleException(exception, "IHF Application Exception");
                //throw new CustomException(exception);
                throw exception;
            }
        }

        private List<IDataService> Select(string databaseMethod, object[] parameters, MethodInfo method, IDataService classObject)
        {
            try
            {
                IDataReader dataReader = this.SelectReader(
                                                            databaseMethod,
                                                            parameters);

                object[] methodParameters = new Object[] { dataReader };

                List<IDataService> resultSet = (List<IDataService>)method.Invoke(classObject, methodParameters);

                return resultSet;
            }
            catch (Exception exception)
            {
                //ExceptionPolicy.HandleException(exception, "IHF Application Exception");
                //throw new CustomException(exception);
                throw exception;
            }
        }

        private string GetDatabaseMethod(MethodInfo method, string methodName)
        {
            try
            {
                MethodMapperAttribute[] attribute = (MethodMapperAttribute[])method.GetCustomAttributes(typeof(MethodMapperAttribute), false);


                if (attribute[0].ClassMethod == methodName)
                {
                    return attribute[0].DatabaseMethod;
                }


                return string.Empty;
            }
            catch (Exception exception)
            {
                //ExceptionPolicy.HandleException(exception, "IHF Application Exception");
                //throw new CustomException(exception);
                throw exception;
            }
        }
       
        #endregion

        #region "Methods for working with class objects"

        public List<IDataService> Get(string methodName, IDataService classObject)
        {
            //call Execute overload passing null for parameters
            return Get(methodName, 
                       classObject, 
                       null);
        }

        public List<IDataService> Get(string methodName, IDataService classObject, object[] parameters)
        {
            try
            {
                IEnumerable<MethodInfo> methods = classObject
                                                    .GetType()
                                                    .GetMethods()
                                                    .Where(m => m.GetCustomAttributes(
                                                                                    typeof(MethodMapperAttribute),
                                                                                    false)
                                                                    .Length > 0);

                string databaseMethod = string.Empty;

                foreach (MethodInfo method in methods)
                {
                    databaseMethod = GetDatabaseMethod(method, methodName);

                    if (databaseMethod != string.Empty)
                    {
                        return Select(
                                      databaseMethod,
                                      parameters,
                                      method,
                                      classObject);
                    }

                }
                //in case method not found
                throw new Exception("Method with attribute MethodMapperAttribute not found");

            }
            catch (Exception exception)
            {
                //ExceptionPolicy.HandleException(exception, "IHF Application Exception");
                //throw new CustomException(exception);
                throw exception;

            }
        }

        
        
        #endregion

        #region "Methods to execute SELECT(general) and INSERT, UPDATE, DELETE"

        public DataSet ExecuteDataset(string databaseMethod, object[] parameters)
        {
            return this.SelectDataSet(
                                databaseMethod, 
                                parameters);
        }

        public IDataReader ExecuteReader(string databaseMethod, object[] parameters)
        {
            return this.SelectReader(
                                databaseMethod,
                                parameters);
        }

        public decimal ExecuteReturnMethod(string databaseMethod, object[] parameters)
        {
            try
            {
                //decimal returnResult = 0;
                OracleCommand command = new OracleCommand(databaseMethod);
                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (object parameter in parameters)
                    {
                        if (command.Parameters.Count == 0)
                        {
                            OracleParameter inoutParameter = new OracleParameter();

                            inoutParameter.Direction = ParameterDirection.InputOutput;
                            inoutParameter.OracleDbType = OracleDbType.Decimal;
                            inoutParameter.Value = parameter;
                            command.Parameters.Add(inoutParameter);
                            continue;
                        }
                        command.Parameters.Add("parameter", parameter);
                    }
                }

                this._dataAccess.Database.ExecuteNonQuery(command);
                return decimal.Parse(command.Parameters[0].Value.ToString());
            }
            catch (Exception exception)
            {
                //ExceptionPolicy.HandleException(exception, "IHF Application Exception");
                //throw new CustomException(exception);
                throw exception;
            }

            //return returnResult;
        }


        public decimal ExecuteReturnMethodDecimal(string databaseMethod, object[] parameters)
        {
            try
            {
                //decimal returnResult = 0;
                OracleCommand command = new OracleCommand(databaseMethod);
                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (object parameter in parameters)
                    {
                        if (command.Parameters.Count == 0)
                        {
                            OracleParameter inoutParameter = new OracleParameter();

                            inoutParameter.Direction = ParameterDirection.Output;
                            inoutParameter.OracleDbType = OracleDbType.Decimal;
                            inoutParameter.Value = parameter;
                            command.Parameters.Add(inoutParameter);
                            continue;
                        }
                        command.Parameters.Add("parameter", parameter);
                    }
                }

                this._dataAccess.Database.ExecuteNonQuery(command);
                return decimal.Parse(command.Parameters[0].Value.ToString());
            }
            catch (Exception exception)
            {
                ExceptionPolicy.HandleException(exception, "IHF Application Exception");
                //throw new CustomException(exception);
                throw exception;
            }

            //return returnResult;
        }

        public void ExecuteNonQuery(string databaseMethod, object[] parameters)
        {
            try
            {
                OracleCommand command = new OracleCommand(databaseMethod);
                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (object parameter in parameters)
                    {
                        command.Parameters.Add("parameter", parameter);
                    }
                }

                this._dataAccess.Database.ExecuteNonQuery(command);
            }
            catch (Exception exception)
            {
                //ExceptionPolicy.HandleException(exception, "IHF Application Exception");
                //throw new CustomException(exception);
                throw exception;
            }

        }


        public void ExecuteSQL(OracleCommand cmd)
        {
            try
            {
                
                this._dataAccess.Database.ExecuteNonQuery(cmd);
                
            }
            catch (Exception exception)
            {
                //ExceptionPolicy.HandleException(exception, "IHF Application Exception");
                //throw new CustomException(exception);
                throw exception;
            }

            //return returnResult;
        }


        public IDataReader pipeReader(string databaseMethod)
        {
            try
            {
                OracleCommand command = new OracleCommand(databaseMethod);
                const long rowSize = 64;
                const long numRows = 50;
                command.FetchSize = rowSize * numRows;  // Process 50 labels per Read().
                IDataReader pipelinedReader = this._dataAccess.Database.ExecuteReader(command);
                return pipelinedReader;
            }
            catch (Exception exception)
            {
                //throw new CustomException(exception);
                throw exception;
            }
        }


        #endregion

        #region "Methods to return a boolean or a string value"

        public bool CheckBooleanValue(string databaseMethod, object[] parameters)
        {
            OracleCommand command = new OracleCommand(databaseMethod);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter outputParam = new OracleParameter();
            outputParam.Direction = ParameterDirection.ReturnValue;
            outputParam.Size = 1;
            outputParam.OracleDbType = Oracle.DataAccess.Client.OracleDbType.Varchar2;

            command.Parameters.Add(outputParam);

            if (parameters != null)
            {
                foreach (object parameter in parameters)
                {
                    command.Parameters.Add("ParamName", parameter);
                }
            }
            try
            {
                _dataAccess.Database.ExecuteNonQuery(command);

                return (char.Parse(command.Parameters[0].Value.ToString()) == TRUE_CONSTANT);
            }
            catch (Exception exception)
            {
                //ExceptionPolicy.HandleException(exception, "IHF Application Exception");
                throw exception;
            }
        }

        public string GetValue(string databaseMethod, object[] parameters)
        {
            OracleCommand command = new OracleCommand(databaseMethod);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter outputParam = new OracleParameter();
            outputParam.Direction = ParameterDirection.ReturnValue;
            outputParam.Size = 3648946;
            outputParam.OracleDbType = Oracle.DataAccess.Client.OracleDbType.Varchar2;

            command.Parameters.Add(outputParam);

            if (parameters != null)
                foreach (object parameter in parameters)
                    command.Parameters.Add("ParamName", parameter);
            try
            {
                _dataAccess.Database.ExecuteNonQuery(command);

                return command.Parameters[0].Value.ToString();
            }
            catch (Exception exception)
            {
                //ExceptionPolicy.HandleException(exception, "IHF Application Exception");
                throw exception;
            }
        }

        public decimal GetValuedecimal(string databaseMethod, object[] parameters)
        {
            OracleCommand command = new OracleCommand(databaseMethod);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter outputParam = new OracleParameter();
            outputParam.Direction = ParameterDirection.ReturnValue;
            outputParam.Size = 3648946;
            outputParam.OracleDbType = Oracle.DataAccess.Client.OracleDbType.Decimal; ;

            command.Parameters.Add(outputParam);

            if (parameters != null)
                foreach (object parameter in parameters)
                    command.Parameters.Add("ParamName", parameter);
            try
            {
                _dataAccess.Database.ExecuteNonQuery(command);

                return decimal.Parse(command.Parameters[0].Value.ToString());
            }
            catch (Exception exception)
            {
               // ExceptionPolicy.HandleException(exception, "IHF Application Exception");
                throw exception;
            }
        }

        public string GetStringforProcedure(string databaseMethod, object[] parameters)
        {
            OracleCommand command = new OracleCommand(databaseMethod);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter outputParam = new OracleParameter();
            outputParam.Direction = ParameterDirection.Output;
            outputParam.Size = 3648946;
            outputParam.OracleDbType = Oracle.DataAccess.Client.OracleDbType.Varchar2;

            command.Parameters.Add(outputParam);

            if (parameters != null)
                foreach (object parameter in parameters)
                    command.Parameters.Add("ParamName", parameter);
            try
            {
                _dataAccess.Database.ExecuteNonQuery(command);

                return command.Parameters[0].Value.ToString();
            }
            catch (Exception exception)
            {
                //ExceptionPolicy.HandleException(exception, "IHF Application Exception");
                throw exception;
            }
        }

        public IDataReader GetListFromProcedure(string databaseMethod, object[] parameters)
        {
            OracleCommand command = new OracleCommand(databaseMethod);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter outputParam = new OracleParameter();
            outputParam.Direction = ParameterDirection.Output;
            outputParam.Size = 3648946;
            outputParam.OracleDbType = Oracle.DataAccess.Client.OracleDbType.RefCursor;

            command.Parameters.Add(outputParam);

            if (parameters != null)
                foreach (object parameter in parameters)
                    command.Parameters.Add("ParamName", parameter);
            try
            {
                return _dataAccess.Database.ExecuteReader(command);

                //return (IDataReader)command.Parameters[0].Value;
            }
            catch (Exception exception)
            {
                //ExceptionPolicy.HandleException(exception, "IHF Application Exception");
                throw exception;
            }
        }

        #endregion

    }

    
}
