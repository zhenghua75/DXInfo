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
using System.Collections.ObjectModel;
using DXInfo.Models;
using DXInfo.Data.Contracts;
using FairiesCoolerCash.ViewModel;
using GalaSoft.MvvmLight.Messaging;
namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// 会员积分兑换
    /// </summary>
    public partial class PointsExchangeUserControl : UserControl
    {
        public PointsExchangeUserControl()
        {
            InitializeComponent();
            Messenger.Default.Send(new ViewCollectionViewSourceMessageToken() { CVS = (CollectionViewSource)(this.Resources["CVS_OCInventory"]) });
            Messenger.Default.Send(new DataGridMessageToken() { MyDataGrid = this.MyDataGrid });
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
    }
}
