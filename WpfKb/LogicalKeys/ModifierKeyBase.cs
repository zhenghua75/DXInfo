using WindowsInput;

namespace WpfKb.LogicalKeys
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ModifierKeyBase : VirtualKey
    {
        private bool _isInEffect;
        /// <summary>
        /// 
        /// </summary>
        public bool IsInEffect
        {
            get { return _isInEffect; }
            set
            {
                if (value != _isInEffect)
                {
                    _isInEffect = value;
                    OnPropertyChanged("IsInEffect");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyCode"></param>
        protected ModifierKeyBase(VirtualKeyCode keyCode) :
            base(keyCode)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public abstract void SynchroniseKeyState();
    }
}