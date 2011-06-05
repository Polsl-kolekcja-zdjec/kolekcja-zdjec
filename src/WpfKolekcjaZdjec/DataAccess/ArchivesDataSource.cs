using System.Collections.Generic;
using System.Linq;
using WpfKolekcjaZdjec.Entities;

namespace WpfKolekcjaZdjec.DataAccess
{
    /// <summary>
    /// Data source for archives.
    /// </summary>
    public partial class ArchivesDataSource
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        private string _connectionString = string.Empty;

        /// <summary>
        /// Default contstructor.
        /// </summary>
        public ArchivesDataSource()
        {
        }

        /// <summary>
        /// Parametrized connection string.
        /// </summary>
        /// <param name="actualConnectionString">Actual connection string.</param>
        public ArchivesDataSource(string actualConnectionString)
        {
            _connectionString = actualConnectionString;
        }

        /// <summary>
        /// Get one archive.
        /// </summary>
        /// <param name="id">Archive ID.</param>
        /// <returns>One Archive.</returns>
        public Archive GetArchive(int id)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                IEnumerable<Archive> archive = from o in context.Archives where o.ID == id select o;

                if (archive.Count() != 0)
                {
                    return archive.First();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}