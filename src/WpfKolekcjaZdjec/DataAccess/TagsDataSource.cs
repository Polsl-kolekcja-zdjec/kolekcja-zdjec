using System.Collections.Generic;
using System.Linq;
using WpfKolekcjaZdjec.Entities;

namespace WpfKolekcjaZdjec.DataAccess
{
    /// <summary>
    /// Data source for tags.
    /// </summary>
    public partial class TagsDataSource
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