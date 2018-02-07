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

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// DeskQuantityWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeskQuantityWindow : Window
    {
        public int Quantity;
        public DeskQuantityWindow()
        {
            InitializeComponent();
        }
        #region 返回人数
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuantity.Text)) Quantity = 0;
            else
                Quantity = Convert.ToInt32(txtQuantity.Text);
            this.DialogResult = true;
            this.Close();
        }
        #endregion
        private void Text_GotFocus(object sender, RoutedEventArgs e)
        {
            Keyboard.IsOpen = App.IsOpen;
            Keyboard.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
        }
        private void Text_LostFocus(object sender, RoutedEventArgs e)
        {
            Keyboard.IsOpen = false;
        }

    }
}
