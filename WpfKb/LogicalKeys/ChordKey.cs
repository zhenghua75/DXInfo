using System.Collections.Generic;
using WindowsInput;

namespace WpfKb.LogicalKeys
{
    /// <summary>
    /// 
    /// </summary>
    public class ChordKey : LogicalKeyBase
    {
        /// <summary>
        /// 
        /// </summary>
        public IList<VirtualKeyCode> ModifierKeys { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public IList<VirtualKeyCode> Keys { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="modifierKey"></param>
        /// <param name="key"></param>
        public ChordKey(string displayName, VirtualKeyCode modifierKey, VirtualKeyCode key)
            : this(displayName, new List<VirtualKeyCode> { modifierKey }, new List<VirtualKeyCode> { key })
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="modifierKeys"></param>
        /// <param name="key"></param>
        public ChordKey(string displayName, IList<VirtualKeyCode> modifierKeys, VirtualKeyCode key)
            : this(displayName, modifierKeys, new List<VirtualKeyCode> { key })
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="modifierKey"></param>
        /// <param name="keys"></param>
        public ChordKey(string displayName, VirtualKeyCode modifierKey, IList<VirtualKeyCode> keys)
            : this(displayName, new List<VirtualKeyCode> { modifierKey }, keys)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="modifierKeys"></param>
        /// <param name="keys"></param>
        public ChordKey(string displayName, IList<VirtualKeyCode> modifierKeys, IList<VirtualKeyCode> keys)
        {
            DisplayName = displayName;
            ModifierKeys = modifierKeys;
            Keys = keys;
        }
        /// <summary>
        /// 
        /// </summary>
        public override void Press()
        {
            InputSimulator.SimulateModifiedKeyStroke(ModifierKeys, Keys);
            base.Press();
        }
    }
}