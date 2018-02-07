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
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Media_glass.Common;
using Media_glass.Common.Enums;
using Media_glass.Common.Helpers;
using Media_glass.Common.Items_operations;
using Media_glass.Properties;


namespace Media_glass
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// This is main window. it contain library manager logic - common logic for 4 main windows.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Initialization
        /// <summary>
        /// Settings loading at the next order :
        /// - constructor ( main window )
        /// - Window_Loaded event
        /// - CreateMainWindow 
        /// 
        /// Settings saved at the next order :
        /// - Window_Closed
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            SetTitle();

            //restore settings
            this.volumeSlider.Value = Settings.Default.Volume;
            this.CurrentRepeatState = (Repeat)Settings.Default.RepeatState;
            this.PlayedListIndex = Settings.Default.PlayedListIndex;

            //Load window corner values. Centered if opened first.
            if (Settings.Default.MainLeft == -1 || Settings.Default.MainTop == -1)
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            else
            {
                this.WindowStartupLocation = WindowStartupLocation.Manual;
                this.Left = Settings.Default.MainLeft;
                this.Top = Settings.Default.MainTop;
            }

            //initialize varialbes for running played media info label
            runningMediaInfoTimer.Elapsed += new ElapsedEventHandler(running_media_info_timer_Elapsed);
            runningMediaInfoTimer.Interval = 150;
            runningMediaInfoFontFamily = this.runLabel.FontFamily.ToString();
            runningMediaInfoFontSize = this.runLabel.FontSize;
        }

        private HwndSource hwndSource;
        public ILibraryManager LibraryManager { get; set; }
        /// <summary>
        /// Load event handler.
        /// Show animation. Load settings.
        /// </summary>        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //open play list if needed
            this.IsPlayListCheckBoxChecked = Settings.Default.PlayListChecked;
            hwndSource = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            hwndSource.AddHook(new HwndSourceHook(WndProc));

            nextButton_Click(null, null);
        }
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case Win32.WM_COPYDATA:
                    //try
                    //{
                        //System.Windows.MessageBox.Show("copydata");
                        Win32.COPYDATASTRUCT cd = (Win32.COPYDATASTRUCT)Marshal.PtrToStructure(lParam, typeof(Win32.COPYDATASTRUCT));                        
                        this.PlayFile(cd.lpData);
                        handled = true;
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message);
                    //}
                    break;
            }

            return IntPtr.Zero;
        }

        /// <summary>
        /// Save changes before we close main window
        /// </summary>        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Default.PlayListChecked = this.playListCheckBox.IsChecked ?? false;
            Settings.Default.Volume = this.volumeSlider.Value;
            Settings.Default.RepeatState = (int)this.CurrentRepeatState;

            this.CloseMediaWindow();
            this.CloseListWindow();
            this.CloseFileTypesWindow();
            this.CloseHelpWindow();
            this.CloseAboutWindow();

            Settings.Default.PlayedListIndex = this.PlayedListIndex;

            Settings.Default.MainLeft = this.Left;
            Settings.Default.MainTop = this.Top;

            Settings.Default.Save();
        }

        /// <summary>
        /// Set window title.
        /// </summary>
        void SetTitle()
        {
            this.headerLabel.Content = Settings.Default.ProductName + " " + Settings.Default.ProductVersion;
            this.Title = this.headerLabel.Content.ToString();
            this.runLabel.Content = this.headerLabel.Content;
        }

        #endregion

        #region Buttons logic

        /// <summary>
        /// Opne button event handler.
        /// Open one file.
        /// </summary>        
        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = MediaOpenFileDialog.New;
            dlg.Multiselect = false;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFileName = dlg.FileName;
                this.ClearCurrentPlayedMediaItem();
                PlayFile(selectedFileName);
            }
        }

        /// <summary>
        /// Show pause button if media played.
        /// Show play button if media is not played
        /// </summary>
        bool PlayedNowPauseIsVisible
        {
            get
            {
                return this.pauseButton.Visibility == Visibility.Visible;
            }

            set
            {
                //media played now. pause is visible
                if (value)
                {
                    this.pauseButton.Visibility = Visibility.Visible;
                    this.playButton.Visibility = Visibility.Hidden;
                }
                //media is not played now. play is visible
                else
                {
                    this.pauseButton.Visibility = Visibility.Hidden;
                    this.playButton.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Play button click event handler.
        /// Play the media. Show pause button.
        /// </summary>        
        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.mediaWindow != null)
            {
                this.mediaWindow.Play();
                this.PlayedNowPauseIsVisible = true;
            }
            else
            {
                //openButton_Click(null, null);
                nextButton_Click(null, null);
            }
        }

        /// <summary>
        /// Stop button click event handler.
        /// Stop playing. Show play button.
        /// </summary>        
        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.mediaWindow != null)
                this.mediaWindow.Stop();

            this.PlayedNowPauseIsVisible = false;
        }

        /// <summary>
        /// Volume slider event handler.
        /// Volume slider position changed.
        /// </summary>        
        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.mediaWindow != null)
                this.mediaWindow.Volume = this.volumeSlider.Value;
        }

        /// <summary>
        /// Pause button event handler.
        /// Pause the media.
        /// </summary>        
        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.mediaWindow != null)
            {
                this.mediaWindow.Pause();
                this.PlayedNowPauseIsVisible = false;
            }
        }

        /// <summary>
        /// Left mouse button down event handler.
        /// Move the window as user want.
        /// </summary>        
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// Minimize button event handler.
        /// Minimize the window.
        /// </summary>        
        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        /// <summary>
        /// Close button event handler.
        /// Close the window.
        /// </summary>        
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Player slider logic

        /// <summary>
        /// Is slider captured
        /// </summary>
        bool IsSliderCaptured = false;

        /// <summary>
        /// Slider lost capture event handler.
        /// 
        /// 1. Standart property IsMouseCaptured has a bug
        /// 2. That's why slider position changed event by user process here
        /// 
        /// It helps to avoid from loops problem with slider position              
        /// i separete user and media window slider position changes into two different methods
        /// </summary>        
        private void positionSlider_LostMouseCapture_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.IsSliderCaptured = false;

            if (this.mediaWindow != null)
                this.mediaWindow.SeekTo((int)this.positionSlider.Value);
        }

        /// <summary>
        /// Not to raise event if we don't need it.
        /// </summary>
        bool dontRaiseMediaWindowEvent = false;

        /// <summary>
        /// Slider position changed event handler.
        /// </summary>        
        private void positionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //this is mean that we click on slider and we need to change media stream position
            if (!this.dontRaiseMediaWindowEvent && !this.IsSliderCaptured)
            {
                if (this.mediaWindow != null)
                    this.mediaWindow.SeekTo((int)this.positionSlider.Value);
            }

            TimeSpan curTime = new TimeSpan(0, 0, (int)(this.positionSlider.Value / 1000));
            TimeSpan allTime = new TimeSpan(0, 0, (int)(this.positionSlider.Maximum / 1000));

            this.timeLabel.Content = String.Format("{0} / {1}", curTime.ToString(), allTime.ToString());//this.positionSlider.Value.ToString();//time.ToShortTimeString();

            Rectangle r = (Rectangle)this.positionSlider.Template.FindName("playedPart", this.positionSlider);
            r.Width = this.positionSlider.ActualWidth / (this.positionSlider.Maximum - this.positionSlider.Minimum) * this.positionSlider.Value;
        }

        /// <summary>
        /// Event from media window 
        /// It helps to avoid loop problems with slider position  
        /// </summary>
        /// <param name="milliseconds">new position in stream</param>
        void mediaWindow_OnPositionChanged(double milliseconds)
        {
            if (!this.IsSliderCaptured)
            {
                this.dontRaiseMediaWindowEvent = true;
                this.positionSlider.Value = milliseconds;
                this.dontRaiseMediaWindowEvent = false;
            }
        }

        /// <summary>
        /// Media opened event handler.
        /// Set Media size when it opened.
        /// </summary>
        /// <param name="milliseconds"></param>
        void mediaWindow_OnMediaOpened(double milliseconds)
        {
            this.positionSlider.Maximum = milliseconds;

            if (this.mediaWindow.HasVideo)
                this.VideoSize = String.Format(" / Video size : {0} x {1}", this.mediaWindow.VideoWidth, this.mediaWindow.VideoHeight);
            else
                this.VideoSize = null;

            string time = ShortTime.TimeToString(new TimeSpan(0, 0, (int)(this.positionSlider.Maximum / 1000)));

            if (this.listWindow != null && this.listWindow.PlayedMediaItem != null)
            {
                ItemContent content = ((ItemContent)this.listWindow.PlayedMediaItem.Content);
                content.Time = time;
                MediaInfo mi = this.mediaWindow.GetPlayedMediaInfo();
                content.MediaType = mi.MediaType;
                content.Resolution = mi.Resolution;
                
            }
        }        

        /// <summary>
        /// Slider captured event handler.
        /// Slider is captured.
        /// </summary>        
        private void positionSlider_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.IsSliderCaptured = true;
        }

        #endregion

        #region Checkbox logic

        /// <summary>
        /// Show this tooltip when plat list hidden
        /// </summary>
        const string playListCheckedToolTip = "显示播放列表";

        /// <summary>
        /// Show this tooltip when play list visible
        /// </summary>
        const string playListUncheckedToolTip = "隐藏播放列表";

        /// <summary>
        /// Click don't event when checkbox checked property changed programmatically
        /// i don't want to use checked and unchecked events instead because this has advantadge too.
        /// </summary>
        bool? IsPlayListCheckBoxChecked
        {
            set
            {
                this.playListCheckBox.IsChecked = value;
                this.playListCheckBox_Click(null, null);
            }

            get
            {
                return this.playListCheckBox.IsChecked;
            }
        }

        /// <summary>
        /// Play list check event handler.
        /// Open play list or hide it.
        /// </summary>        
        private void playListCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (this.playListMenuItem.IsChecked != this.playListCheckBox.IsChecked)
                this.playListMenuItem.IsChecked = this.playListCheckBox.IsChecked ?? false;

            if (this.playListCheckBox.IsChecked == true)
            {
                this.CreateListWindow();
                this.playListCheckBox.ToolTip = playListUncheckedToolTip;
            }
            else
            {
                this.CloseListWindow();
                this.playListCheckBox.ToolTip = playListCheckedToolTip;
            }
        }


        #endregion

        //  Play list already exist ?  |    PlayListPath     |       Save play list           |               Save info to library                  |           Rename play list from play list window            |  
        //                             |                     |                                |    library window open    | library window close    |     library window open     | library window close          |
        // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //            Yes              | play list file path | 1. save to play list file      |              -            |            -            | 1. rename library item name | 1. rename name in library file| 
        //                             |                     |                                |                           |                         | using PlayListPath          | using PlayListPath            |
        //  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
        //                             |                     | 1. generate new play list file | 1. show save dialog       | 1. show save dialog     |                             |                               |
        //             No              |       null          | 2. save to play list file      | 2. save to library window | 1. save to library file |             -               |               -               |
        //                             |                     | 3. save info to library ->     |                           |                         |                             |                               |

        // So, we need :
        //
        // 1. PlayListPath property
        // 2. generate new play list file property
        // 3. save to play list file method ( XmlLibrarySource method )
        // 4. save info to library file method ( XmlLibrarySource method )
        // 5. play list rename method 
        // 6. load and save play list for library ( XmlLibrarySource method )
        // 7. get full path to file method ( XmlLibrarySource method )

        #region Context Menu Item Click event handler

        /// <summary>
        /// About context menu item event handler.
        /// Show about window.
        /// </summary>        
        private void MenuItem_About_Click(object sender, RoutedEventArgs e)
        {
            this.CreateAboutWindow();
        }


        /// <summary>
        /// Play list window context menu item event handler.
        /// Show play list.
        /// </summary>        
        private void MenuItem_PlayList_Click(object sender, RoutedEventArgs e)
        {
            this.IsPlayListCheckBoxChecked = this.playListMenuItem.IsChecked;
        }


        /// <summary>
        /// Add files  context menu item event handler.
        /// Add files to empty play list.
        /// </summary>       
        private void MenuItem_Add_files_Click(object sender, RoutedEventArgs e)
        {
            this.OpenFiles();
        }

        /// <summary>
        /// Open directory  context menu item event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Open_Directory_Click(object sender, RoutedEventArgs e)
        {
            this.OpenDirectory();
        }

        /// <summary>
        /// Repeat all, one, random or nothing.
        /// </summary>
        Repeat currentRepeatState = Repeat.All;

        /// <summary>
        /// Repeat all, one, random or nothing.
        /// </summary>
        Repeat CurrentRepeatState
        {
            get
            {
                return this.currentRepeatState;
            }

            set
            {
                this.repeatAllMenuItem.IsChecked = this.repeatOneMenuItem.IsChecked = this.repeatOffMenuItem.IsChecked = this.randomMenuItem.IsChecked = false;

                switch (value)
                {
                    case Repeat.All:
                        this.repeatAllMenuItem.IsChecked = true;
                        break;

                    case Repeat.Off:
                        this.repeatOffMenuItem.IsChecked = true;
                        break;

                    case Repeat.One:
                        this.repeatOneMenuItem.IsChecked = true;
                        break;

                    case Repeat.Random:
                        this.randomMenuItem.IsChecked = true;
                        break;
                }

                this.currentRepeatState = value;
            }
        }

        /// <summary>
        /// Repeat off menu item context menu item event handler.
        /// Repeat is off.
        /// </summary> 
        private void MenuItem_RepeatOff_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentRepeatState = Repeat.Off;
        }

        /// <summary>
        /// Repeat one menu item context menu item event handler.
        /// Repeat only one media.
        /// </summary> 
        private void MenuItem_RepeatOne_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentRepeatState = Repeat.One;
        }

        /// <summary>
        /// Repeat all menu item context menu item event handler.
        /// Repeat all items.
        /// </summary> 
        private void MenuItem_RepeatAll_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentRepeatState = Repeat.All;
        }

        /// <summary>
        /// Repeat random item context menu item event handler.
        /// Repeat random.
        /// </summary> 
        private void MenuItem_Random_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentRepeatState = Repeat.Random;
        }

        /// <summary>
        /// Close current opened media item.
        /// </summary>        
        private void MenuItem_Close_Click(object sender, RoutedEventArgs e)
        {
            this.ClosePlayedMedia();
        }

        /// <summary>
        /// Open file types settings window.
        /// </summary>        
        private void MenuItem_Types_Click(object sender, RoutedEventArgs e)
        {
            //FileTypesWindow fileTypes = new FileTypesWindow();
            //fileTypes.ShowDialog();
            this.CreateFileTypesWindow();
        }

        /// <summary>
        /// Exit. Close application.
        /// </summary>    
        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Open application help.
        /// </summary>        
        private void MenuItem_Help_Click(object sender, RoutedEventArgs e)
        {
            this.CreateHelpWindow();
        }

        #endregion

        #region Gesture key

        /// <summary>
        /// Gesture key event handler.
        /// </summary>  
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //common events

            if (e.Key == Key.P && Keyboard.Modifiers == ModifierKeys.Control)
                this.OpenPlayListWindow();
            else if (e.Key == Key.O && Keyboard.Modifiers == ModifierKeys.Control)
                this.OpenFiles();
        }

        #endregion

        #region Repeat logic

        /// <summary>
        /// Get played files list
        /// </summary>
        /// <returns></returns>
        List<string> GetPlayedList()
        {
            if (this.listWindow != null)
            {
                this.listWindow.LoadPlayList();
                List<string> playedFilesList = new List<string>();

                foreach (System.Windows.Controls.ListViewItem lvi in this.listWindow.ListItems)
                    playedFilesList.Add(lvi.Tag as string);

                return playedFilesList;
            }
            else
            {
                System.Windows.Controls.ListView lv = new System.Windows.Controls.ListView();
                ItemCollection items = lv.Items;
                this.library.LoadPlayList(items);

                List<string> playedFilesList = new List<string>();

                foreach (System.Windows.Controls.ListViewItem lvi in items)
                    playedFilesList.Add(lvi.Tag as string);

                return playedFilesList;
            }
        }

        /// <summary>
        /// Get played media index in the play list.
        /// </summary>
        /// <returns></returns>
        int GetPlayedMediaIndex()
        {
            if (this.listWindow == null)
                return this.PlayedMediaIndex;
            else
            {
                if (this.listWindow.PlayedMediaItem != null)
                {
                    ItemContent playedIc = this.listWindow.PlayedMediaItem.Content as ItemContent;
                    if (playedIc != null)
                    {
                        for (int i = 0; i < this.listWindow.ListItems.Count; i++)
                        {
                            System.Windows.Controls.ListViewItem lvi = this.listWindow.ListItems[i] as System.Windows.Controls.ListViewItem;
                            ItemContent ic = lvi.Content as ItemContent;
                            if (ic.Code == playedIc.Code && ic.Name == playedIc.Name)
                            {
                                return i;
                            }
                        }
                    }
                    //return this.listWindow.ListItems.IndexOf(this.listWindow.PlayedMediaItem);
                }
                return -1;
            }
        }

        /// <summary>
        /// Open previous media in the list. It depends on current Repeat mode.
        /// </summary>
        void OpenPreviousMedia()
        {
            List<string> playedFileList = GetPlayedList();

            if (playedFileList.Count == 0)
                return;

            int playedMediaIndex = GetPlayedMediaIndex();

            int nextMediaIndex = -1;

            switch (this.CurrentRepeatState)
            {
                case Repeat.All:
                    if (playedMediaIndex == 0)
                        nextMediaIndex = playedFileList.Count - 1;
                    else
                        nextMediaIndex = --playedMediaIndex;
                    break;

                case Repeat.One:
                    nextMediaIndex = playedMediaIndex;
                    break;

                case Repeat.Off:
                    if (playedMediaIndex == 0)
                        nextMediaIndex = -1;
                    else
                        nextMediaIndex = --playedMediaIndex;
                    break;

                case Repeat.Random:
                    nextMediaIndex = this.randomMediaIndexGenerator.Next(playedFileList.Count);
                    break;
            }

            if (nextMediaIndex != -1)
            {
                if (this.listWindow != null)
                    this.listWindow.SelectItem((System.Windows.Controls.ListViewItem)this.listWindow.ListItems[nextMediaIndex]);
                else
                    this.PlayedMediaIndex = nextMediaIndex;

                this.PlayFile(playedFileList[nextMediaIndex]);
            }
            else
                this.stopButton_Click(null, null);

        }

        /// <summary>
        /// Helps to get random media from list.
        /// </summary>
        Random randomMediaIndexGenerator = new Random();

        /// <summary>
        /// Open next media in the list. It depends on current Repeat mode.
        /// </summary>
        void OpenNextMedia()
        {
            List<string> playedFileList = GetPlayedList();

            if (playedFileList.Count == 0)
                return;

            int playedMediaIndex = GetPlayedMediaIndex();

            int nextMediaIndex = -1;

            switch (this.CurrentRepeatState)
            {
                case Repeat.All:
                    if (playedMediaIndex >= playedFileList.Count - 1)
                        nextMediaIndex = 0;
                    else
                        nextMediaIndex = ++playedMediaIndex;
                    break;

                case Repeat.One:
                    nextMediaIndex = playedMediaIndex;
                    break;

                case Repeat.Off:
                    if (playedMediaIndex >= playedFileList.Count - 1)
                        nextMediaIndex = -1;
                    else
                        nextMediaIndex = ++playedMediaIndex;
                    break;

                case Repeat.Random:
                    nextMediaIndex = this.randomMediaIndexGenerator.Next(playedFileList.Count);
                    break;
            }

            if (nextMediaIndex != -1)
            {
                if (this.listWindow != null)
                    this.listWindow.SelectItem((System.Windows.Controls.ListViewItem)this.listWindow.ListItems[nextMediaIndex]);
                else
                    this.PlayedMediaIndex = nextMediaIndex;

                this.PlayFile(playedFileList[nextMediaIndex]);
            }
            else
                this.stopButton_Click(null, null);
        }

        /// <summary>
        /// Next button event handler.
        /// Plays next media.
        /// </summary>        
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            OpenNextMedia();
        }

        /// <summary>
        /// Previous button event handler.
        /// Plays previous media in the list.
        /// </summary>        
        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            OpenPreviousMedia();
        }

        /// <summary>
        /// When media play ended - play next media
        /// </summary>        
        void mediaWindow_OnMediaEnded(object sender, EventArgs e)
        {
            pauseButton_Click(null, null);
            OpenNextMedia();
        }

        #endregion

        #region Running media info

        /// <summary>
        /// Running direction.
        /// </summary>
        enum RunningMediaInfoDirection
        {
            Left,
            Right,
            None
        }

        /// <summary>
        /// Running timer.
        /// </summary>
        System.Timers.Timer runningMediaInfoTimer = new System.Timers.Timer();

        /// <summary>
        /// Current position in text.
        /// </summary>
        int runningMediaInfoTextIndex = 0;

        /// <summary>
        /// Stop Running content
        /// </summary>
        bool stopRunMediaInfo = false;

        /// <summary>
        /// Current delay time spent.
        /// </summary>
        int runningMediaInfoDelay = 0;

        /// <summary>
        /// How much time to wait
        /// </summary>
        const int runningMediaInfoStandartDelay = 5;

        /// <summary>
        /// Labe width = Text width + ?? this magic value
        /// </summary>
        int runningMediaInfoBorderWidth = 9;

        /// <summary>
        /// Current label font family.
        /// </summary>
        string runningMediaInfoFontFamily;

        /// <summary>
        /// Current label font size.
        /// </summary>
        double runningMediaInfoFontSize;

        /// <summary>
        /// Current direction.
        /// </summary>
        RunningMediaInfoDirection runningMediaInfoDirection = RunningMediaInfoDirection.None;

        /// <summary>
        /// All text.
        /// </summary>
        string runningCurrentPlayedMediaInfo;

        /// <summary>
        /// Video size : width x height.
        /// </summary>
        string videoSize;

        /// <summary>
        /// All text.
        /// </summary>
        public string RunningCurrentPlayedMediaInfo
        {
            get
            {
                return runningCurrentPlayedMediaInfo;
            }

            set
            {
                this.runningMediaInfoTextIndex = 0;
                this.runningCurrentPlayedMediaInfo = value + VideoSize;
                if (this.GetWidthInPixelFromIndex(runningMediaInfoTextIndex) > this.runLabel.ActualWidth)
                {
                    runningMediaInfoTimer.Start();
                    this.stopRunMediaInfo = false;
                }
                else
                {
                    runningMediaInfoTimer.Stop();
                    this.runLabel.Content = value + VideoSize;
                }
            }
        }

        /// <summary>
        /// Video size : width x height.
        /// </summary>
        public string VideoSize
        {
            get { return this.videoSize; }
            set
            {
                if (value != String.Empty)
                    this.RunningCurrentPlayedMediaInfo += value;

                this.videoSize = value;
            }
        }

        /// <summary>
        /// Move text if needed
        /// </summary>        
        void running_media_info_timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (this.stopRunMediaInfo)
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() => { this.runLabel.Content = this.headerLabel.Content; }));
                this.runningMediaInfoTimer.Stop();
                return;
            }

            if (runningMediaInfoDelay > 0)
            {
                --runningMediaInfoDelay;
                return;
            }

            if (this.runningMediaInfoDirection == RunningMediaInfoDirection.None)
            {
                if (this.GetWidthInPixelFromIndex(runningMediaInfoTextIndex) > this.runLabel.ActualWidth)
                {
                    this.runningMediaInfoDirection = RunningMediaInfoDirection.Right;
                    WaitAndNotRunMediaInfo();
                }
            }
            else if (this.runningMediaInfoDirection == RunningMediaInfoDirection.Right)
            {
                if (this.GetWidthInPixelFromIndex(runningMediaInfoTextIndex) <= this.runLabel.ActualWidth)
                {
                    this.runningMediaInfoDirection = RunningMediaInfoDirection.Left;
                    WaitAndNotRunMediaInfo();
                }
                else
                    ++runningMediaInfoTextIndex;

            }
            else if (this.runningMediaInfoDirection == RunningMediaInfoDirection.Left)
            {
                if (runningMediaInfoTextIndex == 0)
                    this.runningMediaInfoDirection = RunningMediaInfoDirection.None;
                else
                    --runningMediaInfoTextIndex;
            }

            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
            { this.runLabel.Content = RunningCurrentPlayedMediaInfo.Substring(runningMediaInfoTextIndex); }));
        }

        /// <summary>
        /// Wait.
        /// </summary>
        void WaitAndNotRunMediaInfo()
        {
            this.runningMediaInfoDelay = runningMediaInfoStandartDelay;
        }

        /// <summary>
        /// Get text width.
        /// </summary>
        /// <param name="index">Start index in all text.</param>
        /// <returns></returns>
        double GetWidthInPixelFromIndex(int index)
        {
            FormattedText sFT = new FormattedText(this.RunningCurrentPlayedMediaInfo.Substring(index),
            CultureInfo.GetCultureInfo("en-us"),
            System.Windows.FlowDirection.LeftToRight,
            new Typeface(this.runningMediaInfoFontFamily),
            runningMediaInfoFontSize,
            Brushes.Black);

            return sFT.Width + this.runningMediaInfoBorderWidth;
        }

        #endregion

        #region Recent played files

        /// <summary>
        /// Context menu open event handler.
        /// Load recent file history.
        /// </summary> 
        private void contextMenu_Opened(object sender, RoutedEventArgs e)
        {
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
                        this.PlayFile((string)clickedMi.Tag);
                    };

                    this.recentFilesMenuItem.Items.Add(mi);
                }

                this.recentFilesMenuItem.Items.Add(new Separator());

                mi = new System.Windows.Controls.MenuItem();
                mi.Header = "清除文件历史";
                mi.Click += (object sender1, RoutedEventArgs e1) =>
                {
                    History.Clear();
                };

                this.recentFilesMenuItem.Items.Add(mi);
            }
            else
                this.recentFilesMenuItem.IsEnabled = false;

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
                this.OpenFiles(fileNames);
        }

        #endregion

        #region Activate all our window, when needed

        /// <summary>
        /// Window state changed event handler.
        /// Minimize or maximize all child window if needed.
        /// </summary>        
        private void Window_StateChanged(object sender, EventArgs e)
        {
            Visibility newState = this.WindowState == WindowState.Minimized ? Visibility.Hidden : Visibility.Visible;
            
            if (this.listWindow != null)
                this.listWindow.Visibility = newState;

            if (this.fileTypesWindow != null)
                this.fileTypesWindow.Visibility = newState;

            if (this.aboutWindow != null)
                this.aboutWindow.Visibility = newState;

        }

        #endregion



    }
}
