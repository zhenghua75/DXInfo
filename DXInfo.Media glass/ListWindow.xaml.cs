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
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Media_glass.Common;
using Media_glass.Common.Items_operations;
using Media_glass.Controls;
using Media_glass.Properties;

namespace Media_glass
{
    /// <summary>
    /// Interaction logic for ListWindow.xaml
    /// Show play list content.
    /// </summary>
    public partial class ListWindow : Window
    {
        #region Initialization
               
        /// <summary>
        /// Constructor -  initializes the instance of list window
        /// </summary>
        public ListWindow()
        {
            InitializeComponent();
            //load context menus
            LoadAllItemsContextMenu();               
            LoadOneItemContextMenu();
            this.ContextMenu = this.listContextMenu;        

            //load window position
            LoadWindowPosition();
            //load window width and height
            this.LoadWindowSize();
            //load list view column width
            LoadColumnWidthFromSettings();

            //init move up down, cut copy paste controls
            this.moveUpDownItemManager = new MoveUpDownItemManager(this.mediaListView);
            this.copyCutPasteItemManager = new CopyCutPastItemManager(this.mediaListView);

            //init magic timer to fix select item bug
            timer.Interval = 1;
            timer.Tick +=new EventHandler(timer_Tick);

            this.AddResizedBorder();
        }

        /// <summary>
        /// Load window corner values. Centered if opened first. 
        /// </summary>
        private void LoadWindowPosition()
        {
            if (Settings.Default.ListLeft == -1 || Settings.Default.ListTop == -1)
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            else
            {
                this.WindowStartupLocation = WindowStartupLocation.Manual;
                this.Left = Settings.Default.ListLeft;
                this.Top = Settings.Default.ListTop;
            }
        }

        /// <summary>
        /// Load window size.
        /// </summary>
        private void LoadWindowSize()
        {
            if (Settings.Default.ListContentWidth != -1)
            {
                this.contentGrid.Width = Settings.Default.ListContentWidth;
                this.Width = Settings.Default.ListWidth;
            }

            if (Settings.Default.ListContentHeight != -1)
            {
                this.contentGrid.Height = Settings.Default.ListContentHeight;
                this.Height = Settings.Default.ListHeight;
            }
        }

        /// <summary>
        /// Set list view column width.
        /// </summary>
        private void LoadColumnWidthFromSettings()
        {
            GridView gv = this.mediaListView.View as GridView;

            if(Settings.Default.FirstListWindowColumnWidth!=-1)
                gv.Columns[0].Width = Settings.Default.FirstListWindowColumnWidth;

            if (Settings.Default.SecondListWindowColumnWidth != -1)
                gv.Columns[1].Width = Settings.Default.SecondListWindowColumnWidth;

            if (Settings.Default.ThirdListWindowColumnWidth != -1)
                gv.Columns[2].Width = Settings.Default.ThirdListWindowColumnWidth;

            //if (Settings.Default.FourthListWindowColumnWidth != -1)
            //    gv.Columns[3].Width = Settings.Default.FourthListWindowColumnWidth;

            //if (Settings.Default.FifthListWindowColumnWidth != -1)
            //    gv.Columns[4].Width = Settings.Default.FifthListWindowColumnWidth;
        }

        /// <summary>
        /// Load context menu for all items
        /// </summary>
        private void LoadAllItemsContextMenu()
        {
            MenuItem mi = new MenuItem();
            mi.Header = "添加文件...";
            mi.InputGestureText = "Ctrl+O";
            mi.Click += this.MenuItem_Open_Click;
            this.listContextMenu.Items.Add(mi);

            mi = new MenuItem();
            mi.Header = "添加目录";
            mi.Click += new RoutedEventHandler(MenuItem_Add_Directory_Click);
            this.listContextMenu.Items.Add(mi);


            this.listContextMenu.Items.Add(new Separator());

            mi = new MenuItem();
            mi.Header = "删除所有";
            mi.Click += new RoutedEventHandler(MenuItem_Remove_All_Click);
            this.listContextMenu.Items.Add(mi);

            this.listContextMenu.Items.Add(new Separator());

            mi = new MenuItem();
            mi.Header = "按文件名升序";
            mi.Click += new RoutedEventHandler(MenuItem_Sort_Ascending);
            this.listContextMenu.Items.Add(mi);

            mi = new MenuItem();
            mi.Header = "按文件名降序";
            mi.Click += new RoutedEventHandler(MenuItem_Sort_Descending);
            this.listContextMenu.Items.Add(mi);

            mi = new MenuItem();
            mi.Header = "取消排序";
            mi.Click += new RoutedEventHandler(MenuItem_Clear_Sorting);
            this.listContextMenu.Items.Add(mi);            
        }        

