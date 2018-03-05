using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using DXInfo.Data.Contracts;
using System.Threading;
using FairiesCoolerCash.Business;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using AutoMapper;
using System.IO;
using System.Windows.Media.Imaging;

namespace FairiesCoolerCash.ViewModel
{
    public class MyViewModelBase : ViewModelBase, IDataErrorInfo, IValidationExceptionHandler
    {
        #region 构造
        public IFairiesMemberManageUow Uow { get; set; }
        public DXInfo.Business.Common BusinessCommon { get; set; }
        public FairiesCoolerCash.Business.Common ClientCommon { get; set; }
        private DXInfo.Models.FairiesCoolerCashConfiguration conf;
        private string path;
        private System.Xml.Serialization.XmlSerializer xmlSerializer;
        /// <summary>
        /// 抹零
        /// </summary>
        public bool Erasing { get; set; }
        public MyViewModelBase(IFairiesMemberManageUow uow, List<string> lValidationPropertyNames)
        {
            SetDept();
            SetUser();
            SetOper();
            //SetMyPricipal();
            //SetMyIdentity();
            //SetFunc();
            this.Uow = uow;
            this.SetValidate(lValidationPropertyNames);

            //Messenger.Default.Register<IsOpenMessageToken>(this, Handle_IsOpenMessageToken);
            //Messenger.Default.Register<IsStickerPrintToken>(this, Handle_IsStickerPrintToken);
            this.LoadData();

            path = "FairiesCoolerCash.xml";
            xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(DXInfo.Models.FairiesCoolerCashConfiguration));
            InitConf();

