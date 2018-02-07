using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DXInfo.Models
{
    [Serializable]
    public class Entity : IEntity, INotifyPropertyChanged//, ICloneable
    {
        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;
        //[method:NonSerialized]
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        //private string customerNameValue;
        //public string CustomerName
        //{
        //    get
        //    {
        //        return customerNameValue;
        //    }
        //    set
        //    {
        //        if (value != customerNameValue)
        //        {
        //            customerNameValue = value;
        //            OnPropertyChanged("CustomerName");
        //        }
        //    }
        //}

        //public object Clone()
        //{
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
