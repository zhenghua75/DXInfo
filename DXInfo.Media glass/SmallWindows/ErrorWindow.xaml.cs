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
using System.Reflection;
using Media_glass.Common.Helpers;

namespace Media_glass.SmallWindows
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        /// <summary>
        /// Constructor -  initializes the instance of window
        /// </summary>
        public ErrorWindow()
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
        /// Unhandle exception we show.
        /// </summary>
        public Exception Message
        {
            set
            {
                this.messageTextBlock.Text = value.Message;
                this.GetDescription(value);
            }
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
        /// Quit button event handler.
        /// </summary> 
        private void quitButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        /// <summary>
        /// Continue button event handler.
        /// </summary> 
        private void continueButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// Copy button event handler.
        /// Copy to clipboard.
        /// </summary> 
        private void copyButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, this.GetListBoxText());
        }

        /// <summary>
        /// Get errror description.
        /// </summary>        
        public void GetDescription(Exception e)
        {               
            while (e != null)
            {                
                this.PrintLine(e.GetType().ToString()+": "+e.Message);
                
                this.PrintLine("");
                foreach(string line in e.StackTrace.Split(new string[]{"\r\n"},StringSplitOptions.None))                
                    this.PrintLine(line.TrimStart());

                //this.PrintLine("");
                e = e.InnerException;
                if (e != null)
                {                    
                    this.PrintLine("--------------------------------");                    
                    //this.PrintLine("Previous:");
                    //this.PrintLine("");
                }
            }            
        }

        /// <summary>
        /// Get All listbox text.
        /// </summary>
        /// <returns></returns>
        string GetListBoxText()
        {
            StringBuilder builder = new StringBuilder();

            foreach (string line in this.detailsListBox.Items)
                builder.AppendLine(line);

            return builder.ToString();
        }

        /// <summary>
        /// Print one line at listbox
        /// </summary>        
        void PrintLine(string line)
        {
            this.detailsListBox.Items.Add(line);            
        }

        /// <summary>
        /// Keydown event handler.
        /// Raise Ok event if enter is pressed.
        /// </summary>  
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            this.continueButton_Click(null, null);
        }
    }
}
