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
    public class WarehouseInventory : Entity
    {
        
        private Guid _Id;
        
        private Guid _Inventory;
        
        private Guid _Warehouse;
        
        private Decimal _Quantity;
        
        [DataMember()]
        public Guid Id
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
        public Guid Inventory
        {
            get
            {
                return _Inventory;
            }
            set
            {
                if ((value != _Inventory))
                {
                    _Inventory = value;
                    OnPropertyChanged("Inventory");
                }
            }
        }
        
        [DataMember()]
        public Guid Warehouse
        {
            get
            {
                return _Warehouse;
            }
            set
            {
                if ((value != _Warehouse))
                {
                    _Warehouse = value;
                    OnPropertyChanged("Warehouse");
                }
            }
        }
        
        [DataMember()]
        public Decimal Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                if ((value != _Quantity))
                {
                    _Quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }
    }
}
