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

namespace WpfKolekcjaZdjec.View
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
            return true;
        }

        #endregion

        /// <summary>
        /// Constructor for MainWindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            GrdThumbnails.Visibility = System.Windows.Visibility.Hidden;
            GrdSlideshow.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// Handles the Loaded event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Business actions.
            Actions.StartupTests();

            // Load all plugins.
            PluginsBusiness.RegisterPluginsFromDirectory(this);

            // Getting exising photos from database.
            GetAndShowImagesFromDatabase(Actions.GetAllPhotos());
        }

        /// <summary>
        /// Handles the ValueChanged event of the SearchTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.RadRoutedEventArgs"/> instance containing the event data.</param>
        private void SearchTextBox_ValueChanged(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            ShowNotImplentedDialog();
        }

        /// <summary>
        /// Handles the Click event of the Import control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Import_Click(object sender, RoutedEventArgs e)
        {
            IPlugin plugin = PluginsBusiness.GetPlugin(0);

            if (plugin != null)
            {
                MessageBox.Show("Specified plugin is loaded: " + plugin.Name, "Plugin loaded", MessageBoxButton.OK, MessageBoxImage.Information);
                plugin.Execute();
            }
            else
            {
                ShowNotImplentedDialog();
            }
        }

        /// <summary>
        /// Handles the Click event of the Export control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Export_Click(object sender, RoutedEventArgs e)
        {
            ShowNotImplentedDialog();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Personal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Personal_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RedrawArea();

            Photo photoTemp = GetSelectedPhoto();
            if (photoTemp != null)
            {
               Actions.DeletePhoto(photoTemp);
            }
        }

        // TODO: Instalator.
        // TODO: Exif i atrybuty - wypełnianie.
        // TODO: Lokalizacje i źródła.

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the ShowExif control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void ShowExif_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Photo photoTemp = GetSelectedPhoto();

            if (photoTemp != null)
            {
                ImagePropertiesReadOnly exifDataReadOnly = new ImagePropertiesReadOnly();

                foreach (var i in Actions.GetAllExif())
                {
                    exifDataReadOnly.ISO.Value = i.ISO;
                    exifDataReadOnly.WhiteBalance.Value = i.WhiteBalance;
                }

                exifDataReadOnly.Show();
            }
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the EditExif control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void EditExif_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Report control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Report_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Slideshow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Slideshow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GrdThumbnails.Visibility = System.Windows.Visibility.Hidden;
            GrdSlideshow.Visibility = System.Windows.Visibility.Visible;
            GrdDescription.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Thumbails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Thumbails_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GrdThumbnails.Visibility = System.Windows.Visibility.Visible;
            GrdSlideshow.Visibility = System.Windows.Visibility.Hidden;
            GrdDescription.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// Handles the Init event of the Windows control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Windows_Init(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Details control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Details_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GrdThumbnails.Visibility = System.Windows.Visibility.Hidden;
            GrdSlideshow.Visibility = System.Windows.Visibility.Hidden;
            GrdDescription.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// Shows the not implented dialog.
        /// </summary>
        private void ShowNotImplentedDialog()
        {
            RadWindow.Alert("Not implemented yet.");
        }

        /// <summary>
        /// Handles the Click event of the Close control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the CreateStructure control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void CreateStructure_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowNotImplentedDialog();
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the ManageStructure control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void ManageStructure_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowNotImplentedDialog();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the ImgNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void ImgNew_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataAccess.Actions.AddPhoto();
            GetAndShowImagesFromDatabase(Actions.GetAllPhotos());
        }

        /// <summary>
        /// Updates the tag cloud.
        /// </summary>
        public void UpdateTagCloud()
        {
            TagCloud.DataContext = Actions.GetAllTags();
        }

        /// <summary>
        /// Gets the and show images from database.
        /// </summary>
        /// <param name="photos">The photos.</param>
        public void GetAndShowImagesFromDatabase(List<Photo> photos)
        {
            if (photos.Count > 0)
            {
                UpdateTagCloud();

                PhotoDescriptions.DataContext = photos;
                PhotoThumbails.DataContext = photos;
                CarouselPanel.DataContext = photos;
            }
        }

        /// <summary>
        /// Handles the Click event of the AboutItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AboutItem_Click(object sender, EventArgs e)
        {
            About aboutInfo = new About();
            aboutInfo.Show();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Image control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        /// <summary>
        /// Handles the 1 event of the Image_MouseLeftButtonDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the ImgRemove control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void ImgRemove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Actions.DeletePhotos(Actions.GetAllPhotos());
            GetAndShowImagesFromDatabase(Actions.GetAllPhotos());
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the ImgRename control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void ImgRename_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        /// <summary>
        /// Gets the selected photo.
        /// </summary>
        /// <returns>Selected photo.</returns>
        protected Photo GetSelectedPhoto()
        {
            // Returned object.
            object obj = null;

            if (GrdSlideshow.Visibility == System.Windows.Visibility.Visible)
            {
                obj = CarouselPanel.SelectedItem;
            }

            if (GrdThumbnails.Visibility == System.Windows.Visibility.Visible)
            {
                obj = PhotoThumbails.SelectedItem;
            }

            if (GrdDescription.Visibility == System.Windows.Visibility.Visible)
            {
                obj = PhotoDescriptions.SelectedItem;
            }

            return (Photo)obj;
        }

        /// <summary>
        /// Redraws the area.
        /// </summary>
        private void RedrawArea()
        {
            // Tag cloud redraw.
            UpdateTagCloud();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the ImgTag control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void ImgTag_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RedrawArea();

            TagPhotos tagingWindow = new TagPhotos();
            tagingWindow.Refresh(GetSelectedPhoto());
            tagingWindow.Show();

            if (tagingWindow.CloseWindow)
            {
                GetAndShowImagesFromDatabase(Actions.GetAllPhotos());
            }

            tagingWindow = null;
        }
    }
}