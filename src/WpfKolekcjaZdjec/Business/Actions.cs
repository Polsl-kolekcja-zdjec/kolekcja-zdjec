﻿using System.Configuration;
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
            string thumbnailPath = ConfigurationManager.AppSettings["thumbnailDirectory"].ToString();
            string connectionString = Business.ConnectionStringHelper.GetActualConnectionString();

            OpenFileDialog openImage = new OpenFileDialog();
            openImage.Filter = "Pliki obrazów (*.jpg, *.png, *.crt, *.tiff)|*.jpg;*.JPG;*.jpeg;*.JPEG;*.png;*.PNG;*.crt;*.CRT;*.tiff;*.TIFF|Wszystkie pliki (*.*)|*.*";
            openImage.ShowDialog();
            
            
            if (openImage.CheckFileExists)
            {
                string openedImageName = openImage.FileName;
                string fileName = Path.GetFileName(openedImageName);
                string filePath = openedImageName.Substring(0, openedImageName.Length - fileName.Length);

                Bitmap image = AForge.Imaging.Image.FromFile(openedImageName);
                int[] thumbnailSizes = getThumbnailSize(image.Width, image.Height);
                int thumbnailWidth = thumbnailSizes[0];
                int thumbnailHeight = thumbnailSizes[1];
                AForge.Imaging.Filters.ResizeBicubic filter = new AForge.Imaging.Filters.ResizeBicubic(thumbnailWidth, thumbnailHeight);
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
        /// <returns>Deleted photo id or 0 when fails.</returns>
        public static int DeletePhoto(Photo toDelete)
        {
            return 0;
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
        public static int[] getThumbnailSize(int normalSizeWidth, int normalSizeHeight, int maxWidth = 0, int maxHeight = 0)
        {
            if (maxWidth == 0)
            {
                maxWidth = int.Parse(ConfigurationManager.AppSettings["thumbnailMaxWidth"].ToString());
            }
            if (maxHeight == 0)
            {
                maxHeight = int.Parse(ConfigurationManager.AppSettings["thumbnailMaxHeight"].ToString());
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