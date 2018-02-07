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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Web.Security;
using System.Threading;
using DXInfo.Models;
using DXInfo.Data.Contracts;
using GalaSoft.MvvmLight.Messaging;
using FairiesCoolerCash.ViewModel;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// 冷饮店消费,消费
    /// </summary>
    public partial class CardConsumeUserControl : UserControl
    {
        public CardConsumeUserControl()
        {
            InitializeComponent();
            //Messenger.Default.Send(new ViewCollectionViewSourceMessageToken() { CVS = (CollectionViewSource)(this.Resources["CVS_OCInventory"]) });
            Messenger.Default.Send(new DataGridMessageToken() { MyDataGrid = this.MyDataGrid });
            
        }
        private void DecimalUpDown_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Helper.DecimalInput(sender, e);
        }

        private void lvInv_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while (dep is Run)
            {
                Run r = (Run)dep;
                dep = r.Parent;
            }
            if (dep is Visual || dep is System.Windows.Media.Media3D.Visual3D)
            {
                while ((dep != null) && !(dep is ListBoxItem))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }
            }
            
            if (dep == null)
                return;

            ListBoxItem item = (ListBoxItem)dep;
            if (item.IsSelected)
            {
                item.IsSelected = !item.IsSelected;
                e.Handled = true;
            }
        }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.SelectAll();
        }

        private void TextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.SelectAll();
        }

        private void TextBox_GotTouchCapture(object sender, TouchEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.SelectAll();
        }
    }
}
