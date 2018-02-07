namespace Trirand.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;

    public class ChartPoint
    {
        [CompilerGenerated]
        private double? _X_k__BackingField;
        [CompilerGenerated]
        private double? _Y_k__BackingField;

        public ChartPoint()
        {
            this.X = null;
            this.Y = null;
        }

        public ChartPoint(double? x) : this()
        {
            this.X = x;
        }

        public ChartPoint(double? x, double? y) : this()
        {
            this.X = x;
            this.Y = y;
        }

        public double? X
        {
            [CompilerGenerated]
            get
            {
                return this._X_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._X_k__BackingField = value;
            }
        }

        public double? Y
        {
            [CompilerGenerated]
            get
            {
                return this._Y_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Y_k__BackingField = value;
            }
        }
    }
}

