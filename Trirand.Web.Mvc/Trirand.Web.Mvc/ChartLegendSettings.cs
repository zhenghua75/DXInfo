namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Web.Script.Serialization;

    public class ChartLegendSettings
    {
        [CompilerGenerated]
        private ChartHorizontalAlign _Align_k__BackingField;
        [CompilerGenerated]
        private string _BackgroundColor_k__BackingField;
        [CompilerGenerated]
        private string _BorderColor_k__BackingField;
        [CompilerGenerated]
        private int _BorderRadius_k__BackingField;
        [CompilerGenerated]
        private int _BorderWidth_k__BackingField;
        [CompilerGenerated]
        private bool _Enabled_k__BackingField;
        [CompilerGenerated]
        private bool _Floating_k__BackingField;
        [CompilerGenerated]
        private NameValueCollection _ItemHiddenStyle_k__BackingField;
        [CompilerGenerated]
        private NameValueCollection _ItemHoverStyle_k__BackingField;
        [CompilerGenerated]
        private NameValueCollection _ItemStyle_k__BackingField;
        [CompilerGenerated]
        private int _ItemWidth_k__BackingField;
        [CompilerGenerated]
        private string _LabelFormatter_k__BackingField;
        [CompilerGenerated]
        private ChartLegendLayout _Layout_k__BackingField;
        [CompilerGenerated]
        private int _Margin_k__BackingField;
        [CompilerGenerated]
        private bool _Reversed_k__BackingField;
        [CompilerGenerated]
        private bool _Shadow_k__BackingField;
        [CompilerGenerated]
        private NameValueCollection _Style_k__BackingField;
        [CompilerGenerated]
        private int _SymbolPadding_k__BackingField;
        [CompilerGenerated]
        private int _SymbolWidth_k__BackingField;
        [CompilerGenerated]
        private ChartVerticalAlign _VerticalAlign_k__BackingField;
        [CompilerGenerated]
        private int _Width_k__BackingField;
        [CompilerGenerated]
        private int _X_k__BackingField;
        [CompilerGenerated]
        private int _Y_k__BackingField;

        public ChartLegendSettings()
        {
            this.Align = ChartHorizontalAlign.Center;
            this.BackgroundColor = "";
            this.BorderColor = "#909090";
            this.BorderRadius = 5;
            this.BorderWidth = 1;
            this.Enabled = true;
            this.Floating = false;
            this.ItemHiddenStyle = new NameValueCollection();
            this.ItemHoverStyle = new NameValueCollection();
            this.ItemStyle = new NameValueCollection();
            this.ItemWidth = 0;
            this.Layout = ChartLegendLayout.Vertical;
            this.LabelFormatter = "";
            this.Margin = 15;
            this.Reversed = false;
            this.Shadow = false;
            this.Style = new NameValueCollection();
            this.SymbolPadding = 5;
            this.SymbolWidth = 30;
            this.VerticalAlign = ChartVerticalAlign.Bottom;
            this.Width = 0;
            this.X = 15;
            this.Y = 0;
        }

        internal string ToJSON()
        {
            Hashtable hashtable = new Hashtable();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (this.Align != ChartHorizontalAlign.Center)
            {
                hashtable.Add("align", this.Align.ToString().ToLower());
            }
            if (!string.IsNullOrEmpty(this.BackgroundColor))
            {
                hashtable.Add("backgroundColor", this.BackgroundColor);
            }
            if (this.BorderColor != "#909090")
            {
                hashtable.Add("borderColor", this.BorderColor);
            }
            if (this.BorderRadius != 5)
            {
                hashtable.Add("borderRadius", this.BorderRadius);
            }
            if (this.BorderWidth != 1)
            {
                hashtable.Add("borderWidth", this.BorderWidth);
            }
            if (!this.Enabled)
            {
                hashtable.Add("enabled", false);
            }
            if (this.Floating)
            {
                hashtable.Add("floating", true);
            }
            if (this.ItemHiddenStyle.Count > 0)
            {
                hashtable.Add("itemHiddenStyle", this.ItemHiddenStyle);
            }
            if (this.ItemHoverStyle.Count > 0)
            {
                hashtable.Add("itemHoverStyle", this.ItemHoverStyle);
            }
            if (this.ItemStyle.Count > 0)
            {
                hashtable.Add("itemStyle", this.ItemStyle);
            }
            if (this.ItemWidth != 0)
            {
                hashtable.Add("itemWidth", this.ItemWidth);
            }
            if (!string.IsNullOrEmpty(this.LabelFormatter))
            {
                hashtable.Add("labelFormatter", this.LabelFormatter);
            }
            hashtable.Add("layout", this.Layout.ToString().ToLower());
            if (this.Margin != 15)
            {
                hashtable.Add("margin", this.Margin);
            }
            if (this.Reversed)
            {
                hashtable.Add("reversed", true);
            }
            if (this.Shadow)
            {
                hashtable.Add("shadow", true);
            }
            if (this.Style.Count > 0)
            {
                hashtable.Add("style", this.Style);
            }
            if (this.SymbolPadding != 5)
            {
                hashtable.Add("symbolPadding", this.SymbolPadding);
            }
            if (this.SymbolWidth != 30)
            {
                hashtable.Add("symbolWidth", this.SymbolWidth);
            }
            if (this.VerticalAlign != ChartVerticalAlign.Bottom)
            {
                hashtable.Add("verticalAlign", this.VerticalAlign.ToString().ToLower());
            }
            if (this.Width != 0)
            {
                hashtable.Add("width", this.Width);
            }
            if (this.X != 15)
            {
                hashtable.Add("x", this.X);
            }
            if (this.Y != 0)
            {
                hashtable.Add("y", this.Y);
            }
            return serializer.Serialize(hashtable);
        }

        public ChartHorizontalAlign Align
        {
            [CompilerGenerated]
            get
            {
                return this._Align_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Align_k__BackingField = value;
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

        public bool Enabled
        {
            [CompilerGenerated]
            get
            {
                return this._Enabled_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Enabled_k__BackingField = value;
            }
        }

        public bool Floating
        {
            [CompilerGenerated]
            get
            {
                return this._Floating_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Floating_k__BackingField = value;
            }
        }

        public NameValueCollection ItemHiddenStyle
        {
            [CompilerGenerated]
            get
            {
                return this._ItemHiddenStyle_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ItemHiddenStyle_k__BackingField = value;
            }
        }

        public NameValueCollection ItemHoverStyle
        {
            [CompilerGenerated]
            get
            {
                return this._ItemHoverStyle_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ItemHoverStyle_k__BackingField = value;
            }
        }

        public NameValueCollection ItemStyle
        {
            [CompilerGenerated]
            get
            {
                return this._ItemStyle_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ItemStyle_k__BackingField = value;
            }
        }

        public int ItemWidth
        {
            [CompilerGenerated]
            get
            {
                return this._ItemWidth_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ItemWidth_k__BackingField = value;
            }
        }

        public string LabelFormatter
        {
            [CompilerGenerated]
            get
            {
                return this._LabelFormatter_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._LabelFormatter_k__BackingField = value;
            }
        }

        public ChartLegendLayout Layout
        {
            [CompilerGenerated]
            get
            {
                return this._Layout_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Layout_k__BackingField = value;
            }
        }

        public int Margin
        {
            [CompilerGenerated]
            get
            {
                return this._Margin_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Margin_k__BackingField = value;
            }
        }

        public bool Reversed
        {
            [CompilerGenerated]
            get
            {
                return this._Reversed_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Reversed_k__BackingField = value;
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

        public NameValueCollection Style
        {
            [CompilerGenerated]
            get
            {
                return this._Style_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Style_k__BackingField = value;
            }
        }

        public int SymbolPadding
        {
            [CompilerGenerated]
            get
            {
                return this._SymbolPadding_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._SymbolPadding_k__BackingField = value;
            }
        }

        public int SymbolWidth
        {
            [CompilerGenerated]
            get
            {
                return this._SymbolWidth_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._SymbolWidth_k__BackingField = value;
            }
        }

        public ChartVerticalAlign VerticalAlign
        {
            [CompilerGenerated]
            get
            {
                return this._VerticalAlign_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._VerticalAlign_k__BackingField = value;
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

        public int X
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

        public int Y
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

