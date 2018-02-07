using System.Collections.Generic;
using WindowsInput;

namespace WpfKb.LogicalKeys
{
    /// <summary>
    /// 
    /// </summary>
    public class NumLockSensitiveKey : MultiCharacterKey
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="keyDisplays"></param>
        public NumLockSensitiveKey(VirtualKeyCode keyCode, IList<string> keyDisplays)
            : base(keyCode, keyDisplays)
        {
        }
    }
}