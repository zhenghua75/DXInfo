using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using FairiesCoolerCash.Business;
using GalaSoft.MvvmLight.Messaging;

namespace FairiesCoolerCash
{
    public partial class Login : Window
    {
        #region | Move Window |
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        #endregion

        public Login()
        {
            InitializeComponent();
            Messenger.Default.Register<CloseViewMessageToken>(this, Handle_CloseViewMessageToken);
        }
        private void Handle_CloseViewMessageToken(CloseViewMessageToken token)
        {
            this.Close();
        }
        #region |- Events -|
        private void txtUser_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUser.ClipToBounds = true;
        }
        private void txtUser_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text))
            {
                txtUser.ClipToBounds = false;
            }
        }
        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPassword.ClipToBounds = true;
        }
        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Password))
            {
                txtPassword.ClipToBounds = false;
            }
        }
        private void txtUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowMessage(Visibility.Hidden, "");
        }
        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ShowMessage(Visibility.Hidden, "");
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ReleaseCapture();
                SendMessage((new WindowInteropHelper(this)).Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion

        #region Functions
        private void ShowMessage(Visibility Show, string Message)
        {
            this.txtMsg.Text = Message;
            this.txtMsg.Visibility = Show;
        }
        #endregion
    }
}
