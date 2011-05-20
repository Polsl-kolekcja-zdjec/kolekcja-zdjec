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

namespace WpfKolekcjaZdjec
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructor for MainWindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            GrdThumbnails.Visibility = System.Windows.Visibility.Hidden;
            GrdDescription.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
            ShowNotImplentedDialog();
        }

        private void Burn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowNotImplentedDialog();
        }

        private void ShowExif_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            View.ImagePropertiesReadOnly exifDataReadOnly = new View.ImagePropertiesReadOnly();
            exifDataReadOnly.PhotoName.Value = "przykład przypisania w C#";
            exifDataReadOnly.Show();
        }

        private void EditExif_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            View.ImageProperties exifData = new View.ImageProperties();
            exifData.Show();
        }

        private void Report_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowNotImplentedDialog();
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

        private void Slider_Change(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue > 0.03 && e.NewValue < 0.97)
            {
                this.CarouselPanel.TopItemPathFraction = e.NewValue;
                this.pthScaleCenterPoint.PathFraction = e.NewValue;
                this.pthOpacityCenterPoint.PathFraction = e.NewValue;
                this.pthOpacityLeftPoint.PathFraction = e.NewValue - 0.03;
                this.pthOpacityRightPoint.PathFraction = e.NewValue + 0.03;
            }
            else if (e.NewValue <= 0.5)
            {
                this.Slider_Change(this, new RoutedPropertyChangedEventArgs<double>(0, 0.04));
            }
            else if (e.NewValue >= 0.97)
            {
                this.Slider_Change(this, new RoutedPropertyChangedEventArgs<double>(0, 0.96));
            }
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

        private void Close_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
            string openedImageName = string.Empty;
            Microsoft.Win32.OpenFileDialog openImage = new Microsoft.Win32.OpenFileDialog();
            openImage.Filter = "*.jpg|*.jpg|*.png|*.png| *.crt |*.crt| *.tiff| *.tiff";
            openImage.ShowDialog();
            openedImageName = openImage.FileName;          
        } 

        private void AboutItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            View.About aboutInfo = new View.About();
           //// aboutInfo.Visibility = System.Windows.Visibility.Visible;
           aboutInfo.Show();
        }
    }
}