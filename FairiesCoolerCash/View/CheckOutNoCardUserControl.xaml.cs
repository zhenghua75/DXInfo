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
using DXInfo.Data.Contracts;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// CheckOutNoCardPage.xaml 的交互逻辑
    /// </summary>
    public partial class CheckOutNoCardUserControl : UserControl
    {
        private readonly IFairiesMemberManageUow uow;
        public CheckOutNoCardUserControl(IFairiesMemberManageUow uow)
        {
            this.uow = uow;
            InitializeComponent();
            
        }

        private DXInfo.Models.Cards getCard(string strCardNo)
        {
            //DXInfo.Models.FairiesMemberManage db = new DXInfo.Models.FairiesMemberManage();
            DXInfo.Models.Cards card = uow.Cards.GetAll().Where(w => w.CardNo == strCardNo).FirstOrDefault();
            return card;
        }

        private List<DXInfo.Models.OrderDishes> getOrder(string deskNo,DateTime beginDate,DateTime endDate)
        {
            //DXInfo.Models.FairiesMemberManage db = new DXInfo.Models.FairiesMemberManage();
            DXInfo.Models.Desks desk = uow.Desks.GetAll().Where(w => w.Code == deskNo).FirstOrDefault();
            if (desk == null)
            {
                MessageBox.Show("未找到桌台");
                return new List<DXInfo.Models.OrderDishes>();
            }

            var q = (from d in uow.OrderDeskes.GetAll()
                     join o in uow.OrderDishes.GetAll() on d.OrderId equals o.Id
                    where d.DeskId == desk.Id && o.CreateDate >= beginDate && o.CreateDate <= endDate
                    select o).Distinct().ToList();
            return q;
        }

        private List<DXInfo.Models.OrderMenus> getMenu(Guid orderId)
        {
            //DXInfo.Models.FairiesMemberManage db = new DXInfo.Models.FairiesMemberManage();
            var q = (from m in uow.OrderMenus.GetAll() where m.OrderId == orderId select m).ToList();
            return q;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            card.DataContext = getCard(CardNo.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DateTime dtBeginDate = Convert.ToDateTime(BeginDate.Text + " " + BeginTime.Value.Value.ToShortTimeString());
            DateTime dtEndDate = Convert.ToDateTime(EndDate.Text + " " + EndTime.Value.Value.ToShortTimeString());
            OrderDishes.ItemsSource = getOrder(DeskNo.Text, dtBeginDate, dtEndDate);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (OrderDishes.SelectedItem != null)
            {
                DXInfo.Models.OrderDishes od = OrderDishes.SelectedItem as DXInfo.Models.OrderDishes;
                 //DXInfo.Models.FairiesMemberManage db = new DXInfo.Models.FairiesMemberManage();
                var q = (from m in uow.OrderMenus.GetAll()
                         join i in uow.Inventory.GetAll() on m.InventoryId equals i.Id into mi
                          from mis in mi.DefaultIfEmpty()

                         join u in uow.aspnet_CustomProfile.GetAll() on m.UserId equals u.UserId into mu
                          from mus in mu.DefaultIfEmpty()

                          where m.OrderId == od.Id
                          select new
                          {
                              m.Id,
                              m.InventoryId,
                              mis.Name,
                              m.Price,
                              m.Quantity,
                              m.Amount,
                              mus.FullName,m.CreateDate,m.Status,
                          }).ToList();
                 MemberList.ItemsSource = q;
                 Amount.Text = q.Sum(s => s.Amount).ToString();
            }
        }
        private void MemberBalance(Guid orderId, string cardNo)
        {
            //结账
            //DXInfo.Models.FairiesMemberManage db = new DXInfo.Models.FairiesMemberManage();

            DXInfo.Models.Cards card = uow.Cards.GetAll().Where(w => w.CardNo == cardNo).FirstOrDefault();
            if (card == null)
            {
                MessageBox.Show(cardNo + "未找到会员卡");
                return;
            }
            DXInfo.Models.Members member = uow.Members.GetAll().Where(w => w.Id == card.Member).FirstOrDefault();
            if (member == null)
            {
                MessageBox.Show("未找到会员信息");
                return;
            }
            DXInfo.Models.CardLevels cardLevel = uow.CardLevels.GetAll().Where(w => w.Id == card.CardLevel).FirstOrDefault();
            if (cardLevel == null)
            {
                MessageBox.Show("未找到卡级别参数");
                return;
            }
            DXInfo.Models.OrderDishes orderDish = uow.OrderDishes.GetAll().Where(w => w.Id == orderId && w.Status == 2).FirstOrDefault();
            if (orderDish == null)
            {
                MessageBox.Show("已撤销桌台才可无卡结账");
                return;
            }
            decimal dDiscount = cardLevel.Discount;
            //bool isOut = false;
            Guid gPayType = Guid.Empty;

            var lsi = (from o in uow.OrderMenus.GetAll()
                       join i in uow.Inventory.GetAll() on o.InventoryId equals i.Id into oi
                       from ois in oi.DefaultIfEmpty()
                       join c in uow.InventoryCategory.GetAll() on ois.Category equals c.Id into ic
                       from ics in ic.DefaultIfEmpty()
                       where o.OrderId == orderId && !(o.Status == 0 || o.Status == 1 || o.Status == 7)
                       select new { ics.IsDiscount, o.InventoryId, ois.Code, ois.Name, ois.EnglishName, o.Price, o.Comment, o.Status, o.Id, o.Amount, o.Quantity, ois.Category }).ToList();

            decimal dSum = lsi.Sum(s => s.Amount);

            decimal dSum1 = lsi.Where(w => w.IsDiscount).Sum(s => s.Amount);
            decimal dSum2 = lsi.Where(w => !w.IsDiscount).Sum(s => s.Amount);

            int iCount = Convert.ToInt32(lsi.Sum(s => s.Quantity));


            decimal dAmount = (Math.Round(dSum1 * dDiscount / 100, 2) + dSum2);

            if (dAmount > card.Balance)
            {
                MessageBox.Show("余额不足");
                return;
            }
            //消费积分
            Guid deptId = App.MyIdentity.oper.DeptId.Value;

            decimal point = 0;
            if (dAmount > 0)
            {
                var cp1 = uow.ConsumePoints.GetAll().Where(w => w.DeptId == deptId);
                var cp = cp1.Count() > 0 ? cp1 : uow.ConsumePoints.GetAll();
                foreach (var si in lsi)
                {
                    if (cp.Count() > 0)
                    {
                        var cpc = cp.Where(w => w.Category == si.Category);
                        if (cpc.Count() > 0)
                        {
                            decimal min = cpc.Min(m => m.Point / m.Amount);
                            point += si.Amount * min;
                        }
                        else
                        {
                            decimal min = cp.Min(m => m.Point / m.Amount);
                            point += si.Amount * min;
                        }
                    }
                }
            }
            var lselInv = lsi.Select(s => new
            {
                OrderMenuId = s.Id,
                Id = s.InventoryId,
                s.Code,
                s.Name,
                s.EnglishName,
                s.Price,
                s.Quantity,
                s.Amount,
                s.Comment,
                s.IsDiscount,
                Status = s.Status
            });
            Guid cardId = card.Id;
            DateTime dtn = DateTime.Now.AddDays(-1);
            var di = (from d1 in uow.CardDonateInventory.GetAll().Where(w => w.CardId == cardId).Where(w => w.IsValidate).Where(w => w.InvalideDate > dtn)
                      join i in uow.Inventory.GetAll() on d1.Inventory equals i.Id into d1i
                      from d1is in d1i.DefaultIfEmpty()
                      select new { d1is.Id, d1is.Name }).ToList();

            var ctx = new
            {
                Id = card.Id,
                CardNo = card.CardNo,
                MemberName = member.MemberName,
                UserId = App.MyIdentity.oper.UserId,
                FullName = App.MyIdentity.oper.FullName,
                DeptId = App.MyIdentity.oper.DeptId.Value,
                DeptName = App.MyIdentity.dept.DeptName,
                Sum = dSum,
                Voucher = 0,
                PayVoucher = 0,
                Discount = dDiscount,
                Amount = dAmount,
                LastBalance = card.Balance,
                Balance = card.Balance - dAmount,
                CreateDate = DateTime.Now,
                lSelInv = lselInv,
                Point = point,
                Count = iCount,
                CardDonateInventory = di,
                PayType = gPayType,
                OrderId = orderId
            };
            DeskNoCardConsume2Window cw = new DeskNoCardConsume2Window(uow,ctx);
            if (cw.ShowDialog().GetValueOrDefault())
            {

            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (OrderDishes.SelectedItem != null)
            {
                if (MessageBox.Show("卡号：" + CardNo.Text, "无卡结账提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DXInfo.Models.OrderDishes orderDishe = OrderDishes.SelectedItem as DXInfo.Models.OrderDishes;
                    MemberBalance(orderDishe.Id, CardNo.Text);

                    card.DataContext = null;
                    OrderDishes.ItemsSource = null;
                    MemberList.ItemsSource = null;
                    Amount.Text = "";
                }
            }
        }
    }
}