        /// <summary>
        /// Load context menu for one item
        /// </summary>
        private void LoadOneItemContextMenu()
        {
            MenuItem mi = new MenuItem();
            mi.Header = "添加文件...";
            mi.InputGestureText = "Ctrl+O";
            mi.Click += this.MenuItem_Open_Click;
            this.itemContextMenu.Items.Add(mi);

            mi = new MenuItem();
            mi.Header = "添加目录";
            mi.Click += new RoutedEventHandler(MenuItem_Add_Directory_Click);
            this.itemContextMenu.Items.Add(mi);


            this.itemContextMenu.Items.Add(new Separator());


            mi = new MenuItem();
            mi.Header = "删除";
            mi.InputGestureText = "Delete";
            mi.Click += new RoutedEventHandler(MenuItem_Remove_Click);
            this.itemContextMenu.Items.Add(mi);

            mi = new MenuItem();
            mi.Header = "删除所有";
            mi.Click += new RoutedEventHandler(MenuItem_Remove_All_Click);
            this.itemContextMenu.Items.Add(mi);            

            this.itemContextMenu.Items.Add(new Separator());

            this.moveUpMenuItem = mi = new MenuItem();
            mi.Header = "上移";
            mi.InputGestureText = "Ctrl+U";
            mi.Click += new RoutedEventHandler(MenuItem_MoveUp_Click);
            this.itemContextMenu.Items.Add(mi);

            this.moveDownMenuItem = mi = new MenuItem();
            mi.Header = "下移";
            mi.InputGestureText = "Ctrl+D";
            mi.Click += new RoutedEventHandler(MenuItem_MoveDown_Click);
            this.itemContextMenu.Items.Add(mi);

            this.itemContextMenu.Items.Add(new Separator());


            this.itemContextMenu.Items.Add(new Separator());

            mi = new MenuItem();
            mi.Header = "按文件名升序";
            mi.Click += new RoutedEventHandler(MenuItem_Sort_Ascending);
            this.itemContextMenu.Items.Add(mi);

            mi = new MenuItem();
            mi.Header = "按文件名降序";
            mi.Click += new RoutedEventHandler(MenuItem_Sort_Descending);
            this.itemContextMenu.Items.Add(mi);

            mi = new MenuItem();
            mi.Header = "取消排序";
            mi.Click += new RoutedEventHandler(MenuItem_Clear_Sorting);
            this.itemContextMenu.Items.Add(mi);

            this.itemContextMenu.Items.Add(new Separator());

            mi = new MenuItem();
            mi.Header = "属性";
            mi.Click += new RoutedEventHandler(MenuItem_Properties_Click);
            this.itemContextMenu.Items.Add(mi);

            this.itemContextMenu.Opened += new RoutedEventHandler(itemContextMenu_Opened);
        }

        /// <summary>
        /// if selected item is not focused - fix it.
        /// </summary>        
        void  timer_Tick(object sender, EventArgs e)
        {
            if (this.mediaListView.SelectedItem != null)
            {
                ListViewItem lvi = (ListViewItem)this.mediaListView.SelectedItem;
                if (lvi.IsFocused)
                    this.timer.Stop();
                else
                    lvi.Focus();
            }            
        }        

        /// <summary>
        /// Show or hide some content menu items if no one item selected
        /// </summary>        
        void itemContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (this.mediaListView.SelectedItems.Count > 1)
            {                
                this.moveUpMenuItem.IsEnabled =
                this.moveDownMenuItem.IsEnabled =false;
            }
            else
            {                
                this.moveUpMenuItem.IsEnabled =
                this.moveDownMenuItem.IsEnabled = true;
            }
        }                                                
        
