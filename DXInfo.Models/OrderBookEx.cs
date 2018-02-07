using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace DXInfo.Models
{
    [NotMapped]
    public class OrderBookEx:OrderBooks
    {
        //private String _StatusName;
        //public String StatusName
        //{
        //    get
        //    {
        //        return _StatusName;
        //    }
        //    set
        //    {
        //        if ((value != _StatusName))
        //        {
        //            _StatusName = value;
        //            OnPropertyChanged("StatusName");
        //        }
        //    }
        //}

        private String _FullName;
        public String FullName
        {
            get
            {
                return _FullName;
            }
            set
            {
                if ((value != _FullName))
                {
                    _FullName = value;
                    OnPropertyChanged("FullName");
                }
            }
        }

        //private String _DeptName;
        //public String DeptName
        //{
        //    get
        //    {
        //        return _DeptName;
        //    }
        //    set
        //    {
        //        if ((value != _DeptName))
        //        {
        //            _DeptName = value;
        //            OnPropertyChanged("DeptName");
        //        }
        //    }
        //}

        private String _DeskNo;
        public String DeskNo
        {
            get
            {
                return _DeskNo;
            }
            set
            {
                if ((value != _DeskNo))
                {
                    _DeskNo = value;
                    OnPropertyChanged("DeskNo");
                }
            }
        }

        //private String _DeskFullName;
        //public String DeskFullName
        //{
        //    get
        //    {
        //        return _DeskFullName;
        //    }
        //    set
        //    {
        //        if ((value != _DeskFullName))
        //        {
        //            _DeskFullName = value;
        //            OnPropertyChanged("DeskFullName");
        //        }
        //    }
        //}

        //private DateTime _DeskCreateDate;
        //public DateTime DeskCreateDate
        //{
        //    get
        //    {
        //        return _DeskCreateDate;
        //    }
        //    set
        //    {
        //        if ((value != _DeskCreateDate))
        //        {
        //            _DeskCreateDate = value;
        //            OnPropertyChanged("DeskCreateDate");
        //        }
        //    }
        //}

        //private int _DeskStatus;
        //public int DeskStatus
        //{
        //    get
        //    {
        //        return _DeskStatus;
        //    }
        //    set
        //    {
        //        if ((value != _DeskStatus))
        //        {
        //            _DeskStatus = value;
        //            OnPropertyChanged("DeskStatus");
        //        }
        //    }
        //}

        //private int _DeskStatusName;
        //public int DeskStatusName
        //{
        //    get
        //    {
        //        return _DeskStatusName;
        //    }
        //    set
        //    {
        //        if ((value != _DeskStatusName))
        //        {
        //            _DeskStatusName = value;
        //            OnPropertyChanged("DeskStatusName");
        //        }
        //    }
        //}

        private Guid _OrderBookDeskId;
        public Guid OrderBookDeskId
        {
            get
            {
                return _OrderBookDeskId;
            }
            set
            {
                if ((value != _OrderBookDeskId))
                {
                    _OrderBookDeskId = value;
                    OnPropertyChanged("OrderBookDeskId");
                }
            }
        }

        private Guid _DeskId;
        public Guid DeskId
        {
            get
            {
                return _DeskId;
            }
            set
            {
                if ((value != _DeskId))
                {
                    _DeskId = value;
                    OnPropertyChanged("DeskId");
                }
            }
        }
    }
}
