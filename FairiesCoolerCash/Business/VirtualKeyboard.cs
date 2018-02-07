using System.Windows;
using WpfKb.Controls;

namespace FairiesCoolerCash.Business
{
    public class VirtualKeyboard : DependencyObject
    {
        #region FloatingTouchScreenKeyboard
        public static readonly DependencyProperty FloatingTouchScreenKeyboardProperty
            = DependencyProperty.RegisterAttached("FloatingTouchScreenKeyboard",
            typeof(bool),
            typeof(VirtualKeyboard),
            new UIPropertyMetadata(default(bool), FloatingTouchScreenKeyboardPropertyChanged));
        static void FloatingTouchScreenKeyboardPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement host = sender as FrameworkElement;
            if (host != null)
            {
                host.GotFocus += new RoutedEventHandler(OnHostGotFocus);
                host.LostFocus += new RoutedEventHandler(OnHostLostFocus);
            }
        }
        private static FloatingTouchScreenKeyboard _InstanceObject;
        private static void OnHostGotFocus(object sender, RoutedEventArgs e)
        {
            if (GetFloatingTouchScreenKeyboard(sender as DependencyObject))
            {
                if (_InstanceObject == null)
                {
                    _InstanceObject = new FloatingTouchScreenKeyboard();
                    _InstanceObject.AreAnimationsEnabled = true;
                    _InstanceObject.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
                    _InstanceObject.Width = 900;
                    _InstanceObject.Height = 400;
                    _InstanceObject.IsOpen = true;
                }
                else
                {
                    _InstanceObject.IsOpen = true;
                }
            }

        }
        private static void OnHostLostFocus(object sender, RoutedEventArgs e)
        {
            if (_InstanceObject != null)
            {
                _InstanceObject.IsOpen = false;
            }
        }
        public bool FloatingTouchScreenKeyboard
        {
            get
            {
                return (bool)GetValue(FloatingTouchScreenKeyboardProperty);
            }
            set
            {
                SetValue(FloatingTouchScreenKeyboardProperty, value);
            }

        }
        public static bool GetFloatingTouchScreenKeyboard(DependencyObject obj)
        {
            return (bool)obj.GetValue(FloatingTouchScreenKeyboardProperty);
        }
        public static void SetFloatingTouchScreenKeyboard(DependencyObject obj, bool value)
        {
            obj.SetValue(FloatingTouchScreenKeyboardProperty, value);
        }
        #endregion
    }
}
