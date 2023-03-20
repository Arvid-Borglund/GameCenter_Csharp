using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCenter.DAL.DALErrorHandling
{ //Written partly with AI assistance
    internal class DAL_ExceptionHandler
    {
        private static string errorMessageStatic = "";
        // create methods for handling sql connection exceptions
        public static void HandleSqlException(Exception ex, string customErrorMessage = null)
        {
            // Find the innermost exception
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            // if the exception is a sql exception
            if (ex is System.Data.SqlClient.SqlException)
            {
                // Check the number
                switch (((System.Data.SqlClient.SqlException)ex).Number)
                {
                    // Cannot open database requested by the login. The login failed.
                    case 4060:
                        errorMessageStatic = customErrorMessage ?? "Cannot open database requested by the login. The login failed.";
                        break;
                    // Cannot open database "%.*ls" requested by the login. The login failed.
                    case 18456:
                        errorMessageStatic = customErrorMessage ?? "Cannot open database requested by the login. The login failed.";
                        break;
                    // A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections. (provider: TCP Provider, error: 0 - The specified network name is no longer available.)                 
                    case 53:
                        errorMessageStatic = customErrorMessage ?? "A network-related or instance-specific error occurred while establishing a connection to SQL Server.";
                        break;
                    // A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections. (provider: TCP Provider, error: 0 - An existing connection was forcibly closed by the remote host.)
                    case 10054:
                        errorMessageStatic = customErrorMessage ?? "A network-related or instance-specific error occurred while establishing a connection to SQL Server.";
                        break;
                    default:
                        errorMessageStatic = customErrorMessage ?? ex.Message;
                        break;
                }
            }
            else
            {
                errorMessageStatic = customErrorMessage ?? ex.Message;
            }
        }
        public string GetErrorMessage()
        {
            // Retrieve the error message from the static variable
            string errorMessage = errorMessageStatic;

            // Clear the error message from the static variable
            errorMessageStatic = "";

            return errorMessage;
        }
    }
}
