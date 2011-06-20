using System.Collections.Generic;
using System.Linq;
using WpfKolekcjaZdjec.Entities;

namespace WpfKolekcjaZdjec.DataAccess
{
    /// <summary>
    /// Data source for reports.
    /// </summary>
    public class ReportsDataSource
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        private string _connectionString = string.Empty;

        /// <summary>
        /// Default contstructor.
        /// </summary>
        public ReportsDataSource()
        {
        }

        /// <summary>
        /// Parametrized connection string.
        /// </summary>
        /// <param name="actualConnectionString">Actual connection string.</param>
        public ReportsDataSource(string actualConnectionString)
        {
            _connectionString = actualConnectionString;
        }

        /// <summary>
        /// Adding new report into database.
        /// </summary>
        /// <param name="newReport">New report entity.</param>
        /// <returns>ID for created report.</returns>
        public int AddReport(SavedReport newReport)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                newReport.ID = context.Tags.NextId(p => p.ID);

                context.SavedReports.AddObject(newReport);
                context.SaveChanges();

                return newReport.ID;
            }
        }

        /// <summary>
        /// Get all reports.
        /// </summary>
        /// <returns>Reports list.</returns>
        public List<SavedReport> GetAllReports()
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                return (from o in context.SavedReports select o).ToList();
            }
        }

        /// <summary>
        /// Deleting report by id.
        /// </summary>
        /// <param name="id">Report ID for deletion.</param>
        /// <returns>Operation status (for future dependencies).</returns>
        public bool DeleteReport(int id)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                context.SavedReports.DeleteObject((from o in context.SavedReports where o.ID == id select o).First());
                context.SaveChanges();

                return true;
            }
        }

        /// <summary>
        /// Get one saved report.
        /// </summary>
        /// <param name="id">Report ID.</param>
        /// <returns>One report.</returns>
        public SavedReport GetReport(int id)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                IEnumerable<SavedReport> report = from o in context.SavedReports where o.ID == id select o;

                if (report.Count() != 0)
                {
                    return report.First();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}