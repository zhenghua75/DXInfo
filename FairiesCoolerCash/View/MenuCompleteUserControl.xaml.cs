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
using System.Timers;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using DXInfo.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using DXInfo.Data.Contracts;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// 已出菜品
    /// </summary>
    public partial class MenuCompleteUserControl : UserControl
    {
        //private DXInfo.Models.FairiesMemberManage db = new DXInfo.Models.FairiesMemberManage();
        private readonly IFairiesMemberManageUow uow;
        public MenuCompleteUserControl(IFairiesMemberManageUow uow)
        {
            this.uow = uow;
            InitializeComponent();
        }
        private void UpdateOrderMenuData()
        {
            //(FindResource("ticker") as MenuInfoTickerComplete).Refresh();
        }
        #region 取消出菜
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //9、点击出菜，打印机打印一条小票，显示桌号、备注信息、名称、份数、开台服务员名字。
            if (GridMenuComplete.SelectedItem != null)
            {
                MenuInfo mi = GridMenuComplete.SelectedItem as MenuInfo;
                if (mi.SelectedDesk != null)
                {
                    DXInfo.Restaurant.DeskManageFacade dmf = new DXInfo.Restaurant.DeskManageFacade();
                    dmf.CancelOutMenu(uow, mi.OrderMenuId, App.MyIdentity.oper.UserId, mi.SelectedDesk.DeskId);

                    List<SelInv> confirmmenu = new List<SelInv>();
                    SelInv si = new SelInv();
                    var inv = (from d in uow.Inventory.GetAll() where d.Id == mi.InventoryId select d).FirstOrDefault();
                    si.Category = inv.Category;
                    si.Amount = mi.Price;
                    si.Quantity = 1;
                    si.Name = mi.InvName;
                    si.Comment = mi.Comment;
                    confirmmenu.Add(si);

                    var ctx = new
                    {
                        DeskNo = mi.SelectedDesk.DeskCode + "(取消出菜)",
                        Sum = mi.Price,
                        Count = 1,
                        lSelInv = confirmmenu,
                        CreateDate = DateTime.Now,
                        DeptName = App.MyIdentity.dept.DeptName,
                    };
                    System.Printing.LocalPrintServer lp = new System.Printing.LocalPrintServer();
                    System.Printing.PrintQueue pq = new System.Printing.PrintQueue(lp, inv.Printer);
                    DeskConfirmWindow dcw = new DeskConfirmWindow(ctx, pq);
                    if (dcw.ShowDialog().GetValueOrDefault())
                    {
                    }

                    MessageBox.Show(mi.SelectedDesk.DeskCode + "(" + mi.InvName + ")" + "取消出菜成功");
                    UpdateOrderMenuData();
                }
                else
                {
                    MessageBox.Show("请选中桌台号");
                }
            }
        }
        #endregion

        #region 缺菜
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            TextBlock txt = btn.Content as TextBlock;
            if (txt.Text.Equals("取消缺菜"))
            {
                if (GridMenuComplete.SelectedItem != null)
                {
                    MenuInfo mi = GridMenuComplete.SelectedItem as MenuInfo;
                    DXInfo.Restaurant.DeskManageFacade dmf = new DXInfo.Restaurant.DeskManageFacade();
                    dmf.CancelMissMenu(uow, mi.OrderMenuId, App.MyIdentity.oper.UserId);
                    MessageBox.Show("(" + mi.InvName + ")已取消缺菜");
                    UpdateOrderMenuData();
                }
            }
        }
        #endregion
    }

   
}
