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
using Media_glass.Properties;
using Media_glass.Common;
using Media_glass.Common.Helpers;

namespace Media_glass
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// My message box analog
    /// </summary>
    public partial class MyMessageBox : Window
    {
        /// <summary>
        /// Constructor -  initializes the instance of  window
        /// </summary>
        public MyMessageBox()
        {
            InitializeComponent();
            this.Style = (Style)this.FindResource("ModalWindow");            
        }

        /// <summary>
        /// Remove icon from window header.
        /// </summary>
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HeaderIconRemover.RemoveIconFrom(this);
        }

        /// <summary>
        /// Show custom message window
        /// </summary>
        /// <param name="messageBoxText">Message text</param>        
        /// <param name="buttons">Message buttons</param>
        /// <param name="icon">Message icons</param>
        /// <returns></returns>
        public static bool? Show(string messageBoxText, MessageBoxButton buttons, MessageBoxImage icon)
        {
            return MyMessageBox.Show(messageBoxText, Settings.Default.Product, buttons, icon);
        }

        /// <summary>
        /// Show custom message window
        /// </summary>
        /// <param name="messageBoxText">Message text</param>
        /// <param name="caption">Message caption</param>
        /// <param name="buttons">Message buttons</param>
        /// <param name="icon">Message icons</param>
        /// <returns></returns>
        public static bool? Show(string messageBoxText, string caption, MessageBoxButton buttons, MessageBoxImage icon)
        {
            MyMessageBox messageBox = new MyMessageBox();
            messageBox.Title = caption;
            messageBox.Text = messageBoxText;

            if (buttons == MessageBoxButton.OKCancel)
            {
                           
            }
            else if (buttons == MessageBoxButton.YesNo)
            {
                messageBox.OkTitle = "Yes";
                messageBox.CancelTitle = "No";
            }
            else if (buttons == MessageBoxButton.OK)
            {                
                messageBox.IsCancelVisible = false;
            }

            if (icon == MessageBoxImage.Error)
                messageBox.Icon = Icons.Error;
            else if (icon == MessageBoxImage.Information)
                messageBox.Icon = Icons.Info;
            else if (icon == MessageBoxImage.Question)
                messageBox.Icon = Icons.Question;
            else if (icon == MessageBoxImage.Warning)
                messageBox.Icon = Icons.Warning;

            return messageBox.ShowDialog();           
        }

        /// <summary>
        /// Message text.
        /// </summary>
        public string Text
        {
            set
            {
                this.textLabel.Content = value;
            }
        }

        /// <summary>
        /// Message icon.
        /// </summary>
        public Image Icon
        {
            set
            {
                this.image.Source = value.Source;                
            }        
        }

        /// <summary>
        /// Ok button title.
        /// </summary>
        public string OkTitle
        {
            set
            {
                this.okButton.Content = value;
            }
        }

        /// <summary>
        /// Cancel button title.
        /// </summary>
        public string CancelTitle
        {
            set
            {
                this.cancelButton.Content = value;
            }
        }

        /// <summary>
        /// Is or dialog with one button or not.
        /// </summary>
        public bool IsCancelVisible
        {
            get
            {
                return this.cancelButton.Visibility==Visibility.Visible;
            }

            set
            {
                if (value)
                    this.cancelButton.Visibility = Visibility.Visible;
                else
                {
                    this.cancelButton.Visibility = Visibility.Collapsed;                    
                }
            }
        }

        /// <summary>
        /// Ok button event handler.
        /// </summary>   
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// Cancel button event handler.
        /// </summary>   
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        /// <summary>
        /// Keydown event handler.
        /// Raise Ok button event if enter is pressed.
        /// </summary>        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                this.okButton_Click(null, null);
        }

        /// <summary>
        /// Left mouse button event handler.
        /// Move window to the specific position.
        /// </summary>   
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
