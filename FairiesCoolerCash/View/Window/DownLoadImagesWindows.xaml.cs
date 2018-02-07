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
using System.ComponentModel;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Threading;
using DXInfo.Data.Contracts;
using DXInfo.Models;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// 下载图片
    /// </summary>
    public partial class DownLoadImagesWindows : Window
    {
        public DownLoadImagesWindows()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    
}