        #endregion

        #region Private fields

        /// <summary>
        /// Context menu for all items
        /// </summary>
        ContextMenu listContextMenu = new ContextMenu();

        /// <summary>
        /// Context menu for selected item
        /// </summary>
        ContextMenu itemContextMenu = new ContextMenu();

        /// <summary>
        /// timer to fix list view bug
        /// </summary>
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        /// <summary>
        /// Help to move up / down item in list 
        /// </summary>
        MoveUpDownItemManager moveUpDownItemManager;

        /// <summary>
        /// Help to copy / cut / paste item in list 
        /// </summary>
        CopyCutPastItemManager copyCutPasteItemManager;

        /// <summary>
        /// Context menu item that moves item up 
        /// </summary>
        MenuItem moveUpMenuItem = null;

        /// <summary>
        /// Context menu item that moves item down
        /// </summary>
        MenuItem moveDownMenuItem = null;


        /// <summary>
        /// Is data changed ?
        /// </summary>
        bool dataChanged = false;

        #endregion

        #region Properties        

        /// <summary>
        /// ListView with played items
        /// </summary>
        public ListView MediaListView
        {
            get
            {
                return this.mediaListView;
            }
        }

        /// <summary>
        /// Header of the window
        /// </summary>
        public string Header
        {
            get { return this.headerLabel.Content as string; }
            set { this.headerLabel.Content = value; }
        }        

        /// <summary>
        /// Manager for all windows
        /// </summary>
        public ILibraryManager LibraryManager { get; set; }

        /// <summary>
        /// Played items
        /// </summary>
        public ItemCollection ListItems
        {
            get
            {
                return this.mediaListView.Items;
            }
        }

        /// <summary>
        /// Media item that played
        /// </summary>
        public ListViewItem PlayedMediaItem { get; set; }

        
        /// <summary>
        /// Is data changed ?
        /// </summary>
        public bool DataChanged
        {
            get { return dataChanged; }
            set 
            {
                this.ClearSorting();
                dataChanged = value; 
            }
        }

        #endregion 

        #region Window message event handlers

        /// <summary>
        /// Double click event handler.
        /// Select and play item when double clicked on it.
        /// </summary>        
        private void mediaListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //when click on column header it is null.
            if (this.mediaListView.SelectedItem == null)
                return;

