using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace DXInfo.Models
{
    [NotMapped]
    public class DownloadFileInfo : Entity
    {
        //public WebClient Client { get; set; }
        private string _Url;
        public string Url
        {
            get { return _Url; }
            set
            {
                _Url = value;
                OnPropertyChanged("Url");
            }
        }

        private string _FileName;
        public string FileName
        {
            get { return _FileName; }
            set
            {
                _FileName = value;
                OnPropertyChanged("FileName");
            }
        }

        private string _DescFilePath;
        public string DescFilePath
        {
            get { return _DescFilePath; }
            set
            {
                _DescFilePath = value;
                OnPropertyChanged("DescFilePath");
            }
        }

        private string _DestDir;
        public string DestDir
        {
            get { return _DestDir; }
            set
            {
                _DestDir = value;
                OnPropertyChanged("DestDir");
            }
        }

        private int _ProgressPercentage;
        public int ProgressPercentage
        {
            get { return _ProgressPercentage; }
            set
            {
                _ProgressPercentage = value;
                OnPropertyChanged("ProgressPercentage");
            }
        }

        private long _BytesReceived;
        public long BytesReceived
        {
            get { return _BytesReceived; }
            set
            {
                _BytesReceived = value;
                OnPropertyChanged("BytesReceived");
            }
        }
        private string _FileSize;
        public string FileSize
        {
            get { return _FileSize; }
            set
            {
                _FileSize = value;
                OnPropertyChanged("FileSize");
            }
        }
        private DateTime _ModifyDate;
        public DateTime ModifyDate
        {
            get { return _ModifyDate; }
            set
            {
                _ModifyDate = value;
                OnPropertyChanged("ModifyDate");
            }
        }

        private string _ResourceType;
        public string ResourceType
        {
            get { return _ResourceType; }
            set
            {
                _ResourceType = value;
                OnPropertyChanged("ResourceType");
            }
        }

        private string _Md5;
        public string Md5
        {
            get { return _Md5; }
            set
            {
                _Md5 = value;
                OnPropertyChanged("Md5");
            }
        }

        private string _CurrentFolder;
        public string CurrentFolder
        {
            get { return _CurrentFolder; }
            set
            {
                _CurrentFolder = value;
                OnPropertyChanged("CurrentFolder");
            }
        }

        private bool _Completed;
        public bool Completed
        {
            get { return _Completed; }
            set
            {
                _Completed = value;
                OnPropertyChanged("Completed");
            }
        }

        private string _ErrorMsg;
        public string ErrorMsg
        {
            get { return _ErrorMsg; }
            set
            {
                _ErrorMsg = value;
                OnPropertyChanged("ErrorMsg");
            }
        }
    }

    public class DownQueue
    {
        public DownloadFileInfo CurrentFile { get; set; }
        public Queue<DownloadFileInfo> QueueFile { get; set; }
    }
}
