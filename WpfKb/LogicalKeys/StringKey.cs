using System;
using System.Linq;
using WindowsInput;

namespace WpfKb.LogicalKeys
{
    /// <summary>
    /// 
    /// </summary>
    public class StringKey : LogicalKeyBase
    {
        private string _stringToSimulate;
        /// <summary>
        /// 
        /// </summary>
        public virtual string StringToSimulate
        {
            get { return _stringToSimulate; }
            set
            {
                if (value != _stringToSimulate)
                {
                    _stringToSimulate = value;
                    OnPropertyChanged("StringToSimulate");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="stringToSimulate"></param>
        public StringKey(string displayName, string stringToSimulate)
        {
            DisplayName = displayName;
            _stringToSimulate = stringToSimulate;
        }
        /// <summary>
        /// 
        /// </summary>
        public StringKey()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public override void Press()
        {
            InputSimulator.SimulateTextEntry(_stringToSimulate);
            base.Press();
        }
    }
}
