using System.ComponentModel;

namespace WpfKb.LogicalKeys
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILogicalKey : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        string DisplayName { get; }
        /// <summary>
        /// 
        /// </summary>
        void Press();
        /// <summary>
        /// 
        /// </summary>
        event LogicalKeyPressedEventHandler LogicalKeyPressed;
    }
}