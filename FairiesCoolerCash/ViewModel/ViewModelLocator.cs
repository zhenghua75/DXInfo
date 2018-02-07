/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:FairiesCoolerCash"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using DXInfo.Data.Contracts;
using DXInfo.Data;
using System.Data.Entity;
using FairiesCoolerCash.Business;
using Microsoft.Practices.Unity;

namespace FairiesCoolerCash.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
        }

        public MainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public LoginViewModel LoginViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }
        public DeskManageViewModel DeskManageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DeskManageViewModel>();
            }
        }
        public AddMemberViewModel AddMemberViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddMemberViewModel>();
            }
        }
        public BarMenuViewModel BarMenuViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<BarMenuViewModel>();
            }
        }
        public BillRepeatViewModel BillRepeatViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<BillRepeatViewModel>();
            }
        }
        public KitchenMenuViewModel KitchenMenuViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<KitchenMenuViewModel>();
            }
        }
        public Kitchen2MenuViewModel Kitchen2MenuViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Kitchen2MenuViewModel>();
            }
        }
        public CardAddViewModel CardAddViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CardAddViewModel>();
            }
        }
        public CardFoundViewModel CardFoundViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CardFoundViewModel>();
            }
        }
        public CardLossViewModel CardLossViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CardLossViewModel>();
            }
        }
        public CardInMoneyViewModel CardInMoneyViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CardInMoneyViewModel>();
            }
        }
        public PutCardInMoneyViewModel PutCardInMoneyViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PutCardInMoneyViewModel>();
            }
        }
        public MemberQueryViewModel MemberQueryViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MemberQueryViewModel>();
            }
        }
        public CardConsumeViewModel CardConsumeViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CardConsumeViewModel>();
            }
        }
        public Report2ViewModel Report2ViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Report2ViewModel>();
            }
        }
        public Report3ViewModel Report3ViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Report3ViewModel>();
            }
        }
        public Report4ViewModel Report4ViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Report4ViewModel>();
            }
        }
        public Report5ViewModel Report5ViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Report5ViewModel>();
            }
        }
        public Report7ViewModel Report7ViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Report7ViewModel>();
            }
        }
        public WRReport10ViewModel WRReport10ViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WRReport10ViewModel>();
            }
        }
        public WRReport2ViewModel WRReport2ViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WRReport2ViewModel>();
            }
        }
        public WRReport3ViewModel WRReport3ViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WRReport3ViewModel>();
            }
        }
        public WRReport4ViewModel WRReport4ViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WRReport4ViewModel>();
            }
        }
        public WRReport5ViewModel WRReport5ViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WRReport5ViewModel>();
            }
        }
        public WRReport7ViewModel WRReport7ViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WRReport7ViewModel>();
            }
        }
        public WRReport8ViewModel WRReport8ViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WRReport8ViewModel>();
            }
        }
        public WRReport9ViewModel WRReport9ViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WRReport9ViewModel>();
            }
        }
        public WRReport11ViewModel WRReport11ViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WRReport11ViewModel>();
            }
        }
        public OrderMenuListViewModel OrderMenuListViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OrderMenuListViewModel>();
            }
        }
        public OrderBookListViewModel OrderBookListViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OrderBookListViewModel>();
            }
        }
        public PointsExchangeViewModel PointsExchangeViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PointsExchangeViewModel>();
            }
        }
        public LackMenuListViewModel LackMenuListViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LackMenuListViewModel>();
            }
        }
        public Mp3DownLoadViewModel Mp3DownLoadViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Mp3DownLoadViewModel>();
            }
        }
        public ImgDownloadViewModel ImgDownloadViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ImgDownloadViewModel>();
            }
        }
        public SyncViewModel SyncViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SyncViewModel>();
            }
        }
        public StockConsumeViewModel StockConsumeViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StockConsumeViewModel>();
            }
        }
        public StockDeskManageViewModel StockDeskManageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StockDeskManageViewModel>();
            }
        }
        public QueryCurrentStockViewModel QueryCurrentStockViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QueryCurrentStockViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}