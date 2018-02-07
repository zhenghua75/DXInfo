using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Xml;
using Media_glass.Common.Items_operations;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Media_glass.Common.Enums;
using System.IO;
using System.Reflection;

namespace Media_glass.Common.Data
{
    class DbLibrary
    {
        private string libraryDirectory;
        private string ConnectionString;
        public DbLibrary(string libraryDirectory)
        {
            this.libraryDirectory = libraryDirectory;
            ExeConfigurationFileMap exeMap =new ExeConfigurationFileMap();
            exeMap.ExeConfigFilename = "FairiesCoolerCash.exe.Config";
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(exeMap, ConfigurationUserLevel.None);//ConfigurationManager.OpenExeConfiguration("FairiesCoolerCash.exe");
            this.ConnectionString = config.ConnectionStrings.ConnectionStrings["FairiesMemberManage"].ConnectionString;
        }
        public DbLibrary()
            : this(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"mp3"))
        {        
        }
        public static bool TryToOpen(string libraryPath)
        {
            try
            {
                System.Windows.Controls.TreeView tv = new System.Windows.Controls.TreeView();
                ItemCollection items = tv.Items;
                DbLibrary library = new DbLibrary(libraryPath);
                System.Windows.Controls.ListView lv = new System.Windows.Controls.ListView();
                library.LoadPlayList(lv.Items);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }     
        string GetPlayListPath(string fileName)
        {
            return System.IO.Path.Combine(libraryDirectory, fileName);
        }
        private DataSet SelectRows(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(queryString, connection);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                return dataset;
            }
        }
        public void LoadPlayList(ItemCollection items)
        {
            items.Clear();

            string sql = @"select * from PlayLists 
where IsEnabled=1 
and cast(GETDATE() as time(7)) between BeginTime and EndTime
and (DeptId is null or DeptId = (select Value from NameCode where Type='LocalDept')) order by Code";
            DataSet ds = SelectRows(sql);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ListViewItem lvi = new ListViewItem();
                ItemContent itemContent = new ItemContent();
                itemContent.Name = dr["Name"].ToString();
                itemContent.MediaType = MediaType.NotPlayed;

                itemContent.Code = dr["Code"].ToString();
                //itemContent.BeginTime = dr["BeginTime"].ToString();
                //itemContent.EndTime = dr["EndTime"].ToString();
                lvi.Content = itemContent;
                lvi.Tag = Path.Combine(this.libraryDirectory, itemContent.Name);
                items.Add(lvi);
            }
        }
    }
}
