using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfKolekcjaZdjec.Entities;

namespace WpfKolekcjaZdjec.DataAccess
{
    class ExifAttributesDataSource
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        private string _connectionString = string.Empty;

        /// <summary>
        /// Default contstructor.
        /// </summary>
        public ExifAttributesDataSource() { }

        /// <summary>
        /// Parametrized connection string.
        /// </summary>
        /// <param name="actualConnectionString">Actual connection string.</param>
        public ExifAttributesDataSource(string actualConnectionString) {
            _connectionString = actualConnectionString;
        }

        /// <summary>
        /// Adding new exif into database.
        /// </summary>
        /// <param name="newExif">New exif entity.</param>
        /// <returns>ID for created exif.</returns>
        public int AddExif(ExifAttribute newExif)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {

               // newExif.ID = context.ExifAttributes //jeszcze nie ogarnelam jak zrobic NextId(p => p.ID);

               context.ExifAttributes.AddObject(newExif);
 
                context.SaveChanges();

                return newExif.ID;
            }
        }

        /// <summary>
        /// Get one exif.
        /// </summary>
        /// <param name="id">Exif ID.</param>
        /// <returns>One exif.</returns>
        public ExifAttribute GetExif(int id)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                return (from o in context.ExifAttributes where o.ID == id select o).First();
            }
        }

        /// <summary>
        /// Gets all exif.
        /// </summary>
        /// <returns>All exif collection.</returns>
        public List<ExifAttribute> GetAllExif()
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                return (from o in context.ExifAttributes select o).ToList();
            }
        }

        /// <summary>
        /// Deleting exif by id.
        /// </summary>
        /// <param name="id">Exif ID for deletion.</param>
        /// <returns>Operation status (for future dependencies).</returns>
        public bool DeleteExif(int id)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                context.ExifAttributes.DeleteObject((from o in context.ExifAttributes where o.ID == id select o).First());
                context.SaveChanges();

                return true;
            }
        }
    }
}