            this.SelectItem((ListViewItem)this.mediaListView.SelectedItem);
            this.LibraryManager.PlayFile(((ListViewItem)this.mediaListView.SelectedItem).Tag as string);
        }

        /// <summary>
        /// Selection changed event handler.
        /// Show correct context menu if needed.
        /// </summary>        
        private void mediaListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            if(this.mediaListView.SelectedItems.Count==1)
                this.timer.Start();

            this.mediaListView.ContextMenu = this.itemContextMenu;
        }

        /// <summary>
        /// Left mouse button event handler.
        /// Move the window to the current position.
        /// </summary>        
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// Minimize button event handler.
        /// Minimize the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Close button event handler.
        /// Close the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }        

        /// <summary>
        /// Change item background
        /// </summary>        
        void SetItemBackground(ListViewItem lvi, Brush brush)
        {
            Border border = (Border)lvi.Template.FindName("Border", lvi);
            border.Background = brush;            
        }

        /// <summary>
        /// Select item as played
        /// </summary>
        /// <param name="item"></param>
        public void SelectItem(ListViewItem item)
        {           
            this.ClearAllSelectedItem();
            
            this.PlayedMediaItem = item;
            item.Background = (Brush)this.FindResource("sliderBrush");            
        }

        /// <summary>
        /// Clear all selected item.
        /// </summary>
        public void ClearAllSelectedItem()
        {
            foreach (ListViewItem lvi in this.mediaListView.Items)
                lvi.Background = Brushes.Transparent;
        }
        

        /// <summary>
        /// Load window event handler.
        /// Load play list items. Show animation.
        /// </summary>                
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.LibraryManager.LoadPlayList(this.mediaListView.Items);
            this.DataChanged = false;

            if (this.LibraryManager.PlayedMediaIndex >= this.mediaListView.Items.Count)
            {
                if (this.mediaListView.Items.Count > 0)
                {
                    this.LibraryManager.PlayedMediaIndex = 0;
                }
                else
                {
                    this.LibraryManager.PlayedMediaIndex = -1;
                }
            }
            if (this.LibraryManager.PlayedMediaIndex != -1)
            {
                this.SelectItem((ListViewItem)this.mediaListView.Items[this.LibraryManager.PlayedMediaIndex]);
            }

            this.ShowLabelIfEmpty();
        }
        public void LoadPlayList()
        {
            this.DataChanged = true;
            this.LibraryManager.LoadPlayList(this.mediaListView.Items);
            this.ShowLabelIfEmpty();
        }
        /// <summary>
        /// Right mouse button click event handler.
        /// Unselect all and show context menu.
        /// </summary>        
        private void mediaListView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {           
            this.mediaListView.UnselectAll();
            this.mediaListView.ContextMenu = this.listContextMenu;
        }

        /// <summary>
        /// Window closed event handler.
        /// Save settings.
        /// </summary>        
        private void Window_Closed(object sender, EventArgs e)
        {            
            GridView gv = this.mediaListView.View as GridView;
            Settings.Default.FirstListWindowColumnWidth = gv.Columns[0].Width;
            Settings.Default.SecondListWindowColumnWidth = gv.Columns[1].Width;
            Settings.Default.ThirdListWindowColumnWidth = gv.Columns[2].Width;
            //Settings.Default.FourthListWindowColumnWidth = gv.Columns[3].Width;
            //Settings.Default.FifthListWindowColumnWidth = gv.Columns[4].Width;

            Settings.Default.ListLeft = this.Left;
            Settings.Default.ListTop = this.Top;
            Settings.Default.ListContentWidth = this.contentGrid.Width;
            Settings.Default.ListContentHeight = this.contentGrid.Height;
            Settings.Default.ListWidth = this.Width;
            Settings.Default.ListHeight = this.Height;

            if (this.PlayedMediaItem != null)
                this.LibraryManager.PlayedMediaIndex = this.mediaListView.Items.IndexOf(this.PlayedMediaItem);
            else
                this.LibraryManager.PlayedMediaIndex = -1;

            Media_glass.Properties.Settings.Default.Save();            
        }


        #endregion

        #region Context menu for all list


        /// <summary>
        /// add list item to the list and change datachanged status
        /// </summary>
        /// <param name="name">list item name</param>
        /// <param name="time">media tiem</param>
        /// <param name="tag">file to play</param>
        void AddListItem(string name, string time, string tag)
        {
            this.DataChanged = true;
            this.mediaListView.Items.Add(new ListViewItem() { Content = new ItemContent() { Name = name }, Tag = tag });
            this.ShowLabelIfEmpty();
        }

        /// <summary>
        /// Load files to play list
        /// </summary>
        /// <param name="fileNames"></param>
        public void OpenFiles(string[] fileNames)
        {
            foreach (string fileName in fileNames)        
                AddListItem(new FileInfo(fileName).Name," ", fileName);                                    
        }

        /// <summary>
        /// Open file context menu item event handler.
        /// add selected files to play list.
        /// </summary>        
        private void MenuItem_Open_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = MediaOpenFileDialog.New;            

            if (dlg.ShowDialog()==System.Windows.Forms.DialogResult.OK) // == System.Windows.Forms.DialogResult.OK)
            {
                this.OpenFiles(dlg.FileNames);
            }
        }

        /// <summary>
        /// Open directory context menu item event handler.
        /// Add all directory content to play list
        /// </summary>        
        void MenuItem_Add_Directory_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            //dlg.InitialDirectory = "c:\\";
            //dlg.Filter = "Media files (*.wmv)|*.wmv|All Files (*.*)|*.*";            

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK) // == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string fileName in Directory.GetFiles(fbd.SelectedPath))
                    AddListItem(new FileInfo(fileName).Name, " ", fileName);
            }
        }  

        /// <summary>
        /// Remove all context menu item event handler.
        /// Clear play list.
        /// </summary> 
        void MenuItem_Remove_All_Click(object sender, RoutedEventArgs e)
        {
            if (this.mediaListView.Items.Count != 0)
                this.DataChanged = true;

            this.mediaListView.Items.Clear();
            this.ShowLabelIfEmpty();
        } 

        #endregion

        #region Contex menu for one item

        /// <summary>
        /// Remove selected item context menu item event handler.
        /// Remove the item
        /// </summary> 
        void MenuItem_Remove_Click(object sender, RoutedEventArgs e)
        {
            int index = -1;
            int selectedItemsCount = this.mediaListView.SelectedItems.Count;

            if (selectedItemsCount > 0)
                index = Math.Min
                    ( this.mediaListView.Items.IndexOf(this.mediaListView.SelectedItems[0]) ,
                      this.mediaListView.Items.IndexOf(this.mediaListView.SelectedItems[selectedItemsCount-1])) - 1;
            
            for(int i=this.mediaListView.SelectedItems.Count-1;i>=0;--i)            
                this.mediaListView.Items.Remove(this.mediaListView.SelectedItems[i]);

            this.LibraryManager.ClosePlayedMedia();

            if (index >= 0)
            {
                ((ListViewItem)this.mediaListView.Items[index]).IsSelected = true;
                ((ListViewItem)this.mediaListView.Items[index]).Focus();
            }

            if (selectedItemsCount > 0)
                this.DataChanged = true;

            this.ShowLabelIfEmpty();
        }


        /// <summary>
        /// Move up selected item context menu item event handler.
        /// Move up the item.
        /// </summary>  
        void MenuItem_MoveUp_Click(object sender, RoutedEventArgs e)
        {
            this.moveUpDownItemManager.MoveUpSelectedItem();
            this.DataChanged = true;
        }

        /// <summary>
        /// Move down selected item context menu item event handler.
        /// Move down the item.
        /// </summary>  
        void MenuItem_MoveDown_Click(object sender, RoutedEventArgs e)
        {
            this.moveUpDownItemManager.MoveDownSelectedItem();
            this.DataChanged = true;
        }

        /// <summary>
        /// Copy item context menu item event handler.
        /// Copy item to bufer.
        /// </summary>  
        void MenuItem_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.copyCutPasteItemManager.CopySelectedItem();            
        }

        /// <summary>
        /// Cut item context menu item event handler.
        /// Cut item to bufer.
        /// </summary>  
        void MenuItem_Cut_Click(object sender, RoutedEventArgs e)
        {
            this.copyCutPasteItemManager.CutSelectedItem();
            this.DataChanged = true;
        }

        /// <summary>
        /// Properties context menu item event handler.
        /// Show file details.
        /// </summary>       
        void MenuItem_Properties_Click(object sender, RoutedEventArgs e)
        {
            ItemContent ic = this.GetMediaContent(this.mediaListView.SelectedItem);
            this.LibraryManager.ShowProperties(ic.Name, this.GetMediaPath(this.mediaListView.SelectedItem), ic.MediaType,ic.Time,ic.Resolution);
        }
        
        #endregion          
        
        #region Common operations

        /// <summary>
        /// Get media item info.
        /// </summary>
        /// <param name="item">list view item</param>
        /// <returns>List view media info</returns>
        ItemContent GetMediaContent(Object item)
        {
            return (ItemContent)((ListViewItem)item).Content;
        }

        /// <summary>
        /// Get media file path from tag.
        /// </summary>
        /// <param name="item">List view selected item.</param>
        /// <returns>List view media info.</returns>
        string GetMediaPath(Object item)
        {
            return ((ListViewItem)this.mediaListView.SelectedItem).Tag as string;
        }

        /// <summary>
        /// Show control with link label if list view is empty.
        /// </summary>
        public void ShowLabelIfEmpty()
        {
            if (this.mediaListView.Items.Count == 0)
            {
                this.mediaListView.Visibility = Visibility.Hidden;
                this.glassLinkPanel.Visibility = Visibility.Visible;
            }
            else
            {
                this.mediaListView.Visibility = Visibility.Visible;
                this.glassLinkPanel.Visibility = Visibility.Collapsed;
            }
        }


        #endregion

        #region Gesture key

        /// <summary>
        /// Gesture key event handler.
        /// </summary>        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            bool oneRowSelected = this.mediaListView.SelectedItems.Count == 1;

            //current window events

            if (e.Key == Key.Delete)
                this.MenuItem_Remove_Click(null, null);
            else if (e.Key == Key.U && Keyboard.Modifiers == ModifierKeys.Control && oneRowSelected)
                this.MenuItem_MoveUp_Click(null, null);
            else if (e.Key == Key.D && Keyboard.Modifiers == ModifierKeys.Control && oneRowSelected)
                this.MenuItem_MoveDown_Click(null, null);
            else if (e.Key == Key.X && Keyboard.Modifiers == ModifierKeys.Control && oneRowSelected)
                this.MenuItem_Cut_Click(null, null);
            else if (e.Key == Key.C && Keyboard.Modifiers == ModifierKeys.Control && oneRowSelected)
                this.MenuItem_Copy_Click(null, null);
            else if (e.Key == Key.Enter)
                mediaListView_MouseDoubleClick(null, null);
            else if ((e.Key == Key.Tab || e.Key == Key.Down) && this.mediaListView.SelectedItems.Count == 0 && this.mediaListView.Items.Count > 0)
                this.mediaListView.SelectedItem = this.mediaListView.Items[0];

            //common events

            else if (e.Key == Key.O && Keyboard.Modifiers == ModifierKeys.Control)
                this.MenuItem_Open_Click(null, null);
            else if (e.Key == Key.P && Keyboard.Modifiers == ModifierKeys.Control)
                this.LibraryManager.OpenPlayListWindow();
        }

        #endregion 

        #region sorting

        /// <summary>
        /// Last header column we clicked.
        /// </summary>
        GridViewColumnHeader _lastHeaderClicked = null;

        /// <summary>
        /// Last direction we sorted.
        /// </summary>
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        /// <summary>
        /// Grid view column click event handler.
        /// Sort the play list.
        /// </summary>        
        private void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (!(e.OriginalSource is GridViewColumnHeader))
                return;

            object obj = ((GridViewColumnHeader)e.OriginalSource).Content;
            if (obj == null) return;

            if (obj.ToString() != "编码" && headerClicked != null)
            {
                ClearSorting();
                return;
            }

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)                    
                        direction = ListSortDirection.Ascending;                    
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)                        
                            direction = ListSortDirection.Descending;                        
                        else                        
                            direction = ListSortDirection.Ascending;                        
                    }
                    
                    Sort(direction);                    
                }
            }
        }               
         
        /// <summary>
        /// Copy item context menu item event handler.
        /// Copy item to bufer.
        /// </summary>  
        void MenuItem_Sort_Ascending(object sender, RoutedEventArgs e)
        {
            this.Sort(ListSortDirection.Ascending);
        }

        /// <summary>
        /// Copy item context menu item event handler.
        /// Copy item to bufer.
        /// </summary>  
        void MenuItem_Sort_Descending(object sender, RoutedEventArgs e)
        {
            this.Sort(ListSortDirection.Descending);
        }

        /// <summary>
        /// Clear sorting context menu item handler.
        /// Clear sorting.
        /// </summary>     
        void MenuItem_Clear_Sorting(object sender, RoutedEventArgs e)
        {
            this.ClearSorting();
        }

        public class SortedItem
        {
            public string Value { get; set; }
            public Object Item { get; set; }
        }

        /// <summary>
        /// Sort item list by specific direction.
        /// </summary>
        /// <param name="direction"></param>
        private void Sort(ListSortDirection direction)
        {
            //it doesn't work, so i use buble sorting
            //this.mediaListView.Items.SortDescriptions.Clear();            
            //SortDescription sd = new SortDescription("Name", direction);            
            //this.mediaListView.Items.SortDescriptions.Add(sd);
            //this.mediaListView.Items.Refresh();          

            this.SetHeaderArrow(direction);

            ItemCollection items = this.mediaListView.Items;
            Object temp;
            Object selectedItem = this.mediaListView.SelectedItem;

            //for (int i = 0; i < items.Count; ++i)
            //    for (int j = items.Count - 1; j > i; --j)
            //    {
            //        if (CompareObject(items[j - 1], items[j], direction))
            //        {
            //            temp = items[j - 1];
            //            this.mediaListView.Items.Remove(temp);
            //            this.mediaListView.Items.Insert(j, temp);
            //        }
            //    }

            List<SortedItem> sortedItems = new List<SortedItem>();

            for (int i = 0; i < items.Count; ++i)
                sortedItems.Add(new SortedItem { Value = ((ItemContent)((ListViewItem)items[i]).Content).Name, Item = items[i] });

            sortedItems.Sort((SortedItem item1, SortedItem item2) =>
            {
                if(direction== ListSortDirection.Ascending)
                    return item1.Value.CompareTo(item2.Value);
                else
                    return item2.Value.CompareTo(item1.Value);
            });

            for (int i = 0; i < sortedItems.Count; ++i)
            {
                if (items[i] != sortedItems[i].Item)
                {
                    this.mediaListView.Items.Remove(sortedItems[i].Item);
                    this.mediaListView.Items.Insert(i, sortedItems[i].Item);
                }
            } 


            if (selectedItem != null)
                this.mediaListView.SelectedItem = selectedItem;

            _lastDirection = direction;
        }

        /// <summary>
        /// Compare two file names depend on direction
        /// </summary>        
        bool CompareObject(Object o1, Object o2, ListSortDirection direction)
        {
            int result = String.Compare(((ItemContent)((ListViewItem)o1).Content).Name, ((ItemContent)((ListViewItem)o2).Content).Name);

            if (direction == ListSortDirection.Ascending)
                return result > 0;
            else
                return result < 0;
        }

        /// <summary>
        /// Set header column arrow up or down depend on direction.
        /// </summary>        
        private void SetHeaderArrow(ListSortDirection direction)
        {
            GridView gv = (GridView)this.mediaListView.View;

            if (direction == ListSortDirection.Ascending)
            {
                gv.Columns[0].HeaderTemplate = Resources["HeaderTemplateArrowDown"] as DataTemplate;
                _lastHeaderClicked = FindTitleHeader();
                _lastHeaderClicked.ToolTip = "Ascending sort by name";
            }
            else
            {
                gv.Columns[0].HeaderTemplate = Resources["HeaderTemplateArrowUp"] as DataTemplate;
                _lastHeaderClicked = FindTitleHeader();
                _lastHeaderClicked.ToolTip = "Descending sort by name";
            }            
        }

        /// <summary>
        /// Clear sorting.
        /// </summary>
        void ClearSorting()
        {
            if (_lastHeaderClicked != null)
            {
                _lastHeaderClicked.Column.HeaderTemplate = null;
                _lastHeaderClicked.ToolTip = "Sort by name";
                _lastHeaderClicked = null;
            }

            _lastDirection = ListSortDirection.Descending;
        }  
                
        /// <summary>
        /// Find our grid template column header.
        /// http://social.msdn.microsoft.com/Forums/en-US/wpf/thread/67cdbaa0-d28a-4a16-8f01-8862469f220d
        /// </summary>        
        private GridViewColumnHeader FindTitleHeader()
        {
            List<Visual> elementList = new List<Visual>();
            FindAllElements2(typeof(GridViewColumnHeader), this, elementList);            
            foreach (Visual element in elementList)
            {
                GridViewColumnHeader header = element as GridViewColumnHeader;
                if (header.Column != null)
                {
                    if (Object.Equals(header.Column.Header, "编码"))
                        return header;
                }
            }

            return null;
        }

        /// <summary>
        /// Find element in template.
        /// </summary>        
        private void FindAllElements2(Type T, Visual root, List<Visual> elementList)
        {
            if (root == null)
                return;
            
            if (T.Equals(root.GetType()))
            {
                elementList.Add(root);
                return;
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(root); i++)
            {
                Visual child = VisualTreeHelper.GetChild(root, i) as Visual;
                FindAllElements2(T, child, elementList);
            }
        }

        #endregion        

        #region Drag and drop

        /// <summary>
        /// Handle dropped files.
        /// </summary>        
        private void Window_Drop(object sender, DragEventArgs e)
        {
            string[] fileNames = e.Data.GetData(DataFormats.FileDrop, true) as string[];

            if(fileNames!=null)
                this.OpenFiles(fileNames);
        }

        #endregion

        #region Resized border

        /// <summary>
        /// Resized border control.
        /// </summary>
        ResizedBorder resizedBorder = null;

        /// <summary>
        /// Add border to our window.
        /// </summary>
        void AddResizedBorder()
        {
            resizedBorder = new ResizedBorder(this.contentGrid.Width, this.contentGrid.Height);
            resizedBorder.DragDelta += resizedBorder_DragDelta;
            resizedBorder.DragCompleted += resizedBorder_StopDragging;
            resizedBorder.DragStarted += new EventHandler(resizedBorder_DragStarted);
            resizedBorder.ThumbMouseEnter += new EventHandler(resizedBorder_ThumbMouseEnter);
            resizedBorder.ThumbMouseLeave += new EventHandler(resizedBorder_ThumbMouseLeave);
            resizedBorder.MinBorderWidth = 319;
            resizedBorder.MinBorderHeight = 319;
            borderGrid.Children.Insert(0,resizedBorder);            
        }

        /// <summary>
        /// Show border.
        /// </summary>
        void SelectBorderSign()
        {
            this.borderSign.Fill = Brushes.White;
            this.borderSign.Opacity = 1;
        }

        /// <summary>
        /// Hide border.
        /// </summary>
        void DeselectBorderSign()
        {
            this.borderSign.Fill = (Brush)this.FindResource("fillBrush");
            this.borderSign.Opacity = 0.7;
        }

        /// <summary>
        /// Thumb leave event handler.
        /// </summary>        
        void resizedBorder_ThumbMouseLeave(object sender, EventArgs e)
        {
            this.DeselectBorderSign();
        }

        /// <summary>
        /// Enter thumb event handler.
        /// </summary>        
        void resizedBorder_ThumbMouseEnter(object sender, EventArgs e)
        {
            this.SelectBorderSign();
        }

        /// <summary>
        /// Border start dragging.
        /// </summary>        
        void resizedBorder_DragStarted(object sender, EventArgs e)
        {
            this.borderGrid.Children.Remove(this.resizedBorder);
            this.borderGrid.Children.Add(this.resizedBorder);

            this.DeselectBorderSign();
        }

        /// <summary>
        /// Layout.
        /// </summary>
        const int borderLayout = 100;

        /// <summary>
        /// Stop border dragging.
        /// </summary>        
        void resizedBorder_StopDragging(double width, double height)
        {
            this.contentGrid.Width = width;
            this.contentGrid.Height = height;

            //to show shadow we need 100 pixels.
            this.Width = width + borderLayout;
            this.Height = height + borderLayout;

            this.borderGrid.Children.Remove(this.resizedBorder);
            borderGrid.Children.Insert(0, this.resizedBorder);
        }

        /// <summary>
        /// If bordeer change it size - resize main window.
        /// </summary>
        /// <param name="horizontalChange"></param>
        /// <param name="verticalChange"></param>
        void resizedBorder_DragDelta(double horizontalChange, double verticalChange)
        {
            if (horizontalChange < 0 && this.Width + horizontalChange > this.contentGrid.Width + borderLayout || horizontalChange > 0)
                this.Width += horizontalChange;

            if (verticalChange < 0 && this.Height + verticalChange > this.contentGrid.Height + borderLayout || verticalChange > 0)
                this.Height += verticalChange;
        }

        #endregion

        /// <summary>
        /// Open file if link has been clicked.
        /// </summary>
        private void glassLinkPanel_LinkClick()
        {
            this.MenuItem_Open_Click(null, null);
        }
    }
}
