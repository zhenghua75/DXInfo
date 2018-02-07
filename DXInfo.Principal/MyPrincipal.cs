using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DXInfo.Principal
{
    [Serializable]
    public class MyPrincipal : System.Security.Principal.IPrincipal//, ICloneable
    {
        private MyIdentity _Identity;
        private List<DXInfo.Models.aspnet_Sitemaps> _Func;
        public MyPrincipal(MyIdentity identity, List<DXInfo.Models.aspnet_Sitemaps> lFunc)
        {
            this._Identity = identity;
            this._Func = lFunc;
        }
        public MyPrincipal()
        {
            this._Identity = new MyIdentity();
            this._Func = new List<DXInfo.Models.aspnet_Sitemaps>();
        }
        #region IPrincipal 成员

        public System.Security.Principal.IIdentity Identity
        {
            get { return _Identity; }
        }

        public bool IsInRole(string role)
        {
            return _Func.FindAll(delegate(DXInfo.Models.aspnet_Sitemaps menu) { return menu.Name == role; }).Count > 0;
        }
        public List<DXInfo.Models.aspnet_Sitemaps> Func
        {
            get { return _Func; }
        }
        //public List<string> AllFunc
        //{
        //    get { return _allFunc; }
        //}
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
        #endregion
    }
}
