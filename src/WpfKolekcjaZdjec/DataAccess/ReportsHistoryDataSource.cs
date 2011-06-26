using System.Collections.Generic;
using System.Linq;
using WpfKolekcjaZdjec.Entities;

namespace WpfKolekcjaZdjec.DataAccess
{
    /// <summary>
    /// Data source for reports history.
    /// </summary>
    public class ReportsHistoryDataSource
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        private string _connectionString = string.Empty;

        /// <summary>
        /// Default contstructor.
        /// </summary>
        public ReportsHistoryDataSource()
        {
        }

        /// <summary>
        /// Parametrized connection string.
        /// </summary>
        /// <param name="actualConnectionString">Actual connection string.</param>
        public ReportsHistoryDataSource(string actualConnectionString)
        {
            _connectionString = actualConnectionString;
        }

        /// <summary>
        /// Adding new historical usage of report into database.
        /// </summary>
        /// <param name="newHistory">New report's history entity.</param>
        /// <returns>ID for created history.</returns>
        public int AddReportHistoricalEntry(ReportsHistory newHistory)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                newHistory.ID = context.ReportsHistories.NextId(p => p.ID);

                context.ReportsHistories.AddObject(newHistory);
                context.SaveChanges();

                return newHistory.ID;
            }
        }

        /// <summary>
        /// Get all history of reports.
        /// </summary>
        /// <returns>Reports history list.</returns>
        public List<ReportsHistory> GetAllReportsHistory()
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                return (from o in context.ReportsHistories select o).ToList();
            }
        }

        /// <summary>
        /// Deleting report history by id.
        /// </summary>
        /// <param name="id">Report's history ID for deletion.</param>
        /// <returns>Operation status (for future dependencies).</returns>
        public bool DeleteReportsHistory(int id)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                context.ReportsHistories.DeleteObject((from o in context.ReportsHistories where o.ID == id select o).First());
                context.SaveChanges();

                return true;
            }
        }

        /// <summary>
        /// Deleting report history by saved report id.
        /// </summary>
        /// <param name="savedReportId">Saved report's ID for deletion.</param>
        /// <returns>Operation status (for future dependencies).</returns>
        public bool DeleteReportsHistoryBySavedReportId(int savedReportId)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                foreach(var entry in (from o in context.ReportsHistories
                                      join report in context.SavedReports on savedReportId equals report.ID
                                      select o))
                {
                    context.ReportsHistories.DeleteObject(entry);
                }

                context.SaveChanges();

                return true;
            }
        }

        /// <summary>
        /// Get one report history.
        /// </summary>
        /// <param name="id">Report's history ID.</param>
        /// <returns>One report's history.</returns>
        public ReportsHistory GetReportHistory(int id)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                IEnumerable<ReportsHistory> report = from o in context.ReportsHistories where o.ID == id select o;

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

        /// <summary>
        /// Get one report history by saved report ID.
        /// </summary>
        /// <param name="savedReportId">Saved report ID.</param>
        /// <returns>One report's history.</returns>
        public ReportsHistory GetReportHistoryBySavedReportId(int savedReportId)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                IEnumerable<ReportsHistory> reportHistory = from o in context.ReportsHistories
                                                            join report in context.SavedReports on savedReportId equals report.ID
                                                            select o;

                if (reportHistory.Count() != 0)
                {
                    return reportHistory.First();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}