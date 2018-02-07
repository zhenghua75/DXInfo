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
using DXInfo.Data.Contracts;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// DataBaseBackupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataBaseBackupWindow : Window
    {
        private bool IsBackup { get; set; }
        private readonly IFairiesMemberManageUow uow;
        public DataBaseBackupWindow(IFairiesMemberManageUow uow,bool isBackup)
        {
            this.uow = uow;
            InitializeComponent();
            this.IsBackup = isBackup;
            if (isBackup)
            {
                btnFileName.Visibility = System.Windows.Visibility.Collapsed;
                btnRestore.Visibility = System.Windows.Visibility.Collapsed;
                
            }
            else
            {
                btnDirectory.Visibility = System.Windows.Visibility.Collapsed;
                btnBackup.Visibility = System.Windows.Visibility.Collapsed;
                this.Title = "恢复数据库";
                this.lblPath.Content = "恢复文件";
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                txtPath.Text = dialog.SelectedPath;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //DXInfo.Models.FairiesMemberManage db = new DXInfo.Models.FairiesMemberManage();
            string strfilename = uow.Db.Connection.Database+DateTime.Now.ToString("yyyyMMddHHmm") + ".bak";
            string backupstr = string.Format("backup database [{0}] to disk='{1}'",uow.Db.Connection.Database,System.IO.Path.Combine(txtPath.Text, strfilename));
            uow.Db.ExecuteSqlCommand(backupstr);
            MessageBox.Show("数据库备份成功,文件名："+strfilename);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //DXInfo.Models.FairiesMemberManage db = new DXInfo.Models.FairiesMemberManage();
            string backupstr = string.Format("restore database [{0}] from disk='{1}'",uow.Db.Connection.Database, txtPath.Text);
            uow.Db.ExecuteSqlCommand(backupstr);
            MessageBox.Show("数据库恢复成功" );
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter = "数据库备份文件(*.bak)|*.bak";
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                txtPath.Text = dialog.FileName;
            }
        }
    }
}
