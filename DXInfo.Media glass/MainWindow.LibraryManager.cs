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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Xml;
using Media_glass.SmallWindows;
using Media_glass.Properties;
using System.Windows.Media.Animation;
using System.IO;
using System.Windows.Media.Effects;
using System.Timers;
using System.Windows.Threading;
using System.Globalization;
using Media_glass.Common.Data;
using Media_glass.Common;
using Media_glass.Common.Enums;

namespace Media_glass
{
    public partial class MainWindow : ILibraryManager
    {
        #region ILibraryManager interface implementation

        /// <summary>
        /// Read / write data from xml library file.
        /// </summary>
        DbLibrary library = new DbLibrary();

        /// <summary>
        /// Load play list from file
        /// </summary>        
        public void LoadPlayList(ItemCollection items)
        {
            this.library.LoadPlayList(items);
        }

        /// <summary>
        /// Open play list window.
        /// </summary>
        public void OpenPlayListWindow()
        {
            this.IsPlayListCheckBoxChecked = !this.IsPlayListCheckBoxChecked;
        }

        /// <summary>
        /// Let user to open files he want. Using for gesture key.
        /// </summary>
        public void OpenFiles()
        {
            System.Windows.Forms.OpenFileDialog dlg = MediaOpenFileDialog.New;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //if window isn't open
                if (this.IsPlayListCheckBoxChecked == false)
                    this.OpenPlayListWindow();
                else
                    this.listWindow.Focus();

                this.listWindow.OpenFiles(dlg.FileNames);
            }
        }

        /// <summary>
        /// Open and play selected file.
        /// </summary>
        public void OpenFile()
        {
            this.openButton_Click(null, null);
        }

        /// <summary>
        /// Open selected files in play list window.
        /// </summary>
        public void OpenFiles(string[] files)
        {
            if (this.IsPlayListCheckBoxChecked == false)
                this.OpenPlayListWindow();
            else
                this.listWindow.Focus();

            this.listWindow.OpenFiles(files);
        }

        /// <summary>
        /// Let user to open directory for playing. Using for gesture key.
        /// </summary>
        public void OpenDirectory()
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            //dlg.InitialDirectory = "c:\\";
            //dlg.Filter = "Media files (*.wmv)|*.wmv|All Files (*.*)|*.*";            

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //if window isn't open
                if (this.IsPlayListCheckBoxChecked == false)
                    this.OpenPlayListWindow();
                else
                    this.listWindow.Focus();

