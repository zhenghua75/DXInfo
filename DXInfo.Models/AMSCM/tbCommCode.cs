//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18052
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DXInfo.Models
{
    using System;
    using System.Runtime.Serialization;
    
    
    [Serializable()]
    [DataContract()]
    public class tbCommCode : Entity
    {
        private int _Id;
        private String _vcCommName;
        
        private String _vcCommCode;
        
        private String _vcCommSign;
        
        private String _vcComments;

        [DataMember()]
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if ((value != _Id))
                {
                    _Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        [DataMember()]
        public String vcCommName
        {
            get
            {
                return _vcCommName;
            }
            set
            {
                if ((value != _vcCommName))
                {
                    _vcCommName = value;
                    OnPropertyChanged("vcCommName");
                }
            }
        }
        
        [DataMember()]
        public String vcCommCode
        {
            get
            {
                return _vcCommCode;
            }
            set
            {
                if ((value != _vcCommCode))
                {
                    _vcCommCode = value;
                    OnPropertyChanged("vcCommCode");
                }
            }
        }
        
        [DataMember()]
        public String vcCommSign
        {
            get
            {
                return _vcCommSign;
            }
            set
            {
                if ((value != _vcCommSign))
                {
                    _vcCommSign = value;
                    OnPropertyChanged("vcCommSign");
                }
            }
        }
        
        [DataMember()]
        public String vcComments
        {
            get
            {
                return _vcComments;
            }
            set
            {
                if ((value != _vcComments))
                {
                    _vcComments = value;
                    OnPropertyChanged("vcComments");
                }
            }
        }
    }
}
