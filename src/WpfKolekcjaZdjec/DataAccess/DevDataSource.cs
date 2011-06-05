using System.Collections.Generic;
using System.Linq;
using WpfKolekcjaZdjec.Business;
using WpfKolekcjaZdjec.Entities;

namespace WpfKolekcjaZdjec.DataAccess
{
    /// <summary>
    /// Testing data source (only for development).
    /// </summary>
    public class DevDataSource
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        private string _connectionString = string.Empty;

        /// <summary>
        /// Default contstructor.
        /// </summary>
        public DevDataSource()
        {
        }

        /// <summary>
        /// Parametrized connection string.
        /// </summary>
        /// <param name="actualConnectionString">Actual connection string.</param>
        public DevDataSource(string actualConnectionString)
        {
            _connectionString = actualConnectionString;
        }

        /// <summary>
        /// Adds the objects required by photo.
        /// </summary>
        public void AddObjectsRequiredByPhoto()
        {
            AddTemplateTag();
            AddTemplateArchive();
        }

        /// <summary>
        /// Adds the template tag.
        /// </summary>
        /// <returns>Sample tag ID.</returns>
        public int AddTemplateTag()
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                Tag tag = new Tag();

                tag.ID = context.Tags.NextId(t => t.ID);
                tag.IconPath = "none";
                tag.CreationDate = System.Convert.ToDateTime(System.DateTime.Now);
                tag.Name = "TemporaryTag";
                tag.ParentID = null;

                context.Tags.AddObject(tag);
                context.SaveChanges();

                return tag.ID;
            }
        }

        /// <summary>
        /// Adds the template archive.
        /// </summary>
        /// <returns>Sample archive ID.</returns>
        public int AddTemplateArchive()
        {
            using (PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString))
            {
                Archive archive = new Archive();

                archive.ID = context.Archives.NextId(a => a.ID);
                archive.Capacity = 1048567;
                archive.IsExternal = false;
                archive.DeviceID = "007";
                archive.HddLetter = "C";

                context.Archives.AddObject(archive);
                context.SaveChanges();

                return archive.ID;
            }
        }
    }
}