using System;
using System.ComponentModel;

namespace WpfKb.LogicalKeys
{
    /// <summary>
    /// 
    /// </summary>
    public class LogicalKeyEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public ILogicalKey Key { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public LogicalKeyEventArgs(ILogicalKey key)
        {
            Key = key;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void LogicalKeyPressedEventHandler(object sender, LogicalKeyEventArgs e);
    /// <summary>
    /// 
    /// </summary>
    public abstract class LogicalKeyBase : ILogicalKey
    {
        /// <summary>
        /// 
        /// </summary>
        public event LogicalKeyPressedEventHandler LogicalKeyPressed;
        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private string _displayName;
        /// <summary>
        /// 
        /// </summary>
        public virtual string DisplayName
        {
            get { return _displayName; }
            set
            {
                if (value != _displayName)
                {
                    _displayName = value;
                    OnPropertyChanged("DisplayName");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void Press()
        {
            OnKeyPressed();
        }
        /// <summary>
        /// 
        /// </summary>
        protected void OnKeyPressed()
        {
            if (LogicalKeyPressed != null) LogicalKeyPressed(this, new LogicalKeyEventArgs(this));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                var args = new PropertyChangedEventArgs(propertyName);
                handler(this, args);
            }
        }
    }
}