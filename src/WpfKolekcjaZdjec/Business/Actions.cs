using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using WpfKolekcjaZdjec.DataAccess;
using WpfKolekcjaZdjec.Entities;

namespace WpfKolekcjaZdjec.Business
{
    /// <summary>
    /// Business actions.
    /// </summary>
    public class Actions
    {
        /// <summary>
        /// Startups the tests.
        /// </summary>
        public static void StartupTests()
        {
            ThumbnailDirectoryExist();
        }

        /// <summary>
        /// Adds the photo.
        /// </summary>
        public static void AddPhoto()
        {
            int thumbnailHeight = int.Parse(ConfigurationManager.AppSettings["thumbnailMaxHeight"].ToString());
            int thumbnailWidth = int.Parse(ConfigurationManager.AppSettings["thumbnailMaxWidth"].ToString());
            string thumbnailPath = ConfigurationManager.AppSettings["thumbnailDirectory"].ToString();
            string connectionString = Business.ConnectionStringHelper.GetActualConnectionString();

            OpenFileDialog openImage = new OpenFileDialog();
            openImage.Filter = "Pliki obrazów (*.jpg, *.png, *.crt, *.tiff)|*.jpg;*.JPG;*.jpeg;*.JPEG;*.png;*.PNG;*.crt;*.CRT;*.tiff;*.TIFF|Wszystkie pliki (*.*)|*.*";
            openImage.ShowDialog();

            string openedImageName = openImage.FileName;
            string fileName = Path.GetFileName(openedImageName);
            string filePath = openedImageName.Substring(0, openedImageName.Length - fileName.Length);

            Bitmap image = AForge.Imaging.Image.FromFile(openedImageName);
            AForge.Imaging.Filters.ResizeNearestNeighbor filter = new AForge.Imaging.Filters.ResizeNearestNeighbor(thumbnailWidth, thumbnailHeight);
            Bitmap thumbnail = filter.Apply(image);

            string thumbnailFileName = LookForFreeFilename(thumbnailPath, fileName);
            string thumbnailSavedPath = Path.Combine(thumbnailPath, thumbnailFileName);
            thumbnail.Save(thumbnailSavedPath);

            PhotosDataSource db = new PhotosDataSource(connectionString);
            Photo photoObject = new Photo();

            photoObject.FilePath = filePath;
            photoObject.ThumbnailPath = thumbnailSavedPath;

            photoObject.Title = fileName;
            photoObject.Description = string.Empty;

            photoObject.Archive = null;
            photoObject.Attribute = null;

            db.AddPhoto(photoObject);
        }

        /// <summary>
        /// Looks for free filename.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>File name.</returns>
        public static string LookForFreeFilename(string path, string fileName)
        {
            if (File.Exists(path + fileName))
            {
                string[] splittedFilename = fileName.Split('.');
                string file = string.Empty, extension = string.Empty;
                int i = 0;

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
                while (File.Exists(path + file + "[" + counter + "]." + extension)) 
                {
                    counter++;
                }

                fileName = file + "[" + counter + "]." + extension;
            }

            return fileName;
        }

        /// <summary>
        /// Thumbnails the directory exist.
        /// </summary>
        /// <returns>Value indicates whether a thumbnails directory exists.</returns>
        public static bool ThumbnailDirectoryExist()
        {
            DirectoryInfo dInfo = new DirectoryInfo(ConfigurationManager.AppSettings["thumbnailDirectory"].ToString());
            if (dInfo.Exists)
            {
                return true;
            }

            dInfo.Create();
            return false;
        }
    }
}