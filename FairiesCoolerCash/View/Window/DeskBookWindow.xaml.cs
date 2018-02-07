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
using System.Threading;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// DeskQuantityWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeskBookWindow : Window
    {
        //private DXInfo.Models.FairiesMemberManage db = new DXInfo.Models.FairiesMemberManage();
        private readonly IFairiesMemberManageUow uow;
        private bool isModify=false;
        private DXInfo.Models.OrderBooks orderBook;
        public DeskBookWindow(IFairiesMemberManageUow uow, bool isModify, DXInfo.Models.OrderBooks orderBook)
        {
            this.uow = uow;
            InitializeComponent();
            this.isModify = isModify;
            this.orderBook = orderBook;
            txtLinkName.Text = orderBook.Customer;
            txtLinkPhone.Text = orderBook.LinkPhone;
            txtQuantity.Text = Convert.ToString(orderBook.Quantity);

            dpBeginDate.Text = orderBook.BookBeginDate.ToString("yyyy/MM/dd");
            dpEndDate.Text = orderBook.BookEndDate.ToString("yyyy/MM/dd");

            tpBeginTime.Value = Convert.ToDateTime(orderBook.BookBeginDate.ToString("yyyy/MM/dd HH:mm"));
            tpEndTime.Value = Convert.ToDateTime(orderBook.BookEndDate.ToString("yyyy/MM/dd HH:mm"));
        }
        public DeskBookWindow()
        {
            InitializeComponent();
            InitDateFormat();
        }
        private void InitDateFormat()
        {
            dpBeginDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            dpEndDate.Text = DateTime.Now.ToString("yyyy/MM/dd");

            tpBeginTime.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
            tpEndTime.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
        }
        #region 返回人数
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isModify)
            {
                int quantity = 0;
                if (!string.IsNullOrEmpty(txtQuantity.Text)) quantity = Convert.ToInt32(txtQuantity.Text);
                //using (TransactionScope transaction = new TransactionScope())
                //{
                //DXInfo.Models.OrderBooks orderBook = uow.OrderBooks.GetById(g=>g.Id==this.orderBook.Id);//.Where(w => w.Id == this.orderBook.Id).FirstOrDefault();
                if (orderBook != null)
                {
                    DXInfo.Principal.MyPrincipal mp = Thread.CurrentPrincipal as DXInfo.Principal.MyPrincipal;
                    DXInfo.Principal.MyIdentity mi = mp.Identity as DXInfo.Principal.MyIdentity;
                    orderBook.CreateDate = DateTime.Now;
                    orderBook.DeptId = mi.dept.DeptId;
                    orderBook.UserId = mi.user.UserId;
                    orderBook.Quantity = quantity;
                    orderBook.Customer = txtLinkName.Text;
                    orderBook.LinkPhone = txtLinkPhone.Text;
                    orderBook.BookBeginDate = Convert.ToDateTime(dpBeginDate.Text + " " + tpBeginTime.Value.GetValueOrDefault().Hour + ":" + tpBeginTime.Value.GetValueOrDefault().Minute);
                    orderBook.BookEndDate = Convert.ToDateTime(dpEndDate.Text + " " + tpEndTime.Value.GetValueOrDefault().Hour + ":" + tpEndTime.Value.GetValueOrDefault().Minute);
                    orderBook.Status = 0;
                    uow.OrderBooks.Update(orderBook);

                    DXInfo.Models.OrderBooksHis orderBookHis = new DXInfo.Models.OrderBooksHis();
                    orderBookHis.LinkId = orderBook.Id;
                    orderBookHis.CreateDate = DateTime.Now;
                    orderBookHis.DeptId = mi.dept.DeptId;
                    orderBookHis.UserId = mi.user.UserId;
                    orderBookHis.Quantity = orderBook.Quantity;
                    orderBookHis.Customer = orderBook.Customer;
                    orderBookHis.LinkPhone = orderBook.LinkPhone;
                    orderBookHis.BookBeginDate = orderBook.BookBeginDate;
                    orderBookHis.BookEndDate = orderBook.BookEndDate;
                    orderBookHis.Status = orderBook.Status;
                    uow.OrderBooksHis.Add(orderBookHis);

                    uow.Commit();

                    //transaction.Complete();
                    //}
                }
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                this.DialogResult = true;
                this.Close();
            }
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
