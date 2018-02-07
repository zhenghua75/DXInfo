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
using System.Timers;
using System.Windows.Threading;
using System.IO;
using Media_glass.Common.Enums;
using Media_glass.Common.Helpers;

namespace Media_glass.SmallWindows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class FilePropertiesWindow : Window
    {
        /// <summary>
        /// Constructor -  initializes the instance of window
        /// </summary>
        public FilePropertiesWindow()
        {
            InitializeComponent();
            this.Style = (Style)this.FindResource("ModalWindow");
        }

        /// <summary>
        /// User file name.
        /// </summary>
        public string FileDisplayedName { get; set; }

        /// <summary>
        /// File system location.
        /// </summary>
        public string FileLocation { get; set; }

        /// <summary>
        /// Video or audio.
        /// </summary>
        public MediaType MediaType { get; set; }

        /// <summary>
        /// Video resolution.
        /// </summary>
        public string Resolution { get; set; }

        /// <summary>
        /// Media duration.
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Load window event handler.
        /// Load file info.
        /// </summary>   
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.nameLabel.Content = this.nameLabel.ToolTip = this.FileDisplayedName;

            this.listView1.Items.Add(new FileProperty() { Name = "路径", Value = this.FileLocation });
            this.listView1.Items.Add(new FileProperty() { Name = "大小", Value = FileSize.GetValue(new FileInfo(this.FileLocation).Length) });

            string type;
            string duration;

            if (this.MediaType == MediaType.NotPlayed)
                type = duration = "File is not played yet";
            else
            {
                type = this.MediaType.ToString();
                duration = this.Duration;
            }

            this.listView1.Items.Add(new FileProperty() { Name = "类型", Value = type });
            this.listView1.Items.Add(new FileProperty() { Name = "长度", Value = duration });

            if (this.MediaType == MediaType.Video)
                this.listView1.Items.Add(new FileProperty() { Name = "Resolution", Value = this.Resolution });
        }

        /// <summary>
        /// Mouse button down event handler.
        /// Drag window.
        /// </summary>   
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// OK click event handler.
        /// If Ok close the window.
        /// </summary>        
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
