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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using Media_glass.Properties;
using System.Windows.Media.Effects;
using System.Media;
using System.Windows.Threading;
using Media_glass.Common.Helpers;
using Media_glass.Common;
using Media_glass.Common.Enums;


namespace Media_glass
{
    /// <summary>
    /// Interaction logic for MediaWindow.xaml
    /// Show video.
    /// </summary>
    public partial class MediaWindow : Window
    {
        #region Initialization

        /// <summary>
        /// Constructor -  initializes the instance of list window
        /// </summary>
        public MediaWindow()
        {
            InitializeComponent();

            //Help raise the event if needed.
            timer.Interval = 1;
            timer.Tick += new EventHandler(timer_Tick);
        
        }

        /// <summary>
        /// If position changed we raise the event.
        /// </summary>        
        void timer_Tick(object sender, EventArgs e)
        {
            if (this.mediaElement.Position != this.oldPosition && this.OnPositionChanged != null)
                this.OnPositionChanged(this.mediaElement.Position.TotalMilliseconds);
        }

        #endregion

        #region Fields

        /// <summary>
        /// When media opened we raise this event
        /// </summary>
        public event Action<double> OnMediaOpened;

        /// <summary>
        /// When position changes we raise this event.
        /// </summary>
        public event Action<double> OnPositionChanged;

        /// <summary>
        /// When media ended we raise this event.
        /// </summary>
        public event EventHandler OnMediaEnded;

        /// <summary>
        /// Timer that helps position changes event.
        /// </summary>
        Timer timer = new Timer();

        /// <summary>
        /// Help to determinate position changed.
        /// </summary>
        TimeSpan oldPosition;

        /// <summary>
        /// Minimum window width.
        /// </summary>
        const double minWidth = 100;

        /// <summary>
        /// Minimum window height.
        /// </summary>
        const double minHeight = 100;

        #endregion

        #region Properties

        ILibraryManager libraryManager = null;

        /// <summary>
        /// Common window library.
        /// </summary>
        public ILibraryManager LibraryManager
        {
            get
            {
                return this.libraryManager;
            }

            set
            {
                this.libraryManager = value;
            }
        }

        /// <summary>
        /// Played media source.
        /// </summary>
        public Uri Source
        {
            get { return this.mediaElement.Source; }
            set { this.mediaElement.Source = value; }
        }

        /// <summary>
        /// Current played volume.
        /// </summary>
        public double Volume
        {
            get { return this.mediaElement.Volume; }
            set { this.mediaElement.Volume = value; }
        }

        /// <summary>
        /// Speed played ratio.
        /// </summary>
        public double SpeedRatio
        {
            get { return this.mediaElement.SpeedRatio; }
            set { this.mediaElement.SpeedRatio = value; }
        }

        /// <summary>
        /// Does video update when it stopped and we change position.
        /// </summary>
        public bool ScrabbingEnabled
        {
            set
            {
                this.mediaElement.ScrubbingEnabled = value;
            }
        }
        /// <summary>
        /// Has current media video or not. Need to determinate shold we get Video info or not.
        /// </summary>
        public bool HasVideo
        {
            get { return this.mediaElement.HasVideo; }
        }

        /// <summary>
        /// Get natural video Width
        /// </summary>
        public int VideoWidth
        {
            get { return this.mediaElement.NaturalVideoWidth; }
        }

        /// <summary>
        /// Get natural video Width
        /// </summary>
        public int VideoHeight
        {
            get { return this.mediaElement.NaturalVideoHeight; }
        }

