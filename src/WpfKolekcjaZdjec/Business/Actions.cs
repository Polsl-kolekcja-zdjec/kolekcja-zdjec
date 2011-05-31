using System.Configuration;
using System.Linq;
using WpfKolekcjaZdjec.Entities;

namespace WpfKolekcjaZdjec.Business
{
    public class Actions
    {
        public static void StartupTests()
        {
            ThumbnailDirectoryExist();
        }

        public static void AddPhoto()
        {
            int thumbnailHeight = int.Parse(ConfigurationManager.AppSettings["thumbnailMaxHeight"].ToString());
            int thumbnailWidth = int.Parse(ConfigurationManager.AppSettings["thumbnailMaxWidth"].ToString());
            string thumbnailPath = ConfigurationManager.AppSettings["thumbnailDirectory"].ToString();
            string connectionString = Business.ConnectionStringHelper.GetActualConnectionString();

            Microsoft.Win32.OpenFileDialog openImage = new Microsoft.Win32.OpenFileDialog();
            openImage.Filter = "Pliki obrazów (*.jpg, *.png, *.crt, *.tiff)|*.jpg;*.JPG;*.jpeg;*.JPEG;*.png;*.PNG;*.crt;*.CRT;*.tiff;*.TIFF|Wszystkie pliki (*.*)|*.*";
            openImage.ShowDialog();
            string openedImageName = openImage.FileName;
            string fileName = System.IO.Path.GetFileName(openedImageName);
            string filePath = openedImageName.Substring(0, openedImageName.Length - fileName.Length);
            System.Drawing.Bitmap image = AForge.Imaging.Image.FromFile(openedImageName);
            AForge.Imaging.Filters.ResizeNearestNeighbor filter = new AForge.Imaging.Filters.ResizeNearestNeighbor(thumbnailWidth, thumbnailHeight);
            System.Drawing.Bitmap thumbnail = filter.Apply(image);
            string thumbnailFileName = LookForFreeFilename(thumbnailPath, fileName);
            thumbnail.Save(thumbnailPath + thumbnailFileName);
            
            DataAccess.PhotosDataSource db = new DataAccess.PhotosDataSource(connectionString);
            Photo lastPhoto = db.GetLastPhoto();
            Photo photoObject = new Photo();
            if (lastPhoto != null)
                photoObject.Id = lastPhoto.Id + 1;
            else
                photoObject.Id = 1;

            photoObject.Filename = StrToByteArray(fileName);
            photoObject.FilePath = filePath;
            photoObject.ThumbnailPath = StrToByteArray(thumbnailPath + thumbnailFileName);
            photoObject.Title = fileName;
            photoObject.Description = string.Empty;
            DataAccess.TagsDataSource tableTags = new DataAccess.TagsDataSource(connectionString);
            photoObject.Tag = tableTags.GetTag(0);

            // TEMPORARY CONDITION
            if (photoObject.Tag == null) 
            {
                DataAccess.DevDataSource tmpDb = new DataAccess.DevDataSource(connectionString);
                tmpDb.AddTemplateTag();
                photoObject.Tag = tableTags.GetTag(0);
            }

            DataAccess.ArchivesDataSource tableArchives = new DataAccess.ArchivesDataSource(connectionString);
            photoObject.Archives = tableArchives.GetArchive(0);

            // TEMPORARY CONDITION
            if (photoObject.Archives == null) 
            {
                DataAccess.DevDataSource tmpDb = new DataAccess.DevDataSource(connectionString);
                tmpDb.AddTemplateArchive();
                photoObject.Archives = tableArchives.GetArchive(0);
            }

            db.AddPhoto(photoObject);

            // db.SaveChanges();
        }

        public static byte[] StrToByteArray(string str)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetBytes(str);
        }

        public static string LookForFreeFilename(string path, string fileName)
        {
            if (System.IO.File.Exists(path + fileName))
            {
                string[] splittedFilename = fileName.Split(".".ToCharArray());
                string file = string.Empty;
                string extension;
                int i;
                for (i = 0; i < splittedFilename.Count() - 1; ++i) 
                {
                    if (i > 0)
                    {
                        file = file + "." + splittedFilename[i];
                    }
                    else
                    {
                        file = splittedFilename[i];
                    }
                }

                extension = splittedFilename[i];
                int counter = 2;
                while (System.IO.File.Exists(path + file + "[" + counter + "]." + extension)) 
                {
                    counter++;
                }

                fileName = file + "[" + counter + "]." + extension;
            }

            return fileName;
        }

        public static bool ThumbnailDirectoryExist()
        {
            System.IO.DirectoryInfo dInfo = new System.IO.DirectoryInfo(ConfigurationManager.AppSettings["thumbnailDirectory"].ToString());
            if (dInfo.Exists)
            {
                return true;             
            }

            dInfo.Create();
            return false;
        }
    }
}
