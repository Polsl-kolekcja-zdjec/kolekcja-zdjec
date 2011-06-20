using System;
using System.Configuration;

namespace WpfKolekcjaZdjec.DataAccess
{
    /// <summary>
    /// Static helper for connection strings.
    /// </summary>
    public static class ConnectionStringHelper
    {
        /// <summary>
        /// Get actual connection string.
        /// </summary>
        /// <returns>Connection string value.</returns>
        public static string GetActualConnectionString()
        {
            return ConfigurationManager.AppSettings["PhotoCollectionDatabaseEntities"].ToString();
        }
    }
}