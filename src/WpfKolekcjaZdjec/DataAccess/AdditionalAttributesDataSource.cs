using System.Collections.Generic;
using System.Linq;
using WpfKolekcjaZdjec.Entities;

namespace WpfKolekcjaZdjec.DataAccess
{
    /// <summary>
    /// Data source for additional attributes.
    /// </summary>
    public class AdditionalAttributesDataSource
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        private string _connectionString = string.Empty;

        /// <summary>
        /// Default contstructor.
        /// </summary>
        public AdditionalAttributesDataSource()
        {
        }

        /// <summary>
        /// Parametrized connection string.
        /// </summary>
        /// <param name="actualConnectionString">Actual connection string.</param>
        public AdditionalAttributesDataSource(string actualConnectionString)
        {
            _connectionString = actualConnectionString;
        }

        /// <summary>
        /// Adding new attributes into database.
        /// </summary>
        /// <param name="newAttributes">New attribute entity.</param>
        /// <returns>ID for created attribute.</returns>
        public int AddAttribute(AdditionalAttribute newAttributes)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                newAttributes.ID = context.AdditionalAttributes.NextId(p => p.ID);

                context.AdditionalAttributes.AddObject(newAttributes);
                context.SaveChanges();

                return newAttributes.ID;
            }
        }

        /// <summary>
        /// Get all attributes.
        /// </summary>
        /// <returns>Attributes list.</returns>
        public List<AdditionalAttribute> GetAllAttributes()
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                return (from o in context.AdditionalAttributes select o).ToList();
            }
        }

        /// <summary>
        /// Deleting attribute by id.
        /// </summary>
        /// <param name="id">Attribute ID for deletion.</param>
        /// <returns>Operation status (for future dependencies).</returns>
        public bool DeleteAttribute(int id)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                context.AdditionalAttributes.DeleteObject((from o in context.AdditionalAttributes where o.ID == id select o).First());
                context.SaveChanges();

                return true;
            }
        }

        /// <summary>
        /// Get one AdditionalAttribute.
        /// </summary>
        /// <param name="id">Additional Attribute ID.</param>
        /// <returns>One AdditionalAttributes entity.</returns>
        public AdditionalAttribute GetAttribute(int id)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                IEnumerable<AdditionalAttribute> attribute = from o in context.AdditionalAttributes where o.ID == id select o;

                if (attribute.Count() != 0)
                {
                    return attribute.First();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Get one AdditionalAttribute by photo ID.
        /// </summary>
        /// <param name="photoId">Photo ID.</param>
        /// <returns>One AdditionalAttributes entity.</returns>
        public AdditionalAttribute GetAttributeByPhoto(int photoId)
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                IEnumerable<AdditionalAttribute> attribute = from o in context.AdditionalAttributes
                                                             join photo in context.Photos on photoId equals photo.ID
                                                             select o;

                if (attribute.Count() != 0)
                {
                    return attribute.First();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}