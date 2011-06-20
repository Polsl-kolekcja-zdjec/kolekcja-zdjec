using System.Collections.Generic;
using System.Linq;
using WpfKolekcjaZdjec.Entities;

namespace WpfKolekcjaZdjec.DataAccess
{
    /// <summary>
    /// Data source for tags.
    /// </summary>
    public class TagsDataSource
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        private string _connectionString = string.Empty;

        /// <summary>
        /// Default contstructor.
        /// </summary>
        public TagsDataSource()
        {
        }

        /// <summary>
        /// Parametrized connection string.
        /// </summary>
        /// <param name="actualConnectionString">Actual connection string.</param>
        public TagsDataSource(string actualConnectionString)
        {
            _connectionString = actualConnectionString;
        }

        /// <summary>
        /// Adding new tag into database.
        /// </summary>
        /// <param name="newTag">New tag entity.</param>
        /// <returns>ID for created tag.</returns>
        public int AddTag(Tag newTag)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                newTag.ID = context.Tags.NextId(p => p.ID);

                context.Tags.AddObject(newTag);
                context.SaveChanges();

                return newTag.ID;
            }
        }

        /// <summary>
        /// Gets all tags.
        /// </summary>
        /// <returns>All photos collection.</returns>
        public List<Tag> GetAllTags()
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                return (from o in context.Tags select o).ToList();
            }
        }

        /// <summary>
        /// Gets all tags for photo id.
        /// </summary>
        /// <param name="photoId">The photo id.</param>
        /// <returns>All assigned tags to the photo.</returns>
        public List<Tag> GetTagsForPhoto(int photoId)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                return (from o in context.Tags
                        join p in context.Photos on photoId equals p.ID
                        select o).ToList();
            }
        }

        /// <summary>
        /// Deleting tag by id.
        /// </summary>
        /// <param name="id">Tag ID for deletion.</param>
        /// <returns>Operation status (for future dependencies).</returns>
        public bool DeleteTag(int id)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                context.Tags.DeleteObject((from o in context.Tags where o.ID == id select o).First());
                context.SaveChanges();

                return true;
            }
        }

        /// <summary>
        /// Gets the tag.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Tag entity.</returns>
        public Tag GetTag(int id)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                IEnumerable<Tag> tag = from o in context.Tags where o.ID == id select o;

                if (tag.Count() != 0)
                {
                    return tag.First();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}