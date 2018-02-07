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
using System.Windows.Navigation;
using Media_glass.Properties;

namespace Media_glass.SmallWindows
{
    /// <summary>
    /// Interaction logic for HelpWindow2.xaml
    /// </summary>
    public partial class HelpWindow : NavigationWindow
    {
        /// <summary>
        /// Constructor -  initializes the instance of window
        /// </summary>
        public HelpWindow()
        {
            InitializeComponent();
            this.Title = Settings.Default.Product + " Help";
            this.Source = new Uri(String.Format("pack://application:,,,/Resources/Help/{0}", "MainDocument.xaml"), UriKind.RelativeOrAbsolute);
            
            //I try to make glass style not for all context menu
            //But it is very difficult.
            //So this context menu was glass too, but this is bad style
            //that's why i disabled it.
            this.ContextMenu = null;

            //this.Style = (Style)this.FindResource("ModalWindow");

            //this.NavigationService.LoadCompleted += new LoadCompletedEventHandler(NavigationService_LoadCompleted);
            //this.NavigationService.Navigated += new NavigatedEventHandler(NavigationService_Navigated);
        }

        //void NavigationService_Navigated(object sender, NavigationEventArgs e)
        //{
        //    try
        //    {
        //        FlowDocument doc = (FlowDocument)e.Content;
        //        doc.ContextMenu.Style = null;
        //        doc.ContextMenuOpening += new ContextMenuEventHandler(doc_ContextMenuOpening);
        //    }
        //    catch
        //    {
        //    }
        //}

        //void doc_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        //{
        //    try
        //    {
        //        FlowDocument doc = (FlowDocument)e.Content;
        //        doc.ContextMenu.Style = null;
        //    }
        //    catch
        //    {
        //    }
        //}
    }
}
