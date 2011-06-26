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
    /// Interaction logic for TagPhotos.xaml
    /// </summary>
    public partial class TagPhotos : Window
    {
        Entities.Photo selectedPhoto;

        public TagPhotos()
        {
            InitializeComponent();
            
            //wypelnij liste tagow
            List<Entities.Tag> allTags = DataAccess.Actions.GetAllTags();
            foreach (Entities.Tag currentTag in allTags)
            {
                TagsListBox.Items.Add(currentTag.Name);
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
        }

        /// <summary>
        /// Viewing a thumbnail and text information.
        /// </summary>
        private void ViewThumbnail()
        {
            //wyswietlanie miniaturki + info
            if (selectedPhoto == null)
            {
                BitmapImage thumbnailBitmap = new BitmapImage();
                thumbnailBitmap.BeginInit();
                thumbnailBitmap.UriSource = new Uri(@"D:\GIT clone\kolekcja-zdjec\src\WpfKolekcjaZdjec\Images\NothingSelected.jpg");
                thumbnailBitmap.DecodePixelWidth = 200;
                thumbnailBitmap.EndInit();
                PhotoThumbnail.Width = 200;
                PhotoThumbnail.Source = thumbnailBitmap;
                //MessageBox.Show("No photo selected.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                SelectTagTextBlock.Text = "You can not tag a photo now.";
                PhotoInfoTextBlock.Text = "Please select a photo first.";
            }
            else
            {
                BitmapImage thumbnailBitmap = new BitmapImage();
                //to bedzie dobre jak bedzie miniaturka
                /*
                 thumbnailBitmap.BeginInit();
                thumbnailBitmap.UriSource = new Uri(@p1.ThumbnailPath);
                thumbnailBitmap.EndInit();
                */
                //to jest dobre tymczasowo
                thumbnailBitmap.BeginInit();
                thumbnailBitmap.UriSource = new Uri(@selectedPhoto.FilePath);
                thumbnailBitmap.DecodePixelWidth = 200;
                thumbnailBitmap.EndInit();
                PhotoThumbnail.Width = 200;
                PhotoThumbnail.Source = thumbnailBitmap;
                SelectTagTextBlock.Text = "Please select a tag:";
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
            //if empty returning false
            if (tagName.Length < 1)
            {
                MessageBox.Show("Error.\n\nMissing text.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            //else if meaning less returning false
            else if (tagName.Length == 1)
            {
                if (tagName[0] < 33)
                {
                    MessageBox.Show("Error.\n\nMissing text.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            //if not in database adding to TagsList
            if (DataAccess.Actions.AddTag(tagName))
            {
                //add a new tag to comboBox of tags
                TagsListBox.Items.Add(tagName);
                //show confirmation
                //MessageBox.Show("Tag added to database", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
            //tag selected photo
            if (DataAccess.Actions.TagPhoto(selectedPhoto.ID, tagName))
            {
                String messageText = "\"" + tagName + "\" tag added to photo.";
                MessageBox.Show(messageText, "Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                MessageBox.Show("An error occurred", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void TagPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            //jezeli nie wybrano zdjecia
            if (selectedPhoto == null)
            {
                MessageBox.Show("Please select a photo first.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //jezeli nie wpisano tekstu nowego taga
            else if (NewTagTextBox.Text == "")
            {
                //sprawdzam czy cos zaznaczono na liscie
                if (TagsListBox.SelectedItems.Count == 0)
                    MessageBox.Show("Please select a tag or enter name of a new one.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    if (DataAccess.Actions.TagPhoto(selectedPhoto.ID, TagsListBox.SelectedItems[0].ToString()))
                    {
                        String messageText = "\"" + TagsListBox.SelectedItems[0] + "\" tag added to photo.";
                        MessageBox.Show(messageText, "Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                        MessageBox.Show("An error occurred", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //jezeli wybrano zdjecie i wpisano taga recznie
            else
            {
                AddTag(NewTagTextBox.Text);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            selectedPhoto = null;
            Close();
        }
    }
}
