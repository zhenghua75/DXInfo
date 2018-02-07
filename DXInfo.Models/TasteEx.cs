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
    public class TasteExList : List<TasteEx>, ICloneable
    {
        public TasteExList() : base() { }
        public TasteExList(int capacity) : base(capacity) { }
        public TasteExList(IEnumerable<TasteEx> collection)
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
    public class TasteEx : Tastes
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
