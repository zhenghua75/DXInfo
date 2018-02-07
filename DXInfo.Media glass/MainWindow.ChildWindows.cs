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

namespace Media_glass
{
    partial class MainWindow
    {
        #region Media window, List window, Library window and other

        /// <summary>
        /// Window that plays video.
        /// </summary>
        MediaWindow mediaWindow = null;

        /// <summary>
        /// Window that show play list.
        /// </summary>
        ListWindow listWindow = null;

        /// <summary>
        /// File type setting window.
        /// </summary>
        FileTypesWindow fileTypesWindow = null;

        /// <summary>
        /// Window with user help.
        /// </summary>
        HelpWindow helpWindow = null;

        /// <summary>
        /// About program.
        /// </summary>
        AboutWindow aboutWindow = null;

        /// <summary>
        /// Control window z oreder
        /// //http://stackoverflow.com/questions/1532142/need-to-control-z-order-of-windows-within-wpf-application
        /// </summary>        
        Window GetWindowHostControl()
        {
            return Window.GetWindow(this);
        }

        /// <summary>
        /// Create window for show video.
        /// </summary>
        private void CreateMediaWindow()
        {
            if (this.mediaWindow == null)
            {
                this.mediaWindow = new MediaWindow() { LibraryManager = this };
                this.mediaWindow.WindowStartupLocation = WindowStartupLocation.Manual;      

                this.mediaWindow.Left = -10000;
                this.mediaWindow.Top = -10000;

                this.mediaWindow.Show();
                this.mediaWindow.Hide();
                //this.mediaWindow.Owner = this.GetWindowHostControl();

                if (Settings.Default.MediaLeft == -1 || Settings.Default.MediaTop == -1 || Settings.Default.MediaWidth == -1 || Settings.Default.MediaHeight == -1)
                {
                    this.mediaWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    this.mediaWindow.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.mediaWindow.ActualWidth) / 2;
                    this.mediaWindow.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.mediaWindow.ActualHeight) / 2;
                }
                else
                {
                    this.mediaWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                    this.mediaWindow.Left = Settings.Default.MediaLeft;
                    this.mediaWindow.Top = Settings.Default.MediaTop;
                    this.mediaWindow.Width = Settings.Default.MediaWidth;
                    this.mediaWindow.Height = Settings.Default.MediaHeight;
                }

                this.mediaWindow.OnMediaOpened += mediaWindow_OnMediaOpened;
                this.mediaWindow.OnPositionChanged += mediaWindow_OnPositionChanged;
                this.mediaWindow.Closed += new EventHandler(mediaWindow_Closed);
                this.mediaWindow.OnMediaEnded += new EventHandler(mediaWindow_OnMediaEnded);

                this.mediaWindow.Volume = this.volumeSlider.Value;
            }
            else
                this.mediaWindow.Focus();
        }

        /// <summary>
        /// When media window closes - save it changes.
        /// </summary>        
        void mediaWindow_Closed(object sender, EventArgs e)
        {
            Settings.Default.MediaLeft = this.mediaWindow.Left;
            Settings.Default.MediaTop = this.mediaWindow.Top;
            Settings.Default.MediaWidth = this.mediaWindow.ActualWidth;
            Settings.Default.MediaHeight = this.mediaWindow.ActualHeight;

            Settings.Default.Save();

            this.mediaWindow = null;
        }

        /// <summary>
        /// Create play list window.
        /// </summary>
        private void CreateListWindow()
        {
            if (this.listWindow == null)
            {
                this.listWindow = new ListWindow() { LibraryManager = this };
                this.listWindow.Owner = this.GetWindowHostControl();
                this.listWindow.Show();
                this.listWindow.Closed += new EventHandler(listWindow_Closed);
            }
            else
                this.listWindow.Focus();
        }

        /// <summary>
        /// Deselect checboxes and context menu item.
        /// </summary>        
        void listWindow_Closed(object sender, EventArgs e)
        {
            this.IsPlayListCheckBoxChecked = false;
            this.listWindow = null;
        }

        /// <summary>
        /// Create File Type Window.
        /// </summary>
        private void CreateFileTypesWindow()
        {
            if (this.fileTypesWindow != null)
                this.fileTypesWindow.Focus();
            else
            {
                this.fileTypesWindow = new FileTypesWindow();
                //this.fileTypesWindow.Owner = this.GetWindowHostControl();
                this.fileTypesWindow.Show();
                this.fileTypesWindow.Closed += new EventHandler(fileTypesWindow_Closed);
            }
        }

        /// <summary>
        /// Make value empty.
        /// </summary>        
        void fileTypesWindow_Closed(object sender, EventArgs e)
        {
            this.fileTypesWindow = null;
        }

        /// <summary>
        /// Create File Type Window.
        /// </summary>
        private void CreateHelpWindow()
        {
            if (this.helpWindow != null)
            {
                this.helpWindow.WindowState = WindowState.Normal;
                this.helpWindow.Focus();
            }
            else
            {
                this.helpWindow = new HelpWindow();
                this.helpWindow.Show();
                this.helpWindow.Closed += new EventHandler(helpWindow_Closed);
            }
        }

        /// <summary>
        /// Make value empty.
        /// </summary>  
        void helpWindow_Closed(object sender, EventArgs e)
        {
            this.helpWindow = null;
        }

        /// <summary>
        /// Create File Type Window.
        /// </summary>
        private void CreateAboutWindow()
        {
            if (this.aboutWindow != null)
                this.aboutWindow.Focus();
            else
            {
                this.aboutWindow = new AboutWindow();
                //this.aboutWindow.Owner = this.GetWindowHostControl();
                this.aboutWindow.Show();
                this.aboutWindow.Closed += new EventHandler(aboutWindow_Closed);
            }
        }

        /// <summary>
        /// Make value empty.
        /// </summary>  
        void aboutWindow_Closed(object sender, EventArgs e)
        {
            this.aboutWindow = null;
        }

        /// <summary>
        /// Close video window.
        /// </summary>
        void CloseMediaWindow()
        {
            if (this.mediaWindow != null)
                this.mediaWindow.Close();
        }
        /// <summary>
        /// Close play list window.
        /// </summary>
        void CloseListWindow()
        {
            if (this.listWindow != null)
                this.listWindow.Close();
        }

        /// <summary>
        /// Close window with file types.
        /// </summary>
        void CloseFileTypesWindow()
        {
            if (this.fileTypesWindow != null)
                this.fileTypesWindow.Close();
        }

        /// <summary>
        /// Close help window.
        /// </summary>
        void CloseHelpWindow()
        {
            if (this.helpWindow != null)
                this.helpWindow.Close();
        }

        /// <summary>
        /// Close about window.
        /// </summary>
        void CloseAboutWindow()
        {
            if (this.aboutWindow != null)
                this.aboutWindow.Close();
        }

        #endregion                    
    }
}
