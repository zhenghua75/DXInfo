using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DXInfo.Models
{
    [Serializable]
    public class InvPriceExList : List<InvPriceEx>, ICloneable
    {
        public InvPriceExList() : base() { }
        public InvPriceExList(int capacity) : base(capacity) { }
        public InvPriceExList(IEnumerable<InvPriceEx> collection)
            : base(collection)
        {
        }
        public object Clone()
        {
            MemoryStream ms = new MemoryStream();
            object obj;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, this);
                ms.Seek(0, SeekOrigin.Begin);
                obj = bf.Deserialize(ms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
            }

            return obj;
        }
    } 
    [NotMapped]
    [Serializable]
    public class InvPriceEx : InvPrice
    {
        private bool _IsSelected;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
    }
}
