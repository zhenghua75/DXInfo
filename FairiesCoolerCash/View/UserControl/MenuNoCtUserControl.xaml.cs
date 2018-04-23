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
using DXInfo.Models;
using System.Timers;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using DXInfo.Data.Contracts;
using Ninject;
using Unity.ServiceLocation;
namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// 后厨管理
    /// </summary>
    public partial class MenuNoCtUserControl : UserControl
    {
        public MenuNoCtUserControl()
        {
            InitializeComponent();
        }        
    }
}