                this.listWindow.OpenFiles(Directory.GetFiles(fbd.SelectedPath));
            }
        }

        /// <summary>
        /// Play list id that opened.
        /// </summary>
        int playedListId = -1;

        /// <summary>
        /// Play list id that opened.
        /// </summary>
        public int PlayedListIndex
        {
            get
            {
                return this.playedListId;
            }
            set
            {
                this.playedListId = value;
            }
        }

        /// <summary>
        /// Played media id.
        /// </summary>
        int playedMediaId = -1;

        /// <summary>
        /// Played media index.
        /// </summary>
        public int PlayedMediaIndex
        {
            get
            {
                return this.playedMediaId;
            }
            set
            {
                this.playedMediaId = value;
            }
        }

        /// <summary>
        /// Play the specific file
        /// </summary>
        /// <param name="fileName">file name to play</param>
        public void PlayFile(string fileName)
        {
            if (!File.Exists(fileName))
            {                
                //MyMessageBox.Show(fileName+"文件不存在!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CreateMediaWindow();

            //we play a video. and double click on file. we've got this event.
            //ScrabbingEnabled set to true only when media opened
            //so we've got don't scrabbing video.
            //this.mediaWindow.Source.LocalPath != fileName - say that we already open this video
            if (this.mediaWindow.Source != null && this.mediaWindow.Source.LocalPath != fileName)
            {
                this.mediaWindow.ScrabbingEnabled = false;
                this.VideoSize = null;
            }

            //if it's a first open file 
            if (this.mediaWindow.Source == null)
                this.mediaWindow.ScrabbingEnabled = false;

            this.mediaWindow.Source = new Uri(fileName);
            this.mediaWindow.Volume = this.volumeSlider.Value;

            playButton_Click(null, null);

            this.RunningCurrentPlayedMediaInfo/*this.runLabel.Content*/ = System.IO.Path.GetFileName(fileName);

            History.Add(fileName);
        }

        /// <summary>
        /// Clear current played item in List View window.
        /// </summary>
        public void ClearCurrentPlayedMediaItem()
        {
            if (this.listWindow != null)
            {
                this.listWindow.PlayedMediaItem = null;
                this.listWindow.ClearAllSelectedItem();
            }
            else
                this.PlayedMediaIndex = -1;
        }

        /// <summary>
        /// Close played media item.
        /// </summary>
        public void ClosePlayedMedia()
        {
            //if something is played stop it.
            if (this.mediaWindow != null && this.mediaWindow.Source != null)
            {
                //timer run in another thread
                //so we stop it very unusual
                this.stopRunMediaInfo = true;
                this.runningMediaInfoTimer.Start();

                this.stopButton_Click(null, null);
                this.CloseMediaWindow();
                //this.mediaWindow.Hide();
                //this.mediaWindow.Source = null;

                //restore old settings
                //SetTitle();
                this.positionSlider.Value = 0;
                this.positionSlider.Maximum = 3;
                this.timeLabel.Content = "00:00:00 / 00:00:00";
                this.videoSize = null;

                //clear current played item.
                ClearCurrentPlayedMediaItem();
            }
        }

        #endregion

        #region ILibraryManager Members        
        /// <summary>
        /// Open help window.
        /// </summary>
        public void OpenHelpWindow()
        {
            this.MenuItem_Help_Click(null, null);
        }

        /// <summary>
        /// Open about window.
        /// </summary>
        public void OpenAboutWindow()
        {
            this.MenuItem_About_Click(null, null);
        }

        /// <summary>
        /// Exit from applcation.
        /// </summary>
        public new void Exit()
        {
            this.MenuItem_Exit_Click(null, null);
        }

        /// <summary>
        /// Open window with file types.
        /// </summary>
        public void OpenFileTypesWindow()
        {
            this.MenuItem_Types_Click(null, null);
        }        
        
        /// <summary>
        /// Is it at repeat one mode.
        /// </summary>
        public bool IsRepeatOne
        {
            get
            {
                return this.repeatOneMenuItem.IsChecked;
            }
            set
            {
                this.repeatOneMenuItem.IsChecked = value;
                this.MenuItem_RepeatOne_Click(null, null);
            }
        }

        /// <summary>
        /// Is it at repeat all mode.
        /// </summary>
        public bool IsRepeatAll
        {
            get
            {
                return this.repeatAllMenuItem.IsChecked;
            }
            set
            {
                this.repeatAllMenuItem.IsChecked = value;
                this.MenuItem_RepeatAll_Click(null, null);
            }
        }

        /// <summary>
        /// Is it at repeat off mode.
        /// </summary>
        public bool IsRepeatOff
        {
            get
            {
                return this.repeatOffMenuItem.IsChecked;
            }
            set
            {
                this.repeatOffMenuItem.IsChecked=value;
                this.MenuItem_RepeatOff_Click(null, null);
            }
        }

        /// <summary>
        /// Is it at random mode.
        /// </summary>
        public bool IsRandom
        {
            get
            {
                return this.randomMenuItem.IsChecked;
            }
            set
            {
                this.randomMenuItem.IsChecked = value;
                this.MenuItem_Random_Click(null, null);
            }
        }

        /// <summary>
        /// Is media played now.
        /// </summary>
        public bool IsPlayed
        {
            get
            {
                return this.PlayedNowPauseIsVisible;
            }
        }

        /// <summary>
        /// Click on play button.
        /// </summary>
        public void PlayClick()
        {
            if (this.PlayedNowPauseIsVisible)
                this.pauseButton_Click(null, null);
            else
                this.playButton_Click(null, null);
        }

        /// <summary>
        /// Click on stop button.
        /// </summary>
        public void StopClick()
        {
            this.stopButton_Click(null, null);
        }

        /// <summary>
        /// Click on previous button.
        /// </summary>
        public void PreviousClick()
        {
            this.previousButton_Click(null, null);
        }

        /// <summary>
        /// Click on next button.
        /// </summary>
        public void NextClick()
        {
            this.nextButton_Click(null, null);
        }       
        
        /// <summary>
        /// Show file property.
        /// </summary>        
        public void ShowProperties(string fileDisplayedName, string fileLocation, MediaType mediaType, string duration, string resolution)
        {
            if (!File.Exists(fileLocation))
            {
                MyMessageBox.Show("文件不存在 !", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            FilePropertiesWindow fpw = new FilePropertiesWindow();
            fpw.FileDisplayedName = fileDisplayedName;
            fpw.FileLocation = fileLocation;
            fpw.MediaType = mediaType;
            fpw.Duration = duration;
            fpw.Resolution = resolution;
            fpw.ShowDialog();
        }               

        /// <summary>
        /// Set volume.
        /// </summary>        
        public void SetVolume(double value)
        {
            this.volumeSlider.Value = value;
            volumeSlider_ValueChanged(null, null);
        }
        
        /// <summary>
        /// Get current volume.
        /// </summary>        
        public double GetVolume()
        {
            return this.volumeSlider.Value;
        }

        #endregion
         
    }
}
