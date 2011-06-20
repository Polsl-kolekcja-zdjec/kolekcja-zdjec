using System.Collections.Generic;
using System.Linq;
using WpfKolekcjaZdjec.Entities;

namespace WpfKolekcjaZdjec.DataAccess
{
    /// <summary>
    /// Data source for archives.
    /// </summary>
    public class ArchivesDataSource
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
        /// Gets all locations for photo id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns>All assigned locations to the photo.</returns>
        public List<Archive> GetLocationsForPhoto(int photoId)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                return (from o in context.Archives
                        join p in context.Photos on photoId equals p.ID
                        select o).ToList();
            }
        }

        /// <summary>
        /// Adding new archive into database.
        /// </summary>
        /// <param name="newArchive">New archive entity.</param>
        /// <returns>ID for created archive.</returns>
        public int AddArchive(Archive newArchive)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                newArchive.ID = context.Tags.NextId(p => p.ID);

                context.Archives.AddObject(newArchive);
                context.SaveChanges();

                return newArchive.ID;
            }
        }

        /// <summary>
        /// Get all archives.
        /// </summary>
        /// <returns>Archives list.</returns>
        public List<Archive> GetAllArchives()
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                return (from o in context.Archives select o).ToList();
            }
        }

        /// <summary>
        /// Deleting archive by id.
        /// </summary>
        /// <param name="id">Archive ID for deletion.</param>
        /// <returns>Operation status (for future dependencies).</returns>
        public bool DeleteArchive(int id)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                context.Archives.DeleteObject((from o in context.Archives where o.ID == id select o).First());
                context.SaveChanges();

                return true;
            }
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