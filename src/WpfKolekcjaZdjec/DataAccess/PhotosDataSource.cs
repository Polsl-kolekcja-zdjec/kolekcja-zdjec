using System.Collections.Generic;
using System.Linq;
using WpfKolekcjaZdjec.Entities;

namespace WpfKolekcjaZdjec.DataAccess
{
    /// <summary>
    /// Data source for photos.
    /// </summary>
    public class PhotosDataSource
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        private string _connectionString = string.Empty;

        /// <summary>
        /// Default contstructor.
        /// </summary>
        public PhotosDataSource()
        {
        }

        /// <summary>
        /// Parametrized connection string.
        /// </summary>
        /// <param name="actualConnectionString">Actual connection string.</param>
        public PhotosDataSource(string actualConnectionString)
        {
            _connectionString = actualConnectionString;
        }

        public IEnumerable<Photo> GetAllPhotos()
        {
            PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString);
            return from o in context.PhotoSet select o;
        }

        public Photo GetLastPhoto() 
        {
            PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString);
            IEnumerable<Photo> query = from o in context.PhotoSet orderby o.Id ascending select o;
            if (query.Count() == 0)
                return null;
            return query.Last();
        }

        /// <summary>
        /// Get one photo.
        /// </summary>
        /// <param name="id">Photo ID.</param>
        /// <returns>One photo.</returns>
        public Photo GetPhoto(int id)
        {
            PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString);

            return (from o in context.PhotoSet where o.Id == id select o).First();
        }

        /// <summary>
        /// Adding new photo into database.
        /// </summary>
        /// <param name="newPhoto">New photo entity.</param>
        /// <returns>ID for created photo.</returns>
        public int AddPhoto(Photo newPhoto)
        {
            PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString);

            context.PhotoSet.AddObject(newPhoto);
            context.SaveChanges();

            return newPhoto.Id;
        }

        /// <summary>
        /// Adding photos by group.
        /// </summary>
        /// <param name="photos">List of photos.</param>
        /// <returns>Row count for added group.</returns>
        public int AddPhotosGroup(List<Photo> photos)
        {
            PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString);

            foreach (Photo each in photos)
            {
                context.PhotoSet.AddObject(each);
            }

            return context.SaveChanges();
        }

        /// <summary>
        /// Deleting photos by id.
        /// </summary>
        /// <param name="id">Photo ID for deletion.</param>
        /// <returns>Operation status (for future dependencies).</returns>
        public bool DeletePhoto(int id)
        {
            PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString);

            context.PhotoSet.DeleteObject((from o in context.PhotoSet where o.Id == id select o).First());
            context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Deleting photos by list of ids.
        /// </summary>
        /// <param name="ids">List of ids.</param>
        /// <returns>Row count number.</returns>
        public int DeletePhotos(List<int> ids)
        {
            PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString);

            foreach (int id in ids)
            {
                context.PhotoSet.DeleteObject((from o in context.PhotoSet where o.Id == id select o).First());
            }

            return context.SaveChanges();
        }
    }
}