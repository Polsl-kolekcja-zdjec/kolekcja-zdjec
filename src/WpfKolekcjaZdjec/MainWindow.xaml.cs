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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            miniaturki.Visibility = System.Windows.Visibility.Hidden;
            description.Visibility = System.Windows.Visibility.Hidden;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void searchTextBox_ValueChanged(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {

        }

    

        private void import_Click(object sender, RoutedEventArgs e)
        {

            RadWindow.Alert("do uzupełnienia");
        }

        private void export_Click(object sender, RoutedEventArgs e)
        {

            RadWindow.Alert("do uzupełnienia");
        }



        private void personal_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RadWindow.Alert("do uzupełnienia");
        }

        private void burn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RadWindow.Alert("do uzupełnienia");
        }

        private void showExif_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RadWindow.Alert("do uzupełnienia");
        }

        private void editExif_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RadWindow.Alert("do uzupełnienia");
        }

        private void report_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RadWindow.Alert("do uzupełnienia");
        }

        private void islajdshow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            miniaturki.Visibility = System.Windows.Visibility.Hidden;
            slajdshow.Visibility = System.Windows.Visibility.Visible;
            description.Visibility = System.Windows.Visibility.Hidden;
        }

        private void thumbails_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            miniaturki.Visibility = System.Windows.Visibility.Visible;
            slajdshow.Visibility = System.Windows.Visibility.Hidden;
            description.Visibility = System.Windows.Visibility.Hidden;
        }


        void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue > 0.03 && e.NewValue < 0.97)
            {
                this.Panel.TopItemPathFraction = e.NewValue;
                this.scaleCenterPoint.PathFraction = e.NewValue;
                this.opacityCenterPoint.PathFraction = e.NewValue;
                this.opacityLeftPoint.PathFraction = e.NewValue - 0.03;
                this.opacityRightPoint.PathFraction = e.NewValue + 0.03;
            }
            else if (e.NewValue <= 0.5)
            {
                this.Slider_ValueChanged(this, new RoutedPropertyChangedEventArgs<double>(0, 0.04));
            }
            else if (e.NewValue >= 0.97)
            {
                this.Slider_ValueChanged(this, new RoutedPropertyChangedEventArgs<double>(0, 0.96));
            }
        }

        private void Windows_Init(object sender, EventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Hidden;
            Login l = new Login();
            bool wartoscDialogResult = (bool)l.ShowDialog();
            if (wartoscDialogResult)
                this.Visibility = System.Windows.Visibility.Visible;
            else
                this.Close();
        }

        private void details_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            miniaturki.Visibility = System.Windows.Visibility.Hidden;
            slajdshow.Visibility = System.Windows.Visibility.Hidden;
            description.Visibility = System.Windows.Visibility.Visible;
        }

        private void Zamknij_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void createStructure_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RadWindow.Alert("do uzupełnienia");
        }

        private void manageStructure_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RadWindow.Alert("do uzupełnienia");
        }
        

     
    }
}
