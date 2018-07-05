// Name: LoggerDAO.cs
// Type: class file 
// Description: DAO class for logger logs
//
//$Revision:   1.1  $
//
// Version   Date        Author     Reason
//  1.0      18/05/18    A Petrescu Initial Revision
//  1.1      21/05/18    M Cacket   Added LogError method for writing errors to the logger log tables.
//                       S Remedios

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IHF.EnterpriseLibrary.Data;

namespace IHF.BusinessLayer.DataAccessObjects
{
    public class LoggerDAO
    {
        private readonly DataManager dataManager = new DataManager(Util.DBInstanceEnum.Ora);

        public void LogException(Exception exception, string username = null, string url = null)
        {
            if (exception == null)
            {
                return;
            }

            var exceptionTextBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(username))
            {
                exceptionTextBuilder.AppendFormat("Username: {0}; \n", username);
            }
            if (!string.IsNullOrWhiteSpace(url))
            {
                exceptionTextBuilder.AppendFormat("Url: {0}; \n", url);
            }

            PrintException(exception, exceptionTextBuilder);

            dataManager.ExecuteNonQuery("logger.log_error", new object[] {
                exception.Message,
                "ihf", //pl/sql logger will make it lowercase anyway
                exceptionTextBuilder.ToString()
            });
        }

        public void LogError(string errorMsg, string username = null, string url = null)
        {
            if (errorMsg == null)
            {
                return;
            }

            string errorText = String.Format("Error {0} \nUsername: {1} \nUrl: {2}\n", errorMsg, username, url);

            dataManager.ExecuteNonQuery("logger.log_error", new object[] {
                errorText,
                "ihf" //pl/sql logger will make it lowercase anyway
            });
        }



        private void PrintException(Exception e, StringBuilder textBuilder)
        {
            textBuilder.AppendFormat("{0}: {1}\n", e.GetType().ToString(), e.Message);
            textBuilder.Append(e.StackTrace);
            textBuilder.Append("\n");

            if (e.InnerException != null)
            {
                textBuilder.Append("Inner exception:\n");
                PrintException(e.InnerException, textBuilder);
            }
        }

    }
}
