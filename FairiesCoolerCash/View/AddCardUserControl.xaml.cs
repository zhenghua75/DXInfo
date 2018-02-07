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
using System.Threading;
using System.Web.Security;
using DXInfo.Data.Contracts;
using System.Transactions;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// 会员卡发卡
    /// </summary>
    public partial class AddCardUserControl : UserControl
    {
        //private DXInfo.Models.FairiesMemberManage db = new DXInfo.Models.FairiesMemberManage();
        private readonly IFairiesMemberManageUow uow;
        public DXInfo.Models.Cards card = new DXInfo.Models.Cards();
        private List<DXInfo.Models.CardLevels> _lcl;
        private List<DXInfo.Models.CardTypes> _lct;
        public List<DXInfo.Models.CardLevels> lcl { get {            
            return _lcl;
        } }
        public List<DXInfo.Models.CardTypes> lct
        {
            get
            {
                return _lct;
            }
        }
        public AddCardUserControl(IFairiesMemberManageUow uow)
        {
            this.uow = uow;
            InitializeComponent();
            _lcl = uow.CardLevels.GetAll().ToList();
            _lct = uow.CardTypes.GetAll().ToList();
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
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //查询

            var ms = from m in uow.Members.GetAll()
                     join c in uow.aspnet_CustomProfile.GetAll() on m.UserId equals c.UserId into mc
                     from mcs in mc.DefaultIfEmpty()

                     join d in uow.Depts.GetAll() on m.DeptId equals d.DeptId into md
                     from mds in md.DefaultIfEmpty()

                     join c1 in uow.aspnet_CustomProfile.GetAll() on m.ModifyUserId equals c1.UserId into mc1
                     from mc1s in mc1.DefaultIfEmpty()

                     join d1 in uow.Depts.GetAll() on m.ModifyDeptId equals d1.DeptId into md1
                     from md1s in md1.DefaultIfEmpty()

                     select new
                     {
                         m.Comments,
                         m.CreateDate,
                         mds.DeptName,
                         m.Email,
                         m.Id,
                         m.IdCard,
                         m.LinkAddress,
                         m.LinkPhone,
                         m.MemberName,
                         m.MemberType,
                         m.ModifyDate,
                         ModifyDeptName = md1s.DeptName,
                         ModifyFullName = mc1s.FullName,
                         mcs.FullName
                     };

            if (!string.IsNullOrWhiteSpace(MemberName.Text))
                ms = ms.Where(w => w.MemberName.Contains(MemberName.Text));
            if (!string.IsNullOrWhiteSpace(IdCard.Text))
                ms = ms.Where(w => w.IdCard.Contains(IdCard.Text));
            if (!string.IsNullOrWhiteSpace(LinkPhone.Text))
                ms = ms.Where(w => w.LinkPhone.Contains(LinkPhone.Text));
            if (!string.IsNullOrWhiteSpace(LinkAddress.Text))
                ms = ms.Where(w => w.LinkAddress.Contains(LinkAddress.Text));
            if (!string.IsNullOrWhiteSpace(Email.Text))
                ms = ms.Where(w => w.Email.Contains(Email.Text));
            if (!string.IsNullOrWhiteSpace(Comments.Text))
                ms = ms.Where(w => w.Comments.Contains(Comments.Text));

            MemberList.ItemsSource = ms.ToObservableCollection();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //取消
            MemberName.Text = "";
            IdCard.Text = "";
            LinkPhone.Text = "";
            LinkAddress.Text = "";
            Email.Text = "";
            Comments.Text = "";

            MemberList.ItemsSource = null;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //编辑
            dynamic m = MemberList.SelectedItem;
            if (string.IsNullOrWhiteSpace(card.CardNo)) throw new ArgumentNullException("卡号", "请输入卡号");
            if (card.CardLevel == Guid.Empty || card.CardLevel == null) throw new ArgumentNullException("卡级别", "请选择卡级别");

            DXInfo.Models.aspnet_CustomProfile user = App.MyIdentity.oper;
            if (user == null) throw new ArgumentException("操作员信息错误");
            Guid userId = user.UserId;
            if (!user.DeptId.HasValue || user.DeptId == Guid.Empty)
            {
                throw new ArgumentException("部门信息错误");
            }

            var c = uow.Cards.GetAll().Where(w => w.CardNo == card.CardNo).FirstOrDefault();
            if (c != null) throw new ArgumentException("卡号已存在");
            StringBuilder sb = new StringBuilder(33);
            sb.Append(card.CardNo);
            int st = CardRef.CoolerPutCard(sb);

            if (st == 0)
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    card.Member = m.Id;
                    card.DeptId = user.DeptId.Value;
                    card.UserId = userId;
                    card.CreateDate = DateTime.Now;
                    uow.Cards.Add(card);
                    uow.Commit();
                    Common.AddCardsLog(uow,card);
                    uow.Commit();
                    transaction.Complete();
                }
                card = new DXInfo.Models.Cards();
                MessageBox.Show("发卡成功");
            }
            else
            {
                MessageBox.Show(CardRef.GetStr(st));
            }
        }

        private void MemberList_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {
            StackPanel sp = e.DetailsElement as StackPanel;
            sp.DataContext = card;
        }

        private void cmbCardLevel_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            box.ItemsSource = null;
            box.ItemsSource = lcl;
        }        
    }
}
