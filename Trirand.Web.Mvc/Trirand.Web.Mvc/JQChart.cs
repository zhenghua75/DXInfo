namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Web.Script.Serialization;

    public class JQChart
    {
        [CompilerGenerated]
        private bool _AlignTicks_k__BackingField;
        [CompilerGenerated]
        private bool _Animation_k__BackingField;
        [CompilerGenerated]
        private string _BackgroundColor_k__BackingField;
        [CompilerGenerated]
        private string _BorderColor_k__BackingField;
        [CompilerGenerated]
        private int _BorderRadius_k__BackingField;
        [CompilerGenerated]
        private int _BorderWidth_k__BackingField;
        [CompilerGenerated]
        private string _ClassName_k__BackingField;
        [CompilerGenerated]
        private int _Height_k__BackingField;
        [CompilerGenerated]
        private string _ID_k__BackingField;
        [CompilerGenerated]
        private bool _IgnoreHiddenSeries_k__BackingField;
        [CompilerGenerated]
        private bool _Inverted_k__BackingField;
        [CompilerGenerated]
        private ChartLegendSettings _Legend_k__BackingField;
        [CompilerGenerated]
        private int _MarginBottom_k__BackingField;
        [CompilerGenerated]
        private int _MarginLeft_k__BackingField;
        [CompilerGenerated]
        private int _MarginRight_k__BackingField;
        [CompilerGenerated]
        private int _MarginTop_k__BackingField;
        [CompilerGenerated]
        private string _PlotBackgroundColor_k__BackingField;
        [CompilerGenerated]
        private string _PlotBackgroundImage_k__BackingField;
        [CompilerGenerated]
        private string _PlotBorderColor_k__BackingField;
        [CompilerGenerated]
        private int _PlotBorderWidth_k__BackingField;
        [CompilerGenerated]
        private bool _PlotShadow_k__BackingField;
        [CompilerGenerated]
        private bool _Reflow_k__BackingField;
        [CompilerGenerated]
        private List<ChartSeriesSettings> Series_k__BackingField;
        [CompilerGenerated]
        private bool _Shadow_k__BackingField;
        [CompilerGenerated]
        private bool _ShowAxes_k__BackingField;
        [CompilerGenerated]
        private int _SpacingBottom_k__BackingField;
        [CompilerGenerated]
        private int _SpacingLeft_k__BackingField;
        [CompilerGenerated]
        private int _SpacingRight_k__BackingField;
        [CompilerGenerated]
        private int _SpacingTop_k__BackingField;
        [CompilerGenerated]
        private ChartTitleSettings _SubTitle_k__BackingField;
        [CompilerGenerated]
        private ChartTitleSettings _Title_k__BackingField;
        [CompilerGenerated]
        private ChartToolTipSettings _ToolTip_k__BackingField;
        [CompilerGenerated]
        private ChartType _Type_k__BackingField;
        [CompilerGenerated]
        private int _Width_k__BackingField;
        [CompilerGenerated]
        private ChartXAxisSettings _XAxis_k__BackingField;
        [CompilerGenerated]
        private ChartYAxisSettings _YAxis_k__BackingField;
        [CompilerGenerated]
        private ChartZoomType _ZoomType_k__BackingField;

        public JQChart()
        {
            this.AlignTicks = true;
            this.Animation = true;
            this.BackgroundColor = "#FFFFFF";
            this.BorderColor = "#4572A7";
            this.BorderRadius = 5;
            this.BorderWidth = 0;
            this.ClassName = "";
            this.Height = 350;
            this.ID = "";
            this.IgnoreHiddenSeries = true;
            this.Inverted = false;
            this.MarginTop = 0;
            this.MarginRight = 50;
            this.MarginBottom = 70;
            this.MarginLeft = 80;
            this.PlotBackgroundColor = "";
            this.PlotBackgroundImage = "";
            this.PlotBorderColor = "#C0C0C0";
            this.PlotBorderWidth = 0;
            this.PlotShadow = true;
            this.Reflow = true;
            this.SpacingBottom = 15;
            this.SpacingLeft = 10;
            this.SpacingRight = 10;
            this.SpacingTop = 10;
            this.ToolTip = new ChartToolTipSettings();
            this.Type = ChartType.Line;
            this.Width = 350;
            this.ZoomType = ChartZoomType.None;
            this.Title = new ChartTitleSettings();
            this.SubTitle = new ChartTitleSettings();
            this.XAxis = new ChartXAxisSettings();
            this.YAxis = new ChartYAxisSettings();
            this.Legend = new ChartLegendSettings();
            this.Series = new List<ChartSeriesSettings>();
        }

        internal string SeriesToJSON()
        {
            List<Hashtable> list = new List<Hashtable>();
            foreach (ChartSeriesSettings settings in this.Series)
            {
                Hashtable item = new Hashtable();
                if (!string.IsNullOrEmpty(settings.Name))
                {
                    item.Add("name", settings.Name);
                }
                if (settings.Data.Count<ChartPoint>() > 0)
                {
                    List<double?> list2 = new List<double?>();
                    foreach (ChartPoint point in settings.Data)
                    {
                        list2.Add(point.X);
                    }
                    item.Add("data", list2);
                }
                list.Add(item);
            }
            return new JavaScriptSerializer().Serialize(list);
        }

        internal string ToJSON()
        {
            Hashtable hashtable = new Hashtable();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            hashtable.Add("renderTo", this.ID);
            if (!this.AlignTicks)
            {
                hashtable.Add("alignKeys", false);
            }
            if (!this.Animation)
            {
                hashtable.Add("animation", false);
            }
            if (this.BackgroundColor != "#FFFFFF")
            {
                hashtable.Add("backgroundColor", this.BackgroundColor);
            }
            if (this.BorderColor != "#4572A7")
            {
                hashtable.Add("borderColor", this.BorderColor);
            }
            if (this.BorderRadius != 5)
            {
                hashtable.Add("borderRadius", this.BorderRadius);
            }
            if (this.BorderWidth != 0)
            {
                hashtable.Add("borderWidth", this.BorderWidth);
            }
            if (!string.IsNullOrEmpty(this.ClassName))
            {
                hashtable.Add("className", this.ClassName);
            }
            if (this.Height != 0)
            {
                hashtable.Add("height", this.Height);
            }
            if (!this.IgnoreHiddenSeries)
            {
                hashtable.Add("IgnoreHiddenSeries", false);
            }
            if (this.Inverted)
            {
                hashtable.Add("inverted", true);
            }
            if (this.MarginBottom != 70)
            {
                hashtable.Add("marginBottom", this.MarginBottom);
            }
            if (this.MarginLeft != 80)
            {
                hashtable.Add("marginLeft", this.MarginLeft);
            }
            if (this.MarginRight != 50)
            {
                hashtable.Add("marginRight", this.MarginRight);
            }
            if (this.MarginTop != 0)
            {
                hashtable.Add("marginTop", this.MarginTop);
            }
            if (!string.IsNullOrEmpty(this.PlotBackgroundColor))
            {
                hashtable.Add("plotBackgroundColor", this.PlotBackgroundColor);
            }
            if (!string.IsNullOrEmpty(this.PlotBackgroundImage))
            {
                hashtable.Add("plotBackgroundImage", this.PlotBackgroundImage);
            }
            if (this.PlotBorderColor != "#C0C0C0")
            {
                hashtable.Add("plotBorderColor", this.PlotBorderColor);
            }
            if (this.PlotBorderWidth != 0)
            {
                hashtable.Add("plotBorderWidth", this.PlotBorderWidth);
            }
            if (this.PlotShadow)
            {
                hashtable.Add("plotShadow", true);
            }
            if (!this.Reflow)
            {
                hashtable.Add("reflow", false);
            }
            if (this.Shadow)
            {
                hashtable.Add("shadow", true);
            }
            if (this.SpacingBottom != 15)
            {
                hashtable.Add("spacingBottom", this.SpacingBottom);
            }
            if (this.SpacingLeft != 10)
            {
                hashtable.Add("spacingLeft", this.SpacingLeft);
            }
            if (this.SpacingRight != 10)
            {
                hashtable.Add("spacingRight", this.SpacingRight);
            }
            if (this.SpacingTop != 10)
            {
                hashtable.Add("spacingTop", this.SpacingTop);
            }
            hashtable.Add("type", this.Type.ToString().ToLower());
            if (this.Width != 0)
            {
                hashtable.Add("width", this.Width);
            }
            if (this.ZoomType != ChartZoomType.None)
            {
                hashtable.Add("zoomType", this.ZoomType.ToString().ToLower());
            }
            return serializer.Serialize(hashtable);
        }

        public bool AlignTicks
        {
            [CompilerGenerated]
            get
            {
                return this._AlignTicks_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._AlignTicks_k__BackingField = value;
            }
        }

        public bool Animation
        {
            [CompilerGenerated]
            get
            {
                return this._Animation_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Animation_k__BackingField = value;
            }
        }

        public string BackgroundColor
        {
            [CompilerGenerated]
            get
            {
                return this._BackgroundColor_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._BackgroundColor_k__BackingField = value;
            }
        }

        public string BorderColor
        {
            [CompilerGenerated]
            get
            {
                return this._BorderColor_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._BorderColor_k__BackingField = value;
            }
        }

        public int BorderRadius
        {
            [CompilerGenerated]
            get
            {
                return this._BorderRadius_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._BorderRadius_k__BackingField = value;
            }
        }

        public int BorderWidth
        {
            [CompilerGenerated]
            get
            {
                return this._BorderWidth_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._BorderWidth_k__BackingField = value;
            }
        }

        public string ClassName
        {
            [CompilerGenerated]
            get
            {
                return this._ClassName_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ClassName_k__BackingField = value;
            }
        }

        public int Height
        {
            [CompilerGenerated]
            get
            {
                return this._Height_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Height_k__BackingField = value;
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

        public bool IgnoreHiddenSeries
        {
            [CompilerGenerated]
            get
            {
                return this._IgnoreHiddenSeries_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._IgnoreHiddenSeries_k__BackingField = value;
            }
        }

        public bool Inverted
        {
            [CompilerGenerated]
            get
            {
                return this._Inverted_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Inverted_k__BackingField = value;
            }
        }

        public ChartLegendSettings Legend
        {
            [CompilerGenerated]
            get
            {
                return this._Legend_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Legend_k__BackingField = value;
            }
        }

        public int MarginBottom
        {
            [CompilerGenerated]
            get
            {
                return this._MarginBottom_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._MarginBottom_k__BackingField = value;
            }
        }

        public int MarginLeft
        {
            [CompilerGenerated]
            get
            {
                return this._MarginLeft_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._MarginLeft_k__BackingField = value;
            }
        }

        public int MarginRight
        {
            [CompilerGenerated]
            get
            {
                return this._MarginRight_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._MarginRight_k__BackingField = value;
            }
        }

        public int MarginTop
        {
            [CompilerGenerated]
            get
            {
                return this._MarginTop_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._MarginTop_k__BackingField = value;
            }
        }

        public string PlotBackgroundColor
        {
            [CompilerGenerated]
            get
            {
                return this._PlotBackgroundColor_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._PlotBackgroundColor_k__BackingField = value;
            }
        }

        public string PlotBackgroundImage
        {
            [CompilerGenerated]
            get
            {
                return this._PlotBackgroundImage_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._PlotBackgroundImage_k__BackingField = value;
            }
        }

        public string PlotBorderColor
        {
            [CompilerGenerated]
            get
            {
                return this._PlotBorderColor_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._PlotBorderColor_k__BackingField = value;
            }
        }

        public int PlotBorderWidth
        {
            [CompilerGenerated]
            get
            {
                return this._PlotBorderWidth_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._PlotBorderWidth_k__BackingField = value;
            }
        }

        public bool PlotShadow
        {
            [CompilerGenerated]
            get
            {
                return this._PlotShadow_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._PlotShadow_k__BackingField = value;
            }
        }

        public bool Reflow
        {
            [CompilerGenerated]
            get
            {
                return this._Reflow_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Reflow_k__BackingField = value;
            }
        }

        public List<ChartSeriesSettings> Series
        {
            [CompilerGenerated]
            get
            {
                return this.Series_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.Series_k__BackingField = value;
            }
        }

        public bool Shadow
        {
            [CompilerGenerated]
            get
            {
                return this._Shadow_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Shadow_k__BackingField = value;
            }
        }

        public bool ShowAxes
        {
            [CompilerGenerated]
            get
            {
                return this._ShowAxes_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ShowAxes_k__BackingField = value;
            }
        }

        public int SpacingBottom
        {
            [CompilerGenerated]
            get
            {
                return this._SpacingBottom_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._SpacingBottom_k__BackingField = value;
            }
        }

        public int SpacingLeft
        {
            [CompilerGenerated]
            get
            {
                return this._SpacingLeft_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._SpacingLeft_k__BackingField = value;
            }
        }

        public int SpacingRight
        {
            [CompilerGenerated]
            get
            {
                return this._SpacingRight_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._SpacingRight_k__BackingField = value;
            }
        }

        public int SpacingTop
        {
            [CompilerGenerated]
            get
            {
                return this._SpacingTop_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._SpacingTop_k__BackingField = value;
            }
        }

        public ChartTitleSettings SubTitle
        {
            [CompilerGenerated]
            get
            {
                return this._SubTitle_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._SubTitle_k__BackingField = value;
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

        public ChartToolTipSettings ToolTip
        {
            [CompilerGenerated]
            get
            {
                return this._ToolTip_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ToolTip_k__BackingField = value;
            }
        }

        public ChartType Type
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

        public ChartXAxisSettings XAxis
        {
            [CompilerGenerated]
            get
            {
                return this._XAxis_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._XAxis_k__BackingField = value;
            }
        }

        public ChartYAxisSettings YAxis
        {
            [CompilerGenerated]
            get
            {
                return this._YAxis_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._YAxis_k__BackingField = value;
            }
        }

        public ChartZoomType ZoomType
        {
            [CompilerGenerated]
            get
            {
                return this._ZoomType_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ZoomType_k__BackingField = value;
            }
        }
    }
}

