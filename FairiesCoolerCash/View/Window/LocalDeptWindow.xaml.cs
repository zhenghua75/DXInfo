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
using System.Data.Entity;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using DXInfo.Data.Contracts;
namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// LocalDeptWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LocalDeptWindow : Window
    {
        private IFairiesMemberManageUow uow;
        public LocalDeptWindow(IFairiesMemberManageUow uow)
        {
            this.uow = uow;
            InitializeComponent();
        }
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var q = from d in uow.Depts.GetAll()
                    orderby d.DeptCode
                    select d;
            cbLocalDept.ItemsSource = q.ToList();
            cbLocalDept.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cbLocalDept.SelectedItem != null)
            {
                var q = from n in uow.NameCode.GetAll()
                        where n.Type == "LocalDept"
                        select n;
                if (q.Count() == 0)
                {
                    DXInfo.Models.Depts d = cbLocalDept.SelectedItem as DXInfo.Models.Depts;

                    DXInfo.Models.NameCode nc = new DXInfo.Models.NameCode();
                    nc.Type = "LocalDept";
                    nc.Code = "001";
                    nc.Name = "本地部门";
                    nc.Value = d.DeptId.ToString();
                    nc.Comment = "启用于：" + DateTime.Now.ToString();
                    uow.NameCode.Add(nc);
                    uow.Commit();
                }
            }
            this.Close();
        }

        //private void Window_Closed(object sender, EventArgs e)
        //{
        //    if (uow != null)
        //    {
        //        uow.Dispose();
        //        uow = null;
        //    }
        //}
        
    }
}
