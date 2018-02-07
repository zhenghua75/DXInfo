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
using System.Collections.ObjectModel;
using System.Transactions;
using System.Collections;
using DXInfo.Models;
using DXInfo.Data.Contracts;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// DeskMenuWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeskMenuWindow : Window
    {
        private readonly IFairiesMemberManageUow uow;
        public ObservableCollection<DXInfo.Models.OrderMenuEx> ocSelDesks;
        public DXInfo.Models.Desks desk;
        public DXInfo.Models.OrderDishes order;
        public DeskMenuWindow(IFairiesMemberManageUow uow)
        {
            this.uow = uow;
            InitializeComponent();
            var q = from d in uow.InventoryCategory.GetAll()
                    join d1 in uow.CategoryDepts.GetAll() on d.Id equals d1.Category into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    where dd1s.Dept == App.MyIdentity.dept.DeptId
                    select d;
            lvCategory.ItemsSource = q.ToList();
            BindInv();
            lvInv.View = lvInv.FindResource("tileView") as ViewBase;
            lvCategory.View = lvInv.FindResource("tileViewCategory") as ViewBase;
            
        }
        private void lvCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindInv();
            lvInv.SelectedItem = null;
            GridSelected.SelectedItem = null;
        }
        private void lvInv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvInv.SelectedItem != null)
                bindSel();
        }
        #region 键盘
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Keyboard.IsOpen = App.IsOpen;
            Keyboard.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Keyboard.IsOpen = false;
            if (GridSelected.SelectedItem != null)
            {
                DXInfo.Models.OrderMenuEx selInv = GridSelected.SelectedItem as DXInfo.Models.OrderMenuEx;
                selInv.Amount = selInv.Quantity * selInv.Price;
            }
            if (GridSelected.ItemsSource != null)
            {
                ObservableCollection<DXInfo.Models.OrderMenuEx> lsi = GridSelected.ItemsSource as ObservableCollection<DXInfo.Models.OrderMenuEx>;

                txtQuantity.Text = lsi.Sum(s => s.Quantity).ToString();
                txtAmount.Text = lsi.Sum(s => s.Amount).ToString("c");
            }
        }
        private void Text_GotFocus(object sender, RoutedEventArgs e)
        {
            Keyboard.IsOpen = App.IsOpen;
            Keyboard.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
        }
        private void Text_LostFocus(object sender, RoutedEventArgs e)
        {
            Keyboard.IsOpen = false; 
        }

        private void TextBox_GotFocus1(object sender, RoutedEventArgs e)
        {
            KeyboardBig.IsOpen = App.IsOpen;
            KeyboardBig.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
        }
        private void TextBox_LostFocus1(object sender, RoutedEventArgs e)
        {
            KeyboardBig.IsOpen = false;
            

        }
        private void Text_GotFocus1(object sender, RoutedEventArgs e)
        {
            KeyboardBig.IsOpen = App.IsOpen;
            KeyboardBig.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
        }
        private void Text_LostFocus1(object sender, RoutedEventArgs e)
        {
            KeyboardBig.IsOpen = false;
        }
        #endregion
        #region 撤销
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //删除记录
            if (GridSelected.SelectedItem != null)
            {
                DXInfo.Models.OrderMenuEx selInv = GridSelected.SelectedItem as DXInfo.Models.OrderMenuEx;
                
                if (desk != null && order != null)
                {
                    
                    DXInfo.Models.OrderMenus orderMenu = uow.OrderMenus.GetById(selInv.Id);//.Where(w => w.Id == selInv.Id).FirstOrDefault();
                    if (orderMenu != null)
                    {
                        if (orderMenu.Status == 2)
                        {
                            MessageBox.Show("已下单不能删除");
                            return;
                        }
                        uow.OrderMenus.Delete(orderMenu);
                        uow.Commit();
                    }
                }
                ObservableCollection<DXInfo.Models.OrderMenuEx> lsi = GridSelected.ItemsSource as ObservableCollection<DXInfo.Models.OrderMenuEx>;
                lsi.Remove(selInv);

                txtQuantity.Text = lsi.Sum(s => s.Quantity).ToString();
                txtAmount.Text = lsi.Sum(s => s.Amount).ToString("c");
            }
        }
        #endregion
        #region 选择菜品
        private void BindInv()
        {
            var invs1 = from idt in uow.InvDepts.GetAll().Where(w => w.Dept == App.MyIdentity.dept.DeptId) select idt.Inv;
            var invs = from i in uow.Inventory.GetAll()
                       orderby i.Code
                       where invs1.Contains(i.Id)
                       select i;
            if (lvCategory.SelectedItem != null)
            {
                DXInfo.Models.InventoryCategory ic = lvCategory.SelectedItem as DXInfo.Models.InventoryCategory;
                invs = invs.Where(w => w.Category == ic.Id);
            }

            lvInv.ItemsSource = invs.ToList();
        }
        #endregion
        #region 选择菜品
        private void bindSel()
        {
            GridSelected.SelectedItem = null;
            ObservableCollection<DXInfo.Models.InventoryEx> lsi = new ObservableCollection<DXInfo.Models.InventoryEx>();
            if (lvInv.SelectedItem != null)
            {
                dynamic d = lvInv.SelectedItem;
                DXInfo.Models.InventoryEx selInv = new DXInfo.Models.InventoryEx();
                selInv.Id = d.Id;
                selInv.Code = d.Code;
                selInv.Name = d.Name;
                selInv.SalePrice = d.SalePrice;
                selInv.Quantity = 1;
                //selInv.Amount = d.SalePrice;
                if (GridSelected.ItemsSource != null)
                {
                    lsi = GridSelected.ItemsSource as ObservableCollection<DXInfo.Models.InventoryEx>;
                    lsi.Add(selInv);
                }
                else
                {
                    lsi.Add(selInv);
                    GridSelected.ItemsSource = lsi;
                }
                txtQuantity.Text = lsi.Sum(s => s.Quantity).ToString();
                txtAmount.Text = lsi.Sum(s => s.Amount).ToString("c");
            }
        }
        #endregion

        private void IntegerUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (GridSelected.SelectedItem != null)
            {
                DXInfo.Models.OrderMenuEx selInv = GridSelected.SelectedItem as DXInfo.Models.OrderMenuEx;
                
                selInv.Amount = selInv.Quantity * selInv.Price;
            }
        }
        #region 确定返回值
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            #region 菜品入库
            Hashtable htAdd = new Hashtable();
            Hashtable htSub = new Hashtable();
            if (GridSelected.ItemsSource == null) ocSelDesks = new ObservableCollection<DXInfo.Models.OrderMenuEx>();
            else
                ocSelDesks = GridSelected.ItemsSource as ObservableCollection<DXInfo.Models.OrderMenuEx>;
            if (desk != null && order != null && ocSelDesks.Count>0)
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    int addmenu = 0;
                    foreach (DXInfo.Models.OrderMenuEx selDesk in ocSelDesks)
                    {
                        if (selDesk.Id != Guid.Empty)
                        {
                            DXInfo.Models.OrderMenus orderMenu = uow.OrderMenus.GetById(selDesk.Id);//.Where(w => w.Id == selDesk.Id).FirstOrDefault();
                            if (orderMenu != null)
                            {
                                if (orderMenu.Status == 2 && orderMenu.Quantity != selDesk.Quantity)
                                {
                                    DXInfo.Models.Inventory inv = uow.Inventory.GetById(orderMenu.InventoryId);//.Where(w => w.Id == orderMenu.InventoryId).FirstOrDefault();
                                    if (!string.IsNullOrEmpty(inv.Printer))
                                    {
                                        if (orderMenu.Quantity > selDesk.Quantity)
                                        {
                                            if (htSub.Contains(inv.Printer))
                                            {

                                                List<InventoryEx> confirmmenu = htSub[inv.Printer] as List<InventoryEx>;
                                                InventoryEx si = new InventoryEx();
                                                si.Category = inv.Category;
                                                //si.Quantity = orderMenu.Quantity - selDesk.Quantity;
                                                si.SalePrice = inv.SalePrice;
                                                //si.Amount = inv.SalePrice * (orderMenu.Quantity - selDesk.Quantity);
                                                si.Quantity = orderMenu.Quantity - selDesk.Quantity;
                                                si.Name = inv.Name;
                                                si.Comment = orderMenu.Comment;
                                                confirmmenu.Add(si);
                                                htSub[inv.Printer] = confirmmenu;
                                            }
                                            else
                                            {
                                                List<InventoryEx> confirmmenu = new List<InventoryEx>();
                                                InventoryEx si = new InventoryEx();
                                                si.Category = inv.Category;
                                                //si.Amount = inv.SalePrice * (orderMenu.Quantity - selDesk.Quantity);
                                                //si.Quantity = orderMenu.Quantity - selDesk.Quantity;
                                                si.SalePrice = inv.SalePrice;
                                                si.Quantity = orderMenu.Quantity - selDesk.Quantity;
                                                si.Name = inv.Name;
                                                si.Comment = orderMenu.Comment;
                                                confirmmenu.Add(si);
                                                htSub.Add(inv.Printer, confirmmenu);

                                            }
                                        }
                                        else
                                        {
                                            if (htAdd.Contains(inv.Printer))
                                            {

                                                List<InventoryEx> confirmmenu = htAdd[inv.Printer] as List<InventoryEx>;
                                                InventoryEx si = new InventoryEx();
                                                si.Category = inv.Category;
                                                //si.Amount = inv.SalePrice*(selDesk.Quantity-orderMenu.Quantity);
                                                //si.Quantity = orderMenu.Quantity - selDesk.Quantity;
                                                si.SalePrice = inv.SalePrice;
                                                si.Quantity = selDesk.Quantity-orderMenu.Quantity;
                                                si.Name = inv.Name;
                                                si.Comment = orderMenu.Comment;
                                                confirmmenu.Add(si);
                                                htAdd[inv.Printer] = confirmmenu;
                                            }
                                            else
                                            {
                                                List<InventoryEx> confirmmenu = new List<InventoryEx>();
                                                InventoryEx si = new InventoryEx();
                                                si.Category = inv.Category;
                                                //si.Amount = inv.SalePrice * (selDesk.Quantity - orderMenu.Quantity);
                                                si.SalePrice = inv.SalePrice;
                                                si.Quantity = selDesk.Quantity - orderMenu.Quantity;
                                                si.Name = inv.Name;
                                                si.Comment = orderMenu.Comment;
                                                confirmmenu.Add(si);
                                                htAdd.Add(inv.Printer, confirmmenu);

                                            }
                                        }
                                    }
                                    
                                }
                                orderMenu.Quantity = selDesk.Quantity;
                                orderMenu.Amount = selDesk.Quantity * orderMenu.Price;
                                orderMenu.Comment = selDesk.Comment;

                                DXInfo.Models.OrderMenusHis menuHis = new DXInfo.Models.OrderMenusHis();
                                menuHis.LinkId = orderMenu.Id;
                                menuHis.OrderId = orderMenu.OrderId;
                                menuHis.InventoryId = orderMenu.InventoryId;
                                menuHis.Price = orderMenu.Price;
                                menuHis.Quantity = orderMenu.Quantity;
                                menuHis.Amount = orderMenu.Amount;
                                menuHis.UserId = App.MyIdentity.oper.UserId;
                                menuHis.CreateDate = DateTime.Now;
                                menuHis.Status = orderMenu.Status;
                                menuHis.Comment = orderMenu.Comment;
                                uow.OrderMenusHis.Add(menuHis);
                            }
                        }
                        else
                        {
                            DXInfo.Models.OrderMenus orderMenu = new DXInfo.Models.OrderMenus();
                            orderMenu.OrderId = order.Id;
                            orderMenu.InventoryId = selDesk.InventoryId;
                            orderMenu.Price = selDesk.Price;
                            orderMenu.Quantity = selDesk.Quantity;
                            orderMenu.Amount = selDesk.Amount;
                            orderMenu.Comment = selDesk.Comment;
                            orderMenu.CreateDate = DateTime.Now;
                            orderMenu.UserId = App.MyIdentity.user.UserId;
                            uow.OrderMenus.Add(orderMenu);

                            uow.Commit();

                            DXInfo.Models.OrderMenusHis menuHis = new DXInfo.Models.OrderMenusHis();
                            menuHis.LinkId = orderMenu.Id;
                            menuHis.OrderId = orderMenu.OrderId;
                            menuHis.InventoryId = orderMenu.InventoryId;
                            menuHis.Price = orderMenu.Price;
                            menuHis.Quantity = orderMenu.Quantity;
                            menuHis.Amount = orderMenu.Amount;
                            menuHis.UserId = App.MyIdentity.oper.UserId;
                            menuHis.CreateDate = DateTime.Now;
                            menuHis.Status = orderMenu.Status;
                            menuHis.Comment = orderMenu.Comment;
                            uow.OrderMenusHis.Add(menuHis);
                            addmenu++;
                        }
                    }
                    if (addmenu > 0)
                    {
                        DXInfo.Models.OrderDishes odish = uow.OrderDishes.GetById(order.Id);//.Where(w => w.Id == order.Id).FirstOrDefault();
                        odish.Status = 0;
                    }
                    uow.Commit();
                    transaction.Complete();
                }
            }
            #endregion
            string deskCodes = "";
            DXInfo.Restaurant.DeskManageFacade dmf = new DXInfo.Restaurant.DeskManageFacade(uow,Guid.Empty,Guid.Empty);
            deskCodes = dmf.GetOrderDeskCodes(uow,order.Id);

            if (htAdd.Count > 0)
            {
               
                foreach (DictionaryEntry de in htAdd)
                {
                    List<InventoryEx> confirmmenu = de.Value as List<InventoryEx>;
                    decimal dsum = confirmmenu.Sum(s => s.Amount);
                    decimal dcount = confirmmenu.Sum(s => s.Quantity);
                    if (confirmmenu.Count > 0)
                    {
                        var ctx = new
                        {
                            DeskNo = deskCodes + "(加单)",
                            Sum = dsum,
                            Count = dcount,
                            lSelInv = confirmmenu,
                            CreateDate = DateTime.Now,
                            DeptName = App.MyIdentity.dept.DeptName,
                        };
                        System.Printing.LocalPrintServer lp = new System.Printing.LocalPrintServer();
                        System.Printing.PrintQueue pq = new System.Printing.PrintQueue(lp, de.Key.ToString());
                        DeskConfirmWindow dcw = new DeskConfirmWindow(ctx, pq);
                        if (dcw.ShowDialog().GetValueOrDefault())
                        {
                        }
                    }
                }
            }
            if (htSub.Count > 0)
            {
                foreach (DictionaryEntry de in htSub)
                {
                    List<InventoryEx> confirmmenu = de.Value as List<InventoryEx>;
                    decimal dsum = confirmmenu.Sum(s => s.Amount);
                    decimal dcount = confirmmenu.Sum(s => s.Quantity);
                    if (confirmmenu.Count > 0)
                    {
                        var ctx = new
                        {
                            DeskNo = deskCodes + "(减单)",
                            Sum = dsum,
                            Count = dcount,
                            lSelInv = confirmmenu,
                            CreateDate = DateTime.Now,
                            DeptName = App.MyIdentity.dept.DeptName,
                        };
                        System.Printing.LocalPrintServer lp = new System.Printing.LocalPrintServer();
                        System.Printing.PrintQueue pq = new System.Printing.PrintQueue(lp, de.Key.ToString());
                        DeskConfirmWindow dcw = new DeskConfirmWindow(ctx, pq);
                        if (dcw.ShowDialog().GetValueOrDefault())
                        {
                        }
                    }
                }
            }
            this.DialogResult = true;
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ocSelDesks != null && ocSelDesks.Count > 0)
            {
                GridSelected.ItemsSource = ocSelDesks;
                txtQuantity.Text = ocSelDesks.Sum(s => s.Quantity).ToString();
                txtAmount.Text = ocSelDesks.Sum(s => s.Amount).ToString("c");
            }
        }
    }
}
