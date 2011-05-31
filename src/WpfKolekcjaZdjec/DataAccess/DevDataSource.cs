using System.Collections.Generic;
using System.Linq;
using WpfKolekcjaZdjec.Entities;

namespace WpfKolekcjaZdjec.DataAccess
{
    public partial class DevDataSource
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

        public void AddObjectsRequiredByPhoto() 
        {
            int tagId = AddTemplateTag();
            int archiveId = AddTemplateArchive();
        }

        public int AddTemplateTag()
        {
            PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString);
            Tag t = new Tag();
            t.Id = 0;
            t.IconPath = "none";
            t.CreationDate = System.Convert.ToDateTime(System.DateTime.Now);
            t.Name = "TemporaryTag";
            t.Tag_ParentTag_Hierarchy = null;
            context.TagSet.AddObject(t);
            context.SaveChanges();
            return t.Id;
        }

        public int AddTemplateArchive()
        {
            PhotoCollectionDatabaseEntities context = new PhotoCollectionDatabaseEntities(_connectionString);
            HddDirectory a = new HddDirectory();
            a.Id = 0;
            a.VolumeName = string.Empty;
            a.Capacity = 1048567;
            a.IsExternal = false;
            a.DeviceId = "007";
            a.HddLetter = "C";
            context.ArchiveSet.AddObject(a);
            context.SaveChanges();
            return a.Id;
        }
    }
}
