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

namespace Media_glass.Controls
{
    /// <summary>
    /// Interaction logic for LinkPanel.xaml
    /// </summary>
    public partial class GlassLinkPanel : UserControl
    {
        /// <summary>
        /// Constructor -  initializes the instance of window
        /// </summary>
        public GlassLinkPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// What we do, when user click.
        /// </summary>
        public event Action LinkClick;

        /// <summary>
        /// Process label click.
        /// </summary>        
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {            
            if (this.LinkClick != null)
                this.LinkClick();
        }
    }
}
