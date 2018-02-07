using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DXInfo.Principal
{
    /// <summary>
    /// 自定义用户信息
    /// </summary>
    [Serializable]
    public class MyIdentity : System.Security.Principal.IIdentity//, ICloneable
    {
        private DXInfo.Models.aspnet_CustomProfile _oper;
        private DXInfo.Models.aspnet_Users _user;
        private DXInfo.Models.Depts _dept;
        private string _AuthenticationType;
        //private int _iDiscount;
        public MyIdentity(DXInfo.Models.aspnet_CustomProfile oper,
            DXInfo.Models.aspnet_Users user, DXInfo.Models.Depts dept, string authenticationType)
        {
            this._oper = oper;
            this._AuthenticationType = authenticationType;
            //this._iDiscount = discount;
            this._dept = dept;
            this._user = user;
        }
        public MyIdentity()
        {
            this._oper = new Models.aspnet_CustomProfile();
            this._AuthenticationType = "MyIdentity";
            this._dept = new Models.Depts();
        }
        public DXInfo.Models.aspnet_CustomProfile oper
        {
            get { return _oper; }
        }
        //public int iDiscount
        //{
        //    get
        //    {
        //        if (_dept.cnnDeptID == 0) return 0;
        //        else return _dept.cnnDiscount;
        //    }
        //}
        public DXInfo.Models.Depts dept
        {
            get { return _dept; }
        }
        public DXInfo.Models.aspnet_Users user
        {
            get { return _user; }
        }
        #region IIdentity 成员
        public string AuthenticationType
        {
            get { return _AuthenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string Name
        {
            get { return _user.UserName; }
        }

        #endregion

        //public object Clone()
        //{
        //    //throw new NotImplementedException();
        //    MemoryStream ms = new MemoryStream();
        //    object obj;
        //    try
        //    {
        //        BinaryFormatter bf = new BinaryFormatter();
        //        bf.Serialize(ms, this);
        //        ms.Seek(0, SeekOrigin.Begin);
        //        obj = bf.Deserialize(ms);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        ms.Close();
        //    }

        //    return obj;
        //}
    }
}
