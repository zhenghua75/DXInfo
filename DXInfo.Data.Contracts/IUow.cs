using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
namespace DXInfo.Data.Contracts
{
    public interface IUow : IDisposable
    {
        void Commit();
        //DbContext Context { get; private set; }
        Database Db { get; set; }
    }
}
