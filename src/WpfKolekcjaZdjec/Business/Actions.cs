using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using WpfKolekcjaZdjec.DataAccess;
using WpfKolekcjaZdjec.Entities;
using System.Collections.Generic;
using System;

namespace WpfKolekcjaZdjec.DataAccess
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
        /// Gets all photos.
        /// </summary>
        /// <returns>Photos list.</returns>
        public static List<Photo> GetAllPhotos()
        {
            PhotosDataSource db = new PhotosDataSource(ConnectionStringHelper.GetActualConnectionString());
            

            return db.GetAllPhotos();
        }

        /// <summary>
        /// Get one photo.
        /// </summary>
        /// <param name="id">Photo ID.</param>
        /// <returns>One photo.</returns>
        public static Photo GetPhoto(int id) { 
            PhotosDataSource db = new PhotosDataSource(ConnectionStringHelper.GetActualConnectionString());
            return db.GetPhoto(id);
        }

        /// <summary>
        /// Gets all tags.
        /// </summary>
        /// <returns>Tags list.</returns>
        public static List<Tag> GetAllTags()
        {
            TagsDataSource db = new TagsDataSource(ConnectionStringHelper.GetActualConnectionString());

            return db.GetAllTags();
        }

        /// <summary>
        /// Gets all exif.
        /// </summary>
        /// <returns>Exif list.</returns>
        public static List<ExifAttribute> GetAllExif()
        {
            ExifAttributesDataSource db = new ExifAttributesDataSource(ConnectionStringHelper.GetActualConnectionString());

            return db.GetAllExif();
        }

        /// <summary>
        /// Gets all archives.
        /// </summary>
        /// <returns>Archives list.</returns>
        public static List<Archive> GetAllArchives()
        {
            ArchivesDataSource db = new ArchivesDataSource(ConnectionStringHelper.GetActualConnectionString());

            return db.GetAllArchives();
        }

        /// <summary>
        /// Adds the exif.
        /// </summary>
        public static void AddExif()
        {
            // Show Exif property window.
            View.ImageProperties exifData = new View.ImageProperties();
            exifData.Show();

            // Connecting data base.
            string connectionString = DataAccess.ConnectionStringHelper.GetActualConnectionString();
            ExifAttributesDataSource db = new ExifAttributesDataSource(connectionString);
            ExifAttribute exifObject = new ExifAttribute();

            // TODO: tu przepisujesz dane ze zdjęcia do odpowiednich pól bazy, napisalam ci przyklad na sztywno
            // TODO: mają się wyswietlac w zaleznosci od zaznaczonego zdjecia (ewentualnie dla ostatnio dodanego jak inaczej nie umiesz)
            exifObject.WhiteBalance = "Auto";
            exifObject.ISO = 400;

            db.AddExif(exifObject);
        }

        /// <summary>
        /// Adds the photo.
        /// </summary>
        public static void AddPhoto()
        {
            string thumbnailPath = ConfigurationManager.AppSettings["thumbnailDirectory"].ToString();
            string connectionString = ConnectionStringHelper.GetActualConnectionString();

            OpenFileDialog openImage = new OpenFileDialog();
            openImage.Filter = "Pliki obrazów (*.jpg, *.png, *.crt, *.tiff)|*.jpg;*.JPG;*.jpeg;*.JPEG;*.png;*.PNG;*.crt;*.CRT;*.tiff;*.TIFF|Wszystkie pliki (*.*)|*.*";
            openImage.ShowDialog();

            if (openImage.CheckFileExists)
            {
                string openedImageName = openImage.FileName;
                string fileName = Path.GetFileName(openedImageName);
                string filePath = openedImageName.Substring(0, openedImageName.Length - fileName.Length);
                string newFilePath = ChangePath(filePath);
                

                Bitmap image = AForge.Imaging.Image.FromFile(openedImageName);

                //int[] thumbnailSizes = GetThumbnailSize(image.Width, image.Height);

                //int thumbnailWidth = thumbnailSizes[0];
                //int thumbnailHeight = thumbnailSizes[1];

                //AForge.Imaging.Filters.ResizeBicubic filter = new AForge.Imaging.Filters.ResizeBicubic(thumbnailWidth, thumbnailHeight);
                //Bitmap thumbnail = filter.Apply(image);

                //string thumbnailFileName = LookForFreeFilename(thumbnailPath, fileName);
                //string thumbnailSavedPath = Path.Combine(thumbnailPath, thumbnailFileName);

                //thumbnail.Save(thumbnailSavedPath);
                newFilePath = newFilePath + fileName;

                PhotosDataSource db = new PhotosDataSource(connectionString);
                Photo photoObject = new Photo();

                photoObject.FilePath = newFilePath;
                photoObject.ThumbnailPath = string.Empty;// thumbnailSavedPath;

                photoObject.Title = fileName;
                photoObject.Description = "fuck jeah";//string.Empty;

                // TODO: get and add archive ID to photo
                photoObject.Archive = null;

                // TODO: get and add attribute ID to photo
                photoObject.Attribute = null;

                db.AddPhoto(photoObject);

            }
        }

        /// <summary>
        /// Delete selected Photo.
        /// </summary>
        /// <param name="toDelete">Photo to delete.</param>
        /// <returns>Deleting photo operation state.</returns>
        public static bool DeletePhoto(Photo toDelete)
        {
            PhotosDataSource db = new PhotosDataSource(ConnectionStringHelper.GetActualConnectionString());
            return db.DeletePhoto(toDelete.ID);
        }

        /// <summary>
        /// Delete all specified photos.
        /// </summary>
        /// <param name="toDeleteList">Photo to delete.</param>
        /// <returns>Number of deleted photos.</returns>
        public static int DeletePhotos(List<Photo> toDeleteList)
        {
            PhotosDataSource db = new PhotosDataSource(ConnectionStringHelper.GetActualConnectionString());

            List<int> ids = new List<int>();
            foreach (Photo p in toDeleteList)
            {
                ids.Add(p.ID);
            }

            return db.DeletePhotos(ids);
        }

        /// <summary>
        /// Change \ to \\ in path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>New path.</returns>
         public static string ChangePath(string path)
        {
            string sign;  // sign = ' \ '
            sign = "\\";
            path =  path.Replace(sign, "\\");
            return path;
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
        /// Get calibrate thumbnail size.
        /// </summary>
        /// <param name="normalSizeWidth">Width of file to calibrate.</param>
        /// <param name="normalSizeHeight">Height of file to calibrate.</param>
        /// <param name="maxWidth">Width of thumbnail.</param>
        /// <param name="maxHeight">Height of thumbnail.</param>
        /// <returns>Array with new Width and Height values.</returns>
        public static int[] GetThumbnailSize(int normalSizeWidth, int normalSizeHeight, int maxWidth = 0, int maxHeight = 0)
        {
            try
            {
                if (maxWidth == 0)
                {
                    maxWidth = int.Parse(ConfigurationManager.AppSettings["ThumbnailMaxWidth"].ToString());
                }
            }
            catch (Exception)
            {
                // Default width;
                maxWidth = 300;
            }

            try
            {
                if (maxHeight == 0)
                {
                    maxHeight = int.Parse(ConfigurationManager.AppSettings["ThumbnailMaxHeight"].ToString());
                }
            }
            catch (Exception)
            {
                maxHeight = 300;
            }

            int calibrateSize = (int)((normalSizeWidth * maxHeight / (double)normalSizeHeight) + 0.5);
            int[] retArr = new int[2];

            if (maxWidth < calibrateSize)
            {
                calibrateSize = (int)((normalSizeHeight * maxWidth / (double)normalSizeWidth) + 0.5);
                retArr[0] = maxWidth;
                retArr[1] = calibrateSize;
            }
            else
            {
                retArr[0] = calibrateSize;
                retArr[1] = maxHeight;
            }

            return retArr;
        }

        /// <summary>
        /// Thumbnails the directory exist.
        /// </summary>
        /// <returns>Value indicates whether a thumbnails directory exists.</returns>
        public static bool ThumbnailDirectoryExist()
        {
            DirectoryInfo dInfo = new DirectoryInfo(ConfigurationManager.AppSettings["ThumbnailDirectory"].ToString());

            if (dInfo.Exists)
            {
                return true;
            }
            else
            {
                dInfo.Create();
                return false;
            }
        }
    }
}