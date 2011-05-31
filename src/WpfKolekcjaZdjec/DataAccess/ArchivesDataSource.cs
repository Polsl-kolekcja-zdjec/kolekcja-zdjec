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
            PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString);
            IEnumerable<Archive> t = from o in context.ArchiveSet where o.Id == id select o;
            if (t.Count() != 0)
                return t.First();
            return null;
        }
    }
}