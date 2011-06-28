using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using WpfKolekcjaZdjec.Entities;
using System.IO;
using WpfKolekcjaZdjec.DataAccess;

namespace WpfKolekcjaZdjec.View
{
    /// <summary>
    /// Interaction logic for TagPhotos.xaml.
    /// </summary>
    public partial class TagPhotos : Window
    {
        /// <summary>
        /// Selected photo.
        /// </summary>
        private Photo selectedPhoto;

        /// <summary>
        /// Error image.
        /// </summary>
        private Image errorImage = new Image();

        /// <summary>
        /// Is closed?
        /// </summary>
        private bool close;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagPhotos"/> class.
        /// </summary>
        public TagPhotos()
        {
            InitializeComponent();

            errorImage.Source = PhotoThumbnail.Source;
            close = false;

            // Wypelnij liste tagow.
            List<Entities.Tag> allTags = DataAccess.Actions.GetAllTags();
            foreach (Tag currentTag in allTags)
            {
                TagsListBox.Items.Add(currentTag.Name);
            }
        }

        /// <summary>
        /// Gets or sets the selected photo.
        /// </summary>
        /// <value>
        /// The selected photo.
        /// </value>
        public Photo SelectedPhoto
        {
            get
            {
                return selectedPhoto;
            }

            set
            {
                selectedPhoto = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [close window].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [close window]; otherwise, <c>false</c>.
        /// </value>
        public bool CloseWindow
        {
            get
            {
                return close;
            }
        }

        /// <summary>
        /// Refresh window.
        /// </summary>
        /// <param name="parameter">Selected photo.</param>
        public void Refresh(Object parameter)
        {
            selectedPhoto = (Entities.Photo)parameter;
            ViewThumbnail();
            ViewPhotoTags();
        }

        /// <summary>
        /// Viewing a thumbnail and text information.
        /// </summary>
        private void ViewThumbnail()
        {
            // Wyswietlanie miniaturki + info.
            if (selectedPhoto == null)
            {
                SelectTagTextBlock.Text = "You can not tag a photo now.";
                UntagTextBlock.Text = "You can not untag a photo now.";
                PhotoInfoTextBlock.Text = "Please select a photo first.";
            }
            else
            {
                BitmapImage thumbnailBitmap = new BitmapImage();

                thumbnailBitmap.BeginInit();
                thumbnailBitmap.UriSource = new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, selectedPhoto.ThumbnailPath));
                thumbnailBitmap.EndInit();

                try
                {
                    PhotoThumbnail.Width = int.Parse(ConfigurationManager.AppSettings["ThumbnailMaxWidth"].ToString());
                    PhotoThumbnail2.Width = int.Parse(ConfigurationManager.AppSettings["ThumbnailMaxWidth"].ToString());
                }
                catch (Exception)
                {
                    PhotoThumbnail.Width = 300;
                    PhotoThumbnail2.Width = 300;
                }

                PhotoThumbnail.Source = thumbnailBitmap;
                PhotoThumbnail2.Source = thumbnailBitmap;

                SelectTagTextBlock.Text = UntagTextBlock.Text = "Please select a tag:";
                PhotoInfoTextBlock.Text = "Selected photo: " + selectedPhoto.Title;
            }
        }

        /// <summary>
        /// Adding a tag from a TextBox (NewTagTextBox) and tagging selected photo with that tag.
        /// </summary>
        /// <param name="tagName">Tag name.</param>
        /// <returns>True if successful</returns>
        private Boolean AddTag(String tagName)
        {
            // If empty returning false.
            if (string.IsNullOrWhiteSpace(tagName))
            {
                MessageBox.Show("Type the tag name, please.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            // If not in database adding to TagsList.
            if (DataAccess.Actions.AddTag(tagName))
            {
                // Add a new tag to comboBox of tags.
                TagsListBox.Items.Add(tagName);
                ((MainWindow)Application.Current.MainWindow).UpdateTagCloud();
            }

            // Tag selected photo.
            if (Actions.TagPhoto(selectedPhoto.ID, tagName))
            {
                // Simulate close button click ;)
                this.CloseButton_Click(this, null);

                return true;
            }
            else
            {
                MessageBox.Show("An error occurred when we insert tag into database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// Handles the Click event of the TagPhotoButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void TagPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPhoto == null)
            {
                MessageBox.Show("Please select a photo first.", "Inforation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (string.IsNullOrWhiteSpace(NewTagTextBox.Text))
            {
                // Sprawdzam czy cos zaznaczono na liscie.
                if (TagsListBox.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please select a tag or enter name of a new one.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (!Actions.TagPhoto(selectedPhoto.ID, TagsListBox.SelectedItems[0].ToString()))
                    {
                        MessageBox.Show("An error occurred when we insert try to tag a photo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        // Simulate close button click ;)
                        this.CloseButton_Click(this, null);
                    }
                }
            }
            else
            {
                // Jezeli wybrano zdjecie i wpisano taga recznie.
                AddTag(NewTagTextBox.Text);
            }
        }

        /// <summary>
        /// Handles the Click event of the CloseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            selectedPhoto = null;
            close = true;

            ((MainWindow)Application.Current.MainWindow).UpdateTagCloud();

            PhotosDataSource dataSource = new PhotosDataSource(ConnectionStringHelper.GetActualConnectionString());
            ((MainWindow)Application.Current.MainWindow).GetAndShowImagesFromDatabase(dataSource.GetAllPhotos());

            PhotoThumbnail.Source = errorImage.Source;

            Close();
        }

        /// <summary>
        /// Filling ListBox of selected photo tags.
        /// </summary>
        private void ViewPhotoTags()
        {
            //fill photo tags list
            if (selectedPhoto != null)
            {
                List<Tag> allPhotoTags = DataAccess.Actions.GetPhotosTags(selectedPhoto.ID);

                foreach (Tag current in allPhotoTags)
                {
                    PhotoTagsListBox.Items.Add(current.Name);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the UntagPhoto button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void UntagButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPhoto == null)
            {
                MessageBox.Show("Please select a photo first.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (PhotoTagsListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a tag.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                if (Actions.UntagPhoto(selectedPhoto.ID, PhotoTagsListBox.SelectedItem.ToString()))
                {
                    // Simulate close button click ;)
                    this.CloseButton_Click(this, null);
                }
                else
                {
                    MessageBox.Show("An error occurred.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}