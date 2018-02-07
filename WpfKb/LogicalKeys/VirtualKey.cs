using WindowsInput;

namespace WpfKb.LogicalKeys
{
    /// <summary>
    /// 
    /// </summary>
    public class VirtualKey : LogicalKeyBase
    {
        private VirtualKeyCode _keyCode;
        /// <summary>
        /// 
        /// </summary>
        public virtual VirtualKeyCode KeyCode
        {
            get { return _keyCode; }
            set
            {
                if (value != _keyCode)
                {
                    _keyCode = value;
                    OnPropertyChanged("KeyCode");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="displayName"></param>
        public VirtualKey(VirtualKeyCode keyCode, string displayName)
        {
            DisplayName = displayName;
            KeyCode = keyCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyCode"></param>
        public VirtualKey(VirtualKeyCode keyCode)
        {
            KeyCode = keyCode;
        }
        /// <summary>
        /// 
        /// </summary>
        public VirtualKey()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public override void Press()
        {
            InputSimulator.SimulateKeyPress(_keyCode);
            base.Press();
        }
    }
}