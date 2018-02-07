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
using DXInfo.Data.Contracts;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// 数据库备份
    /// </summary>
    public partial class DataBaseBackupUserControl : UserControl
    {
        private readonly IFairiesMemberManageUow uow;
        public DataBaseBackupUserControl(IFairiesMemberManageUow uow)
        {
            this.uow = uow;
            InitializeComponent();
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
            string connStr = uow.Db.Connection.ConnectionString;
            string path = txtPath.Text+@"\"+DateTime.Now.ToString("yyyyMMddHHmm")+".bak";
            string backupstr = "backup database " + uow.Db.Connection.Database + " to disk='" + path + "';";
            uow.Db.ExecuteSqlCommand(backupstr);
        }
    }
}
