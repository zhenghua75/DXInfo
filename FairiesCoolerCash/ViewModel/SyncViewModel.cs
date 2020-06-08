using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using DXInfo.Sync;
using System.Collections.ObjectModel;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using FairiesCoolerCash.Business;
using Microsoft.Synchronization;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.ComponentModel;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;
using System.Windows;
using DXInfo.Models;
using AutoMapper;

namespace FairiesCoolerCash.ViewModel
{
    public class SyncViewModel : BusinessViewModelBase
    {
        private DXInfo.Sync.Sync s;   
        private readonly IMapper mapper;
        public SyncViewModel(IFairiesMemberManageUow uow,IMapper mapper)
            : base(uow,mapper, new List<string>())
        {
            this.mapper = mapper;
            s = DXInfo.Sync.Sync.Instance();
            s.SyncMsgEvent += new DXInfo.Sync.Sync.SyncMsgEventHandler(s_SyncMsgEvent1);
        }
        public override void Cleanup()
        {
            base.Cleanup();
            s.SyncMsgEvent -= new DXInfo.Sync.Sync.SyncMsgEventHandler(s_SyncMsgEvent1);
        }
        private void s_SyncMsgEvent1(object sender, SyncMsgEventArgs e)
        {
            //this.CurrentSyncOperate = e.msg;
            UpdateSyncMsg(e.msg);
        }
        private delegate void UpdateSyncMsgDelegate(string msg);
        private void updateSyncMsg(string msg)
        {
            if (SyncProgressMsg == null)
            {
                SyncProgressMsg = new ObservableCollection<string>();
                SyncProgressMsg.Add("开始同步");
            }
            if (SyncProgressMsg.Count > 20)
            {
                SyncProgressMsg.Clear();
            }
            SyncProgressMsg.Add(msg);
        }
        public void UpdateSyncMsg(string msg)
        {
            Application.Current.Dispatcher.BeginInvoke(new UpdateSyncMsgDelegate(updateSyncMsg),msg);
        }     
        
        private bool SyncCanExecute()
        {
            if (!(s.IsRunBlock() ||
                s.IsRun1Block() ||
                s.IsRun2Block() ||
                s.IsRun3Block() ||
                s.IsRun4Block() ||
                s.IsRun5Block() ||
                s.IsRun6Block() ||
                s.IsRun7Block() ||
                s.IsRun8Block() ||
                s.IsRun9Block()))
            {                
                return true;
            }
            return false;
        }
        private void SyncExecute()
        {
            try
            {
                if (s.IsRunBlock() ||
                    s.IsRun1Block() ||
                    s.IsRun2Block() ||
                    s.IsRun3Block() ||
                    s.IsRun4Block() ||
                    s.IsRun5Block() ||
                    s.IsRun6Block() ||
                    s.IsRun7Block() ||
                    s.IsRun8Block() ||
                    s.IsRun9Block())
                {
                    Helper.ShowSuccMsg("同步已在后台运行，请等待！");
                }
                else
                {
                    if (this.SyncProgressMsg != null)
                    {
                        this.SyncProgressMsg.Clear();
                        this.SyncProgressMsg = null;
                    }

                    s.ExcuteSync();
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Policy");
                Helper.ShowErrorMsg(ex.Message);
            }
        }
        public ICommand Sync
        {
            get
            {
                return new RelayCommand(SyncExecute, SyncCanExecute);
            }
        }
    }
}
