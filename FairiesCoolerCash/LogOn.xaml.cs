using System.Windows;
using FairiesCoolerCash.Business;
using GalaSoft.MvvmLight.Messaging;
using System.Runtime.InteropServices;
using System;
using System.Windows.Interop;
namespace FairiesCoolerCash
{
    /// <summary>
    /// 寻仙记登录窗口
    /// </summary>
    public partial class LogOn : Window
    {
        public LogOn()
        {
            InitializeComponent();
            Messenger.Default.Register<CloseViewMessageToken>(this, Handle_CloseViewMessageToken);
        }
        private void Handle_CloseViewMessageToken(CloseViewMessageToken token)
        {
            this.Close();
        }
    }
}
