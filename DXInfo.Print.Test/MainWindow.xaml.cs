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
using System.Drawing;

namespace DXInfo.Print.Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void Print()
        {
            int numCustomers = 1;
            Font PrintFont = new Font("Arial", 10);
            Font headFont = new Font("Arial", 12, System.Drawing.FontStyle.Bold);

            PrintElement Header = new PrintElement(null);
            Header.AddMiddleText("Report", headFont);
            Header.AddHorizontalRule();

            PrintElement Footer = new PrintElement(null);

            Footer.AddHorizontalRule();
            Footer.AddMiddleText("Confidential", headFont);
            PrintEngine _engine = new PrintEngine(Header, Footer);

            for (int n = 0; n < numCustomers; n++)
            {
                Customer theCustomer = new Customer();
                theCustomer.Id = n + 1;
                theCustomer.FirstName = "Darren";
                theCustomer.LastName = "Clarke";
                theCustomer.Company = "Madras inc.";
                theCustomer.Email = "darren@pretendcompany.com";
                theCustomer.Phone = "602 555 1234";

                _engine.AddPrintObject(theCustomer);
            }
            _engine.Print();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Print();
        }
    }
}
