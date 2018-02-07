using System;
using System.Collections.Generic;
using WindowsInput;

namespace WpfKb.LogicalKeys
{
    /// <summary>
    /// 
    /// </summary>
    public class MultiCharacterKey : VirtualKey
    {
        private int _selectedIndex;
        /// <summary>
        /// 
        /// </summary>
        public IList<string> KeyDisplays { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public string SelectedKeyDisplay { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (value != _selectedIndex)
                {
                    _selectedIndex = value;
                    SelectedKeyDisplay = KeyDisplays[value];
                    DisplayName = SelectedKeyDisplay;
                    OnPropertyChanged("SelectedIndex");
                    OnPropertyChanged("SelectedKeyDisplay");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="keyDisplays"></param>
        public MultiCharacterKey(VirtualKeyCode keyCode, IList<string> keyDisplays) :
            base(keyCode)
        {
            if (keyDisplays == null) throw new ArgumentNullException("keyDisplays");
            if (keyDisplays.Count <= 0)
                throw new ArgumentException("Please provide a list of one or more keyDisplays", "keyDisplays");
            KeyDisplays = keyDisplays;
            DisplayName = keyDisplays[0];
        }
    }
}