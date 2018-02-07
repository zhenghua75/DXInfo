#region License

//Media glass - my simple WPF player
//Copyright (C) 2008-2010 Denis Yakimenko <denyakimenko@yandex.ru>

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Media_glass.Controls
{
    /// <summary>
    /// Interaction logic for ResizedBorder.xaml
    /// </summary>
    public partial class ResizedBorder : UserControl
    {
        /// <summary>
        /// Constructor -  initializes the instance of window
        /// </summary>
        public ResizedBorder()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor -  initializes the instance of window
        /// </summary>
        public ResizedBorder(double Width,double Height)
        {
            InitializeComponent();

            this.VerticalAlignment = VerticalAlignment.Top;
            this.HorizontalAlignment = HorizontalAlignment.Left;            
            this.Width = Width;
            this.Height = Height;           
        }

        /// <summary>
        /// Start border dragged.
        /// </summary>
        public event EventHandler DragStarted;

        /// <summary>
        /// During dragged.
        /// </summary>
        public event Action<double,double> DragDelta;
        
        /// <summary>
        /// Stop border dragged.
        /// </summary>
        public event Action<double,double> DragCompleted;

        /// <summary>
        /// User capture thumb.
        /// </summary>
        public event EventHandler ThumbMouseEnter;

        /// <summary>
        /// User stop to capture thumb.
        /// </summary>
        public event EventHandler ThumbMouseLeave;

        /// <summary>
        /// Invisible thumb width.
        /// </summary>
        public double MinBorderWidth { get; set; }

        /// <summary>
        /// Invisible thumb height.
        /// </summary>
        public double MinBorderHeight { get; set; }

        /// <summary>
        /// Redraw border while dragging
        /// </summary>        
        private void onDragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            //Move the Thumb to the mouse position during the drag operation
            double yadjust = myCanvasStretch.Height + e.VerticalChange;
            double xadjust = myCanvasStretch.Width + e.HorizontalChange;

            bool sizeChanged = false;

            double horizontalChange = e.HorizontalChange;
            double verticalChange = e.VerticalChange;

            if (xadjust >= this.MinBorderWidth)
            {
                myCanvasStretch.Width = xadjust;
                Canvas.SetLeft(myThumb, Canvas.GetLeft(myThumb) + e.HorizontalChange);
                this.Width += e.HorizontalChange;                

                sizeChanged = true;
            }
            else
                horizontalChange = 0;

            if (yadjust >= this.MinBorderHeight)
            {                
                myCanvasStretch.Height = yadjust;                
                Canvas.SetTop(myThumb, Canvas.GetTop(myThumb) +e.VerticalChange);                
                this.Height += e.VerticalChange;                

                sizeChanged = true;
            }
            else
                verticalChange = 0;

            if (sizeChanged && this.DragDelta != null)
                this.DragDelta(horizontalChange, verticalChange);
        }

        /// <summary>
        /// Handle border dragging complete.
        /// Hide border when we stop drag it.
        /// </summary>       
        private void onDragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            //myThumb.Background = Brushes.Blue;
            this.rect.Opacity = 0;
            //this.rect2.Opacity = 0;

            if (this.DragCompleted != null)
                this.DragCompleted(this.Width, this.Height);
        }

        /// <summary>
        /// Handle start border dragging.
        /// Show border when we drag it.
        /// </summary>       
        private void onDragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            //myThumb.Background = Brushes.Orange;
            this.rect.Opacity = 1;
            //this.rect2.Opacity = 0.2;

            if (this.DragStarted != null)
                this.DragStarted(this, null);            
        }

        /// <summary>
        /// Handle mouse enter event.
        /// </summary>        
        private void myThumb_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.SizeNWSE;

            if (this.ThumbMouseEnter != null)
                this.ThumbMouseEnter(this, null);
        }

        /// <summary>
        /// Handle mouse leave event.
        /// </summary>        
        private void myThumb_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;

            if (this.ThumbMouseLeave != null)
                this.ThumbMouseLeave(this, null);
        }
    }
}
