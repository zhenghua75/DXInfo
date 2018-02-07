using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncOper
{
    class Program
    {
        static void Main(string[] args)
        {
            string strExit = "";
            do
            {
                Console.WriteLine("请选择重建\n0:重建客户端同步框架,1:重建服务端同步框架,2:重建所有,3:设置备份的数据库,4:建立服务端同步框架");
                string strOper1 = Console.ReadLine();
                DXInfo.Sync.Sync s = DXInfo.Sync.Sync.Instance();
                List<string> scopeNames=new List<string>();
                if (strOper1 != "3")
                {
                    Console.WriteLine("请选择重建的域");
                    Console.WriteLine("-1:所有");
                    for (int i = 0; i < s.ScopeNames.Count; i++)
                    {
                        Console.WriteLine("{0:d}:{1:s}", i, s.ScopeNames[i]);
                    }
                    string strOper2 = Console.ReadLine();
                    int iOper = int.Parse(strOper2);
                    
                    if (iOper == -1)
                    {
                        scopeNames = (from d in s.ScopeNames
                                      select d.Value).ToList();
                    }
                    else
                    {
                        scopeNames = (from d in s.ScopeNames
                                      where d.Key == iOper
                                      select d.Value).ToList();
                    }
                }
                switch (strOper1)
                {
                    case "0":
                        Console.WriteLine("开始重建客户端框架");
                        s.DeProvision(s.ClientConn, scopeNames);
                        Console.WriteLine("客户端框架重建完成");
                        break;
                    case "1":
                        Console.WriteLine("开始重建服务端框架");
                        s.DeProvision(s.ServerConn, scopeNames);
                        Console.WriteLine("服务端框架重建完成");
                        break;
                    case "2":
                        Console.WriteLine("开始重建框架");
                        s.DeProvision(s.ClientConn, scopeNames);
                        s.DeProvision(s.ServerConn, scopeNames);
                        Console.WriteLine("框架重建完成");
                        break;
                    case "3":
                        Console.WriteLine("开始设置备份的数据库");
                        s.RestoreDatabase(s.ClientConn);
                        Console.WriteLine("备份的数据库设置完成");
                        break;
                    case "4":
                        Console.WriteLine("开始建服务端框架");
                        s.ProvisionServer();
                        Console.WriteLine("服务端框架建完成");
                        break;
                }
                strExit = Console.ReadLine();
            }
            while (strExit!="exit");
        }
        
        //private void static testc()
        //{
        //     SqlSyncStoreRestore databaseRestore = new SqlSyncStoreRestore(serverConn);
        //        databaseRestore.PerformPostRestoreFixup();
        //}
    }
}