        /// <summary>
        /// Get duration in milliseconds
        /// </summary>
        public double Duration
        {
            get { return this.mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Play media.
        /// </summary>
        public void Play()
        {
            //this.mediaElement.ScrubbingEnabled = false;         
            this.mediaElement.Play();

            timer.Start();
            this.oldPosition = this.mediaElement.Position;
        }

        /// <summary>
        /// Pause played media.
        /// </summary>
        public void Pause()
        {
            // The Pause method pauses the media if it is currently running.
            // The Play method can be used to resume.
            this.mediaElement.Pause();

            this.timer.Stop();
        }

        /// <summary>
        /// Stop the media.
        /// </summary>
        public void Stop()
        {
            // The Stop method stops and resets the media to be played from
            // the beginning.
            this.mediaElement.Stop();

            if (this.OnPositionChanged != null)
                this.OnPositionChanged(this.mediaElement.Position.TotalMilliseconds);

            this.timer.Stop();
        }

        /// <summary>
        /// Jump to different parts of the media (seek to). 
        /// </summary>
        /// <param name="milliseconds"></param>
        public void SeekTo(int milliseconds)
        {
            // Overloaded constructor takes the arguments days, hours, minutes, seconds, miniseconds.
            // Create a TimeSpan with miliseconds equal to the slider value.
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, milliseconds);
            this.mediaElement.Position = ts;
        }
        /// <summary>
        /// Resize media window, when we wheel the mouse.
        /// </summary>
        /// <param name="eDelta">Mouse delta.</param>
        public void Resize(int eDelta)
        {
            int delta = eDelta / 120 * 24;

            double newWidth = this.ActualWidth + delta;

            if (newWidth < minWidth)
                newWidth = minWidth;

            double newHeight = this.ActualHeight * newWidth / this.ActualWidth;

            if (newHeight < minHeight)
            {
                newHeight = minHeight;
                newWidth = this.ActualWidth * newHeight / this.ActualHeight;
            }

            WindowInteropHelper helper = new WindowInteropHelper(this);
            SetWindowPos(helper.Handle, IntPtr.Zero, (int)this.Left, (int)this.Top, (int)newWidth, (int)newHeight, SWP.SHOWWINDOW);
        }

        /// <summary>
        /// Get played media type and resolution.
        /// </summary>
        public MediaInfo GetPlayedMediaInfo()
        {
            MediaInfo mi = new MediaInfo();

            if (this.HasVideo)
            {
                mi.MediaType = MediaType.Video;
                mi.Resolution = String.Format("{0} x {1}", this.VideoWidth, this.VideoHeight);
            }
            else
                mi.MediaType = MediaType.Audio;

            return mi;
        }

        #endregion

        #region window event handlers

        /// <summary>
        /// Media open event handler.
        /// Raise when media is opened.
        /// </summary>       
        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {

            if (this.mediaElement.HasVideo)
                this.mediaElement.ScrubbingEnabled = true;

            //Set played media duration.        

            if (this.OnMediaOpened != null)
                this.OnMediaOpened(this.mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds);
        }

        /// <summary>
        /// Media ended event handler.
        /// Stop the media playing when we ended.
        /// </summary>        
        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            Stop();

            if (this.OnMediaEnded != null)
                this.OnMediaEnded(sender, e);

        }

        /// <summary>
        /// Exception event handler.
        /// </summary>        
        private void mediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            if (sender is MediaElement)
            {
                MediaElement me = sender as MediaElement;

                MyMessageBox.Show(me.Source.LocalPath + "\n" +
                    e.ErrorException.Message +
                    "\n该文件不能播放 !", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            //throw new Exception("WPF Media Element can't play media stream.");
        }
        /// <summary>
        /// Window closed event handler.
        /// Stop the music.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            this.Stop();                   
        }

        /// <summary>
        /// Window size changes event handles.
        /// Use resolution to show played video.s
        /// </summary>        
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.mediaElement.Width = this.ActualWidth;
            this.mediaElement.Height = this.ActualHeight;
            this.mediaElement.Stretch = Stretch.Uniform;
        }

        /// <summary>
        /// Interop native function to change width and height at the same time.
        /// </summary>        
        [DllImport("user32.dll", SetLastError = true)]
        public extern static int SetWindowPos(IntPtr hWnd, IntPtr pos, int X, int Y, int cx, int cy, SWP uFlags);

