using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXInfo.Models
{
    public class QueueEventArgs : EventArgs
    {
        public object Statistics { get; set; }
        public QueueEventArgs(object statistics)
        {
            this.Statistics = statistics;
        }
    }

    public class SyncMsgEventArgs : EventArgs
    {
        public string msg { get; set; }
        public SyncMsgEventArgs(string msg)
        {
            this.msg = msg;
        }
    }
    public class DownloadMsgEventArgs : EventArgs
    {
        public DownQueue DownQueue { get; set; }
        public Exception Error { get; set; }
        public bool Completed { get; set; }
        public DownloadMsgEventArgs(DownQueue downQueue,Exception error,bool completed)
        {
            this.DownQueue = downQueue;
            this.Error = error;
            this.Completed = completed;
        }
    }
}
