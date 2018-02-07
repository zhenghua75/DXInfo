namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Web.Script.Serialization;

    public class ChartCrossHairSettings
    {
        [CompilerGenerated]
        private string _Color_k__BackingField;
        [CompilerGenerated]
        private ChartGridLineDashStyle _DashStyle_k__BackingField;
        [CompilerGenerated]
        private int _Width_k__BackingField;

        public ChartCrossHairSettings()
        {
            this.Width = 1;
            this.Color = "blue";
            this.DashStyle = ChartGridLineDashStyle.Solid;
        }

        internal Hashtable ToHashtable()
        {
            Hashtable hashtable = new Hashtable();
            if (this.Width != 1)
            {
                hashtable.Add("width", this.Width);
            }
            if (this.Color != "blue")
            {
                hashtable.Add("color", this.Color);
            }
            if (this.DashStyle != ChartGridLineDashStyle.Solid)
            {
                hashtable.Add("dashStyle", this.DashStyle.ToString().ToLower());
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
    }
}

