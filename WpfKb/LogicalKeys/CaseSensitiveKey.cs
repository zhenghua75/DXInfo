using System.Collections.Generic;
using WindowsInput;

namespace WpfKb.LogicalKeys
{
    /// <summary>
    /// 
    /// </summary>
    public class CaseSensitiveKey : MultiCharacterKey
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="keyDisplays"></param>
        public CaseSensitiveKey(VirtualKeyCode keyCode, IList<string> keyDisplays)
            : base(keyCode, keyDisplays)
        {
        }
    }
}