        /// <summary>
        /// Interopted enum.
        /// </summary>
        [Flags()]
        public enum SWP : int
        {
            NOSIZE = 0x0001,
            NOMOVE = 0x0002,
            NOZORDER = 0x0004,
            NOREDRAW = 0x0008,
            NOACTIVATE = 0x0010,
            FRAMECHANGED = 0x0020,
            SHOWWINDOW = 0x0040,
            HIDEWINDOW = 0x0080,
            NOCOPYBITS = 0x0100,
            NOOWNERZORDER = 0x0200,
            NOSENDCHANGING = 0x0400,
            DRAWFRAME = FRAMECHANGED,
            NOREPOSITION = NOOWNERZORDER,
            DEFERERASE = 0x2000,
            ASYNCWINDOWPOS = 0x4000
        }

        /// <summary>
        /// Window mouse wheel event handler.
        /// Resize the window on mouse delta.
        /// </summary>        
        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            this.Resize(e.Delta);
        }

        #endregion

        #region Gesture key

        /// <summary>
        /// Gesture key event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //common events

            if (e.Key == Key.P && Keyboard.Modifiers == ModifierKeys.Control)
                this.LibraryManager.OpenPlayListWindow();
            else if (e.Key == Key.O && Keyboard.Modifiers == ModifierKeys.Control)
                this.LibraryManager.OpenFiles();
        }

        #endregion

        #region Drag and drop

        /// <summary>
        /// Handle dropped files.
        /// </summary>        
        private void Window_Drop(object sender, System.Windows.DragEventArgs e)
        {
            string[] fileNames = e.Data.GetData(System.Windows.DataFormats.FileDrop, true) as string[];

            if (fileNames != null)
                this.LibraryManager.OpenFiles(fileNames);
        }

        #endregion

        #region Context menu

        /// <summary>
        /// Full screen window context menu has been opened.
        /// </summary>        
        private void contextMenu_Opened(object sender, RoutedEventArgs e)
        {
            //Load history

            List<string> files = History.GetRecentPlayedList();

            this.recentFilesMenuItem.Items.Clear();

            if (files.Count > 0)
            {
                this.recentFilesMenuItem.IsEnabled = true;

                System.Windows.Controls.MenuItem mi = null;

                for (int i = 0; i < files.Count; ++i)
                {
                    mi = new System.Windows.Controls.MenuItem();
                    mi.Header = String.Format("{0} {1}", i + 1, files[i]);
                    mi.Tag = files[i];
                    mi.Click += (object sender1, RoutedEventArgs e1) =>
                    {
                        System.Windows.Controls.MenuItem clickedMi = (System.Windows.Controls.MenuItem)sender1;
                        this.LibraryManager.PlayFile((string)clickedMi.Tag);
                    };

                    this.recentFilesMenuItem.Items.Add(mi);
                }

                this.recentFilesMenuItem.Items.Add(new Separator());

                mi = new System.Windows.Controls.MenuItem();
                mi.Header = "Clear history";
                mi.Click += (object sender1, RoutedEventArgs e1) =>
                {
                    History.Clear();
                };

                this.recentFilesMenuItem.Items.Add(mi);
            }
            else
                this.recentFilesMenuItem.IsEnabled = false;

            //Load repeat settings

            this.repeatAllMenuItem.IsChecked =
            this.repeatOffMenuItem.IsChecked =
            this.repeatOneMenuItem.IsChecked =
            this.randomMenuItem.IsChecked = false;

            if (this.LibraryManager.IsRepeatAll)
                this.repeatAllMenuItem.IsChecked = true;

            if (this.LibraryManager.IsRepeatOne)
                this.repeatOneMenuItem.IsChecked = true;

            if (this.LibraryManager.IsRepeatOff)
                this.repeatOffMenuItem.IsChecked = true;

            if (this.LibraryManager.IsRandom)
                this.randomMenuItem.IsChecked = true;
        }

        /// <summary>
        /// Open a file.
        /// </summary>        
        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            this.LibraryManager.OpenFile();
        }

        /// <summary>
        /// Set repeat mode to repeat off.
        /// </summary>        
        private void MenuItem_RepeatOff_Click(object sender, RoutedEventArgs e)
        {
            this.LibraryManager.IsRepeatOff = true;
        }

        /// <summary>
        /// Set repeat mode to repeat one.
        /// </summary>        
        private void MenuItem_RepeatOne_Click(object sender, RoutedEventArgs e)
        {
            this.LibraryManager.IsRepeatOne = true;
        }

        /// <summary>
        /// Set repeat mode to repeat all.
        /// </summary>                
        private void MenuItem_RepeatAll_Click(object sender, RoutedEventArgs e)
        {
            this.LibraryManager.IsRepeatAll = true;
        }

        /// <summary>
        /// Set repeat mode to random.
        /// </summary>                
        private void MenuItem_Random_Click(object sender, RoutedEventArgs e)
        {
            this.LibraryManager.IsRandom = true;
        }

        /// <summary>
        /// Choose file types.
        /// </summary>        
        private void MenuItem_Types_Click(object sender, RoutedEventArgs e)
        {
            this.LibraryManager.OpenFileTypesWindow();
        }

        /// <summary>
        /// Open help.
        /// </summary>        
        private void MenuItem_Help_Click(object sender, RoutedEventArgs e)
        {
            this.LibraryManager.OpenHelpWindow();
        }

        /// <summary>
        /// About.
        /// </summary>        
        private void MenuItem_About_Click(object sender, RoutedEventArgs e)
        {
            this.LibraryManager.OpenAboutWindow();
        }

        /// <summary>
        /// Exit from application.
        /// </summary>        
        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.LibraryManager.Exit();
        }

        /// <summary>
        /// Play click.
        /// </summary>        
        private void MenuItem_Play_Click(object sender, RoutedEventArgs e)
        {
            this.LibraryManager.PlayClick();
        }

        /// <summary>
        /// Stop click.
        /// </summary>        
        private void MenuItem_Stop_Click(object sender, RoutedEventArgs e)
        {
            this.LibraryManager.StopClick();
        }

        /// <summary>
        /// Previous click.
        /// </summary>        
        private void MenuItem_Previous_Click(object sender, RoutedEventArgs e)
        {
            this.LibraryManager.PreviousClick();
        }

        /// <summary>
        /// Next click.
        /// </summary>        
        private void MenuItem_Next_Click(object sender, RoutedEventArgs e)
        {
            this.LibraryManager.NextClick();
        }

        /// <summary>
        /// Open property window.
        /// </summary>        
        private void MenuItem_Properties_Click(object sender, RoutedEventArgs e)
        {
            string filePath = this.mediaElement.Source.LocalPath;
            string fileName = new FileInfo(filePath).Name;
            MediaInfo mi = this.GetPlayedMediaInfo();
            string time = ShortTime.TimeToString(this.mediaElement.NaturalDuration.TimeSpan);
            this.LibraryManager.ShowProperties(fileName, filePath, mi.MediaType, time, mi.Resolution);
        }

        #endregion

        #region Fullscreen mode            
                
        /// <summary>
        /// Hide cursor time.
        /// </summary>
        System.Timers.Timer cursorTimer = new System.Timers.Timer();        

        /// <summary>
        /// Window state changed event handler.
        /// </summary>        
        private void Window_StateChanged(object sender, EventArgs e)
        {
                //Show cursor, hide panel.
                this.ShowCursor();
                this.StartHideCursor();
        }

        /// <summary>
        /// After 3 seconds cursor will be hide.
        /// </summary>
        public void StartHideCursor()
        {
            if (cursorTimer != null && cursorTimer.Enabled)
            {
                cursorTimer.Stop();
                cursorTimer.Close();
            }

            cursorTimer = new System.Timers.Timer();
            cursorTimer.Interval = 3000;
            cursorTimer.AutoReset = false;
            cursorTimer.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) =>
            {
                this.HideCursor();
            };
            cursorTimer.Start();   
        }

        /// <summary>
        /// Show cursor.
        /// </summary>
        public void ShowCursor()
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
            {
                Mouse.OverrideCursor = null;// System.Windows.Input.Cursors.Arrow;
            }));           
        }

        /// <summary>
        /// Hide cursor.
        /// </summary>
        public void HideCursor()
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.None;
            }));                      
        }

        /// <summary>
        /// Is cursor visible.
        /// </summary>
        /// <returns></returns>
        bool IsCursorHidden()
        {
            return Mouse.OverrideCursor == System.Windows.Input.Cursors.None;
        }

        #endregion
    }
}
