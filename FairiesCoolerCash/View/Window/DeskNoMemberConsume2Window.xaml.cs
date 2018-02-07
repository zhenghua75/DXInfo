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
using System.Transactions;
using DXInfo.Data.Contracts;
using AutoMapper;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// NoMemberConsumeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeskNoMemberConsume2Window : Window
    {
        //private DXInfo.Models.FairiesMemberManage db = new DXInfo.Models.FairiesMemberManage();
        private readonly IFairiesMemberManageUow uow;
        public DeskNoMemberConsume2Window(IFairiesMemberManageUow uow,dynamic d,bool isPrint)
        {
            this.uow = uow;
            InitializeComponent();
            GridPrint.DataContext = d;
            GridPrint2.DataContext = d;
            IsPrint = isPrint;
            if (isPrint)
            {
                txtCash.IsEnabled = false;
            }
            string printTitle = Common.PrintTicketTitle(uow, DXInfo.Models.NameCodeType.PrintTicketTitle1OfWR);
            if (!string.IsNullOrEmpty(printTitle))
            {
                this.txtTitle.Text = printTitle;
            }
        }
        public bool IsPrint { get; set; }
        private bool IsCancel { get; set; }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsPrint)
            {
                PrintDialog pDialog1 = new PrintDialog();
                pDialog1.PrintVisual(GridPrint, "非会员消费打印");
                DialogResult = true;
                this.Close();
                return;
            }
            dynamic d = GridPrint.DataContext;
            decimal dCash = Convert.ToDecimal(txtCash.Text);
            decimal dChange = Convert.ToDecimal(txtChange.Text);
            if (dCash-dChange < d.Amount) throw new ArgumentException("收的钱应不小于消费金额");
            Guid orderId = d.OrderId;
            
            using (TransactionScope transaction = new TransactionScope())
            {

                DXInfo.Models.OrderDishes orderDish = uow.OrderDishes.GetById(orderId);//.Where(w => w.Id == orderId).FirstOrDefault();
                if (orderDish.Status != 3)
                {
                    MessageBox.Show("确认下单后才可以结账！");
                    DialogResult = false;
                    this.Close();
                    return;
                }
                orderDish.Status = 1;
                var q = from o in uow.OrderDeskes.GetAll() where o.OrderId == orderDish.Id select o;
                foreach (DXInfo.Models.OrderDeskes orderDesk in q)
                {
                    orderDesk.Status = 1;

                    DXInfo.Models.OrderDeskesHis deskHis = new DXInfo.Models.OrderDeskesHis();
                    deskHis.DeskId = orderDesk.DeskId;
                    deskHis.LinkId = orderDesk.Id;
                    deskHis.OrderId = orderDesk.OrderId;
                    deskHis.UserId = d.UserId;
                    deskHis.CreateDate = DateTime.Now;
                    deskHis.Status = 1;
                    uow.OrderDeskesHis.Add(deskHis);
                }

                DXInfo.Models.Consume consume = new DXInfo.Models.Consume();
                consume.OrderId = d.OrderId;
                consume.Sum = d.Sum;
                consume.Voucher = d.Voucher;
                consume.PayVoucher = d.PayVoucher;
                consume.Amount = d.Amount;
                consume.CreateDate = d.CreateDate;
                consume.DeptId = d.DeptId;
                consume.UserId = d.UserId;
                consume.Cash = dCash;
                consume.Change = dChange;
                consume.ConsumeType = 1;
                consume.PayType = d.PayType;
                consume.Discount = 100;
                consume.Quantity = d.Count;
                consume.SourceType = 1;
                uow.Consume.Add(consume);


                DXInfo.Models.Bills bill = new DXInfo.Models.Bills();
                bill.Sum = d.Sum;
                bill.Voucher = d.Voucher;
                bill.Amount = d.Amount;
                bill.BillType = "WRNoMemberConsumeWindow";
                bill.CreateDate = d.CreateDate;
                bill.DeptName = d.DeptName;
                bill.FullName = d.FullName;
                bill.PayTypeName = d.PayTypeName;
                bill.Cash = dCash;
                bill.Change = dChange;
                uow.Bills.Add(bill);

                uow.Commit();

                //DXInfo.Restaurant.DeskManageFacade dmf = new DXInfo.Restaurant.DeskManageFacade();
                foreach (var si in d.lSelInv)
                {
                    Guid orderMenuId = si.OrderMenuId;
                    DXInfo.Models.OrderMenus om = uow.OrderMenus.GetById(orderMenuId);//.Where(w => w.Id == orderMenuId).FirstOrDefault();                    
                    om.OperDate = DateTime.Now;
                    om.UserId = App.MyIdentity.oper.UserId;
                    om.Status = 8;
                    uow.OrderMenus.Update(om);

                    DXInfo.Models.OrderMenusHis omHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(om);//dmf.CreateOrderMenuHis(om, App.MyIdentity.oper.UserId, DateTime.Now);
                    omHis.LinkId = om.Id;
                    uow.OrderMenusHis.Add(omHis);

                    DXInfo.Models.ConsumeList cl = new DXInfo.Models.ConsumeList();
                    cl.Amount = si.Amount;
                    cl.Consume = consume.Id;
                    cl.CreateDate = d.CreateDate;
                    cl.DeptId = d.DeptId;
                    cl.Inventory = si.Id;
                    cl.Price = si.Price;
                    cl.Quantity = si.Quantity;
                    cl.UserId = d.UserId;
                    cl.IsPackage = si.IsPackage;
                    cl.PackageId = si.PackageId;

                    cl.Discount = 100;
                    cl.Sum = cl.Amount;
                    uow.ConsumeList.Add(cl);

                    DXInfo.Models.BillInvLists bl = new DXInfo.Models.BillInvLists();
                    bl.Amount = si.Amount;
                    bl.Bill = bill.Id;
                    bl.CupType = si.EnglishName;
                    bl.Name = si.Name;
                    bl.Quantity = si.Quantity;
                    bl.SalePrice = si.Price;
                    uow.BillInvLists.Add(bl);

                }
                uow.Commit();
                transaction.Complete();
            }
            PrintDialog pDialog = new PrintDialog();
            pDialog.PageRangeSelection = PageRangeSelection.AllPages;
            pDialog.UserPageRangeEnabled = true;
            pDialog.PrintVisual(GridPrint, "非会员消费打印");
            DialogResult = true;
            this.Close();
        }
        private void Text_GotFocus(object sender, RoutedEventArgs e)
        {
            Keyboard.IsOpen = App.IsOpen;
            Keyboard.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
        }
        private void Text_LostFocus(object sender, RoutedEventArgs e)
        {
            Keyboard.IsOpen = false;

            UpdateCash();
        }
        private void UpdateCash()
        {
            dynamic d = GridPrint.DataContext;
            decimal dCash = 0;
            try
            {
                dCash = Convert.ToDecimal(txtCash.Text);
            }
            catch
            {
                throw new ArgumentException("金额请输入数字");
            }

            if (dCash < d.Amount) throw new ArgumentException("收的钱应不小于消费金额");

            decimal dChange = 0;
            dChange = dCash - d.Amount;
            txtChange.Text = dChange.ToString();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsPrint)
            {
                dynamic d = GridPrint.DataContext;

                DeskNoMemberCashWindow ncw = new DeskNoMemberCashWindow(d.Amount);
                if (ncw.ShowDialog().GetValueOrDefault())
                {
                    decimal dCash = 0;
                    try
                    {
                        if (!string.IsNullOrEmpty(ncw.txtCash.Text))
                        {
                            dCash = Convert.ToDecimal(ncw.txtCash.Text);
                        }
                        else
                        {
                            dCash = d.Amount;
                        }
                    }
                    catch
                    {
                        throw new ArgumentException("金额请输入数字");
                    }

                    if (dCash < d.Amount) throw new ArgumentException("收的钱应不小于消费金额");

                    decimal dChange = 0;
                    dChange = dCash - d.Amount;
                    txtCash.Text = dCash.ToString();
                    txtChange.Text = dChange.ToString();
                    txtCount.Text = Convert.ToString(d.Count);
                    this.IsCancel = false;
                    
                }
                else
                {
                    this.IsCancel = true;
                    this.Close();
                }
            }
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            if (!this.IsCancel)
            {
                Button_Click(null, null);
            }
        }
    }
}
