using Microsoft.Windows.Controls.Ribbon;
using GalaSoft.MvvmLight.Messaging;
using FairiesCoolerCash.Business;
using GalaSoft.MvvmLight.Ioc;
using System.Runtime.InteropServices;
using System;
using System.Windows.Interop;
namespace FairiesCoolerCash
{
    /// <summary>
    /// 主界面
    /// </summary>
    public partial class RibbonMainWindow : RibbonWindow
    {
        public RibbonMainWindow()
        {
            
            InitializeComponent();            
            Messenger.Default.Register<int>(this,"CloseRibbonMainWindow", msg => { 
                this.Close();
                //SimpleIoc.Default.Unregister<RibbonMainWindow>();
                //SimpleIoc.Default.Register<RibbonMainWindow>();
            });
            Messenger.Default.Send(new RibbonMessageToken() { MyRibbon = this.MyRibbon });
        }       
    }

}
