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
using System.Reflection;
using Media_glass.Properties;

namespace Media_glass.SmallWindows
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// Show application info.
    /// </summary>
    public partial class AboutWindow : Window
    {
        /// <summary>
        /// Constructor -  initializes the instance of window
        /// </summary>
        public AboutWindow()
        {
            InitializeComponent();
            this.Title = "About "+ Settings.Default.Product;
            this.versionLabel.Content = String.Format("Version: {0}", this.Version);
            this.Style = (Style)this.FindResource("ModalWindow");
        }        
        
        /// <summary>
        /// Need to get assembly file version.
        /// http://social.msdn.microsoft.com/Forums/en-US/csharpgeneral/thread/37db09be-1227-4b06-8f04-1f75f5613265/
        /// </summary>        
        private static T GetAssemblyAttribute<T>(Assembly assembly) where T : Attribute
        {
            if (assembly == null) return null;
            object[] attributes = assembly.GetCustomAttributes(typeof(T), true);
            if (attributes == null) return null;
            if (attributes.Length == 0) return null;
            return (T)attributes[0];
        }

        /// <summary>
        /// Current application version.
        /// </summary>
        string Version
        {
            get
            {
                AssemblyFileVersionAttribute fileVersion = GetAssemblyAttribute<AssemblyFileVersionAttribute>(Assembly.GetExecutingAssembly());
                return fileVersion.Version;
            }
        }                   
        
        /// <summary>
        /// Ok button event handler.
        /// Close the window.
        /// </summary>        
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Hyperlink event handler.
        /// Go to the application sources web site.
        /// </summary>        
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            // open URL
            Hyperlink source = sender as Hyperlink;

            if (source != null)          
                System.Diagnostics.Process.Start(source.NavigateUri.ToString());
        }

        /// <summary>
        /// Left mouse button event handler.
        /// Move the window to the specific postion.
        /// </summary>        
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// Keydown event hadnler.
        /// Raise Ok button event if enter is pressed.
        /// </summary>        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                this.okButton_Click(null, null);
        }
    }
}
