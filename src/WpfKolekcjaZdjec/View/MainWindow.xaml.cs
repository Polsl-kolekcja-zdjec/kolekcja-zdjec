using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using WpfKolekcjaZdjec.DataAccess;
using WpfKolekcjaZdjec.Plugins;
using WpfKolekcjaZdjec.Business;
using WpfKolekcjaZdjec.Entities;

namespace WpfKolekcjaZdjec
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window, IHostPlugin
    {
        #region IHostPlugin methods.

        /// <summary>
        /// Registers the specified plugin.
        /// </summary>
        /// <param name="plugin">The plugin interface implementation.</param>
        /// <returns>Status of this operation.</returns>
        public bool Register(IPlugin plugin)
        {
            MessageBox.Show(plugin.Name, "Plugin loaded");
            return plugin.Execute();
        }

        #endregion
        bool selectedPhoto;
        List<Photo> photos;
        /// <summary>
        /// Constructor for MainWindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            GrdThumbnails.Visibility = System.Windows.Visibility.Hidden;
            GrdSlideshow.Visibility = System.Windows.Visibility.Hidden;
           selectedPhoto = false;
        } 

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Business actions.
       //   Actions.StartupTests();

            // Load all plugins.
         // PluginsBusiness.RegisterPluginsFromDirectory(this);
            foreach (var i in Actions.GetAllPhotos())
            {
                if(i.ID != null) //jeśli jest coś w bazie :>
                    GetAndShowImagesFromDatabase();
            }
        }

        private void SearchTextBox_ValueChanged(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            ShowNotImplentedDialog();
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            ShowNotImplentedDialog();
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            ShowNotImplentedDialog();
        }

        private void Personal_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ///string curItem = PhotoThumbails.SelectedItem.ToString();

           /// int selectID = pobrane ID z zaznaczonego obiektu

           /// if (selectedPhoto)
          //  {

           //     Actions.DeletePhoto(photos.ElementAt(selectID));
          //  }
        }

        private void ShowExif_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedPhoto)
            {
                View.ImagePropertiesReadOnly exifDataReadOnly = new View.ImagePropertiesReadOnly();
                
                foreach (var i in Actions.GetAllExif())
                {
                    // musisz tylko dopisać wszystkie parametry do odpowiednich pól w formatce ( to po lewej) z bazy 
                    exifDataReadOnly.ISO.Value = i.ISO;
                    exifDataReadOnly.WhiteBalance.Value = i.WhiteBalance;
                }
                exifDataReadOnly.Show();
            }
            selectedPhoto = false;
        }

        private void EditExif_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            DataAccess.Actions.AddExif();
        }

        private void Report_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Slideshow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GrdThumbnails.Visibility = System.Windows.Visibility.Hidden;
            GrdSlideshow.Visibility = System.Windows.Visibility.Visible;
           GrdDescription.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Thumbails_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GrdThumbnails.Visibility = System.Windows.Visibility.Visible;
            GrdSlideshow.Visibility = System.Windows.Visibility.Hidden;
          GrdDescription.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Windows_Init(object sender, EventArgs e)
        {
        }

        private void Details_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           GrdThumbnails.Visibility = System.Windows.Visibility.Hidden;
            GrdSlideshow.Visibility = System.Windows.Visibility.Hidden;
            GrdDescription.Visibility = System.Windows.Visibility.Visible;
        }

        // <summary>
        // Shows the not implented dialog.
        // </summary>
        private void ShowNotImplentedDialog()
        {
            RadWindow.Alert("Not implemented yet.");
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateStructure_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowNotImplentedDialog();
        }

        private void ManageStructure_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowNotImplentedDialog();
        }

        private void ImgNew_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataAccess.Actions.AddPhoto();
            GetAndShowImagesFromDatabase();            
        }

        private void GetAndShowImagesFromDatabase()
        {
            photos = Actions.GetAllPhotos();
            PhotoDescriptions.DataContext = photos;
            PhotoThumbails.DataContext = photos;
            CarouselPanel.DataContext = photos;
        }

        private void AboutItem_Click(object sender, EventArgs e)
        {
            View.About aboutInfo = new View.About();
           aboutInfo.Show();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectedPhoto = true;
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            selectedPhoto = true;
        }

        private void ImgRemove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Actions.DeletePhotos(photos);
            GetAndShowImagesFromDatabase();
        }

        private void ImgRename_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void ImgTag_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            View.TagPhotos tagingWindow = new View.TagPhotos();
            if(GrdSlideshow.Visibility == System.Windows.Visibility.Visible)
                tagingWindow.Refresh(CarouselPanel.SelectedItem);
            if(GrdThumbnails.Visibility == System.Windows.Visibility.Visible)
                tagingWindow.Refresh(PhotoThumbails.SelectedItem);
            if(GrdDescription.Visibility == System.Windows.Visibility.Visible)
                tagingWindow.Refresh(PhotoDescriptions.SelectedItem);
            
            tagingWindow.Show();
        }
    }
}