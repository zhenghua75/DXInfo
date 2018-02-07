namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Web.Script.Serialization;

    public class ChartBaseAxisSettings
    {
        [CompilerGenerated]
        private bool _AllowDecimals_k__BackingField;
        [CompilerGenerated]
        private string _AlternateGridColor_k__BackingField;
        [CompilerGenerated]
        private List<string> Categories_k__BackingField;
        [CompilerGenerated]
        private bool _EndOnTick_k__BackingField;
        [CompilerGenerated]
        private string _GridLineColor_k__BackingField;
        [CompilerGenerated]
        private ChartGridLineDashStyle _GridLineDashStyle_k__BackingField;
        [CompilerGenerated]
        private int _GridLineWidth_k__BackingField;
        [CompilerGenerated]
        private string _ID_k__BackingField;
        [CompilerGenerated]
        private ChartAxisLabelSettings _Labels_k__BackingField;
        [CompilerGenerated]
        private string _LineColor_k__BackingField;
        [CompilerGenerated]
        private int _LineWidth_k__BackingField;
        [CompilerGenerated]
        private int _LinkedTo_k__BackingField;
        [CompilerGenerated]
        private double _Max_k__BackingField;
        [CompilerGenerated]
        private double _MaxPadding_k__BackingField;
        [CompilerGenerated]
        private int _MaxZoom_k__BackingField;
        [CompilerGenerated]
        private double _Min_k__BackingField;
        [CompilerGenerated]
        private string _MinorGridLineColor_k__BackingField;
        [CompilerGenerated]
        private int? _TickInterval_k__BackingField;
        [CompilerGenerated]
        private int _TickWidth_k__BackingField;
        [CompilerGenerated]
        private ChartTitleSettings _Title_k__BackingField;
        [CompilerGenerated]
        private ChartAxisType _Type_k__BackingField;

        public ChartBaseAxisSettings()
        {
            this.AllowDecimals = true;
            this.AlternateGridColor = "";
            this.Categories = new List<string>();
            this.EndOnTick = false;
            this.GridLineColor = "#C0C0C0";
            this.GridLineDashStyle = ChartGridLineDashStyle.ShortDot;
            this.GridLineWidth = 0;
            this.ID = "";
            this.Labels = new ChartAxisLabelSettings();
            this.LineColor = "#C0D0E0";
            this.LineWidth = 1;
            this.LinkedTo = 0;
            this.Max = double.PositiveInfinity;
            this.MaxPadding = 0.01;
            this.MaxZoom = 0;
            this.Min = double.NegativeInfinity;
            this.MinorGridLineColor = "#E0E0E0";
            this.TickInterval = null;
            this.TickWidth = 1;
            this.Title = new ChartTitleSettings();
        }

        internal string ToJSON()
        {
            Hashtable hashtable = new Hashtable();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (!this.AllowDecimals)
            {
                hashtable.Add("allowDecimals", false);
            }
            if (!string.IsNullOrEmpty(this.AlternateGridColor))
            {
                hashtable.Add("alternateGridColor", this.AlternateGridColor);
            }
            if (this.Categories.Count > 0)
            {
                hashtable.Add("categories", this.Categories);
            }
            if (this.EndOnTick)
            {
                hashtable.Add("endOnTick", true);
            }
            if (this.GridLineColor != "#C0C0C0")
            {
                hashtable.Add("gridLineColor", this.GridLineColor);
            }
            if (this.GridLineDashStyle != ChartGridLineDashStyle.ShortDot)
            {
                hashtable.Add("gridLineDashStyle", this.GridLineDashStyle.ToString().ToLower());
            }
            if (this.GridLineWidth != 0)
            {
                hashtable.Add("gridLineWidth", this.GridLineWidth);
            }
            if (!string.IsNullOrEmpty(this.ID))
            {
                hashtable.Add("id", this.ID);
            }
            if (this.LineColor != "#C0D0E0")
            {
                hashtable.Add("lineColor", this.LineColor);
            }
            if (this.LineWidth != 1)
            {
                hashtable.Add("lineWidth", this.LineWidth);
            }
            if (this.LinkedTo != 0)
            {
                hashtable.Add("linkedTo", this.LinkedTo);
            }
            if (this.Max != double.PositiveInfinity)
            {
                hashtable.Add("max", this.Max);
            }
            if (this.MaxPadding != 0.01)
            {
                hashtable.Add("maxPadding", this.MaxPadding);
            }
            if (this.MaxZoom != 0)
            {
                hashtable.Add("maxZoom", this.MaxZoom);
            }
            if (this.Min != double.NegativeInfinity)
            {
                hashtable.Add("min", this.Min);
            }
            if (this.MinorGridLineColor != "#E0E0E0")
            {
                hashtable.Add("minorGridLineColor", this.MinorGridLineColor);
            }
            hashtable.Add("title", this.Title.ToHashtable());
            if (this.TickInterval.HasValue)
            {
                hashtable.Add("tickInterval", this.TickInterval);
            }
            if (this.TickWidth != 1)
            {
                hashtable.Add("tickWidth", this.TickWidth);
            }
            if (this.Type != ChartAxisType.Linear)
            {
                hashtable.Add("type", this.Type.ToString().ToLower());
            }
            return serializer.Serialize(hashtable);
        }

        public bool AllowDecimals
        {
            [CompilerGenerated]
            get
            {
                return this._AllowDecimals_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._AllowDecimals_k__BackingField = value;
            }
        }

        public string AlternateGridColor
        {
            [CompilerGenerated]
            get
            {
                return this._AlternateGridColor_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._AlternateGridColor_k__BackingField = value;
            }
        }

        public List<string> Categories
        {
            [CompilerGenerated]
            get
            {
                return this.Categories_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.Categories_k__BackingField = value;
            }
        }

        public bool EndOnTick
        {
            [CompilerGenerated]
            get
            {
                return this._EndOnTick_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._EndOnTick_k__BackingField = value;
            }
        }

        public string GridLineColor
        {
            [CompilerGenerated]
            get
            {
                return this._GridLineColor_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._GridLineColor_k__BackingField = value;
            }
        }

        public ChartGridLineDashStyle GridLineDashStyle
        {
            [CompilerGenerated]
            get
            {
                return this._GridLineDashStyle_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._GridLineDashStyle_k__BackingField = value;
            }
        }

        public int GridLineWidth
        {
            [CompilerGenerated]
            get
            {
                return this._GridLineWidth_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._GridLineWidth_k__BackingField = value;
            }
        }

        public string ID
        {
            [CompilerGenerated]
            get
            {
                return this._ID_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ID_k__BackingField = value;
            }
        }

        public ChartAxisLabelSettings Labels
        {
            [CompilerGenerated]
            get
            {
                return this._Labels_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Labels_k__BackingField = value;
            }
        }

        public string LineColor
        {
            [CompilerGenerated]
            get
            {
                return this._LineColor_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._LineColor_k__BackingField = value;
            }
        }

        public int LineWidth
        {
            [CompilerGenerated]
            get
            {
                return this._LineWidth_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._LineWidth_k__BackingField = value;
            }
        }

        public int LinkedTo
        {
            [CompilerGenerated]
            get
            {
                return this._LinkedTo_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._LinkedTo_k__BackingField = value;
            }
        }

        public double Max
        {
            [CompilerGenerated]
            get
            {
                return this._Max_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Max_k__BackingField = value;
            }
        }

        public double MaxPadding
        {
            [CompilerGenerated]
            get
            {
                return this._MaxPadding_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._MaxPadding_k__BackingField = value;
            }
        }

        public int MaxZoom
        {
            [CompilerGenerated]
            get
            {
                return this._MaxZoom_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._MaxZoom_k__BackingField = value;
            }
        }

        public double Min
        {
            [CompilerGenerated]
            get
            {
                return this._Min_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Min_k__BackingField = value;
            }
        }

        public string MinorGridLineColor
        {
            [CompilerGenerated]
            get
            {
                return this._MinorGridLineColor_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._MinorGridLineColor_k__BackingField = value;
            }
        }

        public int? TickInterval
        {
            [CompilerGenerated]
            get
            {
                return this._TickInterval_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._TickInterval_k__BackingField = value;
            }
        }

        public int TickWidth
        {
            [CompilerGenerated]
            get
            {
                return this._TickWidth_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._TickWidth_k__BackingField = value;
            }
        }

        public ChartTitleSettings Title
        {
            [CompilerGenerated]
            get
            {
                return this._Title_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Title_k__BackingField = value;
            }
        }

        public ChartAxisType Type
        {
            [CompilerGenerated]
            get
            {
                return this._Type_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Type_k__BackingField = value;
            }
        }
    }
}

