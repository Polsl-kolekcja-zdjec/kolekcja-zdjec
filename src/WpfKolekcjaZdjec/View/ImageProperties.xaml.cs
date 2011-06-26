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
using System.Windows.Shapes;

namespace WpfKolekcjaZdjec.View
{
    /// <summary>
    /// Interaction logic for ImageProperties.xaml.
    /// </summary>
    public partial class ImageProperties : Window
    {
        /// <summary>
        /// Constructor for Image properties window.
        /// </summary>
        public ImageProperties()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the ValueChanged event of the PhotoName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.RadRoutedEventArgs"/> instance containing the event data.</param>
        private void PhotoName_ValueChanged(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
        }
    }
}