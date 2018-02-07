#region License

//Media glass - my simple WPF player
//Copyright (C) 2008-2010 Denis Yakimenko <denyakimenko@yandex.ru>

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

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
using FormatRegistrClassLibrary;

namespace Media_glass.SmallWindows
{
    /// <summary>
    /// Interaction logic for FileTypesWindow.xaml
    /// Format that Media Glass played.
    /// </summary>
    public partial class FileTypesWindow : Window
    {
        /// <summary>
        /// Constructor -  initializes the instance of window
        /// </summary>
        public FileTypesWindow()
        {
            InitializeComponent();
            this.Style = (Style)this.FindResource("ModalWindow");            

            LoadFormats();           
        }        

        /// <summary>
        /// Load format list to the control.
        /// </summary>
        void LoadFormats()
        {
            this.listBox1.Items.Add(new FileTypeUserChanges(MediaFormatGroups.Asf, "Windows Media file (ASF)"));
            this.listBox1.Items.Add(new FileTypeUserChanges(MediaFormatGroups.Wma, "Windows Media Audio file (WMA)"));
            this.listBox1.Items.Add(new FileTypeUserChanges(MediaFormatGroups.Wmv, "Windows Media Video file (WMV)"));            
            this.listBox1.Items.Add(new FileTypeUserChanges(MediaFormatGroups.Cda, "Music CD Playback"));
            this.listBox1.Items.Add(new FileTypeUserChanges(MediaFormatGroups.Avi, "Windows video file (AVI)"));
            this.listBox1.Items.Add(new FileTypeUserChanges(MediaFormatGroups.Wav, "Windows audio file (WAV)"));
            this.listBox1.Items.Add(new FileTypeUserChanges(MediaFormatGroups.Mpeg, "Movie file (MPEG)"));
            this.listBox1.Items.Add(new FileTypeUserChanges(MediaFormatGroups.Mp3, "MP3 audio file (MP3)"));
            this.listBox1.Items.Add(new FileTypeUserChanges(MediaFormatGroups.Midi, "MIDI file (MIDI)"));
            this.listBox1.Items.Add(new FileTypeUserChanges(MediaFormatGroups.Aiff, "AIFF audio file (AIFF)"));
            this.listBox1.Items.Add(new FileTypeUserChanges(MediaFormatGroups.Au, "AU audio file (AU)"));
            this.listBox1.Items.Add(new FileTypeUserChanges(MediaFormatGroups.Mp4v, "MPEG-4 Video file (MP4)"));
            this.listBox1.Items.Add(new FileTypeUserChanges(MediaFormatGroups.Mp4a, "MPEG-4 Audio file (M4A)"));

            this.listBox1.Items.Add(new FileTypeUserChanges(MediaFormatGroups.Mkv, "Matroska Video file (MKV)"));
            this.listBox1.Items.Add(new FileTypeUserChanges(MediaFormatGroups.Mka, "Matroska Audio file (MKA)"));
            this.listBox1.Items.Add(new FileTypeUserChanges(MediaFormatGroups.Flv, "Flash Video (FLV)"));

            if (this.listBox1.Items.Count > 0)
                this.listBox1.SelectedIndex = 0;

            this.listBox1.Focus();

            EnableSelectIfNeeded();
        }

        /// <summary>
        /// Is user change format settings.
        /// </summary>        
        bool IsChanged()
        {
            foreach (FileTypeUserChanges fileType in this.listBox1.Items)            
                if (fileType.IsChanged)
                    return true;            

            return false;
        }

        /// <summary>
        /// Show or hide apply if needed.
        /// </summary>
        void EnableApplyIfNeeded()
        {
            this.applyButton.IsEnabled = this.IsChanged();
        }

        /// <summary>
        /// Apply changes.
        /// </summary>
        void Apply()
        {
            foreach (FileTypeUserChanges fileType in this.listBox1.Items)
                fileType.Save();
        }

        /// <summary>
        /// Is all format is selected.
        /// </summary>
        /// <returns></returns>
        bool IsSelectedAll()
        {
            foreach (FileTypeUserChanges fileType in this.listBox1.Items)
                if (!fileType.IsAssigned)
                    return false;

            return true;            
        }

        /// <summary>
        /// Is no one format selected.
        /// </summary>        
        bool IsDeselectedAll()
        {
            foreach (FileTypeUserChanges fileType in this.listBox1.Items)
                if (fileType.IsAssigned)
                    return false;

            return true;
        }

        /// <summary>
        /// Select all formats.
        /// </summary>
        void SelectAll()
        {
            foreach (FileTypeUserChanges fileType in this.listBox1.Items)
                fileType.IsAssigned = true;                    
        }

        /// <summary>
        /// Deselect formats.
        /// </summary>
        void DeselectAll()
        {
            foreach (FileTypeUserChanges fileType in this.listBox1.Items)
                fileType.IsAssigned = false;                    
        }

        /// <summary>
        /// Enable select button if needed.
        /// </summary>
        void EnableSelectIfNeeded()
        {
            this.selectAllButton.IsEnabled = !this.IsSelectedAll();
            this.deselectAllButton.IsEnabled = !this.IsDeselectedAll();
        }

        /// <summary>
        /// Apply button event handler.
        /// </summary>        
        private void apply_Button_Click(object sender, RoutedEventArgs e)
        {
            Apply();
            this.applyButton.IsEnabled = false;
        }        

        /// <summary>
        /// Cancle button event handler.
        /// </summary>        
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// don't handle check event.
        /// </summary>
        bool stopCheck = false;

        /// <summary>
        /// Check box event handler.
        /// </summary>        
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (this.stopCheck)
                return;
            
            this.EnableApplyIfNeeded();
            this.EnableSelectIfNeeded();
        }

        /// <summary>
        /// Left mouse button event handler.
        /// Move window to the specific position.
        /// </summary>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// List selection changed event handler.        
        /// </summary>
        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.listBox1.SelectedItem != null)
            {
                this.descriptionTextBlock.Inlines.Clear();
                this.descriptionTextBlock.Inlines.AddRange(((FileTypeUserChanges)this.listBox1.SelectedItem).Description);
            }
        }

        /// <summary>
        /// Select All event handler.
        /// </summary>        
        private void selectAllButton_Click(object sender, RoutedEventArgs e)
        {
            this.stopCheck = true;
            this.SelectAll();
            this.stopCheck = false;

            this.CheckBox_Click(null, null);
        }

        /// <summary>
        /// Deselect All event handler.
        /// </summary>        
        private void deselectAllButton_Click(object sender, RoutedEventArgs e)
        {
            this.stopCheck = true;
            this.DeselectAll();
            this.stopCheck = false;

            this.CheckBox_Click(null, null);
        }
    }
}