            this.ClientCommon = new Common(uow);
            this.Erasing = this.ClientCommon.Erasing();
            if (this.Oper != null && this.Dept != null)
            {
                this.BusinessCommon = new DXInfo.Business.Common(uow, this.Oper.UserId, this.Dept.DeptId, this.Dept.OrganizationId,this.Dept.DeptCode,this.User.UserName);
            }
        }
        protected void SetValidate(List<string> lValidationPropertyNames)
        {
            this.validators = this.GetType()
                .GetProperties()
                .Where(p => this.GetValidations(p).Length != 0 && lValidationPropertyNames.Exists(delegate(string str) { return str == p.Name; }))
                .ToDictionary(p => p.Name, p => this.GetValidations(p));

            this.propertyGetters = this.GetType()
                .GetProperties()
                .Where(p => this.GetValidations(p).Length != 0 && lValidationPropertyNames.Exists(delegate(string str) { return str == p.Name; }))
                .ToDictionary(p => p.Name, p => this.GetValueGetter(p));
        }
        private void InitConf()
        {
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    conf = (DXInfo.Models.FairiesCoolerCashConfiguration)xmlSerializer.Deserialize(fs);
                }
            }
            else
            {
                conf = new DXInfo.Models.FairiesCoolerCashConfiguration();
                conf.IsOpen = false;
                conf.IsStickerPrint = false;
                conf.IsTicket1 = true;
                conf.IsTicket2 = true;
                conf.IsTicket3 = false;
                conf.IsThree = false;
                conf.IsPrintOrder = true;
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    xmlSerializer.Serialize(fs, conf);
                }
            }
            App.IsOpen = conf.IsOpen;
            App.IsStickerPrint = conf.IsStickerPrint;
            App.IsTicket1 = conf.IsTicket1;
            App.IsTicket2 = conf.IsTicket2;
            App.IsTicket3 = conf.IsTicket3;
            App.IsThree = conf.IsThree;
        }
        private void SaveConf()
        {
            conf.IsOpen = App.IsOpen;
            conf.IsStickerPrint = App.IsStickerPrint;
            conf.IsTicket1 = App.IsTicket1;
            conf.IsTicket2 = App.IsTicket2;
            conf.IsTicket3 = App.IsTicket3;
            conf.IsThree = App.IsThree;
            conf.IsPrintOrder = App.IsPrintOrder;
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                xmlSerializer.Serialize(fs, conf);
            }
        }  
        public override void Cleanup()
        {
            base.Cleanup();
            this.Amount = null;
            this.BalanceSum = null;
            this.Card = null;
            this.CardBalance = null;
            this.CardDept = null;
            this.CardLevel = null;
            this.CardNo = null;
            this.CardPwd = null;            
            this.CardType = null;
            this.Cash = null;
            this.Comments = null;
            this.CurrentOperate = null;
            this.CurrentSyncOperate = null;
            this.Customer = null;
            this.DeskNo = null;
            this.DialogResult = null;
            this.Donate = null;
            this.Email = null;            
            this.IdCard = null;
            if (this.lBillType != null)
            {
                this.lBillType.Clear();
                this.lBillType = null;
            }
            if (this.lCardDonateInventoryEx != null)
            {
                this.lCardDonateInventoryEx.Clear();
                this.lCardDonateInventoryEx = null;
            }
            if (this.lCardLevel != null)
            {
                this.lCardLevel.Clear();
                this.lCardLevel = null;
            }
            if (this.lCardStatus != null)
            {
                this.lCardStatus.Clear();
                this.lCardStatus = null;
            }
            if (this.lCardType != null)
            {
                this.lCardType.Clear();
                this.lCardType = null;
            }
            if (this.lCupType != null)
            {
                this.lCupType.Clear();
                this.lCupType = null;
            }
            if (this.lDeskEx != null)
            {
                this.lDeskEx.Clear();
                this.lDeskEx = null;
            }            
            this.LinkAddress = null;
            this.LinkPhone = null;
            if (this.lOper != null)
            {
                this.lOper.Clear();
                this.lOper = null;
            }
            if (this.lPayType != null)
            {
                this.lPayType.Clear();
                this.lPayType = null;
            }
            if (this.lPayTypeAll != null)
            {
                this.lPayTypeAll.Clear();
                this.lPayTypeAll = null;
            }
            if (this.lPayTypeCard != null)
            {
                this.lPayTypeCard.Clear();
                this.lPayTypeCard = null;
            }
            if (this.lPayTypeOfPutCard != null)
            {
                this.lPayTypeOfPutCard.Clear();
                this.lPayTypeOfPutCard = null;
            }            
            if (this.lRoom != null)
            {
                this.lRoom.Clear();
                this.lRoom = null;
            }
            if (this.lSelectedOrderPackage != null)
            {
                this.lSelectedOrderPackage.Clear();
                this.lSelectedOrderPackage = null;
            }
            if (this.lTaste != null)
            {
                this.lTaste.Clear();
                this.lTaste = null;
            }
            if (this.lTasteEx != null)
            {
                this.lTasteEx.Clear();
                this.lTasteEx = null;
            }
            this.Member = null;
            this.MemberName = null;
            this.MyDataGrid = null;
            this.MyQuery = null;
            this.Name = null;
            if (this.OCCupType != null)
            {
                this.OCCupType.Clear();
                this.OCCupType = null;
            }
            if (this.OCDeskEx != null)
            {
                this.OCDeskEx.Clear();
                this.OCDeskEx = null;
            }
            if (this.OCDownloadFileInfo != null)
            {
                this.OCDownloadFileInfo.Clear();
                this.OCDownloadFileInfo = null;
            }
            if (this.OCInventory != null)
            {
                this.OCInventory.Clear();
                this.OCInventory = null;
            }
            if (this.OCInventoryCategory != null)
            {
                this.OCInventoryCategory.Clear();
                this.OCInventoryCategory = null;
            }
            if (this.OCInventoryEx != null)
            {
                this.OCInventoryEx.Clear();
                this.OCInventoryEx = null;
            }
            if (this.OCOrderBookEx != null)
            {
                this.OCOrderBookEx.Clear();
                this.OCOrderBookEx = null;
            }
            if (this.OCOrderMenuEx != null)
            {
                this.OCOrderMenuEx.Clear();
                this.OCOrderMenuEx = null;
            }
            if (this.OCRoom != null)
            {
                this.OCRoom.Clear();
                this.OCRoom = null;
            }
            this.OpenOperName = null;
            this.Password = null;
            this.Points = null;
            this.SelectedBillType = null;            
            this.SelectedCardLevel = null;
            this.SelectedCardStatus = null;
            this.SelectedCardType = null;
            this.SelectedConsumeType = null;
            this.SelectedCupType = null;
            this.SelectedDeskEx = null;
            this.SelectedInventory = null;
            this.SelectedInventoryCategory = null;
            this.SelectedInventoryEx = null;
            this.SelectedOper = null;
            this.SelectedOrderBook = null;
            this.SelectedOrderBookDesk = null;
            this.SelectedOrderBookEx = null;
            this.SelectedOrderBookStatus = null;
            this.SelectedOrderDesk = null;
            this.SelectedOrderDish = null;
            this.SelectedOrderDishStatus = null;
            this.SelectedOrderMenuEx = null;
            this.SelectedOrderMenuStatus = null;
            this.SelectedPayType = null;
            this.SelectedRechargeType = null;
            this.SelectedResult = null;
            this.SelectedRoom = null;
            this.SelectedTaste = null;
            this.SelectedTasteEx = null;
            this.Sum = null;
            this.SumAmount = null;
            this.SumDonate = null;
            this.SumPayable = null;
            this.SumQuantity = null;
            if (this.SyncProgressMsg != null)
            {
                this.SyncProgressMsg.Clear();
                this.SyncProgressMsg = null;
            }
            this.Title = null;
            
            //if (this.Uow != null)
            //{
            //    this.Uow.Dispose();
            //    this.Uow = null;
            //}
            this.UserName = null;
            this.Voucher = null;

            this.Dept = null;
            this.User = null;
            this.Oper = null;
            //this.MyPricipal = null;
            //this.MyIdentity = null;
            //if (this.Func != null)
            //{
            //    this.Func.Clear();
            //    this.Func = null;
            //}
        }
        public virtual void LoadData()
        {
        }
        #endregion

        #region 登录对象
        private void SetDept()
        {
            DXInfo.Principal.MyPrincipal mp = Thread.CurrentPrincipal as DXInfo.Principal.MyPrincipal;
            if (mp != null)
            {
                DXInfo.Principal.MyIdentity mi = mp.Identity as DXInfo.Principal.MyIdentity;
                DXInfo.Models.Depts dept = DXInfo.Business.Helper.CloneOf(mi.dept);
                mp = null;
                mi = null;
                this.Dept = dept;
            }
        }
        private void SetUser()
        {
            DXInfo.Principal.MyPrincipal mp = Thread.CurrentPrincipal as DXInfo.Principal.MyPrincipal;
            if (mp != null)
            {
                DXInfo.Principal.MyIdentity mi = mp.Identity as DXInfo.Principal.MyIdentity;
                DXInfo.Models.aspnet_Users user = DXInfo.Business.Helper.CloneOf(mi.user);
                mp = null;
                mi = null;
                this.User = user;
            }
        }
        private void SetOper()
        {
            DXInfo.Principal.MyPrincipal mp = Thread.CurrentPrincipal as DXInfo.Principal.MyPrincipal;
            if (mp != null)
            {
                DXInfo.Principal.MyIdentity mi = mp.Identity as DXInfo.Principal.MyIdentity;
                DXInfo.Models.aspnet_CustomProfile oper = DXInfo.Business.Helper.CloneOf(mi.oper);
                mp = null;
                mi = null;
                this.Oper = oper;
            }
        }
        //private void SetMyPricipal()
        //{
        //    DXInfo.Principal.MyPrincipal mp = Thread.CurrentPrincipal as DXInfo.Principal.MyPrincipal;
        //    if (mp != null)
        //    {
        //        this.MyPricipal = Common.CloneOf(mp);
        //        mp = null;
        //    }
        //}
        //private void SetMyIdentity()
        //{
        //    DXInfo.Principal.MyPrincipal mp = Thread.CurrentPrincipal as DXInfo.Principal.MyPrincipal;
        //    if (mp != null)
        //    {
        //        DXInfo.Principal.MyIdentity mi = mp.Identity as DXInfo.Principal.MyIdentity;                
        //        mp = null;
        //        this.MyIdentity = Common.CloneOf(mi);
        //        mi = null;
        //    }
        //}
        //private void SetFunc()
        //{
        //    DXInfo.Principal.MyPrincipal mp = Thread.CurrentPrincipal as DXInfo.Principal.MyPrincipal;
        //    if (mp != null)
        //    {
        //        this.Func = new List<DXInfo.Models.aspnet_Sitemaps>();
        //        foreach (DXInfo.Models.aspnet_Sitemaps sitemap in mp.Func)
        //        {
        //            this.Func.Add(Common.CloneOf(sitemap));
        //        }
        //        mp = null;
        //    }
        //}
        public DXInfo.Models.Depts Dept { get; set; }
        public DXInfo.Models.aspnet_Users User { get; set; }
        public DXInfo.Models.aspnet_CustomProfile Oper { get; set; }
        //public DXInfo.Principal.MyPrincipal MyPricipal { get; set; }
        //public DXInfo.Principal.MyIdentity MyIdentity { get; set; }
        //public List<DXInfo.Models.aspnet_Sitemaps> Func { get; set; }

        #region 用户名
        private string _UserName;
        [Required(ErrorMessage = "请输入用户名。")]
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                //if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("请输入用户名");
                _UserName = value;
                this.RaisePropertyChanged("UserName");
            }
        }
        #endregion

        #region 密码
        private string _Password;
        [Required(ErrorMessage = "请输入密码。")]
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                this.RaisePropertyChanged("Password");
            }
        }
        #endregion
        #endregion

        #region 支付方式
        public void SetlPayType()
        {
            SetlPayTypeOfPutCard();
            SetlPayTypeList();
            SetlPayTypeAll();
            SetlPayTypeCard();
        }
        private void SetlPayTypeOfPutCard()
        {
            lPayTypeOfPutCard = Uow.PayTypes.GetAll().OrderBy(w => w.Code).ToList();
        }
        private List<DXInfo.Models.PayTypes> _lPayTypeOfPutCard;
        public List<DXInfo.Models.PayTypes> lPayTypeOfPutCard
        {
            get
            {
                return _lPayTypeOfPutCard;
            }
            set
            {
                _lPayTypeOfPutCard = value;
                this.RaisePropertyChanged("lPayTypeOfPutCard");
            }
        }

        private void SetlPayTypeList()
        {
            List<DXInfo.Models.PayTypes> pt = _lPayTypeOfPutCard.Where(w => w.Code != "999").OrderBy(w => w.Code).ToList();            
            this.lPayType = pt;
        }
        private void SetlPayTypeAll()
        {
            Guid payType_Card = Guid.Parse(DXInfo.Business.Helper.PayType_Card);
            Guid payType_TakeOut = Guid.Parse(DXInfo.Business.Helper.PayType_TakeOut);

            List<DXInfo.Models.PayTypes> pt = _lPayTypeOfPutCard.Where(w => w.Code != "999").OrderBy(w => w.Code).ToList();
            DXInfo.Models.PayTypes p = new DXInfo.Models.PayTypes();
            p.Id = payType_Card;
            p.Code = "Card";
            p.Name = "会员卡";
            pt.Add(p);

            DXInfo.Models.PayTypes p1 = new DXInfo.Models.PayTypes();
            p1.Id = payType_TakeOut;
            p1.Code = "TakeOut";
            p1.Name = "会员卡外卖";
            pt.Add(p1);

            this.lPayTypeAll = pt;
        }
        private void SetlPayTypeCard()
        {
            Guid payType_Card = Guid.Parse(DXInfo.Business.Helper.PayType_Card);
            Guid payType_TakeOut = Guid.Parse(DXInfo.Business.Helper.PayType_TakeOut);

            List<DXInfo.Models.PayTypes> pt = new List<DXInfo.Models.PayTypes>();
            DXInfo.Models.PayTypes p = new DXInfo.Models.PayTypes();
            p.Id = payType_Card;
            p.Code = "Card";
            p.Name = "会员卡";
            pt.Add(p);

            DXInfo.Models.PayTypes p1 = new DXInfo.Models.PayTypes();
            p1.Id = payType_TakeOut;
            p1.Code = "TakeOut";
            p1.Name = "会员卡外卖";
            pt.Add(p1);

            this.lPayTypeCard = pt;
        }

        private List<DXInfo.Models.PayTypes> _lPayType;
        public List<DXInfo.Models.PayTypes> lPayType
        {
            get
            {
                return _lPayType;
            }
            set
            {
                _lPayType = value;
                this.RaisePropertyChanged("lPayType");
            }
        }

        private List<DXInfo.Models.PayTypes> _lPayTypeAll;
        public List<DXInfo.Models.PayTypes> lPayTypeAll
        {
            get
            {
                return _lPayTypeAll;
            }
            set
            {
                _lPayTypeAll = value;
                this.RaisePropertyChanged("lPayTypell");
            }
        }
        
        private List<DXInfo.Models.PayTypes> _lPayTypeCard;
        public List<DXInfo.Models.PayTypes> lPayTypeCard
        {
            get
            {
                return _lPayTypeCard;
            }
            set
            {
                _lPayTypeCard = value;
                this.RaisePropertyChanged("lPayTypeCard");
            }
        }

        private DXInfo.Models.PayTypes _SelectedPayType;
        [Required(ErrorMessage = "请选择支付方式")]
        public DXInfo.Models.PayTypes SelectedPayType
        {
            get
            {
                return _SelectedPayType;
            }
            set
            {
                _SelectedPayType = value;
                this.RaisePropertyChanged("SelectedPayType");
            }
        }
        #endregion

        #region 卡类型
        public void SetlCardType()
        {
            this.lCardType = Uow.CardTypes.GetAll().ToList(); 
        }
        private List<DXInfo.Models.CardTypes> _lCardType;
        public List<DXInfo.Models.CardTypes> lCardType
        {
            get
            {
                return _lCardType;
            }
            set
            {
                _lCardType = value;
                this.RaisePropertyChanged("lCardType");
            }
        }

        private DXInfo.Models.CardTypes _SelectedCardType;
        [Required(ErrorMessage = "请选择卡类型")]
        public DXInfo.Models.CardTypes SelectedCardType
        {
            get
            {
                return _SelectedCardType;
            }
            set
            {
                _SelectedCardType = value;
                this.RaisePropertyChanged("SelectedCardType");
            }
        }
        #endregion

        #region 卡级别
        public void SetlAllCardLevel()
        {
            this.lCardLevel = (from d in Uow.CardLevels.GetAll()
                               select d).ToList();
        }
        public void SetlCardLevel()
        {
            this.lCardLevel = (from d in Uow.CardLevels.GetAll()
                               where d.DeptId == this.Dept.DeptId || d.DeptId == Guid.Empty
                               select d).ToList();
        }
        private List<DXInfo.Models.CardLevels> _lCardLevel;
        public List<DXInfo.Models.CardLevels> lCardLevel
        {
            get
            {
                return _lCardLevel;
            }
            set
            {
                _lCardLevel = value;
                this.RaisePropertyChanged("lCardLevel");
            }
        }
        private DXInfo.Models.CardLevels _SelectedCardLevel;
        [Required(ErrorMessage = "请选择卡级别")]
        public DXInfo.Models.CardLevels SelectedCardLevel
        {
            get
            {
                return _SelectedCardLevel;
            }
            set
            {
                _SelectedCardLevel = value;
                this.RaisePropertyChanged("SelectedCardLevel");
                AfterSelectCardLevel();
            }
        }
        protected virtual void AfterSelectCardLevel() { }
        #endregion

        #region 是否打开虚拟键盘
        public bool IsOpen 
        {
            get
            {
                return App.IsOpen;
            }
            set
            {;
                App.IsOpen = value;
                base.RaisePropertyChanged("IsOpen");
                SaveConf();
                this.AfterIsOpen();
            }
        }
        protected virtual void AfterIsOpen() { }
        #endregion

        #region 是否打印不干胶
        public bool IsStickerPrint
        {
            get
            {
                return App.IsStickerPrint;
            }
            set
            {
                App.IsStickerPrint = value;
                base.RaisePropertyChanged("IsStickerPrint");
                SaveConf();
                this.AfterIsStickerPrint();
            }
        }
        protected virtual void AfterIsStickerPrint() { }
        #endregion

        #region 是否打印小票
        public bool IsTicket1
        {
            get
            {
                return App.IsTicket1;
            }
            set
            {
                App.IsTicket1 = value;
                base.RaisePropertyChanged("IsTicket1");
                SaveConf();
            }
        }
        public bool IsTicket2
        {
            get
            {
                return App.IsTicket2;
            }
            set
            {
                App.IsTicket2 = value;
                base.RaisePropertyChanged("IsTicket2");
                SaveConf();
            }
        }
        public bool IsTicket3
        {
            get
            {
                return App.IsTicket3;
            }
            set
            {
                App.IsTicket3 = value;
                base.RaisePropertyChanged("IsTicket3");
                SaveConf();
            }
        }
        #endregion

        #region 打印三联单
        public bool IsThree
        {
            get
            {
                return App.IsThree;
            }
            set
            {
                App.IsThree = value;
                base.RaisePropertyChanged("IsThree");
                SaveConf();
            }
        }
        #endregion

        #region 是否打印订单
        public bool IsPrintOrder
        {
            get
            {
                return App.IsPrintOrder;
            }
            set
            {
                App.IsPrintOrder = value;
                base.RaisePropertyChanged("IsPrintOrder");
                SaveConf();
            }
        }
        #endregion

        #region IDataErrorInfo, IValidationExceptionHandler
        protected override void RaisePropertyChanged(string propertyName)
        {
            base.RaisePropertyChanged(propertyName);
            this.PropertyChangedCompleted(propertyName);
        }
        private void PropertyChangedCompleted(string propertyName)
        {
            //if (propertyName != "IsValid")
            //{
            if (string.IsNullOrEmpty(this.Error) && this.ValidPropertiesCount == this.TotalPropertiesWithValidationCount)
            {
                this.IsValid = true;
            }
            else
            {
                this.IsValid = false;
            }
            //}
        }
        private bool _IsValid;
        public bool IsValid
        {
            get
            {
                return this._IsValid;
            }
            protected set
            {
                this._IsValid = value;
                base.RaisePropertyChanged("IsValid");
            }
        }
        private Dictionary<string, Func<MyViewModelBase, object>> propertyGetters;
        private Dictionary<string, ValidationAttribute[]> validators;
        public string this[string propertyName]
        {
            get
            {
                if (this.propertyGetters.ContainsKey(propertyName))
                {
                    var propertyValue = this.propertyGetters[propertyName](this);
                    var errorMessages = this.validators[propertyName]
                        .Where(v => !v.IsValid(propertyValue))
                        .Select(v => v.ErrorMessage).ToArray();

                    return string.Join(Environment.NewLine, errorMessages);
                }

                return string.Empty;
            }
        }
        public string Error
        {
            get
            {
                var errors = from validator in this.validators
                             from attribute in validator.Value
                             where !attribute.IsValid(this.propertyGetters[validator.Key](this))
                             select attribute.ErrorMessage;

                return string.Join(Environment.NewLine, errors.ToArray());
            }
        }
        public int ValidPropertiesCount
        {
            get
            {
                var query = from validator in this.validators
                            where validator.Value.All(attribute => attribute.IsValid(this.propertyGetters[validator.Key](this)))
                            select validator;

                var count = query.Count() - this.validationExceptionCount;
                return count;
            }
        }
        public int TotalPropertiesWithValidationCount
        {
            get
            {
                return this.validators.Count();
            }
        }
        private ValidationAttribute[] GetValidations(PropertyInfo property)
        {
            return (ValidationAttribute[])property.GetCustomAttributes(typeof(ValidationAttribute), true);
        }
        private Func<MyViewModelBase, object> GetValueGetter(PropertyInfo property)
        {
            return new Func<MyViewModelBase, object>(viewmodel => property.GetValue(viewmodel, null));
        }
        private int validationExceptionCount;
        public void ValidationExceptionsChanged(int count)
        {
            this.validationExceptionCount = count;
            base.RaisePropertyChanged("ValidPropertiesCount");
        }
        #endregion

        #region 刷卡对象
        public void ResetSwipingCard()
        {
            this.Card = null;
            this.Member = null;
            this.CardType = null;
            this.CardLevel = null;
            this.CardDept = null;
            this.CardBalance = null;
            this.Points = null;
        }
        private DXInfo.Models.Cards _Card;
        public DXInfo.Models.Cards Card
        {
            get
            {
                return _Card;
            }
            set
            {
                _Card = value;
                this.RaisePropertyChanged("Card");
            }
        }
        private DXInfo.Models.Members _Member;
        public DXInfo.Models.Members Member
        {
            get
            {
                return _Member;
            }
            set
            {
                _Member = value;
                this.RaisePropertyChanged("Member");
            }
        }
        private DXInfo.Models.CardTypes _CardType;
        public DXInfo.Models.CardTypes CardType
        {
            get
            {
                return _CardType;
            }
            set
            {
                _CardType = value;
                this.RaisePropertyChanged("CardType");
            }
        }
        private DXInfo.Models.CardLevels _CardLevel;
        public DXInfo.Models.CardLevels CardLevel
        {
            get
            {
                return _CardLevel;
            }
            set
            {
                _CardLevel = value;
                this.RaisePropertyChanged("CardLevel");
            }
        }
        private DXInfo.Models.Depts _CardDept;
        public DXInfo.Models.Depts CardDept
        {
            get
            {
                return _CardDept;
            }
            set
            {
                _CardDept = value;
                this.RaisePropertyChanged("CardDept");
            }
        }
        private decimal? _CardBalance;
        public decimal? CardBalance
        {
            get
            {
                return _CardBalance;
            }
            set
            {
                _CardBalance = value;
                this.RaisePropertyChanged("CardBalance");
            }
        }
        private decimal? _Points;
        public decimal? Points
        {
            get
            {
                return _Points;
            }
            set
            {
                _Points = value;
                this.RaisePropertyChanged("Points");
            }
        }
        #endregion

        #region 刷卡
        protected virtual void AfterSwipingCard()
        {
        }
        protected void swipingCard()
        {
            //刷卡
            StringBuilder sb = new StringBuilder(33);
            int value = 0;
//#if !DEBUG
            int st = CardRef.CoolerReadCard(sb, ref value);
//#else
//            int st = 0;
//            value = 1158965;
//            Random r = new Random();
//            double rd = r.NextDouble();
//            int i = Convert.ToInt32(Math.Round(rd));
//            string[] strcardnos = { "12345678901", "12345678902" };//{ "A99993", "A99994" };
//            sb.Append(strcardnos[i]);////"12347";"C00623";// 
//#endif
            if (st != 0)
            {
                MessageBox.Show(CardRef.GetStr(st));
                return;
            }

            string strCardNo = sb.ToString();
            var cs = Uow.Cards.GetAll().Where(w => w.CardNo == strCardNo).FirstOrDefault();
            getCard(cs, strCardNo, value);
        }
        private void getCard(DXInfo.Models.Cards cs,string strCardNo,int value)
        {
            if (cs == null)
            {
                Helper.ShowErrorMsg("未找到此卡信息，卡号：" + strCardNo);
                this.ResetSwipingCard();
                return;
            }

            if (cs.Status != 0)
            {
                Helper.ShowErrorMsg("此卡已挂失，或已补卡，卡号：" + strCardNo);
                this.ResetSwipingCard();
                return;
            }
            if (value == 0)
            {
                this.CardBalance = cs.Balance;
            }
            else
            {
                this.CardBalance = Convert.ToDecimal(value) / 100;
            }
            if (cs.Member == Guid.Empty)
            {
                Helper.ShowErrorMsg("此卡无对应会员信息，卡号：" + strCardNo);
                this.ResetSwipingCard();
                return;
            }
            this.Card = cs;

            var ms = Uow.Members.GetById(g => g.Id == cs.Member);
            if (ms == null)
            {
                Helper.ShowErrorMsg("未找到此卡对应的会员信息，卡号：" + strCardNo);
                this.ResetSwipingCard();
                return;
            }
            this.Member = ms;
            if (cs.DeptId == Guid.Empty)
            {
                Helper.ShowErrorMsg("此卡无对应门店信息，卡号：" + strCardNo);
                this.ResetSwipingCard();
                return;
            }
            var ds = Uow.Depts.GetById(g => g.DeptId == cs.DeptId);
            if (ds == null)
            {
                Helper.ShowErrorMsg("未找到此卡对应的门店信息，卡号：" + strCardNo);
                this.ResetSwipingCard();
                return;
            }
            this.CardDept = ds;

            var pointList = (from d in Uow.CardPoints.GetAll()
                             where d.Card == cs.Id
                             select d.Point).ToList();
            if (pointList.Count > 0)
            {
                this.Points = pointList.Sum();
            }

            var cls = this.lCardLevel.Find(f => f.Id == cs.CardLevel);//Uow.CardLevels.GetById(cs.CardLevel);
            if (cls == null)
            {
                Helper.ShowErrorMsg("此卡的折扣不能在本门店使用，或者无对应的卡级别信息，卡号：" + strCardNo);
                this.ResetSwipingCard();
                return;
            }
            this.CardLevel = cls;

            var ct = this.lCardType.Find(f => f.Id == cs.CardType);//Uow.CardTypes.GetById(cs.CardType);//.Where(w => w.Id == c.CardType).FirstOrDefault();
            if (ct == null)
            {
                Helper.ShowErrorMsg("此卡无对应的卡类型信息，卡号：" + strCardNo);
                this.ResetSwipingCard();
                return;
            }
            this.CardType = ct;
            this.IsCard = true;
            this.IsMoney = this.CardType.IsMoney;
            if (this.lPayTypeCard != null && this.IsCard && this.IsMoney)
            {
                this.SelectedPayType = this.lPayTypeCard.Find(f => f.Name == "会员卡");
            }

            if (this.lPayTypeAll != null && this.IsCard && !this.IsMoney)
            {
                this.SelectedPayType = this.lPayTypeAll.Find(f => f.Name == "现金");
            }

            if (this.lPayType != null && !this.IsCard && !this.IsMoney)
            {
                this.SelectedPayType = this.lPayType.Find(f => f.Name == "现金");
            }

            this.AfterSwipingCard();
        }
        public void SearchCardById(Guid cardId)
        {
            DXInfo.Models.Cards cs = Uow.Cards.GetById(c => c.Id == cardId);
            var ct = this.lCardType.Find(f => f.Id == cs.CardType);
            if (!ct.IsMoney)
            {
                getCard(cs, cs.CardNo, 0);
            }
            else
            {
                Helper.ShowErrorMsg("非折扣卡，必须刷卡！");
            }
        }
        private void SearchCardExecute()
        {
            MemberQueryWindow mqw = new MemberQueryWindow();
            if (mqw.ShowDialog().GetValueOrDefault())
            {
                MemberQueryViewModel mqvm = mqw.DataContext as MemberQueryViewModel;
                dynamic dresult = mqvm.SelectedResult;
                if (dresult != null)
                {
                    Guid cardId = dresult.CardId;
                    string strCardNo = dresult.CardNo;
                    //MessageBox.Show(cardId.ToString());
                    DXInfo.Models.Cards cs = Uow.Cards.GetById(c => c.Id == cardId);
                    var ct = this.lCardType.Find(f => f.Id == cs.CardType);
                    if (!ct.IsMoney)
                    {
                        getCard(cs, strCardNo, 0);
                    }
                    else
                    {
                        Helper.ShowErrorMsg("非折扣卡，必须刷卡！");
                    }
                }
            }
        }
        public ICommand SwipingCard
        {
            get
            {
                return new RelayCommand(swipingCard);
            }
        }
        public ICommand SearchCard
        {
            get
            {
                return new RelayCommand(SearchCardExecute);
            }
        }
        private void CancelSwipingCardExecute()
        {
            this.ResetSwipingCard();
            this.IsCard = false;
            this.IsMoney = false;
        }
        private bool CancelSwipingCardCanExecute()
        {
            if (IsCard)
            {
                return true;
            }
            return false;
        }
        public ICommand CancelSwipingCard
        {
            get
            {
                return new RelayCommand(CancelSwipingCardExecute, CancelSwipingCardCanExecute);
            }
        }
        #endregion
        
        #region 杯型

        //private bool CupTypeVisible(IFairiesMemberManageUow uow, string type)
        //{
        //    bool visible = false;
        //    var nameCode = uow.NameCode.GetAll().Where(w => w.Type == type).FirstOrDefault();
        //    if (nameCode != null)
        //    {
        //        visible = nameCode.Value.ToLower() == "true";
        //    }
        //    return visible;
        //}
        private ObservableCollection<DXInfo.Models.MyEnum> _OCCupType;
        public ObservableCollection<DXInfo.Models.MyEnum> OCCupType
        {
            get
            {
                return _OCCupType;
            }
            set
            {
                _OCCupType = value;
                this.RaisePropertyChanged("OCCupType");
            }
        }

        public void SetlCupType()
        {
            List<DXInfo.Models.MyEnum> lMyEnum = DXInfo.Business.Helper.GetlMyEnum(typeof(DXInfo.Models.CupType));
            List<DXInfo.Models.NameCode> lNameCode = Uow.NameCode.GetAll().Where(w => w.Type.Contains("CupType")).ToList();
            if (lNameCode.Count > 0)
            {
                for (int i = -1; i < 4; i++)
                {
                    DXInfo.Models.MyEnum myEnum = lMyEnum.Find(f => f.Id == i);
                    if (myEnum != null)
                    {
                        string visibleType = "CupType" + myEnum.Code + "Visible";
                        DXInfo.Models.NameCode nc = lNameCode.Find(f => f.Code == visibleType);
                        if (nc != null)
                        {
                            if (nc.Value == "false")
                            {
                                lMyEnum.Remove(myEnum);
                            }
                            else
                            {
                                string titleType = "CupType" + myEnum.Code + "Title";
                                DXInfo.Models.NameCode nc1 = lNameCode.Find(f => f.Code == titleType);
                                if (nc1 != null && !string.IsNullOrEmpty(nc1.Value))
                                {
                                    myEnum.Name = nc1.Value;
                                }
                            }
                        }
                        else
                        {
                            if (i > 0)
                            {
                                lMyEnum.Remove(myEnum);
                            }
                        }
                    }
                }
            }
            this.lCupType = lMyEnum;//new DXInfo.Models.MyEnumList(lMyEnum);
        }
        private List<DXInfo.Models.MyEnum> _lCupType;
        public List<DXInfo.Models.MyEnum> lCupType
        {
            get
            {
                return _lCupType;
            }
            set
            {
                _lCupType = value;
                this.RaisePropertyChanged("lCupType");
            }
        }

        private DXInfo.Models.MyEnum _SelectedCupType;
        public DXInfo.Models.MyEnum SelectedCupType
        {
            get
            {
                return _SelectedCupType;
            }
            set
            {
                _SelectedCupType = value;
                this.RaisePropertyChanged("SelectedCupType");
                this.AfterSelectCupType();
            }
        }
        protected virtual void AfterSelectCupType() { }

        public Dictionary<int, decimal> GetdSalePrice(DXInfo.Models.Inventory inventory)
        {
            Dictionary<int, decimal> dSalePrice = new Dictionary<int, decimal>();
            dSalePrice.Add(-1, inventory.SalePrice);
            dSalePrice.Add(0, inventory.SalePrice0);
            dSalePrice.Add(1, inventory.SalePrice1);
            dSalePrice.Add(2, inventory.SalePrice2);
            return dSalePrice;
        }
        public Dictionary<int, decimal> GetdSalePoint(DXInfo.Models.Inventory inventory)
        {
            Dictionary<int, decimal> dSalePoint = new Dictionary<int, decimal>();
            dSalePoint.Add(-1, inventory.SalePoint);
            dSalePoint.Add(0, inventory.SalePoint0);
            dSalePoint.Add(1, inventory.SalePoint1);
            dSalePoint.Add(2, inventory.SalePoint2);
            return dSalePoint;
        }

        #endregion

        #region 分类
        public List<DXInfo.Models.InventoryCategory> lInventoryCategory
        {
            get
            {
                var q = from d in Uow.InventoryCategory.GetAll()
                        join d1 in Uow.CategoryDepts.GetAll() on d.Id equals d1.Category into dd1
                        from dd1s in dd1.DefaultIfEmpty()
                        where dd1s.Dept == this.Dept.DeptId
                        select d;
                return q.ToList();
            }
        }
        private ObservableCollection<DXInfo.Models.InventoryCategory> _OCInventoryCategory;
        public ObservableCollection<DXInfo.Models.InventoryCategory> OCInventoryCategory
        {
            get
            {
                return _OCInventoryCategory;
            }
            set
            {
                _OCInventoryCategory = value;
                this.RaisePropertyChanged("OCInventoryCategory");
            }
        }
        private DXInfo.Models.InventoryCategory _SelectedInventoryCategory;
        public DXInfo.Models.InventoryCategory SelectedInventoryCategory
        {
            get
            {
                return _SelectedInventoryCategory;
            }
            set
            {
                _SelectedInventoryCategory = value;
                this.RaisePropertyChanged("SelectedInventoryCategory");
                this.AfterSelectInventoryCategory();
            }
        }
        protected virtual void AfterSelectInventoryCategory() { }
        #endregion

        #region 存货
        private ObservableCollection<DXInfo.Models.Inventory> _OCInventory;
        public ObservableCollection<DXInfo.Models.Inventory> OCInventory
        {
            get
            {
                return _OCInventory;
            }
            set
            {
                _OCInventory = value;
                this.RaisePropertyChanged("OCInventory");
            }
        }
        private DXInfo.Models.Inventory _SelectedInventory;
        public DXInfo.Models.Inventory SelectedInventory
        {
            get
            {
                return _SelectedInventory;
            }
            set
            {
                _SelectedInventory = value;
                this.RaisePropertyChanged("SelectedInventory");
                this.AfterSelectInventory();
            }
        }
        protected virtual void AfterSelectInventory() { }
        private ObservableCollection<DXInfo.Models.InventoryEx> _OCInventoryEx;
        public ObservableCollection<DXInfo.Models.InventoryEx> OCInventoryEx
        {
            get
            {
                return _OCInventoryEx;
            }
            set
            {
                _OCInventoryEx = value;
                this.RaisePropertyChanged("OCInventoryEx");
            }
        }
        private DXInfo.Models.InventoryEx _SelectedInventoryEx;
        public DXInfo.Models.InventoryEx SelectedInventoryEx
        {
            get
            {
                return _SelectedInventoryEx;
            }
            set
            {
                _SelectedInventoryEx = value;
                this.RaisePropertyChanged("SelectedInventoryEx");
                AfterSelectInventoryEx();
            }
        }
        protected virtual void AfterSelectInventoryEx() { }
        #endregion        

        #region 预定
        private ObservableCollection<DXInfo.Models.OrderBookEx> _OCOrderBookEx;
        public ObservableCollection<DXInfo.Models.OrderBookEx> OCOrderBookEx
        {
            get
            {
                return _OCOrderBookEx;
            }
            set
            {
                _OCOrderBookEx = value;
                this.RaisePropertyChanged("OCOrderBookEx");
            }
        }
        private DXInfo.Models.OrderBookEx _SelectedOrderBookEx;
        public DXInfo.Models.OrderBookEx SelectedOrderBookEx
        {
            get
            {
                return _SelectedOrderBookEx;
            }
            set
            {
                _SelectedOrderBookEx = value;
                this.RaisePropertyChanged("SelectedOrderBookEx");
                AfterSelectOrderBookEx();
            }
        }
        protected virtual void AfterSelectOrderBookEx() { }
        #endregion

        #region 会员名
        private string _MemberName;
        [Required(ErrorMessage = "请输入会员名")]
        public string MemberName
        {
            get
            {
                return _MemberName;
            }
            set
            {
                _MemberName = value;
                this.RaisePropertyChanged("MemberName");
            }
        }
        #endregion

        #region 卡号
        private string _CardNo;
        [Required(ErrorMessage = "请输入卡号")]
        public string CardNo
        {
            get
            {
                return _CardNo;
            }
            set
            {
                _CardNo = value;
                this.RaisePropertyChanged("CardNo");
            }
        }
        #endregion

        #region 卡密码
        private string _CardPwd;
        public string CardPwd
        {
            get
            {
                return _CardPwd;
            }
            set
            {
                _CardPwd = value;
                this.RaisePropertyChanged("CardPwd");
            }
        }
        #endregion

        #region 卡状态
        public void SetlCardStatus()
        {
            lCardStatus = DXInfo.Business.Helper.GetlMyEnum(typeof(DXInfo.Models.CardStatus));
        }
        private List<DXInfo.Models.MyEnum> _lCardStatus;
        public List<DXInfo.Models.MyEnum> lCardStatus
        {
            get
            {                
                return _lCardStatus;
            }
            set
            {
                _lCardStatus = value;
                this.RaisePropertyChanged("lCardStatus");
            }
        }
        public DXInfo.Models.MyEnum _SelectedCardStatus;
        public virtual DXInfo.Models.MyEnum SelectedCardStatus
        {
            get
            {
                return _SelectedCardStatus;
            }
            set
            {
                _SelectedCardStatus = value;
                this.RaisePropertyChanged("SelectedCardStatus");
            }
        }
        #endregion

        #region 小票类型
        public void SetlBillType()
        {
            lBillType = DXInfo.Business.Helper.GetlMyEnum(typeof(DXInfo.Models.BillType));
        }
        private List<DXInfo.Models.MyEnum> _lBillType;
        public List<DXInfo.Models.MyEnum> lBillType
        {
            get
            {
                return _lBillType;
            }
            set
            {
                _lBillType = value;
                this.RaisePropertyChanged("lBillType");
            }
        }
        public DXInfo.Models.MyEnum _SelectedBillType;
        public virtual DXInfo.Models.MyEnum SelectedBillType
        {
            get
            {
                return _SelectedBillType;
            }
            set
            {
                _SelectedBillType = value;
                this.RaisePropertyChanged("SelectedBillType");
            }
        }
        #endregion

        #region 操作员
        public void SetlOper()
        {
            lOper = Uow.aspnet_CustomProfile.GetAll().Where(w => w.DeptId == this.Dept.DeptId).ToList();
        }
        private List<DXInfo.Models.aspnet_CustomProfile> _lOper;
        public List<DXInfo.Models.aspnet_CustomProfile> lOper
        {
            get
            {
                return _lOper;
            }
            set
            {
                _lOper = value;
                this.RaisePropertyChanged("lOper");
            }
        }
        public DXInfo.Models.aspnet_CustomProfile _SelectedOper;
        public DXInfo.Models.aspnet_CustomProfile SelectedOper
        {
            get
            {
                return _SelectedOper;
            }
            set
            {
                _SelectedOper = value;
                this.RaisePropertyChanged("SelectedOper");
            }
        }
        #endregion

        #region 关闭
        private void closeUserControl()
        {
            Messenger.Default.Send(new CloseUserControlMessageToken());
        }
        public ICommand CloseUserControl
        {
            get
            {
                return new RelayCommand(closeUserControl);
            }
        }
        #endregion

        #region 部门类型
        public int SectionType { get; set; }
        #endregion

        #region 导航
        public void NavigationUserControl(UserControl uc)
        {
            Messenger.Default.Send(new ChangeUserControlMessageToken() { MyContent = uc });
        }
        #endregion

        #region 口味
        private List<DXInfo.Models.Tastes> _lTaste;
        public List<DXInfo.Models.Tastes> lTaste
        {
            get
            {
                return _lTaste;
            }
            set
            {
                _lTaste = value;
                this.RaisePropertyChanged("lTaste");
            }
        }
        public void SetlTaste()
        {
            this.lTaste = (from d in Uow.Tastes.GetAll()
                           where d.DeptId == null || d.DeptId == this.Dept.DeptId
                           select d).ToList();
        }

        private DXInfo.Models.Tastes _SelectedTaste;
        public DXInfo.Models.Tastes SelectedTaste
        {
            get
            {
                return _SelectedTaste;
            }
            set
            {
                _SelectedTaste = value;
                this.RaisePropertyChanged("SelectedTaste");
                AfterSelectTaste();
            }
        }
        protected virtual void AfterSelectTaste() { }

        private DXInfo.Models.TasteExList _lTasteEx;
        public DXInfo.Models.TasteExList lTasteEx
        {
            get
            {
                return _lTasteEx;
                
            }
            set
            {
                _lTasteEx = value;
                this.RaisePropertyChanged("lTasteEx");
            }
        }
        public void SetlTasteEx()
        {
            List<DXInfo.Models.TasteEx> lte = (from d in Uow.Tastes.GetAll()
                             where d.DeptId == null || d.DeptId == this.Dept.DeptId
                             select new DXInfo.Models.TasteEx()
                             {
                                 Id = d.Id,
                                 Code = d.Code,
                                 Name = d.Name,
                                 Comment = d.Comment,
                                 DeptId = d.DeptId,
                                 IsSelected = false,
                             }).ToList();
            this.lTasteEx = new DXInfo.Models.TasteExList(lte);
            //lte.ForEach(delegate(DXInfo.Models.TasteEx te) { this.lTasteEx.Add(te); });
            
        }

        private DXInfo.Models.TasteEx _SelectedTasteEx;
        public DXInfo.Models.TasteEx SelectedTasteEx
        {
            get
            {
                return _SelectedTasteEx;
            }
            set
            {
                _SelectedTasteEx = value;
                this.RaisePropertyChanged("SelectedTasteEx");
                AfterSelectTasteEx();
            }
        }
        protected virtual void AfterSelectTasteEx() { }
        #endregion

        #region 菜品
        private ObservableCollection<DXInfo.Models.OrderMenuEx> _OCOrderMenuEx;// = new ObservableCollection<SelectedOrderMenu>();
        public ObservableCollection<DXInfo.Models.OrderMenuEx> OCOrderMenuEx
        {
            get
            {
                return _OCOrderMenuEx;
            }
            set
            {
                _OCOrderMenuEx = value;
                this.RaisePropertyChanged("OCOrderMenuEx");
            }
        }

        private DXInfo.Models.OrderMenuEx _SelectedOrderMenuEx;
        public DXInfo.Models.OrderMenuEx SelectedOrderMenuEx
        {
            get
            {
                return _SelectedOrderMenuEx;
            }
            set
            {
                _SelectedOrderMenuEx = value;
                this.RaisePropertyChanged("SelectedOrderMenuEx");
            }
        }
        #endregion

        #region 证件号码
        private string _IdCard;
        public string IdCard
        {
            get
            {
                return _IdCard;
            }
            set
            {
                _IdCard = value;
                this.RaisePropertyChanged("IdCard");
            }
        }
        #endregion

        #region 客户
        private string _Customer;
        public string Customer
        {
            get
            {
                return _Customer;
            }
            set
            {
                _Customer = value;
                this.RaisePropertyChanged("Customer");
            }
        }
        #endregion

        #region 桌号
        private string _DeskNo;
        [Required(ErrorMessage = "请输入号牌")]
        public string DeskNo
        {
            get
            {
                return _DeskNo;
            }
            set
            {
                _DeskNo = value;
                this.RaisePropertyChanged("DeskNo");
            }
        }
        #endregion

        #region 联系电话
        private string _LinkPhone;
        [Required(ErrorMessage = "请输入联系电话")]
        [RegularExpression(DXInfo.Models.MyReg.Phone, ErrorMessage = "匹配格式：11位手机号码3-4位区号，7-8位直播号码，1－4位分机号 如：12345678901、1234-12345678-1234")]
        public string LinkPhone
        {
            get
            {
                return _LinkPhone;
            }
            set
            {
                _LinkPhone = value;
                this.RaisePropertyChanged("LinkPhone");
            }
        }
        #endregion

        #region 联系地址
        private string _LinkAddress;
        public string LinkAddress
        {
            get
            {
                return _LinkAddress;
            }
            set
            {
                _LinkAddress = value;
                this.RaisePropertyChanged("LinkAddress");
            }
        }
        #endregion

        #region EMAIL
        private string _Email;
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
                this.RaisePropertyChanged("Email");
            }
        }
        #endregion

        #region 描述
        private string _Comments;
        public string Comments
        {
            get
            {
                return _Comments;
            }
            set
            {
                _Comments = value;
                this.RaisePropertyChanged("Comments");
            }
        }
        #endregion

        #region 余额合计
        private decimal? _BalanceSum;
        public decimal? BalanceSum
        {
            get
            {
                return _BalanceSum;
            }
            set
            {
                _BalanceSum = value;
                this.RaisePropertyChanged("BalanceSum");
            }
        }
        #endregion

        #region 开始日期
        private DateTime _BeginDate = DateTime.Now.Date;
        public DateTime BeginDate
        {
            get
            {
                return _BeginDate;
            }
            set
            {
                _BeginDate = value;
                this.RaisePropertyChanged("BeginDate");
            }
        }
        #endregion

        #region 结束日期
        private DateTime _EndDate = DateTime.Now.Date.AddDays(1);
        public DateTime EndDate
        {
            get
            {
                return _EndDate;
            }
            set
            {
                _EndDate = value;
                this.RaisePropertyChanged("EndDate");
            }
        }
        #endregion

        #region 开始时间
        private DateTime _BeginTime = DateTime.Now.Date;
        public DateTime BeginTime
        {
            get
            {
                return _BeginTime;
            }
            set
            {
                _BeginTime = value;
                this.RaisePropertyChanged("BeginTime");
            }
        }
        #endregion

        #region 结束时间
        private DateTime _EndTime = DateTime.Now.Date;
        public DateTime EndTime
        {
            get
            {
                return _EndTime;
            }
            set
            {
                _EndTime = value;
                this.RaisePropertyChanged("EndTime");
            }
        }
        #endregion

        #region 预定状态
        public List<DXInfo.Models.MyEnum> lOrderBookStatus
        {
            get
            {
                return DXInfo.Business.Helper.GetlMyEnum(typeof(DXInfo.Models.OrderBookStatus));
            }
        }
        private DXInfo.Models.MyEnum _SelectedOrderBookStatus;
        public DXInfo.Models.MyEnum SelectedOrderBookStatus
        {
            get
            {
                return _SelectedOrderBookStatus;
            }
            set
            {
                _SelectedOrderBookStatus = value;
                this.RaisePropertyChanged("SelectedOrderBookStatus");
            }
        }
        #endregion

        #region 订单状态
        public List<DXInfo.Models.MyEnum> lOrderDishStatus
        {
            get
            {
                return DXInfo.Business.Helper.GetlMyEnum(typeof(DXInfo.Models.OrderDishStatus));
            }
        }
        private DXInfo.Models.MyEnum _SelectedOrderDishStatus;
        public DXInfo.Models.MyEnum SelectedOrderDishStatus
        {
            get
            {
                return _SelectedOrderDishStatus;
            }
            set
            {
                _SelectedOrderDishStatus = value;
                this.RaisePropertyChanged("SelectedOrderDishStatus");
            }
        }
        #endregion

        #region 订单菜品状态
        public List<DXInfo.Models.MyEnum> lOrderMenuStatus
        {
            get
            {
                return DXInfo.Business.Helper.GetlMyEnum(typeof(DXInfo.Models.OrderMenuStatus));
            }
        }
        //public ObservableCollection<DXInfo.Models.MyEnum> OCOrderMenuStatus
        //{
        //    get
        //    {
        //        return new ObservableCollection<DXInfo.Models.MyEnum>(DXInfo.Business.Common.GetlMyEnum(typeof(DXInfo.Models.OrderMenuStatus)));
        //    }
        //}
        private DXInfo.Models.MyEnum _SelectedOrderMenuStatus;
        public DXInfo.Models.MyEnum SelectedOrderMenuStatus
        {
            get
            {
                return _SelectedOrderMenuStatus;
            }
            set
            {
                _SelectedOrderMenuStatus = value;
                this.RaisePropertyChanged("SelectedOrderMenuStatus");
            }
        }
        #endregion

        public DataGrid MyDataGrid { get; set; }

        #region 查询
        public ICommand Query
        {
            get
            {
                return new RelayCommand(query);
            }
        }
        protected virtual void query()
        {

        }
        private IQueryable _MyQuery;
        public IQueryable MyQuery
        {
            get { return _MyQuery; }
            set
            {
                _MyQuery = value;
                this.RaisePropertyChanged("MyQuery");
            }
        }
        #endregion

        #region 选择的结果
        private object _SelectedResult;
        public object SelectedResult
        {
            get
            {
                return _SelectedResult;
            }
            set
            {
                _SelectedResult = value;
                this.RaisePropertyChanged("SelectedResult");
                this.AfterSelectResult();
            }
        }
        protected virtual void AfterSelectResult() { }
        #endregion

        #region 结账对象
        public bool IsOut
        {
            get
            {
                Guid payType_TakeOut = Guid.Parse(DXInfo.Business.Helper.PayType_TakeOut);
                return this.SelectedPayType.Id == payType_TakeOut;
            }
        }
        #endregion

        #region 数量合计
        private decimal? _SumQuantity;
        public decimal? SumQuantity
        {
            get
            {
                return _SumQuantity;
            }
            set
            {
                _SumQuantity = value;
                this.RaisePropertyChanged("SumQuantity");
            }
        }
        #endregion

        #region 金额合计
        private decimal? _SumAmount;
        public decimal? SumAmount
        {
            get
            {
                return _SumAmount;
            }
            set
            {
                _SumAmount = value;
                this.RaisePropertyChanged("SumAmount");
            }
        }
        #endregion

        #region 应付合计
        private decimal? _SumPayable;
        public decimal? SumPayable
        {
            get
            {
                return _SumPayable;
            }
            set
            {
                _SumPayable = value;
                this.RaisePropertyChanged("SumPayable");
            }
        }
        #endregion

        #region 代金券
        private decimal? _Voucher;
        [RegularExpression(@"(^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$)|(^$)", ErrorMessage = "请输入正浮点数。")]
        public decimal? Voucher
        {
            get
            {
                return _Voucher;
            }
            set
            {
                _Voucher = value;
                this.RaisePropertyChanged("Voucher");
            }
        }
        #endregion

        #region 金额
        private decimal? _Amount;
        [Required(ErrorMessage = "请输入金额")]
        public decimal? Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                _Amount = value;
                this.RaisePropertyChanged("Amount");
            }
        }
        #endregion

        #region 赠送金额
        private decimal? _Donate;
        public decimal? Donate
        {
            get
            {
                return _Donate;
            }
            set
            {
                _Donate = value;
                this.RaisePropertyChanged("Donate");
            }
        }
        #endregion

        #region 赠送合计
        private decimal? _SumDonate;
        public decimal? SumDonate
        {
            get
            {
                return _SumDonate;
            }
            set
            {
                _SumDonate = value;
                this.RaisePropertyChanged("SumDonate");
            }
        }
        #endregion

        #region 合计金额
        private decimal? _Sum;

        public decimal? Sum
        {
            get
            {
                return _Sum;
            }
            set
            {
                _Sum = value;
                this.RaisePropertyChanged("Sum");
            }
        }
        #endregion

        #region 结账对象

        private DXInfo.Models.OrderDishes _SelectedOrderDish;
        public DXInfo.Models.OrderDishes SelectedOrderDish
        {
            get
            {
                return _SelectedOrderDish;
            }
            set
            {
                _SelectedOrderDish = value;
                this.RaisePropertyChanged("SelectedOrderDish");
            }
        }

        private DXInfo.Models.OrderDeskes _SelectedOrderDesk;
        public DXInfo.Models.OrderDeskes SelectedOrderDesk
        {
            get
            {
                return _SelectedOrderDesk;
            }
            set
            {
                _SelectedOrderDesk = value;
                this.RaisePropertyChanged("SelectedOrderDesk");
            }
        }

        private List<DXInfo.Models.OrderPackages> _lSelectedOrderPackage;
        public List<DXInfo.Models.OrderPackages> lSelectedOrderPackage
        {
            get
            {
                return _lSelectedOrderPackage;
            }
            set
            {
                _lSelectedOrderPackage = value;
                this.RaisePropertyChanged("lSelectedOrderPackage");
            }
        }

        private DXInfo.Models.OrderBooks _SelectedOrderBook;
        public DXInfo.Models.OrderBooks SelectedOrderBook
        {
            get
            {
                return _SelectedOrderBook;
            }
            set
            {
                _SelectedOrderBook = value;
                this.RaisePropertyChanged("SelectedOrderBook");
            }
        }

        private DXInfo.Models.OrderBookDeskes _SelectedOrderBookDesk;
        public DXInfo.Models.OrderBookDeskes SelectedOrderBookDesk
        {
            get
            {
                return _SelectedOrderBookDesk;
            }
            set
            {
                _SelectedOrderBookDesk = value;
                this.RaisePropertyChanged("SelectedOrderBookDesk");
            }
        }

        private string _OpenOperName;
        public string OpenOperName
        {
            get
            {
                return _OpenOperName;
            }
            set
            {
                _OpenOperName = value;
                this.RaisePropertyChanged("OpenOperName");
            }
        }

        //private bool _IsUse = false;
        //public bool IsUse
        //{
        //    get
        //    {
        //        return _IsUse;
        //    }
        //    set
        //    {
        //        _IsUse = value;
        //        this.RaisePropertyChanged("IsUse");
        //    }
        //}

        //private bool _IsBook = false;
        //public bool IsBook
        //{
        //    get
        //    {
        //        return _IsBook;
        //    }
        //    set
        //    {
        //        _IsBook = value;
        //        this.RaisePropertyChanged("IsBook");
        //    }
        //}
        #endregion  

        #region 房间
        public void SetOCRoom()
        {
            var q = Uow.Rooms.GetAll().Where(w => w.DeptId == this.Dept.DeptId).OrderBy(o => o.Code).ToList();
            this.OCRoom = new ObservableCollection<DXInfo.Models.Rooms>(q);
        }
        public void SetlRoom()
        {
            this.lRoom = Uow.Rooms.GetAll().Where(w => w.DeptId == this.Dept.DeptId).OrderBy(o => o.Code).ToList();            
        }
        private ObservableCollection<DXInfo.Models.Rooms> _OCRoom;
        public ObservableCollection<DXInfo.Models.Rooms> OCRoom
        {
            get
            {
                return _OCRoom;
            }
            set
            {
                _OCRoom = value;
                this.RaisePropertyChanged("OCRoom");
            }
        }
        private List<DXInfo.Models.Rooms> _lRoom;
        public List<DXInfo.Models.Rooms> lRoom
        {
            get
            {
                return _lRoom;
            }
            set
            {
                _lRoom = value;
                this.RaisePropertyChanged("lRoom");
            }
        }
        private DXInfo.Models.Rooms _SelectedRoom;
        public DXInfo.Models.Rooms SelectedRoom
        {
            get
            {
                return _SelectedRoom;
            }
            set
            {
                _SelectedRoom = value;
                this.RaisePropertyChanged("SelectedRoom");
                this.AfterSelectRoom();
            }
        }
        protected virtual void AfterSelectRoom() { }
        #endregion

        #region 桌台
        public void SetlDeskEx()
        {
            List<DXInfo.Models.Desks> ldesk = (from d in Uow.Desks.GetAll()
                          join d1 in Uow.Rooms.GetAll() on d.RoomId equals d1.Id into dd1
                          from dd1s in dd1.DefaultIfEmpty()
                          where d.Status == (int)DXInfo.Models.DeskStatus.InUse
                          && dd1s.DeptId == this.Dept.DeptId
                          select d).ToList();
            lDeskEx = new List<DXInfo.Models.DeskEx>();
            foreach (DXInfo.Models.Desks desk in ldesk)
            {
                DXInfo.Models.DeskEx deskEx = Mapper.Map<DXInfo.Models.DeskEx>(desk);
                lDeskEx.Add(deskEx);
            }
        }
        private List<DXInfo.Models.DeskEx> _lDeskEx;
        public List<DXInfo.Models.DeskEx> lDeskEx
        {
            get
            {
                return _lDeskEx;
            }
            set
            {
                _lDeskEx = value;
                this.RaisePropertyChanged("lDeskEx");
            }
        }

        private ObservableCollection<DXInfo.Models.DeskEx> _OCDeskEx;
        public ObservableCollection<DXInfo.Models.DeskEx> OCDeskEx
        {
            get
            {
                return _OCDeskEx;
            }
            set
            {
                _OCDeskEx = value;
                this.RaisePropertyChanged("OCDeskEx");
            }
        }
        private DXInfo.Models.DeskEx _SelectedDeskEx;
        public DXInfo.Models.DeskEx SelectedDeskEx
        {
            get
            {
                return _SelectedDeskEx;
            }
            set
            {
                _SelectedDeskEx = value;
                this.RaisePropertyChanged("SelectedDeskEx");
                this.AfterSelectDeskEx();
            }
        }
        protected virtual void AfterSelectDeskEx() { }

        
        #endregion

        #region 导出EXCEL
        private void ExportToExcelExecute()
        {
            this.MyDataGrid.ExportToExcel(this.MyQuery.GetEnumerator());
        }
        public ICommand ExportToExcel
        {
            get
            {
                return new RelayCommand(ExportToExcelExecute);
            }
        }
        #endregion

        #region 消费类型
        public List<DXInfo.Models.MyEnum> lConsumeType
        {
            get
            {
                return DXInfo.Business.Helper.GetlMyEnum(typeof(DXInfo.Models.ConsumeType));
            }
        }
        private DXInfo.Models.MyEnum _SelectedConsumeType;
        public DXInfo.Models.MyEnum SelectedConsumeType
        {
            get
            {
                return _SelectedConsumeType;
            }
            set
            {
                _SelectedConsumeType = value;
                this.RaisePropertyChanged("SelectedConsumeType");
            }
        }
        #endregion

        #region 充值类型
        public List<DXInfo.Models.MyEnum> lRechargeType
        {
            get
            {
                return DXInfo.Business.Helper.GetlMyEnum(typeof(DXInfo.Models.RechargeType));
            }
        }
        private DXInfo.Models.MyEnum _SelectedRechargeType;
        public DXInfo.Models.MyEnum SelectedRechargeType
        {
            get
            {
                return _SelectedRechargeType;
            }
            set
            {
                _SelectedRechargeType = value;
                this.RaisePropertyChanged("SelectedRechargeType");
            }
        }
        #endregion

        #region IsCard
        private bool _IsCard;
        public bool IsCard
        {
            get
            {
                return _IsCard;
            }
            set
            {
                _IsCard = value;
                this.RaisePropertyChanged("IsCard");
            }
        }
        #endregion

        #region IsMoney
        private bool _IsMoney;
        public bool IsMoney
        {
            get
            {
                return _IsMoney;
            }
            set
            {
                _IsMoney = value;
                this.RaisePropertyChanged("IsMoney");
            }
        }
        #endregion

        #region 应收金额
        private decimal _ReceivableAmount;
        public decimal ReceivableAmount
        {
            get
            {
                return _ReceivableAmount;
            }
            set
            {
                _ReceivableAmount = value;
                this.RaisePropertyChanged("ReceivableAmount");
            }
        }
        #endregion

        #region 实收金额
        private decimal? _Cash;
        [Required(ErrorMessage = "请输入实收金额")]
        public decimal? Cash
        {
            get
            {
                return _Cash;
            }
            set
            {
                _Cash = value;
                this.RaisePropertyChanged("Cash");
                this.RaisePropertyChanged("Change");
            }
        }
        #endregion

        #region 找零
        //private decimal _Change;
        public decimal Change
        {
            get
            {
                //return _Change;
                return ReceivableAmount > Cash || !Cash.HasValue ? 0 : Cash.Value - ReceivableAmount;
            }
            //set
            //{
            //    _Change = value;
            //    this.RaisePropertyChanged("Change");
            //}
        }
        #endregion

        #region 对话框关闭
        private bool? _DialogResult;
        public bool? DialogResult
        {
            get { return _DialogResult; }
            set
            {
                if (_DialogResult != value)
                {
                    _DialogResult = value;
                    this.RaisePropertyChanged("DialogResult");
                }
            }
        }
        #endregion

        #region 标题
        private string _Title;
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
                this.RaisePropertyChanged("Title");
            }
        }
        #endregion

        #region 卡赠送
        private List<DXInfo.Models.CardDonateInventoryEx> _lCardDonateInventoryEx;
        public List<DXInfo.Models.CardDonateInventoryEx> lCardDonateInventoryEx
        {
            get
            {
                return _lCardDonateInventoryEx;
            }
            set
            {
                _lCardDonateInventoryEx = value;
                this.RaisePropertyChanged("lCardDonateInventoryEx");
            }
        }
        #endregion

        #region 编码
        private string _Code;
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
                this.RaisePropertyChanged("Code");
            }
        }
        #endregion

        #region 名称
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                this.RaisePropertyChanged("_Name");
            }
        }
        #endregion

        #region 下载文件信息
        private ObservableCollection<DXInfo.Models.DownloadFileInfo> _OCDownloadFileInfo;
        public ObservableCollection<DXInfo.Models.DownloadFileInfo> OCDownloadFileInfo
        {
            get
            {
                return _OCDownloadFileInfo;
            }
            set
            {
                _OCDownloadFileInfo = value;
                this.RaisePropertyChanged("OCDownloadFileInfo");
            }
        }

        //private DXInfo.Models.DownloadFileInfo _CurrentDownloadFileInfo;
        //public DXInfo.Models.DownloadFileInfo CurrentDownloadFileInfo
        //{
        //    get
        //    {
        //        return _CurrentDownloadFileInfo;
        //    }
        //    set
        //    {
        //        _CurrentDownloadFileInfo = value;
        //        this.RaisePropertyChanged("CurrentDownloadFileInfo");
        //    }
        //}
        #endregion

        #region 当前同步操作
        private string _CurrentSyncOperate;
        public string CurrentSyncOperate
        {
            get
            {
                return _CurrentSyncOperate;
            }
            set
            {
                _CurrentSyncOperate = value;
                this.RaisePropertyChanged("CurrentSyncOperate");
            }
        }
        #endregion

        #region 当前操作
        //object lockObject = new object();
        private string _CurrentOperate;
        public string CurrentOperate
        {
            get
            {
                return _CurrentOperate;
            }
            set
            {
                //lock (lockObject)
                //{
                    _CurrentOperate = value;
                    this.RaisePropertyChanged("CurrentOperate");
                //}
            }
        }
        #endregion

        #region 当前日期时间
        private DateTime _CurrentDateTime;
        public DateTime CurrentDateTime
        {
            get
            {
                return _CurrentDateTime;
            }
            set
            {
                _CurrentDateTime = value;
                this.RaisePropertyChanged("CurrentDateTime");
            }
        }
        #endregion        

        #region 内容页
        private UserControl _MyContent;
        public UserControl MyContent
        {
            get
            {
                return _MyContent;
            }
            set
            {
                _MyContent = value;
                this.RaisePropertyChanged("MyContent");
            }
        }
        #endregion

        #region 背景
        public string BackgroundImgPath
        {
            get
            {
                return ClientCommon.BackgroundImgPath();
            }
        }
        public BitmapImage BackgroundImg
        {
            get
            {
                return ClientCommon.BackgroundImg();
            }
        }
        #endregion

        #region 同步进度
        private ObservableCollection<string> _SyncProgressMsg;
        public ObservableCollection<string> SyncProgressMsg
        {
            get
            {
                return _SyncProgressMsg;
            }
            set
            {
                _SyncProgressMsg = value;
                this.RaisePropertyChanged("SyncProgressMsg");
            }
        }
        #endregion

        public string GetThreePrintFile(DXInfo.Models.NameCodeType type)
        {
            return ClientCommon.ThreePrintFile(type);
        }
        public string GetButtomTitle(DXInfo.Models.DeptType deptType)
        {
            string title = "";
            switch (deptType)
            {
                case DXInfo.Models.DeptType.Sale:
                    title = ClientCommon.PrintTicketButtomTitle(DXInfo.Models.NameCodeType.PrintTicketButtomTitle);
                    break;
                case DXInfo.Models.DeptType.Shop:
                    title = ClientCommon.PrintTicketButtomTitle(DXInfo.Models.NameCodeType.PrintTicketButtomTitleOfWR);
                    break;
            }
            return title;
        }

        #region 当班操作员
        private string _OperatorsOnDuty;
        public string OperatorsOnDuty
        {
            get
            {
                return _OperatorsOnDuty;
            }
            set
            {
                _OperatorsOnDuty = value;
                this.RaisePropertyChanged("OperatorsOnDuty");
                App.OperatorsOnDuty = _OperatorsOnDuty;
            }
        }
        #endregion

        #region 存货名称
        private string _InvName;
        public string InvName
        {
            get
            {
                return _InvName;
            }
            set
            {
                _InvName = value;
                this.RaisePropertyChanged("InvName");
            }
        }
        #endregion

        #region 服务器地址
        public Uri BaseUri { get; set; }
        protected void SetBaseUri()
        {
            string remoteDownloadServer = ClientCommon.RemoteDownloadServer();
            BaseUri = new Uri(remoteDownloadServer);
        }
        #endregion

        #region 单据
        private DXInfo.Models.Receipts _Receipt;
        public DXInfo.Models.Receipts Receipt
        {
            get
            {
                return _Receipt;
            }
            set
            {
                _Receipt = value;
                this.RaisePropertyChanged("Receipt");
            }
        }
        #endregion

        #region 内容
        private string _Content;
        [Required(ErrorMessage = "请输入内容")]
        public string Content
        {
            get
            {
                return _Content;
            }
            set
            {
                _Content = value;
                this.RaisePropertyChanged("Content");
            }
        }
        #endregion
        public int ReceiptType { get; set; }

        #region 卡级别自动
        public bool IsCardLevelAuto { get; set; }
        public Visibility CardLevelColumnVisibility { get; set; }
        public bool IsCardLevelManual { get; set; }
        public void SetCardLevelAuto()
        {
            DXInfo.Business.Common common = new DXInfo.Business.Common(Uow, this.Oper.UserId, this.Dept.DeptId, this.Dept.OrganizationId,this.Dept.DeptCode,this.User.UserName);
            this.IsCardLevelAuto = common.IsCardLevelAuto();
            if (this.IsCardLevelAuto)
            {
                this.CardLevelColumnVisibility = Visibility.Collapsed;
            }
            else
            {
                this.CardLevelColumnVisibility = Visibility.Visible;
            }
            this.IsCardLevelManual = !this.IsCardLevelAuto;
        }
        #endregion

    }
}
