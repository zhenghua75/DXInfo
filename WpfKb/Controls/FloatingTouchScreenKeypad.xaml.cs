using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfKb.Controls
{
    /// <summary>
    /// FloatingTouchScreenKeypad.xaml 的交互逻辑
    /// </summary>
    public partial class FloatingTouchScreenKeypad
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty AreAnimationsEnabledProperty = DependencyProperty.Register("AreAnimationsEnabled", typeof(bool), typeof(FloatingTouchScreenKeypad), new UIPropertyMetadata(true));
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IsAllowedToFadeProperty = DependencyProperty.Register("IsAllowedToFade", typeof(bool), typeof(FloatingTouchScreenKeypad), new UIPropertyMetadata(true));
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IsDraggingProperty = DependencyProperty.Register("IsDragging", typeof(bool), typeof(FloatingTouchScreenKeypad), new UIPropertyMetadata(false));
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IsDragHelperAllowedToHideProperty = DependencyProperty.Register("IsDragHelperAllowedToHide", typeof(bool), typeof(FloatingTouchScreenKeypad), new UIPropertyMetadata(false));
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IsKeypadShownProperty = DependencyProperty.Register("IsKeypadShown", typeof(bool), typeof(FloatingTouchScreenKeypad), new UIPropertyMetadata(true));
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty MaximumKeypadOpacityProperty = DependencyProperty.Register("MaximumKeypadOpacity", typeof(double), typeof(FloatingTouchScreenKeypad), new UIPropertyMetadata(0.9d));
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty MinimumKeypadOpacityProperty = DependencyProperty.Register("MinimumKeypadOpacity", typeof(double), typeof(FloatingTouchScreenKeypad), new UIPropertyMetadata(0.2d));
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty KeypadHideDelayProperty = DependencyProperty.Register("KeypadHideDelay", typeof(double), typeof(FloatingTouchScreenKeypad), new UIPropertyMetadata(5d));
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty KeypadHideAnimationDurationProperty = DependencyProperty.Register("KeypadHideAnimationDuration", typeof(double), typeof(FloatingTouchScreenKeypad), new UIPropertyMetadata(0.5d));
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty KeypadShowAnimationDurationProperty = DependencyProperty.Register("KeypadShowAnimationDuration", typeof(double), typeof(FloatingTouchScreenKeypad), new UIPropertyMetadata(0.5d));
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty DeadZoneProperty = DependencyProperty.Register("DeadZone", typeof(double), typeof(FloatingTouchScreenKeypad), new UIPropertyMetadata(5d));


       
        private Point _mouseDownPosition;
        private Point _mouseDownOffset;
        private bool _isAllowedToFadeValueBeforeDrag;
        /// <summary>
        /// 
        /// </summary>
        public FloatingTouchScreenKeypad()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets a value indicating whether animations are enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if animations are enabled; otherwise, <c>false</c>.
        /// </value>
        public bool AreAnimationsEnabled
        {
            get { return (bool)GetValue(AreAnimationsEnabledProperty); }
            set { SetValue(AreAnimationsEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the Keypad is allowed to fade. This is a dependency property.
        /// </summary>
        public bool IsAllowedToFade
        {
            get { return (bool)GetValue(IsAllowedToFadeProperty); }
            set { SetValue(IsAllowedToFadeProperty, value); }
        }

        /// <summary>
        /// Gets a value that indicates if the Keypad is currently being dragged. This is a dependency property.
        /// </summary>
        public bool IsDragging
        {
            get { return (bool)GetValue(IsDraggingProperty); }
            private set { SetValue(IsDraggingProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates if the drag helper text is allowed to hide. This is a dependency property.
        /// </summary>
        public bool IsDragHelperAllowedToHide
        {
            get { return (bool)GetValue(IsDragHelperAllowedToHideProperty); }
            set { SetValue(IsDragHelperAllowedToHideProperty, value); }
        }

        /// <summary>
        /// Gets a value that indicates that the Keypad is shown (not faded). This is a dependency property.
        /// </summary>
        public bool IsKeypadShown
        {
            get { return (bool)GetValue(IsKeypadShownProperty); }
            private set { SetValue(IsKeypadShownProperty, value); }
        }

        /// <summary>
        /// Gets or sets the maximum opacity for a fully displayed Keypad. This is a dependency property.
        /// </summary>
        public double MaximumKeypadOpacity
        {
            get { return (double)GetValue(MaximumKeypadOpacityProperty); }
            set { SetValue(MaximumKeypadOpacityProperty, value); }
        }

        /// <summary>
        /// Gets or sets the opacity to use for a partially hidden Keypad. This is a dependency property.
        /// </summary>
        public double MinimumKeypadOpacity
        {
            get { return (double)GetValue(MinimumKeypadOpacityProperty); }
            set { SetValue(MinimumKeypadOpacityProperty, value); }
        }

        /// <summary>
        /// Gets or sets the number of seconds to wait after the last Keypad activity before hiding the Keypad. This is a dependency property.
        /// </summary>
        public double KeypadHideDelay
        {
            get { return (double)GetValue(KeypadHideDelayProperty); }
            set { SetValue(KeypadHideDelayProperty, value); }
        }

        /// <summary>
        /// Gets or sets the duration in seconds for the Keypad hide animation. This is a dependency property.
        /// </summary>
        public double KeypadHideAnimationDuration
        {
            get { return (double)GetValue(KeypadHideAnimationDurationProperty); }
            set { SetValue(KeypadHideAnimationDurationProperty, value); }
        }

        /// <summary>
        /// Gets or sets the duration in seconds for the Keypad show animation. This is a dependency property.
        /// </summary>
        public double KeypadShowAnimationDuration
        {
            get { return (double)GetValue(KeypadShowAnimationDurationProperty); }
            set { SetValue(KeypadShowAnimationDurationProperty, value); }
        }

        /// <summary>
        /// Gets or sets the maximum opacity for a fully displayed Keypad. This is a dependency property.
        /// </summary>
        public double DeadZone
        {
            get { return (double)GetValue(DeadZoneProperty); }
            set { SetValue(DeadZoneProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnOpened(EventArgs e)
        {
            IsKeypadShown = true;
            base.OnOpened(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            IsKeypadShown = false;
            base.OnClosed(e);
        }

        private void DragHandle_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _mouseDownPosition = e.GetPosition(PlacementTarget);
            _mouseDownOffset = new Point(HorizontalOffset, VerticalOffset);
        }

        private void DragHandle_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var delta = e.GetPosition(PlacementTarget) - _mouseDownPosition;
                if (!IsDragging && delta.Length > DeadZone)
                {
                    IsDragging = true;
                    IsDragHelperAllowedToHide = true;
                    _isAllowedToFadeValueBeforeDrag = IsAllowedToFade;
                    IsAllowedToFade = false;
                    DragHandle.CaptureMouse();
                }

                if (IsDragging)
                {
                    HorizontalOffset = _mouseDownOffset.X + delta.X;
                    VerticalOffset = _mouseDownOffset.Y + delta.Y;
                }
            }
        }

        private void DragHandle_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            DragHandle.ReleaseMouseCapture();
            IsDragging = false;
            IsAllowedToFade = _isAllowedToFadeValueBeforeDrag;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //IsKeypadShown = false;
            //base.OnClosed(e);
            IsOpen = false;
        }

    }
}
