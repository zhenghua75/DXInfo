using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace FairiesCoolerCash.Business
{
    public class PagerSettings : ObservableObject
    {
        #region 当前页
        private int _CurrentPage;
        public int CurrentPage
        {
            get
            {
                return _CurrentPage;
            }
            set
            {
                _CurrentPage = value;
                this.RaisePropertyChanged("CurrentPage");
            }
        }
        #endregion

        #region 页面记录数
        private int _PageSize;
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = value;
                this.RaisePropertyChanged("PageSize");
            }
        }
        #endregion

        #region 页面记录数选项
        private ObservableCollection<int> _PageSizeOptions;
        public ObservableCollection<int> PageSizeOptions
        {
            get
            {
                return _PageSizeOptions;
            }
            set
            {
                _PageSizeOptions = value;
                this.RaisePropertyChanged("PageSizeOptions");
            }
        }
        #endregion

        private int _TotalCount;
        public int TotalCount
        {
            get
            {
                return _TotalCount;
            }
            set
            {
                _TotalCount = value;
                this.RaisePropertyChanged("TotalCount");
            }
        }

        private int _TotalPageCount;
        public int TotalPageCount
        {
            get
            {
                return _TotalPageCount;
            }
            set
            {
                _TotalPageCount = value;
                this.RaisePropertyChanged("TotalPageCount");
            }
        }

        public PagerSettings()
        {
            this.PageSize = 100;
            this.CurrentPage = 1;
            List<int> li = new List<int>() { 100, 200, 300, 500, 1000 };
            this.PageSizeOptions = new ObservableCollection<int>(li);
        }
    }
}
