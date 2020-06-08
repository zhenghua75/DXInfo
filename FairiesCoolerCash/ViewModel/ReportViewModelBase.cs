using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using DXInfo.Data.Contracts;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using FairiesCoolerCash.Business;
using AutoMapper;

namespace FairiesCoolerCash.ViewModel
{
    public class ReportViewModelBase : MyViewModelBase
    {
        #region 构造
        private readonly IMapper mapper;
        public ReportViewModelBase(IFairiesMemberManageUow uow, IMapper mapper)
            : base(uow,mapper,new List<string>())
        {
            this.mapper = mapper;
            this.SetlOper();
            this.SetlPayType();
            Messenger.Default.Register<DataGridMessageToken>(this, Handle_DataGridMessageToken);

            bool bCupTypeColVis = BusinessCommon.CupTypeColumnVisible();
            this.CupTypeColumnVisibility = bCupTypeColVis ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;

            bool bUnitOfMeasureColVis = BusinessCommon.UnitOfMeasureColumnVisibility();
            this.UnitOfMeasureColumnVisibility = bUnitOfMeasureColVis ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;

            bool bOperatorsOnDuty = BusinessCommon.OperatorsOnDuty();
            this.OperatorsOnDutyColumnVisibility = bOperatorsOnDuty ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }
        private void Handle_DataGridMessageToken(DataGridMessageToken token)
        {
            this.MyDataGrid = token.MyDataGrid;
        }
        public override void Cleanup()
        {
            base.Cleanup();
            Messenger.Default.Unregister<DataGridMessageToken>(this);
        }

        private System.Windows.Visibility _CupTypeColumnVisibility;
        public System.Windows.Visibility CupTypeColumnVisibility
        {
            get
            {
                return _CupTypeColumnVisibility;
            }
            set
            {
                _CupTypeColumnVisibility = value;
                this.RaisePropertyChanged("CupTypeColumnVisibility");
            }
        }
        private System.Windows.Visibility _UnitOfMeasureColumnVisibility;
        public System.Windows.Visibility UnitOfMeasureColumnVisibility
        {
            get
            {
                return _UnitOfMeasureColumnVisibility;
            }
            set
            {
                _UnitOfMeasureColumnVisibility = value;
                this.RaisePropertyChanged("UnitOfMeasureColumnVisibility");
            }
        }
        private System.Windows.Visibility _OperatorsOnDutyColumnVisibility;
        public System.Windows.Visibility OperatorsOnDutyColumnVisibility
        {
            get
            {
                return _OperatorsOnDutyColumnVisibility;
            }
            set
            {
                _OperatorsOnDutyColumnVisibility = value;
                this.RaisePropertyChanged("OperatorsOnDutyColumnVisibility");
            }
        }
        #endregion      
    }
}
