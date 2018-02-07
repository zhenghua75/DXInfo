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
using System.Windows;
using System.Timers;

namespace Media_glass.Common.Items_operations
{
    /// <summary>
    /// Help to copy / cut / paste item in ListView and TreeView
    /// </summary>
    public class CopyCutPastItemManager
    {
        /// <summary>
        /// TreeView or ListView control.
        /// </summary>
        Object control = null;

        /// <summary>
        /// Constructor -  initializes the instance of window
        /// </summary>
        public CopyCutPastItemManager(Object control)
        {
            this.control = control;
        }

        /// <summary>
        /// All operation with library.
        /// </summary>
        public ILibraryManager LibraryManager { get; set; }

        /// <summary>
        /// Current selected item.
        /// </summary>
        Object SelectedItem
        {
            get
            {
                if (this.control.GetType() == (new ListView()).GetType())
                    return (this.control as ListView).SelectedItem;
                else if (this.control.GetType() == (new TreeView()).GetType())
                    return (this.control as TreeView).SelectedItem;

                return null;
            }

            set
            {
                if (this.control.GetType() == (new ListView()).GetType())
                {
                    (this.control as ListView).SelectedItem = value;
                    ((ListViewItem)(this.control as ListView).SelectedItem).Focus();
                }
                else if (this.control.GetType() == (new TreeView()).GetType())
                {
                    ((TreeViewItem)value).IsSelected = true;
                    ((TreeViewItem)value).Focus();
                }
            }
        }

        /// <summary>
        /// Collection in which selected item is placed.
        /// </summary>
        ItemCollection ControlItems
        {
            get
            {
                if (this.control.GetType() == (new ListView()).GetType())
                    return (this.control as ListView).Items;
                else if (this.control.GetType() == (new TreeView()).GetType())
                {
                    TreeView tv = (TreeView)this.control;

                    foreach (TreeViewItem tvi in tv.Items)
                    {
                        if (tvi.IsSelected)
                            return tv.Items;

                        foreach (TreeViewItem child in tvi.Items)
                        {
                            if (child.IsSelected)
                                return tvi.Items;
                        }
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Buffer that stored copy.
        /// </summary>
        Object bufferItem = null;

        /// <summary>
        /// Buffer that stored copy.
        /// </summary>
        public Object BufferItem
        {
            get { return bufferItem; }
            set { bufferItem = value; }
        }

        /// <summary>
        /// Copy item.
        /// </summary>        
        public Object MakeItemCopy(Object obj)
        {
            if (this.control.GetType() == (new ListView()).GetType())
            {
                ListViewItem lvi = (ListViewItem)obj;
                return new ListViewItem() { Content = (lvi.Content as ItemContent).Clone(), Tag = lvi.Tag };
            }
            else if (this.control.GetType() == (new TreeView()).GetType())
            {
                TreeViewItem tvi = (TreeViewItem)obj;

                if (tvi.Items.Count == 0)
                    return new TreeViewItem() { Header = tvi.Header, Tag = tvi.Tag, FontWeight = FontWeights.Normal };
                else
                {
                    TreeViewItem newTvi = new TreeViewItem() { Header = tvi.Header, Tag = tvi.Tag, IsExpanded = tvi.IsExpanded, FontWeight = FontWeights.Bold };

                    foreach (TreeViewItem child in tvi.Items)
                        newTvi.Items.Add(new TreeViewItem() { Header = child.Header, Tag = child.Tag, FontWeight = FontWeights.Normal });

                    return newTvi;
                }
            }
            return null;
        }

        /// <summary>
        /// Copy selected item.
        /// </summary>
        public void CopySelectedItem()
        {
            this.bufferItem = MakeItemCopy(this.SelectedItem);
        }

        /// <summary>
        /// Cut item.
        /// </summary>
        public void CutSelectedItem()
        {
            this.bufferItem = this.SelectedItem;

            Object obj = this.SelectedItem;
            ItemCollection items = this.ControlItems;
            int index = items.IndexOf(obj);
            items.Remove(obj);

            if (--index >= 0)
                this.SelectedItem = items[index];
        }
    }
}
