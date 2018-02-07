namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Web.Script.Serialization;

    public class ChartPlotLineSettings
    {
        [CompilerGenerated]
        private string _Color_k__BackingField;
        [CompilerGenerated]
        private ChartGridLineDashStyle _DashStyle_k__BackingField;
        [CompilerGenerated]
        private double? _Number_k__BackingField;
        [CompilerGenerated]
        private int _Width_k__BackingField;
        [CompilerGenerated]
        private int? _ZIndex_k__BackingField;

        public ChartPlotLineSettings()
        {
            this.Color = "";
            this.DashStyle = ChartGridLineDashStyle.Solid;
            this.Number = null;
            this.Width = 1;
            this.ZIndex = null;
        }

        internal Hashtable ToHashtable()
        {
            Hashtable hashtable = new Hashtable();
            if (!string.IsNullOrEmpty(this.Color))
            {
                hashtable.Add("color", this.Color);
            }
            if (this.DashStyle != ChartGridLineDashStyle.Solid)
            {
                hashtable.Add("dashStyle", this.DashStyle.ToString().ToLower());
            }
            if (this.Number.HasValue)
            {
                hashtable.Add("number", this.Number);
            }
            if (this.Width != 1)
            {
                hashtable.Add("width", this.Width);
            }
            if (this.ZIndex.HasValue)
            {
                hashtable.Add("zIndex", this.ZIndex);
            }
            return hashtable;
        }

        internal string ToJSON()
        {
            return new JavaScriptSerializer().Serialize(this.ToHashtable());
        }

        public string Color
        {
            [CompilerGenerated]
            get
            {
                return this._Color_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Color_k__BackingField = value;
            }
        }

        public ChartGridLineDashStyle DashStyle
        {
            [CompilerGenerated]
            get
            {
                return this._DashStyle_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._DashStyle_k__BackingField = value;
            }
        }

        public double? Number
        {
            [CompilerGenerated]
            get
            {
                return this._Number_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Number_k__BackingField = value;
            }
        }

        public int Width
        {
            [CompilerGenerated]
            get
            {
                return this._Width_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Width_k__BackingField = value;
            }
        }

        public int? ZIndex
        {
            [CompilerGenerated]
            get
            {
                return this._ZIndex_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ZIndex_k__BackingField = value;
            }
        }
    }
}

