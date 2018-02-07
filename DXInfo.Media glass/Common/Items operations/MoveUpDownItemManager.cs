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
using System.Windows.Input;

namespace Media_glass.Common.Items_operations
{
    /// <summary>
    /// Help to move up / down item in ListView and TreeView
    /// </summary>
    public class MoveUpDownItemManager
    {
        /// <summary>
        /// ListView or TreeView control.
        /// </summary>
        Object control = null;

        /// <summary>
        /// Constructor -  initializes the instance of window
        /// </summary>
        public MoveUpDownItemManager(Object control)
        {
            this.control = control;            
        }

        /// <summary>
        /// Current selected item.
        /// </summary>
        Object SelectedItem
        {
            get
            {
                if(this.control.GetType()==(new ListView()).GetType())
                    return (this.control as ListView).SelectedItem;
                else if(this.control.GetType()==(new TreeView()).GetType())
                    return (this.control as TreeView).SelectedItem;

                return null;
            }

            set
            {
                if (this.control.GetType() == (new ListView()).GetType())
                {                       
                    (this.control as ListView).SelectedItem = value;                                        
                    //this.FocusSelectedListViewItem();
                    //int index = (this.control as ListView).Items.IndexOf(value);

                    //((ListViewItem)(this.control as ListView).Items[index]).IsSelected = true;
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
        /// Focus selected list view item.
        /// </summary>
        void FocusSelectedListViewItem()
        {
            ListView lv = (ListView)this.control;
            //lv.Focus();
            lv.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

            int index = lv.SelectedIndex;

            for(int i = 0; i<index; ++i)
                ((ListViewItem)lv.Items[i]).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

            //foreach (ListViewItem lvi in lv.Items)
            //{
            //    lvi.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

            //    if (((ListViewItem)lv.SelectedItem).IsFocused)
            //        break;                
            //}            
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
        /// Move selected item up.
        /// </summary>
        public void MoveUpSelectedItem()
        {
            Object obj = this.SelectedItem;
            ItemCollection items = this.ControlItems;    
            int index = items.IndexOf(obj);
            --index;
            if (index >= 0)
            {
                items.Remove(obj);
                items.Insert(index, obj);
                this.SelectedItem = obj;
            }
        }

        /// <summary>
        /// Move selected item down.
        /// </summary>
        public void MoveDownSelectedItem()
        {
            Object obj = this.SelectedItem;
            ItemCollection items = this.ControlItems;    
            int index = items.IndexOf(obj);
            ++index;
            if (index < items.Count)
            {
                items.Remove(obj);
                items.Insert(index, obj);
                this.SelectedItem = obj;
            }
        }
    }
}
