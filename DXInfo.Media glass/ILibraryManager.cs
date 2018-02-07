#region License

//Media glass - my simple WPF player
//Copyright (C) 2008-2009 Denis Yakimenko <denyakimenko@yandex.ru>

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
using System.Windows.Controls;
using Media_glass.Common.Enums;


namespace Media_glass
{
    /// <summary>
    /// Library manager - help to manage one window from another.
    /// </summary>
    public interface ILibraryManager
    {
        #region common methods and properties

        /// <summary>
        /// Play the specific file.
        /// </summary>
        /// <param name="fileName">File name to play.</param>
        void PlayFile(string fileName);

        /// <summary>
        /// Clear current played item in List View window.
        /// </summary>
        void ClearCurrentPlayedMediaItem();

        /// <summary>
        /// Close played media item.
        /// </summary>
        void ClosePlayedMedia();

        #endregion

        #region load / write data to .xml

        /// <summary>
        /// Load play list xml data to UI.
        /// </summary>        
        void LoadPlayList(ItemCollection items);
        #endregion

        #region for repeat operation

        /// <summary>
        /// Play list in which item is played.
        /// </summary>
        int PlayedListIndex { get; set; }

        /// <summary>
        /// Played item play list index.
        /// </summary>
        int PlayedMediaIndex { get; set; }

        #endregion

        #region need for all windows - gesture keys and etc.

        /// <summary>
        /// Open play list window.
        /// </summary>
        void OpenPlayListWindow();

        /// <summary>
        /// Show file dialog. And open selected file.
        /// </summary>
        void OpenFile();

        /// <summary>
        /// Show file dialog. And open selected files in play list window.
        /// </summary>
        void OpenFiles();

        /// <summary>
        /// Open selected files in play list window.
        /// </summary>
        void OpenFiles(string[] files);

        /// <summary>
        /// Show folder dialog. And open selected files in play list window.
        /// </summary>
        void OpenDirectory();

        #endregion

        #region for fullscreen mode

        /// <summary>
        /// Are we repeat current item.
        /// </summary>
        bool IsRepeatOne { get; set; }

        /// <summary>
        /// Are we repeat all. 
        /// </summary>
        bool IsRepeatAll { get; set; }

        /// <summary>
        /// Is there no repeat.
        /// </summary>
        bool IsRepeatOff { get; set; }

        /// <summary>
        /// Is it random.
        /// </summary>
        bool IsRandom { get; set; }

        /// <summary>
        /// Is our media is played and not stopped.
        /// </summary>
        bool IsPlayed { get; }

        /// <summary>
        /// Open help window.
        /// </summary>
        void OpenHelpWindow();

        /// <summary>
        /// Open about window.
        /// </summary>
        void OpenAboutWindow();

        /// <summary>
        /// Exit from application.
        /// </summary>
        void Exit();

        /// <summary>
        /// Open file types window.
        /// </summary>
        void OpenFileTypesWindow();

        /// <summary>
        /// Click on play button.
        /// </summary>
        void PlayClick();

        /// <summary>
        /// Click on stop button.
        /// </summary>
        void StopClick();

        /// <summary>
        /// Click on previous button.
        /// </summary>
        void PreviousClick();

        /// <summary>
        /// Click on next button.
        /// </summary>
        void NextClick();

        /// <summary>
        /// Show file property.
        /// </summary>
        void ShowProperties(string fileDisplayedName, string fileLocation, MediaType mediaType, string duration, string resolution);

        /// <summary>
        /// Get volume value.
        /// </summary>        
        double GetVolume();

        /// <summary>
        /// Set volume.
        /// </summary>        
        void SetVolume(double value);

        #endregion
    }
}
