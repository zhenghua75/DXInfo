using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using System.Collections.ObjectModel;
using System.Net;
using DXInfo.Models;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using FairiesCoolerCash.Business;
using System.Configuration;
using System.Windows;

namespace FairiesCoolerCash.ViewModel
{
    public class DownLoadViewModel: BusinessViewModelBase
    {
        public override void Cleanup()
        {
            base.Cleanup();
            DownloadBusiness.DownloadMsgEvent -= new MyDownloadBusiness.DownloadMsgEventHandler(DownloadBusiness_DownloadMsgEvent);
            if (DownloadBusiness.client != null)
            {
                DownloadBusiness.client.Dispose();
            }
        }
        protected MyDownloadBusiness DownloadBusiness;
        public DownLoadViewModel(IFairiesMemberManageUow uow,string resourceType)
            : base(uow, new List<string>() )
        {
            this.OCDownloadFileInfo = new ObservableCollection<DXInfo.Models.DownloadFileInfo>();
            string connectorUrlString = ClientCommon.GetConnectorUrlString();
            switch (resourceType)
            {
                case "mp3":
                    DownloadBusiness = MyDownloadBusiness.DownloadMp3BusinessInstance(connectorUrlString);
                    break;
                case "images":
                    DownloadBusiness = MyDownloadBusiness.DownloadImageBusinessInstance(connectorUrlString);
                    break;
            }
            DownloadBusiness.DownloadMsgEvent += new MyDownloadBusiness.DownloadMsgEventHandler(DownloadBusiness_DownloadMsgEvent);
        }
        private delegate void UpdateDownloadFileInfoDelegate(DownloadFileInfo dfi);
        private void UpdateDownloadFileInfo(DownloadFileInfo dfi)
        {
            DownloadFileInfo oldDfi = this.OCDownloadFileInfo.FirstOrDefault(f => f.DescFilePath == dfi.DescFilePath);
            if (oldDfi == null)
            {
                this.OCDownloadFileInfo.Add(dfi);
            }
            else
            {
                oldDfi.BytesReceived = dfi.BytesReceived;
                oldDfi.ProgressPercentage = dfi.ProgressPercentage;
                oldDfi.Completed = dfi.Completed;
                oldDfi.ErrorMsg = dfi.ErrorMsg;
            }
        }
        void DownloadBusiness_DownloadMsgEvent(object sender, DownloadMsgEventArgs e)
        {
            DownloadFileInfo dfi = e.DownQueue.CurrentFile;
            if (dfi != null)
            {
                Application.Current.Dispatcher.BeginInvoke(new UpdateDownloadFileInfoDelegate(UpdateDownloadFileInfo), dfi);
                
            }
            if (e.Completed)
            {
                if (!e.DownQueue.QueueFile.Any())
                {
                    Helper.ShowSuccMsg("下载完成");
                }
            }
        }

        #region 下载
        private bool DownloadCanExecute()
        {
            if (!(DownloadBusiness.client.IsBusy ||
                DownloadBusiness.IsRunBlock()))
            {
                return true;
            }
            return false;
        }
        private void DownloadExecute()
        {
            if (!(DownloadBusiness.client.IsBusy ||
                DownloadBusiness.IsRunBlock()))
            {
                if (this.OCDownloadFileInfo != null)
                {
                    if (OCDownloadFileInfo.Count > 0)
                    {
                        OCDownloadFileInfo.Clear();
                    }
                }
                DownloadBusiness.DownloadFile();
            }
        }        
        public ICommand Download
        {
            get
            {
                return new RelayCommand(DownloadExecute, DownloadCanExecute);
            }
        }
        #endregion
    }
    public class Mp3DownLoadViewModel : DownLoadViewModel
    {
        public Mp3DownLoadViewModel(IFairiesMemberManageUow uow)
            : base(uow,"mp3")
        {
        }
    }
    public class ImgDownloadViewModel : DownLoadViewModel
    {
        public ImgDownloadViewModel(IFairiesMemberManageUow uow)
            : base(uow,"images")
        {
        }
    }
}
