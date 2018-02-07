using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DXInfo.CodeGenerate
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectiongStringName = "FairiesMemberManage";
            //modelGenerate();
            string strsql = "select name from sys.tables where name not like '%_tracking' and type='U' order by name";
            DXInfoGenerate dg = new DXInfoGenerate(strsql, connectiongStringName);
            dg.GenerateCode();

            connectiongStringName = "AMSCM";
            strsql = @"select name from sys.tables where name not like '%_tracking' and type='U' and name not in (
select name from zx20111224.sys.tables) and name not in ('dtproperties','tbCommCodeTmp','tbConsItemHung','tbPyCode','tbAssChange','tbEmpSchLog','tbDeptMapInfo')
order by name";
            dg = new DXInfoGenerate(strsql, connectiongStringName);
            dg.GenerateCode();
        }        
    }
}
