namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Web.Script.Serialization;

    public class ChartTitleSettings
    {
        [CompilerGenerated]
        private ChartHorizontalAlign _Align_k__BackingField;
        [CompilerGenerated]
        private bool _Floating_k__BackingField;
        [CompilerGenerated]
        private int _Margin_k__BackingField;
        [CompilerGenerated]
        private string _Text_k__BackingField;
        [CompilerGenerated]
        private ChartVerticalAlign _VerticalAlign_k__BackingField;
        [CompilerGenerated]
        private int _X_k__BackingField;
        [CompilerGenerated]
        private int _Y_k__BackingField;

        public ChartTitleSettings()
        {
            this.Align = ChartHorizontalAlign.Center;
            this.Floating = false;
            this.Margin = 15;
            this.Text = "Chart Title";
            this.VerticalAlign = ChartVerticalAlign.Top;
            this.X = 0;
            this.Y = 0x19;
        }

        internal Hashtable ToHashtable()
        {
            Hashtable hashtable = new Hashtable();
            if (this.Align != ChartHorizontalAlign.Center)
            {
                hashtable.Add("align", this.Align.ToString().ToLower());
            }
            if (this.Floating)
            {
                hashtable.Add("floating", true);
            }
            if (this.Margin != 15)
            {
                hashtable.Add("margin", this.Margin);
            }
            if (!string.IsNullOrEmpty(this.Text))
            {
                hashtable.Add("text", this.Text);
            }
            if (this.VerticalAlign != ChartVerticalAlign.Top)
            {
                hashtable.Add("verticalAlign", this.VerticalAlign.ToString().ToLower());
            }
            if (this.X != 0)
            {
                hashtable.Add("x", this.X);
            }
            if (this.Y != 0x19)
            {
                hashtable.Add("y", this.Y);
            }
            return hashtable;
        }

        internal string ToJSON()
        {
            return new JavaScriptSerializer().Serialize(this.ToHashtable());
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

        public string Text
        {
            [CompilerGenerated]
            get
            {
                return this._Text_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Text_k__BackingField = value;
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

