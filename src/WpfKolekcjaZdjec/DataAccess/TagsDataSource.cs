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

        public Tag GetTag(int id)
        {
            PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString);
            IEnumerable<Tag> t = from o in context.TagSet where o.Id == id select o;
            if (t.Count() != 0)
                return t.First();
            return null;
        }
    }
}
