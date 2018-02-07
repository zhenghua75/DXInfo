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

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// 非会员消费
    /// </summary>
    public partial class NoMemberConsumeUserControl : UserControl
    {
        //private DXInfo.Models.FairiesMemberManage db = new DXInfo.Models.FairiesMemberManage();
        private readonly IFairiesMemberManageUow uow;
        public NoMemberConsumeUserControl(IFairiesMemberManageUow uow)
        {
            this.uow = uow;
            InitializeComponent();
            lvCategory.ItemsSource = uow.InventoryCategory.GetAll().OrderBy(o => o.Code).ToList();
            BindInv();
            lvInv.View = lvInv.FindResource("tileView") as ViewBase;
            lvCategory.View = lvInv.FindResource("tileViewCategory") as ViewBase;
        }

        private void BindInv()
        {
            var invs = from i in uow.Inventory.GetAll()
                       join idt in uow.InvDepts.GetAll().Where(w => w.Dept == App.MyIdentity.dept.DeptId) on i.Id equals idt.Inv
                       select i;
           if (lvCategory.SelectedItem != null)
            {
                DXInfo.Models.InventoryCategory ic = lvCategory.SelectedItem as DXInfo.Models.InventoryCategory;
                invs = invs.Where(w => w.Category == ic.Id);
            }
            var linvs = invs.ToList();
            string strpath = AppDomain.CurrentDomain.BaseDirectory;
            var inv = linvs.Select(s => new { s.Id, s.Code,s.Name,s.Category,s.SalePrice,s.SalePrice0,s.SalePrice1,s.SalePrice2
            ,ImageFileName=strpath+@"images\"+s.ImageFileName
            } );

            lvInv.ItemsSource = inv;
        }
        private void bindSel()
        {

            GridSelected.SelectedItem = null;
            ObservableCollection<SelInv> lsi = new ObservableCollection<SelInv>();
            if (lvInv.SelectedItem != null)
            {
                dynamic d = lvInv.SelectedItem;
                SelInv selInv = new SelInv();
                selInv.Id = d.Id;
                selInv.Code = d.Code;
                selInv.Name = d.Name;
                selInv.SalePrice = d.SalePrice;
                selInv.Quantity = 1;
                selInv.Amount = d.SalePrice;
                selInv.CupType = -1;
                selInv.Category = d.Category;

                selInv.lTaste = uow.Tastes.GetAll().Select(s => new SelTaste { Id = s.Id, Title = s.Name, IsSelected = false }).ToList();
                if (GridSelected.ItemsSource != null)
                {
                    lsi = GridSelected.ItemsSource as ObservableCollection<SelInv>;
                    SelInv oldselinv = lsi.FirstOrDefault(delegate(SelInv sim) { return sim.Id == selInv.Id; });
                    if (oldselinv == null)
                    {
                        lsi.Add(selInv);
                    }
                }
                else
                {
                    lsi.Add(selInv);
                    
                    GridSelected.ItemsSource = lsi;
                }
                lbSelected.DataContext = selInv;
            }
            
        }


        private void GridCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindInv();
        }

        private void lvCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindInv();
        }

        private void lvInv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lvInv.SelectedItem!=null)
            bindSel();
        }

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
                SelInv selInv = GridSelected.SelectedItem as SelInv;
                selInv.Amount = selInv.Quantity * selInv.SalePrice;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //删除记录
            if (GridSelected.SelectedItem != null)
            {
                SelInv selInv = GridSelected.SelectedItem as SelInv;
                ObservableCollection<SelInv> lsi = GridSelected.ItemsSource as ObservableCollection<SelInv>;
                lsi.Remove(selInv);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (GridSelected.SelectedItem != null)
            {                
                SelInv si = GridSelected.SelectedItem as SelInv;
                ComboBox cb = sender as ComboBox;
                if (cb.SelectedItem != null)
                {
                    CupTypes ct = cb.SelectedItem as CupTypes;

                    var inv = uow.Inventory.GetById(si.Id);//.Where(w => w.Id == si.Id).FirstOrDefault();
                    if (inv != null)
                    {
                        int cuptype = ct.Id;
                        si.CupType = ct.Id;
                        si.SalePrice = cuptype == -1 ? inv.SalePrice : cuptype == 0 ? inv.SalePrice0 : cuptype == 1 ? inv.SalePrice1 : inv.SalePrice2;
                        si.Amount = si.SalePrice * si.Quantity;
                    }
                }
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
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //结账
            if (GridSelected.ItemsSource == null)
            {
                MessageBox.Show("请选择商品");
                return;
            }
            if (cmbPayType.SelectedItem == null)
            {
                MessageBox.Show("请选择支付方式");
                return;
            }
            DXInfo.Models.PayTypes pt = cmbPayType.SelectedItem as DXInfo.Models.PayTypes;

            if (GridSelected.ItemsSource != null)
            {
                ObservableCollection<SelInv> lsi = GridSelected.ItemsSource as ObservableCollection<SelInv>;
                decimal dAmount = lsi.Sum(s => s.Amount);

                decimal dVoucher = 0;
                if (!string.IsNullOrEmpty(txtVoucher.Text))
                {
                    try
                    {
                        dVoucher = Convert.ToDecimal(txtVoucher.Text);
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("代金券请输入数字");
                    }
                }

                Guid deptId = App.MyIdentity.oper.DeptId.Value;

                var lselInv = lsi.Select(s => new
                {
                    s.Id,
                    s.Code,
                    s.Name,
                    s.SalePrice,
                    s.Quantity,
                    s.Amount,
                    CupType = s.CupType == -1 ? "标准杯" : s.CupType == 0 ? "大杯" : s.CupType == 1 ? "中杯" : "小杯",
                    Cup = s.CupType,
                    lTastes=s.lTaste.Where(w => w.IsSelected).ToList(),
                    Tastes = s.lTaste.Where(w => w.IsSelected == true).Count() == 0 ? "" : s.lTaste.Where(w => w.IsSelected == true).Select(l => l.Title).Aggregate((total, next) => (total + "," + next))
                    ,
                    ConsumeTastes = s.lTaste.Where(w => w.IsSelected == true)
                });
                decimal dCash = dAmount > dVoucher ? dAmount - dVoucher : 0;
                decimal dChange = 0;
                var ctx = new 
                {
                    
                    UserId = App.MyIdentity.oper.UserId,
                    FullName = App.MyIdentity.oper.FullName,
                    DeptId = App.MyIdentity.oper.DeptId.Value,
                    DeptName = App.MyIdentity.dept.DeptName,
                    Sum = dAmount,                    
                    Voucher = dVoucher,
                    PayVoucher = dVoucher > dAmount ? dAmount : dVoucher,
                    Amount = dAmount>dVoucher?dAmount-dVoucher:0,    
                    PayType = pt.Id,
                    PayTypeName = pt.Name,
                    CreateDate = DateTime.Now,
                    lSelInv = lselInv,
                    Cash = dCash,
                    Change=dChange
                };
                NoMemberConsumeWindow cw = new NoMemberConsumeWindow(uow,ctx,false);
                if (cw.ShowDialog().GetValueOrDefault())
                {
                    MessageBox.Show("非会员消费成功");
                    GridSelected.ItemsSource = null;
                    lvInv.SelectedItem = null;
                    lvCategory.SelectedItem = null;
                    lbSelected.DataContext = null;
                }
                
            }
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            List<DXInfo.Models.CupTypes> lst;
            DXInfo.Models.CupTypes.GetCupTypes(out lst);
            cb.ItemsSource = lst;
        }

        private void cmbPayType_Loaded(object sender, RoutedEventArgs e)
        {
            var pt = uow.PayTypes.GetAll().OrderBy(w => w.Code).ToList();
            cmbPayType.ItemsSource = pt;
            cmbPayType.SelectedIndex = 0;
        }

        private void RadioButton_Loaded(object sender, RoutedEventArgs e)
        {
        }


        private void lbCupType_Loaded(object sender, RoutedEventArgs e)
        {
            //CupTypeHelper ch = new CupTypeHelper();
            List<DXInfo.Models.CupTypes> lst;
            DXInfo.Models.CupTypes.GetCupTypes(out lst);
            lbCupType.ItemsSource = lst;
        }

        private void lbTast_Loaded(object sender, RoutedEventArgs e)
        {
            lbTast.ItemsSource = uow.Tastes.GetAll().Select(s => new SelTaste { Id = s.Id, Title = s.Name, IsSelected = false }).ToList();
        }

        private void GridSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GridSelected.SelectedItem != null)
            {
                SelInv si = GridSelected.SelectedItem as SelInv;
                lbSelected.DataContext = si;
            }
        }

        private void lbTast_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lbCupType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lbSelected.DataContext !=null)
            {
                SelInv si = lbSelected.DataContext as SelInv;
                ListBox cb = sender as ListBox;
                if (cb.SelectedItem != null)
                {
                    CupTypes ct = cb.SelectedItem as CupTypes;

                    var inv = uow.Inventory.GetById(si.Id);//.Where(w => w.Id == si.Id).FirstOrDefault();
                    if (inv != null)
                    {
                        int cuptype = ct.Id;
                        si.CupType = ct.Id;
                        si.SalePrice = cuptype == -1 ? inv.SalePrice : cuptype == 0 ? inv.SalePrice0 : cuptype == 1 ? inv.SalePrice1 : inv.SalePrice2;
                        si.Amount = si.SalePrice * si.Quantity;
                    }
                }
            }
        }

    }
}
