using WindowsInput;

namespace WpfKb.LogicalKeys
{
    /// <summary>
    /// 
    /// </summary>
    public class TogglingModifierKey : ModifierKeyBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="keyCode"></param>
        public TogglingModifierKey(string displayName, VirtualKeyCode keyCode) :
            base(keyCode)
        {
            DisplayName = displayName;
        }
        /// <summary>
        /// 
        /// </summary>
        public override void Press()
        {
            // This is a bit tricky because we can only get the state of a toggling key after the input has been
            // read off the MessagePump.  Ergo if we make that assumption that in the time it takes to run this method
            // we will be toggling the state of the key, set IsInEffect to the new state and then press the key.
            IsInEffect = !InputSimulator.IsTogglingKeyInEffect(KeyCode);
            base.Press();
        }
        /// <summary>
        /// 
        /// </summary>
        public override void SynchroniseKeyState()
        {
            IsInEffect = InputSimulator.IsTogglingKeyInEffect(KeyCode);
        }
    }
}