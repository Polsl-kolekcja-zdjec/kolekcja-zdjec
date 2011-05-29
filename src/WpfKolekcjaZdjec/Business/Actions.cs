using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfKolekcjaZdjec.Entities;

namespace WpfKolekcjaZdjec.Business
{
    public class Actions
    {
        public static void AddPhoto()
        {
            int thumbnailHeight = 400;
            int thumbnailWidth = 400;
            string thumbnailPath = "C:/program1/";
            string openedImageName = string.Empty;
            Microsoft.Win32.OpenFileDialog openImage = new Microsoft.Win32.OpenFileDialog();
            openImage.Filter = "Pliki obrazów (*.jpg, *.png, *.crt, *.tiff)|*.jpg;*.JPG;*.jpeg;*.JPEG;*.png;*.PNG;*.crt;*.CRT;*.tiff;*.TIFF|Wszystkie pliki (*.*)|*.*";
            openImage.ShowDialog();
            openedImageName = openImage.FileName;
            string fileName = System.IO.Path.GetFileName(openedImageName);
            string filePath = openedImageName.Substring(0, openedImageName.Length - fileName.Length);
            System.Drawing.Bitmap image = AForge.Imaging.Image.FromFile(openedImageName);
            AForge.Imaging.Filters.ResizeNearestNeighbor filter = new AForge.Imaging.Filters.ResizeNearestNeighbor(thumbnailWidth, thumbnailHeight);
            System.Drawing.Bitmap thumbnail = filter.Apply(image);
            string thumbnailFileName = LookForFreeFilename(thumbnailPath, fileName);
            thumbnail.Save(thumbnailPath + thumbnailFileName);
            WpfKolekcjaZdjecEntitiesDataModelContainer db = new WpfKolekcjaZdjecEntitiesDataModelContainer();
            var results = (from p in db.PhotoSet
                          orderby p.Id descending
                          select p.Id).Take(1);

            foreach (int ph in results) 
            {
                Console.WriteLine(ph.ToString());
                Console.WriteLine("Siema!");
            }

            Console.WriteLine("Siema!2");

            //// Consoe.WriteLine(results.ToString());
            // db.PhotoSet.AddObject(Photo.CreatePhoto(0, StrToByteArray(fileName), filePath, StrToByteArray(thumbnailPath + thumbnailFileName)));
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
    }
}
