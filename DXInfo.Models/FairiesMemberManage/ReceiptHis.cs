namespace DXInfo.Models
{
    using System;
    using System.Runtime.Serialization;
    [Serializable()]
    [DataContract()]
    public class ReceiptHis : Entity
    {
        private Guid _Id;
        private Guid _LinkId;
        private Guid _Member;
        private String _Sn;
        private String _Content;
        private Int32 _Status;
        private Int32 _ReceiptType;
        private String _Comment;
        private Guid _UserId;
        private Guid _DeptId;
        private DateTime _CreateDate;
        private Guid? _ModifyUserId;
        private Guid? _ModifyDeptId;
        private DateTime? _ModifyDate;
        
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
        public Guid LinkId
        {
            get
            {
                return _LinkId;
            }
            set
            {
                if ((value != _LinkId))
                {
                    _LinkId = value;
                    OnPropertyChanged("LinkId");
                }
            }
        }
        [DataMember()]
        public Guid Member
        {
            get
            {
                return _Member;
            }
            set
            {
                if ((value != _Member))
                {
                    _Member = value;
                    OnPropertyChanged("Member");
                }
            }
        }
        [DataMember()]
        public String Sn
        {
            get
            {
                return _Sn;
            }
            set
            {
                if ((value != _Sn))
                {
                    _Sn = value;
                    OnPropertyChanged("Sn");
                }
            }
        }
        [DataMember()]
        public String Content
        {
            get
            {
                return _Content;
            }
            set
            {
                if ((value != _Content))
                {
                    _Content = value;
                    OnPropertyChanged("Content");
                }
            }
        }
        [DataMember()]
        public Int32 Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if ((value != _Status))
                {
                    _Status = value;
                    OnPropertyChanged("Status");
                }
            }
        }
        [DataMember()]
        public Int32 ReceiptType
        {
            get
            {
                return _ReceiptType;
            }
            set
            {
                if ((value != _ReceiptType))
                {
                    _ReceiptType = value;
                    OnPropertyChanged("ReceiptType");
                }
            }
        }
        [DataMember()]
        public String Comment
        {
            get
            {
                return _Comment;
            }
            set
            {
                if ((value != _Comment))
                {
                    _Comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }
        [DataMember()]
        public Guid UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if ((value != _UserId))
                {
                    _UserId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }
        [DataMember()]
        public Guid DeptId
        {
            get
            {
                return _DeptId;
            }
            set
            {
                if ((value != _DeptId))
                {
                    _DeptId = value;
                    OnPropertyChanged("DeptId");
                }
            }
        }
        [DataMember()]
        public DateTime CreateDate
        {
            get
            {
                return _CreateDate;
            }
            set
            {
                if ((value != _CreateDate))
                {
                    _CreateDate = value;
                    OnPropertyChanged("CreateDate");
                }
            }
        }
        [DataMember()]
        public Guid? ModifyUserId
        {
            get
            {
                return _ModifyUserId;
            }
            set
            {
                if ((value != _ModifyUserId))
                {
                    _ModifyUserId = value;
                    OnPropertyChanged("ModifyUserId");
                }
            }
        }
        [DataMember()]
        public Guid? ModifyDeptId
        {
            get
            {
                return _ModifyDeptId;
            }
            set
            {
                if ((value != _ModifyDeptId))
                {
                    _ModifyDeptId = value;
                    OnPropertyChanged("ModifyDeptId");
                }
            }
        }
        [DataMember()]
        public DateTime? ModifyDate
        {
            get
            {
                return _ModifyDate;
            }
            set
            {
                if ((value != _ModifyDate))
                {
                    _ModifyDate = value;
                    OnPropertyChanged("ModifyDate");
                }
            }
        }        
    }
}
